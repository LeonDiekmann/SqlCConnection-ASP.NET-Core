using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SqlCConnection_ASP_Net_Core.Helper
{
    public class Authorization
    {
        public static bool decode(string authVal)
        {
            try
            {
                string[] authValSplit = authVal.Split(" ");
                Console.WriteLine(authValSplit[1]);
                var handler = new JwtSecurityTokenHandler();
                var token = handler.ReadToken(authValSplit[1]) as JwtSecurityToken;
                var PersonID = token.Claims.First(claim => claim.Type == "PersonID").Value;
                Console.WriteLine(token);
                Console.WriteLine(PersonID);
                string[] adminList = new string[] { "128-77449" };

                for (int i = 0; i < adminList.Length; i++)
                {
                    if (adminList[i] == PersonID)
                    {
                        return true;
                    }
                }
            }
            catch (Exception)
            {

                throw new Helper.RepoException<Helper.UpdateResultType>(Helper.UpdateResultType.INVALIDEARGUMENT);
            }
            return false;
        }
    }
}
