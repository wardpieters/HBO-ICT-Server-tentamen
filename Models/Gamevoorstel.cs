using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace SamenGamen.Models
{
    public class Gamevoorstel
    { 
        [Key]
        public int Id { get; set; }
        
        [MaxLength(20)]
        public string Beschrijving { get; set; }
        
        public List<Aanmelding> Aanmeldingen { get; set; }
    }
}