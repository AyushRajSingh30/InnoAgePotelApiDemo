using InnoAgePotelApi.DbContext;
using InnoAgePotelApi.Models;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Driver;
using MongoDB.Driver.Linq;
using System.Threading.Tasks;


namespace InnoAgePotelApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PostController : ControllerBase
    {
        private readonly MongoDbContext _dbcontext;

        public PostController(MongoDbContext dbContext)
        {
            _dbcontext = dbContext;
        }

        [HttpGet("GetAllData")]
        public async Task<IActionResult> GetAllPosts()
        {
            var posts = await _dbcontext.Posts.Find(_ => true).ToListAsync();
            return Ok(posts);
        }

        [HttpPost("InsertPost")]
        public async Task<IActionResult> Post(Post post)
        {
            post.Id = null;
            await _dbcontext.Posts.InsertOneAsync(post);
            return Ok(post);
        }

        [HttpPatch("Update/{id}")]
        public async Task<IActionResult> Patch(string id, [FromBody] Post post)
        {
            var filter = Builders<Post>.Filter.Eq("_id", id);
            var update = Builders<Post>.Update.Set("Title", post.Title).Set("Content", post.Description);
            await _dbcontext.Posts.UpdateOneAsync(filter, update);
            return Ok(post);
        }

        [HttpDelete("Delete/{id}")]
        public async Task<IActionResult> Delete(string id)
        {
            var filter = Builders<Post>.Filter.Eq("_id", id);
            await _dbcontext.Posts.DeleteOneAsync(filter);
            return Ok();
        }

        [HttpGet("GetPostById/{id}")]
        public async Task<IActionResult> GetPostById(string id)
        {
            var post = await _dbcontext.Posts.AsQueryable()
                .Where(x => x.Id == id)
                .FirstOrDefaultAsync(); // Use FirstOrDefaultAsync()

            return post != null ? Ok(post) : NotFound();
        }


    }
}
