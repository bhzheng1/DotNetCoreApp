using System;
using System.Linq;
using System.Threading.Tasks;

namespace WebApi.BasicAuthentication
{
    public class UserValidate
    {
        //This method is used to check the user credentials
        public static Task<bool> ValidateCredentials(string username, string password)
        {
            UsersBL userBL = new UsersBL();
            var UserLists = userBL.GetUsers();
            return Task.FromResult(UserLists.Any(user =>
                user.UserName.Equals(username, StringComparison.OrdinalIgnoreCase)
                && user.Password == password));
        }
    }
}
