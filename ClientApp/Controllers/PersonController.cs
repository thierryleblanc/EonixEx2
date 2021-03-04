using ClientApp.Entity;
using ClientApp.Models;
using ClientApp.Repos;
using CLientApp.IRepository;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace ClientApp.Controllers
{
    public class PersonController : Controller
    {
        HttpClient client;
        Uri baseAddress;
        IConfiguration configuration;
        IPersonRepository repo;

        public PersonController(IConfiguration Configuration, IPersonRepository persRepo)
        {
            configuration = Configuration;
            baseAddress = new Uri(configuration["ApiAddress"]);
            client = new HttpClient();
            client.BaseAddress = baseAddress;

            repo = persRepo;
            repo.Client = client;
        }

        // requête asynchrone avec async et await
        public async Task<IActionResult> Index(string searchString)
        {
            ViewData["CurrentFilter"] = searchString;
            List<Person> model = null;

            //get data from repo
            model = (List<Person>)await repo.GetPeopleAsync();

            //filter by name & firstname
            if (model != null)
            {
                if (!String.IsNullOrEmpty(searchString))
                {
                    model = repo.FilterPeopleList(searchString, model);
                }
            }

            return View(model);
        }

        public IActionResult Create()
        {
            return View();
        }



        [HttpPost]
        public async Task<IActionResult> Create(Person model)
        {
            if (ModelState.IsValid)
            {
                bool isSuccessStatusCode = await repo.AddAsync(model);

                if (isSuccessStatusCode)
                {
                    return RedirectToAction("index");
                }
            }
            return View();
        }



        public async Task<IActionResult> Delete(int id)
        {
            Person model = await repo.GetPerson(id);
            
            return View(model);
        }

        [HttpPost]
        [ActionName("Delete")]
        public async Task<IActionResult> ConfirmDelete(int id)
        {
            
            Person model = await repo.GetPerson(id);
            if (model != null) {
                await repo.Delete(model);
            }

            return RedirectToAction("index");
        }



        [HttpGet]
        public async Task<ActionResult> Edit(int id)
        {
            /*
            PersonViewModel model = new PersonViewModel();
            var response = await client.GetAsync($"{client.BaseAddress}/{id}");
            if (response.IsSuccessStatusCode)
            {
                var data = await response.Content.ReadAsStringAsync();
                model = (PersonViewModel)JsonConvert.DeserializeObject<PersonViewModel>(data);
            }*/

            Person model = null;
            model = await repo.GetPerson(id);

            return View(model);
        }


        [HttpPost]
        public async Task<ActionResult> Edit(Person model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    bool isSuccessStatus = await repo.UpdateAsync(model);
                    //string strData = JsonConvert.SerializeObject(model);
                    //StringContent content = new StringContent(strData, Encoding.UTF8, "application/json");

                    //var response = await client.PutAsync($"{ client.BaseAddress}/{model.PersonId}", content);
                    if (isSuccessStatus)
                    {
                        return RedirectToAction("index");
                    }
                }
                else
                {
                    return View(model);
                }

            }
            catch
            {
                return View();
            }
            return RedirectToAction("index");
        }
    }
}
