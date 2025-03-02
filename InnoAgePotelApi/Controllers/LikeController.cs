using InnoAgePotelApi.DbContext;
using InnoAgePotelApi.Models;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Driver;
using System.Threading.Tasks;

namespace InnoAgePotelApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class LikeController : ControllerBase
    {
        private readonly MongoDbContext _dbcontext;

        public LikeController(MongoDbContext dbcontext)
        {
            _dbcontext = dbcontext;
        }

        [HttpGet("GetAllData")]
        public async Task<IActionResult> GetAll()
        {
            var likes = await _dbcontext.Like.Find(_ => true).ToListAsync();
            return Ok(likes);
        }

        [HttpPost("InsertLike")]
        public async Task<IActionResult> Insert(Like like)
        {
            like.Id = null;
            await _dbcontext.Like.InsertOneAsync(like);
            return Ok(like);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(string id)
        {
            var filter = Builders<Like>.Filter.Eq("Id", id);
            await _dbcontext.Like.DeleteOneAsync(filter);
            return Ok();
        }

        [HttpGet("GetByUserId/{UserId}")]
        public async Task<IActionResult> GetByUserId(string UserId)
        {
            var like = await _dbcontext.Like.Find(Builders<Like>.Filter.Eq("UserId", UserId)).FirstOrDefaultAsync();
            return Ok(like);
        }
    }
}
