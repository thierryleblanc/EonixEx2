using ClientApp.Entity;
using CLientApp.IRepository;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace ClientApp.Repos
{
    /// <summary>
    /// PersonRepository, communique avec l'API Gateway Ocelot (OcelotAPIGateway) pour joindre le microservice "PersonService"
    /// c'est un intermédiaire entre le domaine (entités) et la couche de mappage des données comme Entity Framework
    /// </summary>
    public class PersonRepository : IPersonRepository
    {
        HttpClient client;



        public PersonRepository()
        {

        }

        public HttpClient Client
        {
            get
            {
                return client;
            }
            set
            {
                client = value;
            }
        }


        public async Task<bool> AddAsync(Person person)
        {

            string strData = JsonConvert.SerializeObject(person);
            StringContent content = new StringContent(strData, Encoding.UTF8, "application/json");

            var response = await client.PostAsync(client.BaseAddress, content);
            if (response.IsSuccessStatusCode)
            {
                return true;
            }

            return false;
        }



        public async Task<bool> Delete(Person person)
        {
            var response = await client.DeleteAsync($"{client.BaseAddress}/{person.PersonId}");
            if (response.IsSuccessStatusCode)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public async Task<IList<Person>> GetPeopleAsync()
        {
            List<Person> model = null;
            var response = await client.GetAsync(client.BaseAddress);
            if (response.IsSuccessStatusCode)
            {
                var data = await response.Content.ReadAsStringAsync();
                model = (List<Person>)JsonConvert.DeserializeObject<IEnumerable<Person>>(data);
            }


            return model;
        }


        public async Task<Person> GetPerson(int personId)
        {
            Person model = null;
            var response = await client.GetAsync($"{client.BaseAddress}/{personId}");

            if (response.IsSuccessStatusCode)
            {
                var data = await response.Content.ReadAsStringAsync();
                model = (Person)JsonConvert.DeserializeObject<Person>(data);
            }

            return model;
        }

        public async Task<bool> UpdateAsync(Person person)
        {
            string strData = JsonConvert.SerializeObject(person);
            StringContent content = new StringContent(strData, Encoding.UTF8, "application/json");

            var response = await client.PutAsync($"{ client.BaseAddress}/{person.PersonId}", content);

            return response.IsSuccessStatusCode;
    }


        public List<Person> FilterPeopleList(string searchString, List<Person> model)
        {
            model = model.Where(
                c => c.Prenom.ToLower().Contains(searchString.ToLower()) ||
                c.Nom.ToLower().Contains(searchString.ToLower()))
                .ToList();

            return model;
        }
    }
}
