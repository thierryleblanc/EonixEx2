using ClientApp.Entity;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;

namespace CLientApp.IRepository
{
    /// <summary>
    /// Interface IPersonRepository, permet d'exposer les méthodes de PersonRepository. Cette interface est actuellement utilisée par le client (application ClientApp). 
    /// évolution possible: implémenter ceci par le microservice PersonService pour son controlleur.
    /// </summary>
    public interface IPersonRepository
    {
        Task<IList<Person>> GetPeopleAsync();
        Task<Person>  GetPerson(int personId);
        Task<bool> AddAsync(Person person);
        Task<bool> UpdateAsync(Person person);
        Task<bool> Delete(Person person);
        List<Person> FilterPeopleList(string searchString, List<Person> model);

        HttpClient Client { get; set; }
    }
}
