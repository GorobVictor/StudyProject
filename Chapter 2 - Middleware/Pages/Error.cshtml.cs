using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Middleware.Pages
{
    public class ErrorModel : PageModel
    {
        public string _code { get; set; }
        public void OnGet(string code)
        {
            _code = code;
        }
    }
}
