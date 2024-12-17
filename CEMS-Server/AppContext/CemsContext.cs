
using Microsoft.EntityFrameworkCore;
using CEMS_Server.Models;

namespace CEMS_Server.AppContext;

public partial class CemsContext : DbContext
{
    public CemsContext()
    {
    }

    public CemsContext(DbContextOptions<CemsContext> options)
        : base(options)
    {
    }

    public virtual DbSet<CemsApprover> CemsApprovers { get; set; }

    public virtual DbSet<CemsApproverRequisition> CemsApproverRequisitions { get; set; }

    public virtual DbSet<CemsCompany> CemsCompanies { get; set; }

    public virtual DbSet<CemsDepartment> CemsDepartments { get; set; }

    public virtual DbSet<CemsNotification> CemsNotifications { get; set; }

    public virtual DbSet<CemsPosition> CemsPositions { get; set; }

    public virtual DbSet<CemsProject> CemsProjects { get; set; }

    public virtual DbSet<CemsRequisition> CemsRequisitions { get; set; }

    public virtual DbSet<CemsRequisitionType> CemsRequisitionTypes { get; set; }

    public virtual DbSet<CemsRole> CemsRoles { get; set; }

    public virtual DbSet<CemsSection> CemsSections { get; set; }

    public virtual DbSet<CemsStatus> CemsStatuses { get; set; }

    public virtual DbSet<CemsUser> CemsUsers { get; set; }

