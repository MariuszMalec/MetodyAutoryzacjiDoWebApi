using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Net.Http.Headers;
using System.Text;
using System.Text.RegularExpressions;
using WepAppMvc.Models;

namespace WepAppMvc.Controllers
{
    public class BasicAuthController : Controller
    {
        private const string AppiUrl = "https://localhost:7022/api";
        IHttpClientFactory httpClientFactory;

        public BasicAuthController(IHttpClientFactory httpClientFactory)
        {
            this.httpClientFactory = httpClientFactory;
        }

        public async Task<ActionResult> Index()
        {

            var model = new BasicAuthenticateModel()
            {
               Username = "Mariusz",
               Password= "mariusz"
            };

            HttpClient client = httpClientFactory.CreateClient();

            var request = new HttpRequestMessage(HttpMethod.Get, $"{AppiUrl}/User");

            request.Headers.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            request.Content = new StringContent(JsonConvert.SerializeObject(model), Encoding.UTF8, "application/json");

            string username = model.Username;
            string password = model.Password;
            string svcCredentials = Convert.ToBase64String(ASCIIEncoding.ASCII.GetBytes(username + ":" + password));

            //request.Headers.Add("Authorization", "Basic " + svcCredentials);

            request.Headers.Authorization = new AuthenticationHeaderValue("Authorization", svcCredentials);

            var result = await client.SendAsync(request);

            var content = await result.Content.ReadAsStringAsync();

            if (result.StatusCode == System.Net.HttpStatusCode.BadRequest)
            {
                throw new Exception("Bad request!");
            }

            if (result.StatusCode == System.Net.HttpStatusCode.Unauthorized)
                throw new Exception("Unauthorized!");

            var users = JsonConvert.DeserializeObject<List<UserView>>(content);

            return View(users);
        }


        public ActionResult GetAllUsersWithAuthenticate()
        {
            return View();
        }

        [HttpPost]
        public async Task<ActionResult> GetAllUsersWithAuthenticate(ViewAuthenticationModel model)//pusty model
        {
            var client = httpClientFactory.CreateClient();

            var request = new HttpRequestMessage(HttpMethod.Get, $"{AppiUrl}/User");

            request.Headers.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            request.Content = new StringContent(JsonConvert.SerializeObject(model), Encoding.UTF8, "application/json");

            string username = model.Username;
            string password = model.Password;
            string svcCredentials = Convert.ToBase64String(ASCIIEncoding.ASCII.GetBytes(username + ":" + password));

            //request.Headers.Add("Authorization", "Basic " + svcCredentials);

            request.Headers.Authorization = new AuthenticationHeaderValue("Authorization", svcCredentials);

            var result = await client.SendAsync(request);

            if (result.StatusCode == System.Net.HttpStatusCode.Unauthorized)
            {
                return Content("Unauthorized!");
            }
            if (result.StatusCode == System.Net.HttpStatusCode.NotFound)
            {
                return Content("NotFound!");
            }

            var content = await result.Content.ReadAsStringAsync();

            if (result.StatusCode == System.Net.HttpStatusCode.BadRequest)
            {
                return Content($"Bad Request! {content}");
            }

            var viewModel = JsonConvert.DeserializeObject<List<UserFromApi>>(content);

            //return Content($"Result get from Api {content}");

            if (viewModel != null)
            {
                //trzeba serilize aby poszlo pomiedzy modelami albo poslac od razu content
                TempData["UserFromApi"] = JsonConvert.SerializeObject(viewModel);
                return RedirectToAction(nameof(ViewUsersFromApi));
            }

            return View();
        }

        public ActionResult ViewUsersFromApi()
        {
            var viewModel = JsonConvert.DeserializeObject<List<UserFromApi>>((string)TempData["UserFromApi"]);

            ViewBag.Users = TempData["UserFromApi"];

            return View(viewModel);
        }


    }
}
