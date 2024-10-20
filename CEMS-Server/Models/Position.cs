namespace CEMS_Server.Models;
using System.ComponentModel.DataAnnotations;

public class cems_position
{
    [Key]
    public int pst_id {get; set;}
    public string? pst_name {get; set;}
}
