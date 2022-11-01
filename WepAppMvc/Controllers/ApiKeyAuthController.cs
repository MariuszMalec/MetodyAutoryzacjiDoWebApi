using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Net.Http.Headers;
using System.Net.Http;
using WepAppMvc.Models;
using System.Text;

namespace WepAppMvc.Controllers
{
    public class ApiKeyAuthController : Controller
    {
        // GET: ApiKeyAuthController
        IHttpClientFactory httpClientFactory;
        private const string AppiUrl = "https://localhost:7089/api";

        // GET: UserController
        public ApiKeyAuthController(IHttpClientFactory httpClientFactory)
        {
            this.httpClientFactory = httpClientFactory;
        }
        public async Task<ActionResult> Index()
        {
            HttpClient client = httpClientFactory.CreateClient();

            var request = new HttpRequestMessage(HttpMethod.Get, $"{AppiUrl}/Client/ApiKey");

            request.Headers.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            request.Headers.Add("ApiKey", "8e421ff965cb4935ba56ef7833bf4750");//TODO Apikey do headera

            var result = await client.SendAsync(request);

            var content = await result.Content.ReadAsStringAsync();

            if (result.StatusCode == System.Net.HttpStatusCode.BadRequest)
            {
                throw new Exception("Bad request!");
            }
            if (result.StatusCode == System.Net.HttpStatusCode.NotFound)
            {
                throw new Exception("Not found!");
            }

            var users = JsonConvert.DeserializeObject<List<ClientView>>(content);

            return View(users);
        }

        public ActionResult GetAllUsersWithAuthenticate()
        {
            return View();
        }

        [HttpPost]
        public async Task<ActionResult> GetAllUsersWithAuthenticate(AuthenticateApiKey key)
        {
            var client = httpClientFactory.CreateClient();

            var request = new HttpRequestMessage(HttpMethod.Get, $"{AppiUrl}/Client/ApiKey");

            request.Headers.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            //request.Headers.Authorization = new AuthenticationHeaderValue("ApiKey", key.ApiKey);

            request.Headers.Add("ApiKey", key.ApiKey);//TODO Apikey do headera

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

            var viewModel = JsonConvert.DeserializeObject<List<ClientView>>(content);

            //return Content($"Result get from Api {content}");

            if (viewModel != null)
            {
                //trzeba serilize aby poszlo pomiedzy modelami albo poslac od razu content
                TempData["ClientFromApi"] = JsonConvert.SerializeObject(viewModel);
                return RedirectToAction(nameof(ViewClientFromApi));
            }

            return View();
        }

        public ActionResult ViewClientFromApi()
        {
            var viewModel = JsonConvert.DeserializeObject<List<ClientView>>((string)TempData["ClientFromApi"]);

            ViewBag.Users = TempData["ClientFromApi"];

            return View(viewModel);
        }

        // GET: ApiKeyAuthController/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: ApiKeyAuthController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: ApiKeyAuthController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: ApiKeyAuthController/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: ApiKeyAuthController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: ApiKeyAuthController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: ApiKeyAuthController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}
