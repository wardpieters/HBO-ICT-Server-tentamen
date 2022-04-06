using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace SamenGamen.Models
{
    public class Aanmelding
    {
        [Key]
        public int Id { get; set; }
        
        [DisplayName("Datum")]
        public DateTime Aanmelddatum { get; set; }
        
        [DisplayName("Naam")]
        public int DeelnemerId { get; set; }
        public Deelnemer Deelnemer { get; set; }
        
        [DisplayName("Beschrijving")]
        public int GamevoorstelId { get; set; }
        public Gamevoorstel Gamevoorstel { get; set; }
    }
}