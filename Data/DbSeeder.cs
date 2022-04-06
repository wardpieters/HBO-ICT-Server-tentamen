using System.Linq;
using SamenGamen.Models;

namespace SamenGamen.Data
{
    public class DbSeeder
    {
        private static SamenGamenContext _context;

        public static void SeedData(SamenGamenContext context)
        {
            _context = context;
            
            // Opdracht 2: ... "en er nog geen enkele Deelnemeer in de database staat" ...
            if (!_context.Deelnemer.Any())
            {
                AddPredefinedDeelnemers();
            }

            // Opdracht 4: ... "en er nog geen enkel GameVoorstel in de database is opgenomen" ...
            if (!_context.Gamevoorstel.Any())
            {
                AddPredefinedGamevoorstellen();
            }
            
            _context.SaveChanges();
        }

        private static void AddPredefinedDeelnemers()
        {
            string[] newNames = {"Jonathan", "Mirjam", "Andries", "Gerard", "Tineke"};

            foreach (string name in newNames)
            {
                _context.Deelnemer.Add(new Deelnemer { Naam = name });
            }
        }

        private static void AddPredefinedGamevoorstellen()
        {
            string[] newGames = {"World of Warcraft", "Need for Speed", "Age of Empire"};

            foreach (string desc in newGames)
            {
                _context.Gamevoorstel.Add(new Gamevoorstel { Beschrijving = desc });
            }
        }
    }
}