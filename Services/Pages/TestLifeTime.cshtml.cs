using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Services.Services;

namespace Services.Pages
{
    public class TestLifeTimeModel : PageModel
    {
        public Transient Transient { get; set; }
        public Scoped Scoped { get; set; }
        public Singleton Singleton { get; set; }

        public TestLifeTimeModel(Transient transient, Scoped scoped, Singleton singleton)
        {
            Transient = transient;
            Scoped = scoped;
            Singleton = singleton;
        }

        public void OnGet()
        {
        }
    }
}
