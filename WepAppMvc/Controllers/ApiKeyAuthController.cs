using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Net.Http.Headers;
using System.Net.Http;
using WepAppMvc.Models;

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

            var request = new HttpRequestMessage(HttpMethod.Get, $"{AppiUrl}/User/GetWithApiKey");

            request.Headers.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

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
