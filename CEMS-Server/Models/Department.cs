namespace CEMS_Server.Models;
using System.ComponentModel.DataAnnotations;

public class cems_department
{
    [Key]
    public int dpt_id { get; set; }
    public string dpt_name { get; set; }

    public ICollection<cems_user> Users { get; set; } // เชื่อมกับ User
}