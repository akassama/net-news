using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NetNews.Models.AppModels
{
    public class AppValidations
    {
        //Validate profile completion
        /// <summary>
        /// Check if user has filled account details
        /// </summary>
        /// <returns>boolean</returns>
        public bool ProfileCompleted(string account_id)
        {
            using(var db = new DBConnection())
            {
                //check if first name completed
                if(string.IsNullOrEmpty(db.Accounts.Where(s => s.AccountID == account_id).FirstOrDefault().FirstName))
                {
                    return false;
                }
                //check if last name completed
                if (string.IsNullOrEmpty(db.Accounts.Where(s => s.AccountID == account_id).FirstOrDefault().LastName))
                {
                    return false;
                }
                //check if email completed
                if (string.IsNullOrEmpty(db.Accounts.Where(s => s.AccountID == account_id).FirstOrDefault().Email))
                {
                    return false;
                }
                //check if profile picture completed
                //if (string.IsNullOrEmpty(db.Accounts.Where(s => s.AccountID == account_id).FirstOrDefault().ProfilePicture))
                //{
                //    return false;
                //}
                //check if country is completed
                if (string.IsNullOrEmpty(db.AccountDetails.Where(s => s.AccountID == account_id).FirstOrDefault().Country))
                {
                    return false;
                }
                //check if phone number is completed
                if (string.IsNullOrEmpty(db.AccountDetails.Where(s => s.AccountID == account_id).FirstOrDefault().PhoneNumber))
                {
                    return false;
                }
                //check if biography is completed
                if (string.IsNullOrEmpty(db.AccountDetails.Where(s => s.AccountID == account_id).FirstOrDefault().Biography))
                {
                    return false;
                }
                //check if date of birth is completed
                if (db.AccountDetails.Where(s => s.AccountID == account_id).FirstOrDefault().DateOfBirth == null)
                {
                    return false;
                }
                //check if gender is completed
                if (string.IsNullOrEmpty(db.AccountDetails.Where(s => s.AccountID == account_id).FirstOrDefault().Gender))
                {
                    return false;
                }
            }
            return true;
        }
    }
}
