namespace CEMS_Server.Models;
using System.ComponentModel.DataAnnotations;

public class cems_role
{
    [Key]
    public int rol_id { get; set; }
    public string rol_name { get; set; }
    public bool rol_is_manage_user { get; set; }
    public bool rol_is_manage_expenses { get; set; }
    public bool rol_is_manage_permission { get; set; }
    public bool rol_is_approver { get; set; }

    public ICollection<cems_user> Users { get; set; } // เชื่อมกับ User
}