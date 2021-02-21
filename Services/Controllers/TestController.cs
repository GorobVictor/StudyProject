using Microsoft.AspNetCore.Mvc;
using Services.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Services.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TestController: ControllerBase
    {
        public string _message { get; set; }
        public string _emailMessage { get; set; }
        public string _messageSender { get; set; }
        public TestController(IMessageSender sender, EmailMessageSender emailMessage, MessageSender messageSender)
        {
            _message = sender.Send();
            _emailMessage = emailMessage.Send();
            _messageSender = messageSender.Send();
        }

        [HttpGet]
        [Route("get")]
        public IActionResult Get()
        {
            return Ok(_message);
        }

        [HttpGet]
        [Route("getemail")]
        public IActionResult GetEmail()
        {
            return Ok(_emailMessage);
        }

        [HttpGet]
        [Route("getmessagesender")]
        public IActionResult GetMessageSender()
        {
            return Ok(_messageSender);
        }
    }
}
