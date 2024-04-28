 using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YDNHDotNet.ConsoleApp;

[Table("Tbl_Blog")]
public class BlogDto
{
    [Key]
    public int blogId { get; set; }
    public String blogTitle { get; set; }
    public String blogAuthor { get; set; }
    public String blogContent { get; set; }


}
