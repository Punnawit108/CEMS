namespace CEMS_Server.Models;
using System.ComponentModel.DataAnnotations;

public class cems_section
{
    [Key]
    public int st_id { get; set; }
    public string st_name { get; set; }

    public ICollection<cems_user> Users { get; set; } // เชื่อมกับ User
}