using System;
using csharp.Extensions;
using csharp.Database;
using Microsoft.AspNet.Mvc;
using System.Net.Http;
using System.Threading.Tasks;

namespace csharp.Controllers {
    
    [Route("/api/[controller]")]
    public class SampleController : Controller {
    
        DbConfig db;
        
        public SampleController() {
            db = new DbConfig();
        }
        
        // Sample uri http://localhost:5000/api/sample/1/2
        [HttpGet("{value1}/{value2}")]
        public IActionResult SampleGet(int value1, int value2)
        {
            return Json(new { result = value1 * value2 });
        }

        // Sample uri http://localhost:5000/api/sample body {  "value1": 2,   "value2": 2}
        [HttpPost]
        public IActionResult SamplePostWithDto([FromBody]RequestSampleDto body)
        {
            if (!ModelState.IsValid)
                return HttpBadRequest(ModelState);

            var response = new ResponseSampleDto
            {
                Result = body.Value1 * body.Value2
            };

            return Json(response);
        }

        // Sample uri http://localhost:5000/api/sample/dynamic body {  "value1": 3,   "value2": 2}
        [HttpPost("dynamic")]
        public IActionResult SamplePostWithDynamic([FromBody]dynamic body)
        {
            if (!ModelState.IsValid)
                return HttpBadRequest(ModelState);
                
            try {
                db.insert(body);

                return Json(body);
            } catch (Exception e) {
                Console.WriteLine("{0} Exception caught.", e);
                return HttpBadRequest();
            }
        }

        [HttpGet("/consuming/{value1}/{value2}")]
        public async Task<IActionResult> SampleConsumingAnotherPost(int value1, int value2)
        {
            if (!ModelState.IsValid)
                return HttpBadRequest(ModelState);

            var client = new HttpClient();

            var request = new RequestSampleDto
            {
                Value1 = value1,
                Value2 = value2
            };

            var postResponse = await client.PostAsJsonAsync(@"http://localhost:5000/api/sample", request);

            if (!postResponse.IsSuccessStatusCode)
                return Json(postResponse);

            var result = await postResponse.Content.ReadAsJsonAsync<ResponseSampleDto>();
            return Json(result);
        }
    }

    public class RequestSampleDto
    {
        public int Value1 { get; set; }
        public int Value2 { get; set; }
    }

    public class ResponseSampleDto
    {
        public int Result { get; set; }
    }
}
