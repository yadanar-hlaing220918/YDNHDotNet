using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using YDNHDotNet.RestApi.Db;
using YDNHDotNet.RestApi.Models;

namespace YDNHDotNet.RestApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BlogController : ControllerBase
    {
        private readonly AppDbContext _content;

        public BlogController()
        {
            _content = new AppDbContext();
        }
        [HttpGet]
        public IActionResult Read()
        {
            var lst = _content.Blogs.ToList();
            return Ok(lst);
        }
        [HttpGet("{id}")]
        public IActionResult Edit(int id)
        {
            var item = _content.Blogs.FirstOrDefault(x => x.blogId == id);
            if (item is null)
            {
                return NotFound("No data found!");
            }
            return Ok(item);
        }
        [HttpPost]
        public IActionResult Create(BlogModel blog)
        {
            _content.Blogs.Add(blog);
            var result = _content.SaveChanges();
            string message = result > 0 ? "Saving Successful!" : "Saving Failed!";
            return Ok(message);
        }
        [HttpPut("{id}")]
        public IActionResult Update(int id, BlogModel blog)
        {
            var item = _content.Blogs.FirstOrDefault(x => x.blogId == id);
            if (item is null)
            {
                return NotFound("No data found!");
            }
            item.blogTitle = blog.blogTitle;
            item.blogAuthor = blog.blogAuthor;
            item.blogContent = blog.blogContent;
            var result = _content.SaveChanges();
            string message = result > 0 ? "Updating Successful!" : "Updating Failed!";
            return Ok(message);
        }
        [HttpPatch("{id}")]
        public IActionResult Patch(int id, BlogModel blog)
        {
            var item = _content.Blogs.FirstOrDefault(x => x.blogId == id);
            if (item is null)
            {
                return NotFound("No data found!");
            }
            if (!string.IsNullOrEmpty(blog.blogTitle))
            {
                item.blogTitle = blog.blogTitle;
            }
            if (!string.IsNullOrEmpty(blog.blogAuthor))
            {
                item.blogAuthor = blog.blogAuthor;
            }
            if (!string.IsNullOrEmpty(blog.blogContent))
            {
                item.blogContent = blog.blogContent;
            }
            var result = _content.SaveChanges();
            string message = result > 0 ? "Updating Successful!" : "Updating Failed!";
            return Ok(message);
        }
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var item = _content.Blogs.FirstOrDefault(x => x.blogId == id);
            if (item is null)
            {
                return NotFound("No data found!");
            }
            _content.Blogs.Remove(item);
            var result = _content.SaveChanges();
            string message = result > 0 ? "Deleting Successful!" : "Deleting Failed!";
            return Ok(message);
        }
    }
}
