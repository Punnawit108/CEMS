namespace CEMS_Server.Models;
using System.ComponentModel.DataAnnotations;

public class Position
{
    [Key]
    public int pst_id {get; set;}
    public string? pst_name {get; set;}
}
