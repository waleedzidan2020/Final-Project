using Microsoft.AspNetCore.Mvc;
using System.Dynamic;

namespace Extrade.MVC
{
    public class HomeController : ControllerBase
    {
        
        public IActionResult Index()
        {
            dynamic x = new ExpandoObject();
            x.hi = "Hello World";
            return new ObjectResult(x);
        }
    }
}
