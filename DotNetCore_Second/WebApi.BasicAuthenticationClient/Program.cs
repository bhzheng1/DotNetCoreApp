using Microsoft.Web.Administration;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Security.Principal;
using System.Text.Json;

namespace WebApi.BasicAuthenticationClient
{
    class Program
    {
        static void Main(string[] args)
        {
            var endPointAddress = "http://localhost:8080/";
            var client = new HttpClient();
            client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Basic", GetAuthorization());
            var apiUrl = $"{endPointAddress}api/employee";
            var response = client.GetAsync(apiUrl).Result;
            var rst = response.Content.ReadAsStringAsync().Result;
            var employees = JsonSerializer.Deserialize<IEnumerable<Employee>>(rst);
            foreach (var item in employees)
            {
                Console.WriteLine(item.Name);
            }
            Console.WriteLine("Hello World!");
        }
        static string GetAuthorization() 
        {
            var user = WindowsIdentity.GetCurrent().Name;
            var pwd = "";
            var server = new ServerManager();
            var applicationPools = server.ApplicationPools;
            foreach (var pool in applicationPools)
            {
                var userName = pool.ProcessModel.UserName;
                if (userName == user) {
                    pwd = pool.ProcessModel.Password;
                }
            }

            return Convert.ToBase64String(System.Text.Encoding.ASCII.GetBytes($"{user}:{pwd}"));
        }
    }
}
