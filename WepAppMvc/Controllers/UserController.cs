using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Net.Http.Headers;
using WepAppMvc.Models;

namespace WepAppMvc.Controllers
{
    public class UserController : Controller
    {
        IHttpClientFactory httpClientFactory;
        private const string AppiUrl = "https://localhost:7020/api";

        // GET: UserController
        public UserController(IHttpClientFactory httpClientFactory)
        {
            this.httpClientFactory = httpClientFactory;
        }
        public async Task<ActionResult> Index()
        {
            HttpClient client = httpClientFactory.CreateClient();

            var request = new HttpRequestMessage(HttpMethod.Get, $"{AppiUrl}/User");

            request.Headers.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            var result = await client.SendAsync(request);

            var content = await result.Content.ReadAsStringAsync();

            if (result.StatusCode == System.Net.HttpStatusCode.BadRequest)
            {
                throw new Exception("Bad request!");
            }

            var users = JsonConvert.DeserializeObject<List<ViewUser>>(content);

            return View(users);
        }

        public async Task<ActionResult> IndexWithRoute()
        {
            var model = new AuthenticateModel();
            model.Email = "mariusz";
            model.Password = "mm";

            HttpClient client = httpClientFactory.CreateClient();

            var request = new HttpRequestMessage(HttpMethod.Get, $"{AppiUrl}/User/{model.Email}/{model.Password}");

            request.Headers.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            var result = await client.SendAsync(request);

            var content = await result.Content.ReadAsStringAsync();

            if (result.StatusCode == System.Net.HttpStatusCode.BadRequest)
            {
                throw new Exception("Bad request!");
            }

            var users = JsonConvert.DeserializeObject<List<ViewUser>>(content);

            return View(users);
        }

        public ActionResult GetUsersWithAutorizeRoute()
        {
            return View();
        }
        // POST: UserController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> GetUsersWithAutorizeRoute(AuthenticateModel model)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return View(model);
                }

                HttpClient client = httpClientFactory.CreateClient();

                var request = new HttpRequestMessage(HttpMethod.Get, $"{AppiUrl}/User/{model.Email}/{model.Password}");

                request.Headers.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                var result = client.Send(request);

                var content = await result.Content.ReadAsStringAsync();

                if (result.StatusCode == System.Net.HttpStatusCode.BadRequest)
                {
                    return Content("Bad request!");
                }
                if (result.StatusCode == System.Net.HttpStatusCode.Unauthorized)
                {
                    return Content("Unauthorized!");
                }

                var users = JsonConvert.DeserializeObject<List<ViewUser>>(content);

                return RedirectToAction(nameof(Index2), new { param = JsonConvert.SerializeObject(users) });
            }
            catch
            {
                return View();
            }
        }
        public ActionResult Index2(string param)
        {
            if (param is null)
                return Content("Bad Request!");
            var users = JsonConvert.DeserializeObject<List<ViewUser>>(param);

            return View(users);
        }
        // GET: UserController/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: UserController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: UserController/Create
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

        // GET: UserController/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: UserController/Edit/5
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

        // GET: UserController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: UserController/Delete/5
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
