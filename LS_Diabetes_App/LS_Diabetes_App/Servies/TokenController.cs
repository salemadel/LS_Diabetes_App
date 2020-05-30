using LS_Diabetes_App.Models.Data_Models;
using Plugin.SecureStorage;
using System;
using System.Text;

namespace LS_Diabetes_App.Servies
{
    public class TokenController
    {
        private readonly string key = "hA@!3*%ob7W)/q";

        public (bool, Token_Model) DecodeToken()
        {
            bool token_exist = CrossSecureStorage.Current.HasKey("acces_token");
            if (token_exist)
            {
                string token = CrossSecureStorage.Current.GetValue("acces_token");
                return (true, JWT.JsonWebToken.DecodeToObject<Token_Model>(token, Encoding.Unicode.GetBytes(key), false));
            }
            else
            {
                return (false, null);
            }
        }

        private DateTime UnixTimeStampToDateTime(double unixTimeStamp)
        {
            DateTime dtDateTime = new DateTime(1970, 1, 1, 0, 0, 0, 0, System.DateTimeKind.Utc);
            dtDateTime = dtDateTime.AddSeconds(unixTimeStamp).ToLocalTime();
            return dtDateTime;
        }

        public bool Token_Expired()
        {
            var jwt = DecodeToken();
            if (jwt.Item1)
            {
                DateTime now = DateTime.Now;
                DateTime exp = UnixTimeStampToDateTime(jwt.Item2.exp);
                if (exp >= now)
                {
                    return false;
                }
                else
                {
                    return true;
                }
            }
            else
            {
                return true;
            }
        }
    }
}