using Microsoft.Web.Administration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApi.BasicAuthentication
{
    public class User
    {
        public int ID { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
    }
    public class UsersBL
    {
        public List<User> GetUsers()
        {
            // In Real-time you need to get the data from any persistent storage
            // For Simplicity of this demo and to keep focus on Basic Authentication
            // Here we are hardcoded the data
            var userList = new List<User>();
            userList.Add(new User()
            {
                ID = 101,
                UserName = "MaleUser",
                Password = "123456"
            });
            userList.Add(new User()
            {
                ID = 101,
                UserName = "FemaleUser",
                Password = "abcdef"
            });
            return userList;
        }

        //https://johnlnelson.com/2014/06/15/the-microsoft-web-administration-namespace/
        public List<User> GetApplicationPoolIdentityUsers() {
            var userList = new List<User>();
            ServerManager server = new ServerManager();
            ApplicationPoolCollection applicationPools = server.ApplicationPools;
            foreach (ApplicationPool pool in applicationPools)
            {
                //get the AutoStart boolean value
                bool autoStart = pool.AutoStart;

                //get the name of the ManagedRuntimeVersion
                string runtime = pool.ManagedRuntimeVersion;

                //get the name of the ApplicationPool
                string appPoolName = pool.Name;

                //get the identity type
                ProcessModelIdentityType identityType = pool.ProcessModel.IdentityType;

                //get the username for the identity under which the pool runs
                string userName = pool.ProcessModel.UserName;

                //get the password for the identity under which the pool runs
                string password = pool.ProcessModel.Password;

                userList.Add(new User { UserName = userName, Password = password });
            }
            return userList;
        }
    }
}