    public virtual DbSet<CemsVehicle> CemsVehicles { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder
            .UseCollation("utf8mb3_general_ci")
            .HasCharSet("utf8mb3");

        modelBuilder.Entity<CemsApprover>(entity =>
        {
            entity.HasKey(e => e.ApId).HasName("PRIMARY");

            entity.ToTable("cems_approver");

            entity.HasIndex(e => e.ApUsrId, "fk_approver_user_idx");

            entity.Property(e => e.ApId).HasColumnName("ap_id");
            entity.Property(e => e.ApSequence).HasColumnName("ap_sequence");
            entity.Property(e => e.ApUsrId)
                .HasMaxLength(10)
                .HasColumnName("ap_usr_id");

            entity.HasOne(d => d.ApUsr).WithMany(p => p.CemsApprovers)
                .HasForeignKey(d => d.ApUsrId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_approver_user");
        });

        modelBuilder.Entity<CemsApproverRequisition>(entity =>
        {
            entity.HasKey(e => e.AprId).HasName("PRIMARY");

            entity.ToTable("cems_approver_requisition");

            entity.HasIndex(e => e.AprApId, "fk_requisition_has_approver_approver_idx");

            entity.HasIndex(e => e.AprRqId, "fk_requisition_has_approver_requisition_idx");

            entity.Property(e => e.AprId)
                .ValueGeneratedNever()
                .HasColumnName("apr_id");
            entity.Property(e => e.AprApId).HasColumnName("apr_ap_id");
            entity.Property(e => e.AprDate)
                .HasColumnType("datetime")
                .HasColumnName("apr_date");
            entity.Property(e => e.AprName)
                .HasMaxLength(45)
                .HasColumnName("apr_name");
            entity.Property(e => e.AprRqId)
                .HasMaxLength(10)
                .HasColumnName("apr_rq_id");
            entity.Property(e => e.AprStatus)
                .HasColumnType("enum('waiting','accept','reject','edit')")
                .HasColumnName("apr_status");

            entity.HasOne(d => d.AprAp).WithMany(p => p.CemsApproverRequisitions)
                .HasForeignKey(d => d.AprApId)
                .HasConstraintName("fk_requisition_has_approver_approver");

            entity.HasOne(d => d.AprRq).WithMany(p => p.CemsApproverRequisitions)
                .HasForeignKey(d => d.AprRqId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_requisition_has_approver_requisition");
        });

        modelBuilder.Entity<CemsCompany>(entity =>
        {
            entity.HasKey(e => e.CpnId).HasName("PRIMARY");

            entity.ToTable("cems_company");

            entity.Property(e => e.CpnId).HasColumnName("cpn_id");
            entity.Property(e => e.CpnName)
                .HasMaxLength(45)
                .HasColumnName("cpn_name");
        });

        modelBuilder.Entity<CemsDepartment>(entity =>
        {
            entity.HasKey(e => e.DptId).HasName("PRIMARY");

            entity.ToTable("cems_department");

            entity.Property(e => e.DptId).HasColumnName("dpt_id");
            entity.Property(e => e.DptName)
                .HasMaxLength(45)
                .HasColumnName("dpt_name");
        });

        modelBuilder.Entity<CemsNotification>(entity =>
        {
            entity.HasKey(e => e.NtId).HasName("PRIMARY");

            entity.ToTable("cems_notification");

            entity.HasIndex(e => e.NtAprId, "fk_cems_notification_cems_approver_requistion1_idx");

            entity.Property(e => e.NtId)
                .ValueGeneratedNever()
                .HasColumnName("nt_id");
            entity.Property(e => e.NtAprId).HasColumnName("nt_apr_id");
            entity.Property(e => e.NtDate)
                .HasColumnType("datetime")
                .HasColumnName("nt_date");
            entity.Property(e => e.NtStatus)
                .HasColumnType("enum('read','unread')")
                .HasColumnName("nt_status");

            entity.HasOne(d => d.NtApr).WithMany(p => p.CemsNotifications)
                .HasForeignKey(d => d.NtAprId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_cems_notification_cems_approver_requistion1");
        });

        modelBuilder.Entity<CemsPosition>(entity =>
        {
            entity.HasKey(e => e.PstId).HasName("PRIMARY");

            entity.ToTable("cems_position");

            entity.Property(e => e.PstId).HasColumnName("pst_id");
            entity.Property(e => e.PstName)
                .HasMaxLength(45)
                .HasColumnName("pst_name");
        });

        modelBuilder.Entity<CemsProject>(entity =>
        {
            entity.HasKey(e => e.PjId).HasName("PRIMARY");

            entity.ToTable("cems_project");

            entity.Property(e => e.PjId).HasColumnName("pj_id");
            entity.Property(e => e.PjAmountExpenses).HasColumnName("pj_amount_expenses");
            entity.Property(e => e.PjIsActive).HasColumnName("pj_is_active");
            entity.Property(e => e.PjName)
                .HasMaxLength(45)
                .HasColumnName("pj_name");
        });

        modelBuilder.Entity<CemsRequisition>(entity =>
        {
            entity.HasKey(e => e.RqId).HasName("PRIMARY");

            entity.ToTable("cems_requisition");

            entity.HasIndex(e => e.RqPjId, "fk_requisition_project_idx");

            entity.HasIndex(e => e.RqRqtId, "fk_requisition_requisition_type_idx");

            entity.HasIndex(e => e.RqUsrId, "fk_requisition_user_idx");

            entity.HasIndex(e => e.RqVhId, "fk_requisition_vehicle_idx");

            entity.Property(e => e.RqId)
                .HasMaxLength(10)
                .HasColumnName("rq_id");
            entity.Property(e => e.RqCode)
                .HasMaxLength(10)
                .HasColumnName("rq_code");
            entity.Property(e => e.RqDisburseDate).HasColumnName("rq_disburse_date");
            entity.Property(e => e.RqDistance)
                .HasMaxLength(45)
                .HasColumnName("rq_distance");
            entity.Property(e => e.RqEndLocation)
                .HasMaxLength(45)
                .HasColumnName("rq_end_location");
            entity.Property(e => e.RqExpenses).HasColumnName("rq_expenses");
            entity.Property(e => e.RqInsteadEmail)
                .HasMaxLength(45)
                .HasColumnName("rq_instead_email");
            entity.Property(e => e.RqName)
                .HasMaxLength(45)
                .HasColumnName("rq_name");
            entity.Property(e => e.RqPayDate).HasColumnName("rq_pay_date");
            entity.Property(e => e.RqPjId).HasColumnName("rq_pj_id");
            entity.Property(e => e.RqProgress)
                .HasColumnType("enum('accepting','paying','complete')")
                .HasColumnName("rq_progress");
            entity.Property(e => e.RqProof)
                .HasColumnType("text")
                .HasColumnName("rq_proof");
            entity.Property(e => e.RqPurpose)
                .HasColumnType("text")
                .HasColumnName("rq_purpose");
            entity.Property(e => e.RqReason)
                .HasColumnType("text")
                .HasColumnName("rq_reason");
            entity.Property(e => e.RqRqtId).HasColumnName("rq_rqt_id");
            entity.Property(e => e.RqStartLocation)
                .HasMaxLength(45)
                .HasColumnName("rq_start_location");
            entity.Property(e => e.RqStatus)
                .HasColumnType("enum('sketch','waiting','edit','accept','reject')")
                .HasColumnName("rq_status");
            entity.Property(e => e.RqUsrId)
                .HasMaxLength(10)
                .HasColumnName("rq_usr_id");
            entity.Property(e => e.RqVhId).HasColumnName("rq_vh_id");
            entity.Property(e => e.RqWithdrawDate).HasColumnName("rq_withdraw_date");

            entity.HasOne(d => d.RqPj).WithMany(p => p.CemsRequisitions)
                .HasForeignKey(d => d.RqPjId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_requisition_project");

            entity.HasOne(d => d.RqRqt).WithMany(p => p.CemsRequisitions)
                .HasForeignKey(d => d.RqRqtId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_requisition_requisition_type");

            entity.HasOne(d => d.RqUsr).WithMany(p => p.CemsRequisitions)
                .HasForeignKey(d => d.RqUsrId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_requisition_user");

            entity.HasOne(d => d.RqVh).WithMany(p => p.CemsRequisitions)
                .HasForeignKey(d => d.RqVhId)
                .HasConstraintName("fk_requisition_vehicle");
        });

        modelBuilder.Entity<CemsRequisitionType>(entity =>
        {
            entity.HasKey(e => e.RqtId).HasName("PRIMARY");

            entity.ToTable("cems_requisition_type");

            entity.Property(e => e.RqtId).HasColumnName("rqt_id");
            entity.Property(e => e.RqtName)
                .HasMaxLength(45)
                .HasColumnName("rqt_name");
        });

        modelBuilder.Entity<CemsRole>(entity =>
        {
            entity.HasKey(e => e.RolId).HasName("PRIMARY");

            entity.ToTable("cems_role");

            entity.Property(e => e.RolId).HasColumnName("rol_id");
            entity.Property(e => e.RolIsManageExpenses).HasColumnName("rol_is_manage_expenses");
            entity.Property(e => e.RolIsSettingSystem).HasColumnName("rol_is_setting_system");
            entity.Property(e => e.RolName)
                .HasMaxLength(45)
                .HasColumnName("rol_name");
        });

        modelBuilder.Entity<CemsSection>(entity =>
        {
            entity.HasKey(e => e.StId).HasName("PRIMARY");

            entity.ToTable("cems_section");

            entity.Property(e => e.StId).HasColumnName("st_id");
            entity.Property(e => e.StName)
                .HasMaxLength(45)
                .HasColumnName("st_name");
        });

        modelBuilder.Entity<CemsStatus>(entity =>
        {
            entity
                .HasNoKey()
                .ToTable("cems_status");

            entity.Property(e => e.SttStatus).HasColumnName("stt_status");
        });

        modelBuilder.Entity<CemsUser>(entity =>
        {
            entity.HasKey(e => e.UsrId).HasName("PRIMARY");

            entity.ToTable("cems_user");

            entity.HasIndex(e => e.UsrEmployeeId, "employee_Id_UNIQUE").IsUnique();

            entity.HasIndex(e => e.UsrCpnId, "fk_user_company_idx");

            entity.HasIndex(e => e.UsrDptId, "fk_user_department_idx");

            entity.HasIndex(e => e.UsrPstId, "fk_user_position_idx");

            entity.HasIndex(e => e.UsrRolId, "fk_user_role_idx");

            entity.HasIndex(e => e.UsrStId, "fk_user_section_idx");

            entity.HasIndex(e => e.UsrEmail, "usr_email_UNIQUE").IsUnique();

            entity.Property(e => e.UsrId)
                .HasMaxLength(10)
                .HasColumnName("usr_id");
            entity.Property(e => e.UsrCpnId).HasColumnName("usr_cpn_id");
            entity.Property(e => e.UsrDptId).HasColumnName("usr_dpt_id");
            entity.Property(e => e.UsrEmail)
                .HasMaxLength(45)
                .HasColumnName("usr_email");
            entity.Property(e => e.UsrEmployeeId)
                .HasMaxLength(45)
                .HasColumnName("usr_employee_id");
            entity.Property(e => e.UsrFirstName)
                .HasMaxLength(45)
                .HasColumnName("usr_first_name");
            entity.Property(e => e.UsrIsActive).HasColumnName("usr_is_active");
            entity.Property(e => e.UsrIsSeeReport).HasColumnName("usr_is_see_report");
            entity.Property(e => e.UsrLastName)
                .HasMaxLength(45)
                .HasColumnName("usr_last_name");
            entity.Property(e => e.UsrPhoneNumber)
                .HasMaxLength(10)
                .HasColumnName("usr_phone_number");
            entity.Property(e => e.UsrPstId).HasColumnName("usr_pst_id");
            entity.Property(e => e.UsrRolId).HasColumnName("usr_rol_id");
            entity.Property(e => e.UsrStId).HasColumnName("usr_st_id");

            entity.HasOne(d => d.UsrCpn).WithMany(p => p.CemsUsers)
                .HasForeignKey(d => d.UsrCpnId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_user_company");

            entity.HasOne(d => d.UsrDpt).WithMany(p => p.CemsUsers)
                .HasForeignKey(d => d.UsrDptId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_user_department");

            entity.HasOne(d => d.UsrPst).WithMany(p => p.CemsUsers)
                .HasForeignKey(d => d.UsrPstId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_user_position");

            entity.HasOne(d => d.UsrRol).WithMany(p => p.CemsUsers)
                .HasForeignKey(d => d.UsrRolId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_user_role");

            entity.HasOne(d => d.UsrSt).WithMany(p => p.CemsUsers)
                .HasForeignKey(d => d.UsrStId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_user_section");
        });

        modelBuilder.Entity<CemsVehicle>(entity =>
        {
            entity.HasKey(e => e.VhId).HasName("PRIMARY");

            entity.ToTable("cems_vehicle");

            entity.Property(e => e.VhId).HasColumnName("vh_id");
            entity.Property(e => e.VhPayrate).HasColumnName("vh_payrate");
            entity.Property(e => e.VhType)
                .HasColumnType("enum('private','public')")
                .HasColumnName("vh_type");
            entity.Property(e => e.VhVehicle)
                .HasMaxLength(45)
                .HasColumnName("vh_vehicle");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
