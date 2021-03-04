using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ClientApp.Entity
{
    [Table ("Person")]
    public class Person
    {
        
        public int PersonId { get; set; }

        [Required(ErrorMessage = "Le nom est requis")]
        public string Nom { get; set; }
        [Required(ErrorMessage = "Le prénom est requis")]
        public String Prenom { get; set; }
    }
}
