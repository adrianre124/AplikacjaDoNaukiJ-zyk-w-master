using AplikacjaDoNaukiJęzyków.Data;
using AplikacjaDoNaukiJęzyków.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AplikacjaDoNaukiJęzyków.Controllers
{
    public class SlownikController : Controller
    {
        private readonly DatabaseContext _context;
        public IEnumerable<Slowo> Slowa { get; set; }
        public string SearchTerm { get; set; }
        public SlownikController(DatabaseContext context)
        {
            _context = context;
        }
        public IActionResult Slownik()
        {
            var slowa = _context.Slowa;
            return View(slowa);
        }

        public IActionResult PoziomWyb(string nazwaJezyka)
        {
            ViewBag.NazwaJezyka = nazwaJezyka;
            return View();
        }

        public IActionResult Znajdz(string SearchString)
        {
            ViewData["CurrentFilter"] = SearchString;
            var slowa = from s in _context.Slowa
                        select s;
            if (!String.IsNullOrEmpty(SearchString))
            {
                slowa = slowa.Where(s => s.SlowoAng.Contains(SearchString) ||
                                            s.SlowoPol.Contains(SearchString) ||
                                            s.SlowoUkr.Contains(SearchString) ||
                                            s.SlowoAra.Contains(SearchString));
            }

            return View(slowa);
        }
    }
}
