using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNet.Mvc;

// For more information on enabling Web API for empty projects, visit http://go.microsoft.com/fwlink/?LinkID=397860

namespace MoneyHulkHost.Controllers
{
    [Route("api/[controller]/account/{id}")]
    public class ImportController : Controller
    {

        // POST api/values
        [HttpPost]
        public async Task<ActionResult> Post(int id)
        {
            if (!Request.Form.Files.Any())
            {
                return HttpBadRequest();
            }

            foreach (var file in Request.Form.Files)
            {
                var read = file.OpenReadStream();
                using (var reader = new StreamReader(read))
                {
                    var content = await reader.ReadToEndAsync();
                }
            }

            return Content("Received!");

        }
    }
}
