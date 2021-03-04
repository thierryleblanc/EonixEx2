using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ClientApp.Models
{
    public class PersonViewModel
    {
        /*champs sont similaire à l'entité Person du projet (Microservice) PersonService*/
        public int PersonId { get; set; }

        [Required(ErrorMessage = "Le nom est requis")]
        public string Nom { get; set; }
        [Required(ErrorMessage = "Le prénom est requis")]
        public String Prenom { get; set; }
    }
}
