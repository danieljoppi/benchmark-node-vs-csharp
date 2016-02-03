using Microsoft.AspNet.Mvc;

namespace csharp.Controllers
{
    [Route("/api/[controller]")]
    public class SampleController : Controller
    {
        // Sample uri http://localhost:5000/api/sample/1/2
        [HttpGet("{value1}/{value2}")]
        public IActionResult SampleGet(int value1, int value2)
        {
            return Json(new { result = value1 * value2 });
        }

        // Sample uri http://localhost:5000/api body 
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

        [HttpPost("dynamic")]
        public IActionResult SamplePostWithDynamic([FromBody]dynamic body)
        {
            if (!ModelState.IsValid)
                return HttpBadRequest(ModelState);

            int value1 = 0;
            int value2 = 0;

            if (int.TryParse(body.value1.ToString(), out value1) &&
                int.TryParse(body.value2.ToString(), out value2))
                return Json(new { result = value1 * value2 });

            return HttpBadRequest();
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
