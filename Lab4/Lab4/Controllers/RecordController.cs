using Lab4.Models;
using Lab4.Services;
using Lab4.Services.Interfaces.Services;
using Microsoft.AspNetCore.Mvc;

namespace Lab4.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class RecordController
    {
        private readonly IRecordService recordService;
        public RecordController(IRecordService recordService)
        {
            this.recordService = recordService;
        }

        //GET: /record?user_id=1&category_id=2
        //GET: /record?user_id=2
        //GET: /record?category_id=2
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Record>>> GeRecords([FromQuery] int? user_id = null,[FromQuery] int? category_id = null)
        {
            List<Record>? result;
            try
            {
                result = (List<Record>)await recordService.GetFilteredAsync(user_id, category_id);
            }
            catch (Exception e)
            {
                return new BadRequestObjectResult(e.Message);
            }

            return new ObjectResult(result);
        }

        //GET: /record/id
        [HttpGet("{record_id:int}")]
        public async Task<ActionResult<Record>> GetRecordById(int record_id)
        {
            Record result;
            try
            {
                result = await recordService.GetByIdAsync(record_id);
            }
            catch (Exception e)
            {
                return new BadRequestObjectResult(e.Message);
            }

            return new ObjectResult(result);
        }
        //DELETE: /record/id
        [HttpDelete("{record_id:int}")]
        public async Task<ActionResult<Record>> DeleteRecordById(int record_id)
        {
            Record result;
            try
            {
                result = await recordService.DeleteAsync(record_id);
            }
            catch (Exception e)
            {
                return new BadRequestObjectResult(e.Message);
            }

            return new ObjectResult(result);
        }
        // POST: /record
        [HttpPost]
        public async Task<ActionResult<Record>> AddRecord([FromBody] Record record)
        {
            try
            {
                await recordService.AddAsync(record);
            }
            catch (Exception e)
            {
                return new BadRequestObjectResult(e.Message);
            }
            return new ObjectResult(record);
        }
    }
}
