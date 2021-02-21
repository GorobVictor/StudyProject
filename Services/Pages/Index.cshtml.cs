using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Services.Services;

namespace Services.Pages
{
    public class IndexModel : PageModel
    {
        public string _message { get; set; }

        public IndexModel(IMessageSender sender)
        {
            _message = sender.Send();
        }

        public void OnGet()
        {
        }
    }
}
