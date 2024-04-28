using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YDNHDotNet.ConsoleApp
{
    internal class EFCoreExample

    {
        private readonly AppDbContext db = new AppDbContext();

        public void Run()
        {
            Read();
            //Edit(13);
            //Create("title 100", "author 100", "content 100");
            //Update(15, "title updated", "author updated", "content updated");
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

    }
}
