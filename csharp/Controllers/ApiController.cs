using csharp.Extensions;
using csharp.Database;
using Microsoft.AspNet.Mvc;
using Newtonsoft.Json.Linq;
using System;
using System.Net.Http;
using System.Threading.Tasks;

namespace csharp.Controllers {
    
    [Route("/[controller]")]
    public class ApiController : Controller {
    
        DbConfig db;
        
        public ApiController() {
            db = new DbConfig();
        }

        // Sample uri http://localhost:5000/api/dynamic
        [HttpPost("dynamic")]
        public IActionResult ProcessTransaction([FromBody]dynamic body) {
            if (!ModelState.IsValid) {
                return HttpBadRequest(ModelState);
            }
                
            try {
                if (body.tags == null || body.tags.Count < 2) {
                    body.tags = new JArray();
                    JArray tags = body.tags;
                    tags.Add("legal");
                    tags.Add("opa");
                }
                
                if (body.values == null || body.values.Count < 2) {
                    body.values = new JArray();
                } else {
                    JArray tags = body.tags;
                    string key1 = tags[0].ToString(),
                           key2 = tags[1].ToString();
                    
                    JArray values = body.values;
                    int value1 = 0,
                        value2 = 0;
                    
                    if (int.TryParse(values[0].ToString(), out value1) &&
                        int.TryParse(values[1].ToString(), out value2)) {
                        
                        int fact1 = db.findTag(key1),
                            fact2 = db.findTag(key2);
                        values.Add(value1 * fact1 + value2 * fact2);        
                    }
                }
                db.insertTransaction(body);

                return Json(body);
            } catch (Exception e) {
                Console.WriteLine("{0} Exception caught.", e);
                return HttpBadRequest();
            }
        }
    }
}
