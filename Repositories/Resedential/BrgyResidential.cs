using ProjectBasedSystem.Database;
using ProjectBasedSystem.Interfaces.Residential;
using ProjectBasedSystem.Models;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace ProjectBasedSystem.Repositories.Resedential
{
    public class BrgyResidential: IBrgyResedential
    {
         database _db;
          String Scmd = "";
        public BrgyResidential(database db)
        {
            _db = db;
        }      
        public IEnumerable<barangayResidential> GetResidentials()
        {
            List<barangayResidential> ResidentList = new List<barangayResidential>();
            DataSet dsResult = new DataSet();
            try
            {
                Scmd = "EXEC [a_getBrgyResidentials]";
                dsResult = _db.executeStatement(Scmd);               
                ResidentList = (from DataRow dr in dsResult.Tables[0].Rows
                                select new barangayResidential(){

                                residential_id = Convert.ToInt32(dr[0]),
                                first_Name = Convert.ToString(dr[1]),
                                last_Name  = Convert.ToString(dr[2]),
                                Suffix = Convert.ToString(dr[3]) != "" ? Convert.ToString(dr[3]) : "N/A",
                                birthdate = Convert.ToDateTime(dr[4]).ToString("yyyy/MM/dd"),
                                gender = Convert.ToString(dr[5]),
                                marital_status = Convert.ToString(dr[6]),
                                contact_no = Convert.ToString(dr[7]),
                                email_add = Convert.ToString(dr[8]),
                                household_no = Convert.ToString(dr[9]),
                                zone_no = Convert.ToString(dr[10]),
                                barangay_id = dr[11] is DBNull ? 0 : Convert.ToInt32(dr[11]),
                                household_member = dr[12] is DBNull ? 0 : Convert.ToInt32(dr[12]),
                                length_of_stay = Convert.ToInt32(dr[13]),
                                religion_id = Convert.ToInt32(dr[14]),
                                occupation = Convert.ToString(dr[15]),
                                nationality_id = Convert.ToInt32(dr[16]),
                                Philhealth_no = Convert.ToString(dr[17]),
                                voters = Convert.ToBoolean(dr[18]),
                                residency_status = Convert.ToString(dr[19]),
                                is_Delete = Convert.ToBoolean(dr[20])
                                }).ToList(); 
      
                return ResidentList;
            }
             catch(Exception ex)
            {
                throw new ApplicationException("Error:" + dsResult, ex);
            }
    }

        public bool AddUpdate(barangayResidential items)
        {
           

            Hashtable param = new Hashtable();
            DataSet dsResult = new DataSet();
            try
            {
               
                    Scmd = @"EXEC [a_AddUpdateResident]  @residential_id,@first_Name,@last_Name,@Suffix,@birthdate,@gender,
                @marital_status,@contact_no,@email_add,@household_no,@zone_no,@barangay_id,@household_member,
                @length_of_stay,@religion_id,@occupation,@nationality_id,@Philhealth_no,@voters,@residency_status";
                    param.Add("@residential_id", items.residential_id);
                    param.Add("@first_Name", items.first_Name);
                    param.Add("@last_Name", items.last_Name);
                    param.Add("@Suffix", items.Suffix); 
                    param.Add("@birthdate", Convert.ToDateTime(items.birthdate).ToString("yyyy/MM/dd"));
                    param.Add("@gender", items.gender);
                    param.Add("@marital_status", items.marital_status);
                    param.Add("@contact_no", items.contact_no);
                    param.Add("@email_add", items.email_add);
                    param.Add("@household_no", items.household_no);
                    param.Add("@zone_no", items.zone_no);
                    param.Add("@barangay_id", items.barangay_id);
                    param.Add("@household_member", items.household_member);
                    param.Add("@length_of_stay", items.length_of_stay);
                    param.Add("@religion_id", items.religion_id);
                    param.Add("@occupation", items.occupation);
                    param.Add("@nationality_id", items.nationality_id);
                    param.Add("@Philhealth_no", items.Philhealth_no);
                    param.Add("@voters", items.voters);
                    param.Add("@residency_status", items.residency_status);

                    dsResult = _db.executeStatement(Scmd, param);
                
          

                if(dsResult.Tables[0].Rows[0][0].ToString().IndexOf("Success") > -1)
                {
                     return true;
                }else
                {
                    return false;
                }

            }
            catch(Exception ex)
            {
                throw new ApplicationException("Error:" + dsResult, ex);
            }
        }
        public IEnumerable<residentialImages> GetResidentialsImage()
        {
            IList<residentialImages> ResidentProfileList = new List<residentialImages>();
            DataSet dsResult = new DataSet();
            try
            {
                Scmd = "EXEC [a_GetProfileImageByResidentId]";
                dsResult = _db.executeStatement(Scmd);
                ResidentProfileList = (from DataRow dr in dsResult.Tables[0].Rows
                                select new residentialImages()
                                {
                                    profile_id = Convert.ToInt32(dr[0]),
                                    residential_id = Convert.ToInt32(dr[1]),
                                    FileName = Convert.ToString(dr[2]),
                                    Description = Convert.ToString(dr[3]),
                                    FilePath = Convert.ToString(dr[4]),
                                    FileSize = Convert.ToInt32(dr[5]),
                                }).ToList();

                return ResidentProfileList;
            }
            catch (Exception ex)
            {
                throw new ApplicationException("Error:" + dsResult, ex);
            }
        }
        public bool FileSave(residentialImages image)
        {
            Hashtable param = new Hashtable();
            DataSet dsResult = new DataSet();
            try
            {
                Scmd = "EXEC a_AddUpdateResidential_images @residential_id,@profile_id,@FileName,@Description,@FilePath,@FileSize";
                param.Add("@residential_id", image.residential_id);
                param.Add("@profile_id", image.profile_id);
                param.Add("@FileName", image.FileName);
                param.Add("@Description", image.Description);
                param.Add("@FilePath", image.FilePath);
                param.Add("@FileSize", image.FileSize);
                dsResult = _db.executeStatement(Scmd, param);

                if(dsResult.Tables[0].Rows[0][0].ToString().IndexOf("Success") > -1)
                {
                    return true;
                }else
                {
                    return false;
                }
            }
            catch(Exception ex)
            {
                throw new ApplicationException("Error:" + dsResult, ex);
            }
          
            
        }






        private bool Disposed = false;
        public virtual void Dispose(bool disposing)
        {

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