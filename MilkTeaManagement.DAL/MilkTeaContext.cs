using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using MilkTeaManagement.DAL.Entities;

namespace MilkTeaManagement.DAL;

public partial class MilkTeaContext : DbContext
{
    public MilkTeaContext()
    {
    }

    public MilkTeaContext(DbContextOptions<MilkTeaContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Employee> Employees { get; set; }

    public virtual DbSet<Login> Logins { get; set; }

    public virtual DbSet<LoginRole> LoginRoles { get; set; }

    public virtual DbSet<TbBill> TbBills { get; set; }

    public virtual DbSet<TbBillDetailt> TbBillDetailts { get; set; }

    public virtual DbSet<TbGroupProduct> TbGroupProducts { get; set; }

    public virtual DbSet<TbGroupTb> TbGroupTbs { get; set; }

    public virtual DbSet<TbProduct> TbProducts { get; set; }

    public virtual DbSet<TbRole> TbRoles { get; set; }

    public virtual DbSet<TbStore> TbStores { get; set; }

    public virtual DbSet<TbTable> TbTables { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Server=(local);Database=MilkTea;UID=sa;PWD=1;TrustServerCertificate=True");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Employee>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Employee__3213E83FF69F00CB");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Address)
                .HasMaxLength(500)
                .HasColumnName("address");
            entity.Property(e => e.DateWork).HasColumnName("dateWork");
            entity.Property(e => e.FullName)
                .HasMaxLength(250)
                .HasColumnName("fullName");
            entity.Property(e => e.IdCard)
                .HasMaxLength(150)
                .IsUnicode(false)
                .HasColumnName("idCard");
            entity.Property(e => e.Img).HasColumnName("img");
            entity.Property(e => e.PhoneNumber)
                .HasMaxLength(15)
                .IsUnicode(false)
                .HasColumnName("phoneNumber");
            entity.Property(e => e.Status).HasColumnName("status");
        });

        modelBuilder.Entity<Login>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Login__3213E83FA6958164");

            entity.ToTable("Login");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.IdEmployee).HasColumnName("idEmployee");
            entity.Property(e => e.IsUse).HasColumnName("isUse");
            entity.Property(e => e.Password)
                .IsUnicode(false)
                .HasColumnName("password");
            entity.Property(e => e.UserName)
                .HasMaxLength(250)
                .IsUnicode(false)
                .HasColumnName("userName");

            entity.HasOne(d => d.IdEmployeeNavigation).WithMany(p => p.Logins)
                .HasForeignKey(d => d.IdEmployee)
                .HasConstraintName("FK__Login__idEmploye__4BAC3F29");
        });

        modelBuilder.Entity<LoginRole>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__LoginRol__3213E83F6FB305CE");

            entity.ToTable("LoginRole");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.IdLogin).HasColumnName("idLogin");
            entity.Property(e => e.IdRole).HasColumnName("idRole");

            entity.HasOne(d => d.IdLoginNavigation).WithMany(p => p.LoginRoles)
                .HasForeignKey(d => d.IdLogin)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__LoginRole__idLog__5070F446");

            entity.HasOne(d => d.IdRoleNavigation).WithMany(p => p.LoginRoles)
                .HasForeignKey(d => d.IdRole)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__LoginRole__idRol__5165187F");
        });

        modelBuilder.Entity<TbBill>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__tbBill__3213E83F87C8183D");

            entity.ToTable("tbBill");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.BillDate)
                .HasColumnType("datetime")
                .HasColumnName("billDate");
            entity.Property(e => e.Description)
                .HasMaxLength(500)
                .HasColumnName("description");
            entity.Property(e => e.IdTable).HasColumnName("idTable");
            entity.Property(e => e.IdUser).HasColumnName("idUser");
            entity.Property(e => e.Status).HasColumnName("status");
            entity.Property(e => e.TotalMoney).HasColumnName("totalMoney");

            entity.HasOne(d => d.IdTableNavigation).WithMany(p => p.TbBills)
                .HasForeignKey(d => d.IdTable)
                .HasConstraintName("FK__tbBill__idTable__5DCAEF64");

            entity.HasOne(d => d.IdUserNavigation).WithMany(p => p.TbBills)
                .HasForeignKey(d => d.IdUser)
                .HasConstraintName("FK__tbBill__idUser__5EBF139D");
        });

        modelBuilder.Entity<TbBillDetailt>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__tbBillDe__3213E83FABC6D202");

            entity.ToTable("tbBillDetailt");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Description)
                .HasMaxLength(500)
                .HasColumnName("description");
            entity.Property(e => e.IdBill).HasColumnName("idBill");
            entity.Property(e => e.IdProduct).HasColumnName("idProduct");
            entity.Property(e => e.IntoMoney).HasColumnName("intoMoney");
            entity.Property(e => e.Quantity).HasColumnName("quantity");
            entity.Property(e => e.UnitPrice).HasColumnName("unitPrice");

            entity.HasOne(d => d.IdBillNavigation).WithMany(p => p.TbBillDetailts)
                .HasForeignKey(d => d.IdBill)
                .HasConstraintName("FK__tbBillDet__idBil__619B8048");

            entity.HasOne(d => d.IdProductNavigation).WithMany(p => p.TbBillDetailts)
                .HasForeignKey(d => d.IdProduct)
                .HasConstraintName("FK__tbBillDet__idPro__628FA481");
        });

        modelBuilder.Entity<TbGroupProduct>(entity =>
        {
            entity.HasKey(e => e.IdGr).HasName("PK__tbGroupP__9DB891D50F1F5FDC");

            entity.ToTable("tbGroupProduct");

            entity.Property(e => e.IdGr).HasColumnName("idGr");
            entity.Property(e => e.DescriptionGr)
                .HasMaxLength(500)
                .HasColumnName("descriptionGr");
            entity.Property(e => e.NameGr)
                .HasMaxLength(500)
                .HasColumnName("nameGr");
        });

        modelBuilder.Entity<TbGroupTb>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__tbGroupT__3213E83F71C90357");

            entity.ToTable("tbGroupTb");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Description)
                .HasMaxLength(500)
                .HasColumnName("description");
            entity.Property(e => e.Name)
                .HasMaxLength(50)
                .HasColumnName("name");
        });

        modelBuilder.Entity<TbProduct>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__tbProduc__3213E83F5491166F");

            entity.ToTable("tbProduct");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Description)
                .HasMaxLength(500)
                .HasColumnName("description");
            entity.Property(e => e.IdGroupProduct).HasColumnName("idGroupProduct");
            entity.Property(e => e.Img).HasColumnName("img");
            entity.Property(e => e.Name)
                .HasMaxLength(500)
                .HasColumnName("name");
            entity.Property(e => e.Unit)
                .HasMaxLength(50)
                .HasColumnName("unit");
            entity.Property(e => e.UnitPrice).HasColumnName("unitPrice");

            entity.HasOne(d => d.IdGroupProductNavigation).WithMany(p => p.TbProducts)
                .HasForeignKey(d => d.IdGroupProduct)
                .HasConstraintName("FK__tbProduct__idGro__5629CD9C");
        });

        modelBuilder.Entity<TbRole>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__TbRole__3213E83F21B2C85D");

            entity.ToTable("TbRole");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.RoleName).HasMaxLength(250);
        });

        modelBuilder.Entity<TbStore>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__tbStore__3213E83F7A759281");

            entity.ToTable("tbStore");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.AddressStore)
                .HasMaxLength(500)
                .HasColumnName("addressStore");
            entity.Property(e => e.NameStore)
                .HasMaxLength(500)
                .HasColumnName("nameStore");
            entity.Property(e => e.PhoneStore)
                .HasMaxLength(500)
                .HasColumnName("phoneStore");
            entity.Property(e => e.TaxCode)
                .HasMaxLength(250)
                .HasColumnName("taxCode");
        });

        modelBuilder.Entity<TbTable>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__tbTable__3213E83F4791953A");

            entity.ToTable("tbTable");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Description)
                .HasMaxLength(50)
                .HasColumnName("description");
            entity.Property(e => e.IdGroup).HasColumnName("idGroup");
            entity.Property(e => e.NameTb)
                .HasMaxLength(50)
                .HasColumnName("nameTb");
            entity.Property(e => e.Status).HasColumnName("status");

            entity.HasOne(d => d.IdGroupNavigation).WithMany(p => p.TbTables)
                .HasForeignKey(d => d.IdGroup)
                .HasConstraintName("FK__tbTable__idGroup__5AEE82B9");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
