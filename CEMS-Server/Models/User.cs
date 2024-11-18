namespace CEMS_Server.Models;
using System.ComponentModel.DataAnnotations;

public class cems_user
{
    [Key]
    public int usr_id {get; set;}
    public string usr_employee_id {get; set;}
    public int usr_rol_id {get; set;}
    public int usr_cpn_id {get; set;}
    public int usr_pst_id {get; set;}
    public int usr_dpt_id {get; set;}
    public int usr_st_id {get; set;}
    public string usr_profile_picture {get; set;}
    public string usr_first_name {get; set;}
    public string usr_last_name {get; set;}
    public string usr_phone_number {get; set;}
    public string usr_email {get; set;}
    public bool usr_is_see_report { get; set; }
    public bool usr_is_active { get; set; }

    public cems_role Role { get; set; }
    public cems_position Position { get; set; }
}