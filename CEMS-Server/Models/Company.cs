namespace CEMS_Server.Models;
using System.ComponentModel.DataAnnotations;

public class cems_company
{
    [Key]
    public int cpn_id { get; set; }
    public string cpn_name { get; set; }

    public ICollection<cems_user> Users { get; set; } // เชื่อมกับ User
}