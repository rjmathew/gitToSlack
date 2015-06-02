using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using System.Web.Http;

namespace GithubPRProxy.Controllers
{
    public class ProxyController : ApiController
    {
        // GET api/values
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET api/values/5
        private async Task<string> PostToSlack(string value)
        {
           
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("https://hooks.slack.com/services/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");
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                // New code:
                var obj = new { text="Pull Request Created"};
                HttpResponseMessage response = await client.PostAsJsonAsync(client.BaseAddress,obj);
                if (response.IsSuccessStatusCode)
                {
                    Console.WriteLine("Success");
                }
            }
            return "ok";
        }
        // POST api/values
        public async Task<IHttpActionResult> Post([FromBody]string value)
        {
            await PostToSlack(value);
            return Ok(value);
        }

        // PUT api/values/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE api/values/5
        public void Delete(int id)
        {
        }
    }
}
