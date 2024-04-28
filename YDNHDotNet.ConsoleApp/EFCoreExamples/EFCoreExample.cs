using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using YDNHDotNet.ConsoleApp.Dtos;

namespace YDNHDotNet.ConsoleApp.EFCoreExamples
{
    internal class EFCoreExample

    {
        private readonly AppDbContext db = new AppDbContext();

        public void Run()
        {
            Read();
            //Edit(8);
            //Create("title 100", "author 100", "content 100");
            // Update(9, "title updated", "author updated", "content updated");
            //Delete(7);
        }
        private void Read()
        {
            var lst = db.Blogs.ToList();
            foreach (BlogDto item in lst)
            {
                Console.WriteLine(item.blogId);
                Console.WriteLine(item.blogTitle);
                Console.WriteLine(item.blogAuthor);
                Console.WriteLine(item.blogContent);

                Console.WriteLine("-------------------cccc-----------");
            }
        }
        private void Edit(int id)
        {
            var item = db.Blogs.FirstOrDefault(x => x.blogId == id);
            if (item is null)
            {
                Console.Write("No data found!");
                return;
            }
            Console.WriteLine(item.blogId);
            Console.WriteLine(item.blogTitle);
            Console.WriteLine(item.blogAuthor);
            Console.WriteLine(item.blogContent);

            Console.WriteLine("------------------------------");
        }
        private void Create(string title, string author, string content)
        {
            var item = new BlogDto
            {
                blogTitle = title,
                blogAuthor = author,
                blogContent = content
            };
            db.Blogs.Add(item);
            int result = db.SaveChanges();

            string message = result > 0 ? "Saving Success." : "Saving Failed.";
            Console.WriteLine(message);
        }
        private void Update(int id, string title, string author, string content)
        {
            var item = db.Blogs.FirstOrDefault(x => x.blogId == id);
            if (item is null)
            {
                Console.Write("No data found!");
                return;
            }
            item.blogTitle = title;
            item.blogAuthor = author;
            item.blogContent = content;

            int result = db.SaveChanges();

            string message = result > 0 ? "Updating Success." : "Updating Failed.";
            Console.WriteLine(message);
        }
        private void Delete(int id)
        {
            var item = db.Blogs.FirstOrDefault(x => x.blogId == id);
            if (item is null)
            {
                Console.Write("No data found!");
                return;
            }
            db.Blogs.Remove(item);
            int result = db.SaveChanges();

            string message = result > 0 ? "Deleting Success." : "Deleting Failed.";
            Console.WriteLine(message);

        }

    }
}
