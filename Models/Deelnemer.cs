using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace SamenGamen.Models
{
    public class Deelnemer
    {
        [Key]
        public int Id { get; set; }
        
        [Required]
        [MinLength(4)]
        public string Naam { get; set; }
        
        public List<Aanmelding> Aanmeldingen { get; set; }
    }
}