using CrawlerService;
using CrawlerService.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace WebApiApplication2.Controllers
{
    public class CrawlerController : ApiController
    {
        private readonly IVnExpressParser _parser;

        public CrawlerController(IVnExpressParser parser)
        {
            _parser = parser;
        }

        public IEnumerable<Menu> ExtractMenu(string url)
        {
            return _parser.ExtractMenu(url);
        }
    }
}
