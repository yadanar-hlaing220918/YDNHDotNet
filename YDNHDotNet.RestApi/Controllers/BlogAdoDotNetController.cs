using Dapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using System.Data;
using YDNHDotNet.RestApi.Models;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace YDNHDotNet.RestApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BlogAdoDotNetController : ControllerBase
    {
        [HttpGet]   
        public IActionResult GetBlogs()
        {
            SqlConnection connection = new SqlConnection(ConnectionStrings.SqlConnectionStringBuilder.ConnectionString);
            connection.Open();
            string query = "select * from tbl_blog";
            SqlCommand cmd = new SqlCommand(query, connection);
            SqlDataAdapter sqlDataAdapter = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            sqlDataAdapter.Fill(dt);

            connection.Close();
            List<BlogModel> lst = dt.AsEnumerable().Select(dr => new BlogModel
            {
                blogId = Convert.ToInt32(dr["BlogId"]),
                blogTitle = Convert.ToString(dr["BlogTitle"]),
                blogAuthor = Convert.ToString(dr["BlogAuthor"]),
                blogContent = Convert.ToString(dr["BlogContent"])
            }).ToList();
            return Ok(lst);
        }
        [HttpGet("{id}")]
        public IActionResult EditBlog(int id)
        {
            string query = "select * from Tbl_Blog where BlogId = @BlogId";

            SqlConnection connection = new SqlConnection(ConnectionStrings.SqlConnectionStringBuilder.ConnectionString);
            connection.Open();

            SqlCommand cmd = new SqlCommand(query, connection);
            cmd.Parameters.AddWithValue("@BlogId", id);
            SqlDataAdapter sqlDataAdapter = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            sqlDataAdapter.Fill(dt);

            connection.Close();

            if (dt.Rows.Count == 0)
            {
                return NotFound("No Data Found");
            }

            DataRow dr = dt.Rows[0];
            var item = new BlogModel
            {
                blogId = Convert.ToInt32(dr["BlogId"]),
                blogTitle = Convert.ToString(dr["BlogTitle"]),
                blogAuthor = Convert.ToString(dr["BlogAuthor"]),
                blogContent = Convert.ToString(dr["BlogContent"])
            };

            return Ok(item);
        }
        [HttpPost]
        public IActionResult CreateBlog(BlogModel blog)
        {
            SqlConnection connection = new SqlConnection(ConnectionStrings.SqlConnectionStringBuilder.ConnectionString);

            connection.Open();
            string query = @"INSERT INTO [dbo].[Tbl_Blog]
           ([BlogTitle]
           ,[BlogAuthor]
           ,[BlogContent])
     VALUES
           (@BlogTitle,@BlogAuthor,@BLogContent)";
            SqlCommand cmd = new SqlCommand(query, connection);
            cmd.Parameters.AddWithValue("@BlogTitle", blog.blogTitle);
            cmd.Parameters.AddWithValue("@BlogAuthor", blog.blogAuthor);
            cmd.Parameters.AddWithValue("@BLogContent", blog.blogContent);
            int result = cmd.ExecuteNonQuery();
            connection.Close();
            string message = result > 0 ? "Saving Success." : "Saving Failed.";
            return Ok(message);
        }
        [HttpPut("{id}")]
        public IActionResult UpdateBlog(int id, BlogModel blog)
        {
            string getByIdQuery = "select * from Tbl_Blog where BlogId = @BlogId";
            SqlConnection connection = new SqlConnection(ConnectionStrings.SqlConnectionStringBuilder.ConnectionString);
            connection.Open();
            SqlCommand cmd = new SqlCommand(getByIdQuery, connection);
            cmd.Parameters.AddWithValue("@BlogId", id);
            SqlDataAdapter sqlDataAdapter = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            sqlDataAdapter.Fill(dt);
            if (dt.Rows.Count == 0)
            {
                return NotFound("No Data Found");
            }

            blog.blogId = id;
            string updateQuery = @"UPDATE [dbo].[Tbl_Blog]
                                SET [BlogTitle] = @BlogTitle,
                                    [BlogAuthor] = @BlogAuthor,
                                    [BlogContent] = @BlogContent
                                WHERE BlogId = @BlogId";
            SqlCommand updateCmd = new SqlCommand(updateQuery, connection);

                updateCmd.Parameters.AddWithValue("@BlogId", id);
                updateCmd.Parameters.AddWithValue("@BlogTitle", blog.blogTitle);
                updateCmd.Parameters.AddWithValue("@BlogAuthor", blog.blogAuthor);
                updateCmd.Parameters.AddWithValue("@BlogContent", blog.blogContent);
                int result = updateCmd.ExecuteNonQuery();
                connection.Close();

                string message = result > 0 ? "Updating Success." : "Updating Failed.";
                return Ok(message);
            
        }
        [HttpPatch("{id}")]
        public IActionResult PatchBlog(int id, BlogModel blog)
        {
            string getByIdQuery = "select * from Tbl_Blog where BlogId = @BlogId";
            SqlConnection connection = new SqlConnection(ConnectionStrings.SqlConnectionStringBuilder.ConnectionString);
            connection.Open();
            SqlCommand cmd = new SqlCommand(getByIdQuery, connection);
            cmd.Parameters.AddWithValue("@BlogId", id);
            SqlDataAdapter sqlDataAdapter = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            sqlDataAdapter.Fill(dt);
            if (dt.Rows.Count == 0)
            {
                return NotFound("No Data Found");
            }

            string updateQuery = @"UPDATE Tbl_Blog SET ";
            List<string> updateFields = new List<string>();

            if (!string.IsNullOrEmpty(blog.blogTitle))
            {
                updateFields.Add("[BlogTitle] = @BlogTitle");
            }
            if (!string.IsNullOrEmpty(blog.blogAuthor))
            {
                updateFields.Add("[BlogAuthor] = @BlogAuthor");
            }
            if (!string.IsNullOrEmpty(blog.blogContent))
            {
                updateFields.Add("[BlogContent] = @BlogContent");
            }

            updateQuery += string.Join(", ", updateFields);
            updateQuery += " WHERE BlogId = @BlogId";
            blog.blogId = id;

            SqlCommand updateCmd = new SqlCommand(updateQuery, connection);
            updateCmd.Parameters.AddWithValue("@BlogId", id);

            if (!string.IsNullOrEmpty(blog.blogTitle))
            {
                updateCmd.Parameters.AddWithValue("@BlogTitle", blog.blogTitle);
            }
            if (!string.IsNullOrEmpty(blog.blogAuthor))
            {
                updateCmd.Parameters.AddWithValue("@BlogAuthor", blog.blogAuthor);
            }
            if (!string.IsNullOrEmpty(blog.blogContent))
            {
                updateCmd.Parameters.AddWithValue("@BlogContent", blog.blogContent);
            }

            int result = updateCmd.ExecuteNonQuery();
            connection.Close();

            string message = result > 0 ? "Updating Success." : "Updating Failed.";
            return Ok(message);
        }
        [HttpDelete("{id}")]
        public IActionResult DeleteBlog(int id)
        {
            SqlConnection connection = new SqlConnection(ConnectionStrings.SqlConnectionStringBuilder.ConnectionString);

            connection.Open();
            string query = @"delete from [dbo].[Tbl_Blog] WHERE BlogId = @BlogId ";
            SqlCommand cmd = new SqlCommand(query, connection);
            cmd.Parameters.AddWithValue("@BlogId", id);
            int result = cmd.ExecuteNonQuery();
            connection.Close();
            string message = result > 0 ? "Deleting Success." : "Deleting Failed.";
            return Ok(message);
        }
    }
}
