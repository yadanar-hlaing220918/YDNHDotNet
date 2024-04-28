using Dapper;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YDNHDotNet.ConsoleApp
{
    internal class DapperExample
    {
        public void Run()
        {
            //Read();
            //Edit(1);
            //Edit(7);
            //Create("title N", "author N", "content N");

            //Update(8, "u title", "u author", "u content");
            Delete(5);

        }
        private void Read()
        {  
          using IDbConnection dbConnection = new SqlConnection(ConnectionStrings.SqlConnectionStringBuilder.ConnectionString);
            List<BlogDto> lst = dbConnection.Query<BlogDto>("select * from tbl_blog").ToList();

            foreach (BlogDto item in lst)
            {
                Console.WriteLine(item.blogId);
                Console.WriteLine(item.blogTitle);
                Console.WriteLine(item.blogAuthor);
                Console.WriteLine(item.blogContent );
                Console.WriteLine("-------------------");

            }

        }
        private void Edit(int id)
        {
            using IDbConnection dbConnection = new SqlConnection(ConnectionStrings.SqlConnectionStringBuilder.ConnectionString);
            var item = dbConnection.Query<BlogDto>("select * from tbl_blog where blogId = @BlogId", new BlogDto { blogId = id }).FirstOrDefault();
            if(item is null)
            {
                Console.WriteLine("No data found");
                return;
            }
            Console.WriteLine(item.blogId);
            Console.WriteLine(item.blogTitle);
            Console.WriteLine(item.blogAuthor);
            Console.WriteLine(item.blogContent);
        }
        private void Create(string title, string author, string content)
        {
            var item = new BlogDto
            {
                blogTitle = title,
                blogAuthor = author,
                blogContent = content
            };
            string query = @"INSERT INTO[dbo].[Tbl_Blog]
                        ([blogTitle],
                        [blogAuthor],
                        [blogContent])

                VALUES
                        (@BlogTitle,@BlogAuthor,@BlogCOntent)";
                    using IDbConnection dbConn = new SqlConnection(ConnectionStrings.SqlConnectionStringBuilder.ConnectionString);

                    int result = dbConn.Execute(query, item);
                    string message = result > 0 ? "Saving Success." : "Saving Failesd.";
                    Console.WriteLine(message);

        }
        private void Update(int id, string title, string author, string content)
        {
            var item = new BlogDto
            {
                blogId = id,
                blogTitle = title,
                blogAuthor = author,
                blogContent = content
            };
            string query = @"UPDATE [dbo].[Tbl_Blog]
                  SET [BlogTitle] = @BlogTitle,
                      [BlogAuthor] = @BlogAuthor,
                      [BlogContent] = @BlogContent
                       WHERE BlogId = @BlogId";
            using IDbConnection db = new SqlConnection(ConnectionStrings.SqlConnectionStringBuilder.ConnectionString);
            int result = db.Execute(query, item);

            string message = result > 0 ? "Updating Success." : "Updating Failed.";
            Console.WriteLine(message);
        }
        private void Delete(int id)
        {
            var item = new BlogDto
            {
                blogId = id,
            };
            string query = @"DELETE From [dbo].[Tbl_Blog] WHERE BlogId = @BlogId";
            using IDbConnection db = new SqlConnection(ConnectionStrings.SqlConnectionStringBuilder.ConnectionString);
            int result = db.Execute(query, item);

            string message = result > 0 ? "Deleting Success." : "Deleting Failed.";
            Console.WriteLine(message);
        }


    }

}
