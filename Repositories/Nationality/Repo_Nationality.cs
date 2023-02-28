using ProjectBasedSystem.Database;
using ProjectBasedSystem.Interfaces.Nationality;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ProjectBasedSystem.Models;
using System.Data;

namespace ProjectBasedSystem.Repositories.Nationality
{
    public class Repo_Nationality : INationality
    {
        private database _db;
        private string Scmd { get; set; }
        public Repo_Nationality(database db)
        {
            _db = db;
        }

        public IEnumerable<nationality> getNationality()
        {
            DataSet dsResult = new DataSet();
            List<nationality> nationalityList = new List<nationality>();

            try
            {
                Scmd = "EXEC [a_getNationality]";
                dsResult = _db.executeStatement(Scmd);

                nationalityList = (from DataRow dr in dsResult.Tables[0].Rows
                                   select new nationality()
                                   {
                                       Nationality_id = Convert.ToInt32(dr[0]),
                                       nationality_name = Convert.ToString(dr[1])
                                   }).ToList();

                return nationalityList;
            }
            catch (Exception ex)
            {
                throw new ApplicationException("Error:" + dsResult, ex);
            }
       
           
                
   
            
        }
        private bool Disposed = false;
        public virtual void Dispose(bool disposing){

            if (!this.Disposed)
            {
                if (disposing)
                {
                    _db.Dispose();
                }
            }
            this.Disposed = true;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
            
        }
    }
}