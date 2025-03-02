using InnoAgePotelApi.DbContext;
using InnoAgePotelApi.Models;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Driver;
using System.Threading.Tasks;

namespace InnoAgePotelApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PollController : ControllerBase
    {
        private readonly MongoDbContext _dbcontext;

        public PollController(MongoDbContext dbcontext)
        {
            _dbcontext = dbcontext;
        }

        [HttpGet]
        [Route("GetAllData")]
        public async Task<IActionResult> Get()
        {
            var polls = await _dbcontext.Poll.Find(_ => true).ToListAsync();
            return Ok(polls);
        }
        [HttpPost]
        [Route("InsertPoll")]
        public async Task<IActionResult> Post(Poll poll)
        {
            poll.Id = null;
            await _dbcontext.Poll.InsertOneAsync(poll);
            return Ok(poll);
        }
        [HttpPatch]
        [Route("Update/{id}")]
        public async Task<IActionResult> Patch(string id, Poll poll)
        {
            var filter = Builders<Poll>.Filter.Eq("Id", id);
            var update = Builders<Poll>.Update.Set("Description", poll.Description);
            await _dbcontext.Poll.UpdateOneAsync(filter, update);
            return Ok(poll);
        }

        [HttpDelete]
        [Route("deleted/{id}")]
        public async Task<IActionResult> Delete(string id)
        {
            var filter = Builders<Poll>.Filter.Eq("Id", id);
            await _dbcontext.Poll.DeleteOneAsync(filter);
            return Ok();
        }
        [HttpGet]
        [Route("GetValueByUserId{UserId}")]
        public async Task<IActionResult> Get(string UserId)
        {
            var poll = await _dbcontext.Poll.Find(Builders<Poll>.Filter.Eq("UserId", UserId)).FirstOrDefaultAsync();
            return Ok(poll);
        }
    }
}
