using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YDNHDotNet.RestApi.Models;

[Table("Tbl_Blog")]
public class BlogModel
{
    [Key]
    public int blogId { get; set; }
    public string? blogTitle { get; set; }
    public string? blogAuthor { get; set; }
    public string? blogContent { get; set; }


}
