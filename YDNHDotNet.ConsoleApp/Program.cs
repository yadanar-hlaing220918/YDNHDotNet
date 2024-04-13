using System.Data;
using System.Data.SqlClient;
using YDNHDotNet.ConsoleApp;

Console.WriteLine("Hello, World!");

/*SqlConnectionStringBuilder stringBuilder = new SqlConnectionStringBuilder();
stringBuilder.DataSource = ".";
stringBuilder.InitialCatalog = "DotNetTraining";
stringBuilder.UserID = "sa";
stringBuilder.Password = "sa@123";
SqlConnection connection = new SqlConnection(stringBuilder.ConnectionString);
connection.Open();
Console.WriteLine("Connection open.");

string query = "select * from tbl_blog";
SqlCommand cmd = new SqlCommand(query, connection);
SqlDataAdapter sqlDataAdapter = new SqlDataAdapter(cmd);
DataTable dt = new DataTable();
sqlDataAdapter.Fill(dt);

connection.Close();
Console.WriteLine("Connection close.");

foreach(DataRow dr in dt.Rows)
{
    Console.WriteLine("Blog Id ==>"+dr["BlogId"]);
    Console.WriteLine("Blog Title ==>"+ dr["BlogTitle"]);
    Console.WriteLine("Blog Author ==>"+ dr["BlogAuthor"]);
    Console.WriteLine("Blog Content ==>"+ dr["BlogContent"]);
    Console.WriteLine("------------------------------------");

}*/
AdoDotNetExample adoDotNetExample = new AdoDotNetExample();
//adoDotNetExample.Read();
//adoDotNetExample.Edit(1);
//adoDotNetExample.Create("title1", "author1", "content1");
//adoDotNetExample.Update(1, "update title", "update author", "update content");
adoDotNetExample.Delete(1);

Console.ReadKey();
