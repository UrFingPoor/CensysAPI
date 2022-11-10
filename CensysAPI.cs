using System;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Newtonsoft.Json.Linq;

namespace ExampleProject
{
    public class CensysAPI
    {
        //Python Port To C# By Me For Fuck Sake They Need Better Docs!
        public async Task<string> Search(string host)
        {
            using (var client = new HttpClient())
            {
                dynamic response = string.Empty;
                client.DefaultRequestHeaders.Add("Authorization", $"Basic {Convert.ToBase64String(Encoding.ASCII.GetBytes($"{Properties.Settings.Default.API_ID}:{Properties.Settings.Default.APIKEY}"))}");
                HttpResponseMessage result = await client.GetAsync($"https://search.censys.io/api/v2/hosts/search?q={host}");
                switch (result.StatusCode)
                {
                    case HttpStatusCode.OK:
                        response = JObject.Parse(await result.Content.ReadAsStringAsync());
                        File.WriteAllText(AppDomain.CurrentDomain.BaseDirectory + $"{host}.json", Convert.ToString(response));
                        MessageBox.Show($"Saved To {AppDomain.CurrentDomain.BaseDirectory + $"{host}.json"}!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        break;
                    case HttpStatusCode.BadRequest:
                        MessageBox.Show($"Bad Request: The request you made is invalid.", "Error!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        break;
                    case HttpStatusCode.Unauthorized:
                        MessageBox.Show("Unauthorized: You must authenticate with a valid API ID and secret.", "Error!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        break;
                    case (HttpStatusCode)422:  //since im coding with the 4.8 framework, this is not included so i added it but the updated lib includes this. .Net 6.0 Use: HttpStatusCode.UnprocessableEntity
                        MessageBox.Show("invalid cursor: Remove The https://.", "Error!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        break;
                    case (HttpStatusCode)429: //since im coding with the 4.8 framework, this is not included so i added it but the updated lib includes this.  .Net 6.0 Use: HttpStatusCode.TooManyRequests
                        MessageBox.Show("Too many requests: Please wait abit before resending the request.", "The Bro's", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        break;
                }
                return response;
            }
        }
    }
}
