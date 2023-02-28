using ProjectBasedSystem.Database;
using ProjectBasedSystem.Interfaces.Religion;
using ProjectBasedSystem.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

namespace ProjectBasedSystem.Repositories.Religion
{
    public class Repo_Religion : IReligion
    {
        private readonly database _db;
        private string Scmd { get; set; }
        public Repo_Religion(database db)
        {
            _db = db;
        }


        private bool Disposed = false;
        protected virtual void Dispose(bool disposing)
        {
            if (!this.Disposed)
            {
                _db.Dispose();
            }
            Disposed = true;

        }
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        public IEnumerable<religion> GetReligion()
        {
            DataSet dsResult = new DataSet();
            var religionList = new List<religion>();
            try
            {
                Scmd = "EXEC [a_getReligions]";
                dsResult = _db.executeStatement(Scmd);

                religionList = (from DataRow dr in dsResult.Tables[0].Rows
                                select new religion()
                                {
                                    religion_id = Convert.ToInt32(dr[0]),
                                    religion_name = Convert.ToString(dr[1])

                                }).ToList();
                return religionList;
            }
            catch (Exception ex)
            {
                throw new ApplicationException("Error:" + dsResult, ex);
            }

        }
    }
}