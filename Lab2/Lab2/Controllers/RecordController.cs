using Lab2.Models;
using Microsoft.AspNetCore.Mvc;

namespace Lab2.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class RecordController
    {
        private static List<Record> records = new List<Record> 
        {
            new Record { Id = 1, CategoryId = 1, UserId = 1, Date = DateTime.Now, Amount = 100 },
            new Record { Id = 2, CategoryId = 2, UserId = 2, Date = DateTime.Now.AddDays(-1), Amount = 3453 },
            new Record { Id = 3, CategoryId = 3, UserId = 3, Date = DateTime.Now.AddDays(-3), Amount = 4624 },
            new Record { Id = 4, CategoryId = 3, UserId = 3, Date = DateTime.Now.AddDays(4), Amount = 2324 },
            new Record { Id = 5, CategoryId = 4, UserId = 2, Date = DateTime.Now.AddDays(-3), Amount = 23324 },
            new Record { Id = 6, CategoryId = 2, UserId = 4, Date = DateTime.Now.AddDays(1), Amount = 100 },
            new Record { Id = 7, CategoryId = 2, UserId = 1, Date = DateTime.Now.AddDays(-2), Amount = 35654 },
            new Record { Id = 8, CategoryId = 1, UserId = 5, Date = DateTime.Now.AddDays(2), Amount = 3445 },
        };

        public RecordController()
        { 
        }

        //GET: /record?user_id=1&category_id=2
        //GET: /record?user_id=2
        //GET: /record?category_id=2
        [HttpGet]
        public ActionResult<IEnumerable<Record>> GeRecords([FromQuery] int? user_id = null,[FromQuery] int? category_id = null)
        {
            var result = records;
            if (user_id == null && category_id == null) 
            {
                return new BadRequestObjectResult("Both user_id and category_id parameters are required.");
            }

            if (user_id != null) 
            {
                result = result.Where(x => x.UserId.Equals(user_id)).ToList();
            }
            if (category_id != null)
            {
                result = result.Where(x => x.CategoryId.Equals(category_id)).ToList();
            }

            return new ObjectResult(result);
        }

        //GET: /record/id
        [HttpGet("{record_id:int}")]
        public ActionResult<User> GetRecordById(int record_id)
        {
            return new ObjectResult(records.FirstOrDefault(x => x.Id.Equals(record_id)));
        }
        //DELETE: /record/id
        [HttpDelete("{record_id:int}")]
        public ActionResult<User> DeleteRecordById(int record_id)
        {
            return new ObjectResult(records.Remove(records.FirstOrDefault(x => x.Id.Equals(record_id))));
        }
        // POST: /record
        [HttpPost]
        public ActionResult AddRecord([FromBody] Record record)
        {
            records.Add(record);
            return new OkResult();
        }
    }
}
