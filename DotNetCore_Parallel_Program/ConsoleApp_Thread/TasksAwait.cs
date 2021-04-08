using System;
using System.Net.Http;

//一个CPU在同一时间段内是只能执行一个线程的, 所有async和await建议在IO或者是网络操作的时候使用
namespace ConsoleApp_Thread
{
    class TasksAwait
    {
        public async static void Run() {
            var url = "http://www.google.com/";
            var isValidUrl = Uri.TryCreate(url, UriKind.Absolute, out _);

            if (!isValidUrl) {
                return;
            }

            var httpClient = new HttpClient();
            var response = await httpClient.GetAsync(url);

            try
            {
                response.EnsureSuccessStatusCode();
                var data = await response.Content.ReadAsStringAsync();
                Console.WriteLine(data);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                throw;
            }
        }
    }
}
