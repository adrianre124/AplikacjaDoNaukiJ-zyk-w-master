using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using AplikacjaDoNaukiJęzyków.Models;
using Microsoft.EntityFrameworkCore.Storage;

namespace AplikacjaDoNaukiJęzyków.Data
{
    public class Seed
    {
        public static void SeedWords(DatabaseContext context)
        {
            context.Slowa.RemoveRange(context.Slowa);
            context.SaveChanges();

            if (context.Slowa.Any()) return;

            var wordData = File.ReadAllText("Data/WordSeedData.json");

            var options = new JsonSerializerOptions{PropertyNameCaseInsensitive = true};

            var words = JsonSerializer.Deserialize<List<Slowo>>(wordData);

            foreach (var word in words)
            {
                context.Slowa.Add(word);
            }

            context.SaveChanges();
        }
    }
}