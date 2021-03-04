using ClientApp.Entity;
using ClientApp.Repos;
using CLientApp.IRepository;
using Newtonsoft.Json;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;

namespace UnitTestApiGateway
{
    public class Tests
    {

      
        


        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public async Task IsThierryLeblancIsFirstRowInDatabaseTest()
        {
            string ApiAddress;
            Uri baseAddress;
            HttpClient client;

            ApiAddress = "https://localhost:44371/PersonService";
             baseAddress = new Uri(ApiAddress);
             client = new HttpClient();
            client.BaseAddress = baseAddress;

            List<Person> model = new List<Person>();
            var response = await client.GetAsync(client.BaseAddress);
            if (response.IsSuccessStatusCode)
            {
                var data = await response.Content.ReadAsStringAsync();
                model = (List<Person>)JsonConvert.DeserializeObject<IEnumerable<Person>>(data);
            }


            Person PersonExpected = new Person();
            PersonExpected.PersonId = 1;
            PersonExpected.Nom = "Leblanc";
            PersonExpected.Prenom = "Thierry";

            var tmp = model.Find(c => c.PersonId == 1);

            Assert.AreEqual(PersonExpected.ToString(), tmp.ToString(), "Leblanc Thierry n'existe pas dans le jeu de donnée avec l'ID 1 ou a été modifié");
        }

        [Test]
        public async Task IsRowsExistsInDatabaseTest() {

            string ApiAddress;
            Uri baseAddress;
            HttpClient client;

            ApiAddress = "https://localhost:44371/PersonService";
            baseAddress = new Uri(ApiAddress);
            client = new HttpClient();
            client.BaseAddress = baseAddress;

            List<Person> model = new List<Person>();
            var response = await client.GetAsync(client.BaseAddress);
            if (response.IsSuccessStatusCode)
            {
                var data = await response.Content.ReadAsStringAsync();
                model = (List<Person>)JsonConvert.DeserializeObject<IEnumerable<Person>>(data);
            }


            int count = model.Count;
            Assert.IsTrue(count > 0, "La DB contient des données");

        }

        [Test]
        public async Task IsRowsExistsInDatabaseWithRepositoryTest() {
            string ApiAddress= "https://localhost:44371/PersonService"; 
            Uri baseAddress;
            HttpClient client;

            baseAddress = new Uri(ApiAddress);
            client = new HttpClient();
            client.BaseAddress = baseAddress;

            IPersonRepository repo = new PersonRepository();
            repo.Client = client;

            Person model = await repo.GetPerson(1);

            Assert.AreEqual("Leblanc", model.Nom, "Le premier record ne contient pas le nom Leblanc");
        }
    }
}