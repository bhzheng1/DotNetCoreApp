using System.Net.Http;
using System.Text;
using Newtonsoft.Json;

namespace One.API.Client
{
    public class JsonContent : StringContent
    {
        public JsonContent(object value) : base(JsonConvert.SerializeObject(value, Formatting.None, new JsonSerializerSettings { NullValueHandling = NullValueHandling.Ignore }), Encoding.UTF8, "application/json")
        {
        }
    }
}
