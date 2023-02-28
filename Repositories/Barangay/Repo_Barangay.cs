using ProjectBasedSystem.Interfaces.Barangay;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ProjectBasedSystem.Models;
using ProjectBasedSystem.Database;
using System.Data;

namespace ProjectBasedSystem.Repositories.Barangay
{
    public class Repo_Barangay : IBarangay
    {

        private readonly database _db;
        private string Scmd { get; set; }
        public Repo_Barangay(database db)
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

        public IEnumerable<barangay> GetBarangay()
        {
            DataSet dsResult = new DataSet();
            var barangayList = new List<barangay>();
            try
            {
                Scmd = "EXEC [a_getBarangay]";
                dsResult = _db.executeStatement(Scmd);

                barangayList = (from DataRow dr in dsResult.Tables[0].Rows
                                select new barangay()
                                {
                                    barangay_id = Convert.ToInt32(dr[0]),
                                    barangay_name = Convert.ToString(dr[1])

                                }).ToList();
                return barangayList;
            }
            catch(Exception ex)
            {
                throw new ApplicationException("Error:" + dsResult, ex);
            }
           
        }
    }
}