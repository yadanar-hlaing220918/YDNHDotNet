﻿using Dapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using System.Data;
using YDNHDotNet.RestApi.Models;

namespace YDNHDotNet.RestApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BlogDapperController : ControllerBase
    {
        [HttpGet]
        public IActionResult GetBlogs()
        {
            string query = "select * from tbl_blog";
            using IDbConnection dbConnection = new SqlConnection(ConnectionStrings.SqlConnectionStringBuilder.ConnectionString);
            List<BlogModel > lst = dbConnection.Query<BlogModel>(query).ToList();

            return Ok(lst);
        }
        [HttpGet("{id}")]
        public IActionResult EditBlog(int id)
        {

            var item = FindById(id);
            if (item is null)
            {
                return NotFound("No data found!");
            }
            return Ok(item);
        }
        [HttpPost]
        public IActionResult Create(BlogModel  blog)
        {
            string query = @"INSERT INTO[dbo].[Tbl_Blog]
                        ([blogTitle],
                        [blogAuthor],
                        [blogContent])

                VALUES
                        (@BlogTitle,@BlogAuthor,@BlogCOntent)";
            using IDbConnection dbConn = new SqlConnection(ConnectionStrings.SqlConnectionStringBuilder.ConnectionString);
            int result = dbConn.Execute(query, blog);
            string message = result > 0 ? "Saving Success." : "Saving Failesd.";
            return Ok(message);
        }
        [HttpPut("{id}")]
        public IActionResult UpdateBlog(int id, BlogModel blog)
        {
            var item = FindById(id);
            if (item is null)
            {
                return NotFound("No data found!");
            }
            string query = @"UPDATE [dbo].[Tbl_Blog]
                  SET [BlogTitle] = @BlogTitle,
                      [BlogAuthor] = @BlogAuthor,
                      [BlogContent] = @BlogContent
                       WHERE BlogId = @BlogId";
            blog.blogId = id;

            using IDbConnection db = new SqlConnection(ConnectionStrings.SqlConnectionStringBuilder.ConnectionString);
            int result = db.Execute(query, blog);
            string message = result > 0 ? "Updating Success." : "Updating Failed.";
            return Ok(message);
        }
        [HttpPatch("{id}")]
        public IActionResult PatchBlog(int id , BlogModel blog)
        {
            var item = FindById(id);
            if (item is null)
            {
                return NotFound("No data found!");
            }
            string conditions = string.Empty;
            if (!string.IsNullOrEmpty(blog.blogTitle))
            {
                conditions += " [BlogTitle] = @BlogTitle, ";
            }
            if (!string.IsNullOrEmpty(blog.blogAuthor))
            {
                conditions += " [BlogAuthor] = @BlogAuthor, ";
            }
            if (!string.IsNullOrEmpty(blog.blogContent))
            {
                conditions += " [BlogContent] = @BlogContent, ";
            }
            if (conditions.Length == 0)
            {
                return NotFound("No Data to Update!");
            }
            conditions = conditions.Substring(0, conditions.Length - 2);
            blog.blogId = id;
            string query = $@"UPDATE [dbo].[Tbl_Blog]
                  SET {conditions}
                       WHERE BlogId = @BlogId";
            using IDbConnection db = new SqlConnection(ConnectionStrings.SqlConnectionStringBuilder.ConnectionString);
            int result = db.Execute(query, blog);

            string message = result > 0 ? "Updating Success." : "Updating Failed.";
            return Ok(message);
        }
        [HttpDelete("{id}")]
        public IActionResult DeleteBlog(int id)
        {
            var item = FindById(id);
            if (item is null)
            {
                return NotFound("No data found!");
            }
            string query = @"DELETE From [dbo].[Tbl_Blog] WHERE BlogId = @BlogId";
            using IDbConnection db = new SqlConnection(ConnectionStrings.SqlConnectionStringBuilder.ConnectionString);
            int result = db.Execute(query, item);

            string message = result > 0 ? "Deleting Success." : "Deleting Failed.";
            return Ok(message);
        }

        private BlogModel? FindById(int id)
        {
            string query = "select * from tbl_blog where blogId = @BlogId";
            using IDbConnection dbConnection = new SqlConnection(ConnectionStrings.SqlConnectionStringBuilder.ConnectionString);
            var item = dbConnection.Query<BlogModel>(query, new BlogModel { blogId = id }).FirstOrDefault();
            return item;
        }

    }
}
