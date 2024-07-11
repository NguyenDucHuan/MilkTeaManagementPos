using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
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

    public virtual DbSet<MenuItem> MenuItems { get; set; }

    public virtual DbSet<TbBill> TbBills { get; set; }

    public virtual DbSet<TbBillDetailt> TbBillDetailts { get; set; }

    public virtual DbSet<TbCustomer> TbCustomers { get; set; }

    public virtual DbSet<TbGroupProduct> TbGroupProducts { get; set; }

    public virtual DbSet<TbGroupTb> TbGroupTbs { get; set; }

    public virtual DbSet<TbProduct> TbProducts { get; set; }

    public virtual DbSet<TbStore> TbStores { get; set; }

    public virtual DbSet<TbTable> TbTables { get; set; }

    public MilkTeaContext(string connectionString)
    {
        this.Database.SetConnectionString(connectionString);
    }

    private string? GetConnectionString()
    {
        IConfiguration configuration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", true, true).Build();
        return configuration["ConnectionStrings:DefaultConnectionStringDB"];
    }
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        => optionsBuilder.UseSqlServer(GetConnectionString());

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Employee>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Employee__3213E83F846630E8");

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
            entity.HasKey(e => e.Id).HasName("PK__Login__3213E83FC0C811B0");

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
            entity.HasKey(e => e.Id).HasName("PK__LoginRol__3213E83FBD289CCC");

            entity.ToTable("LoginRole");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.IdLogin).HasColumnName("idLogin");
            entity.Property(e => e.IdMenuItems).HasColumnName("idMenuItems");

            entity.HasOne(d => d.IdLoginNavigation).WithMany(p => p.LoginRoles)
                .HasForeignKey(d => d.IdLogin)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__LoginRole__idLog__114A936A");

            entity.HasOne(d => d.IdMenuItemsNavigation).WithMany(p => p.LoginRoles)
                .HasForeignKey(d => d.IdMenuItems)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__LoginRole__idMen__123EB7A3");
        });

        modelBuilder.Entity<MenuItem>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__MenuItem__3213E83F3BB2132D");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.NameMenu)
                .HasMaxLength(250)
                .HasColumnName("nameMenu");
            entity.Property(e => e.NameShow)
                .HasMaxLength(250)
                .HasColumnName("nameShow");
        });

        modelBuilder.Entity<TbBill>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__tbBill__3213E83F98ECEE8E");

            entity.ToTable("tbBill");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.BillDate)
                .HasColumnType("datetime")
                .HasColumnName("billDate");
            entity.Property(e => e.Description)
                .HasMaxLength(500)
                .HasColumnName("description");
            entity.Property(e => e.IdCustomer).HasColumnName("idCustomer");
            entity.Property(e => e.IdTable).HasColumnName("idTable");
            entity.Property(e => e.IdUser).HasColumnName("idUser");
            entity.Property(e => e.Status).HasColumnName("status");
            entity.Property(e => e.TotalMoney).HasColumnName("totalMoney");

            entity.HasOne(d => d.IdCustomerNavigation).WithMany(p => p.TbBills)
                .HasForeignKey(d => d.IdCustomer)
                .HasConstraintName("FK__tbBill__idCustom__619B8048");

            entity.HasOne(d => d.IdTableNavigation).WithMany(p => p.TbBills)
                .HasForeignKey(d => d.IdTable)
                .HasConstraintName("FK__tbBill__idTable__5FB337D6");

            entity.HasOne(d => d.IdUserNavigation).WithMany(p => p.TbBills)
                .HasForeignKey(d => d.IdUser)
                .HasConstraintName("FK__tbBill__idUser__60A75C0F");
        });

        modelBuilder.Entity<TbBillDetailt>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__tbBillDe__3213E83F0B801213");

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
                .HasConstraintName("FK__tbBillDet__idBil__6477ECF3");

            entity.HasOne(d => d.IdProductNavigation).WithMany(p => p.TbBillDetailts)
                .HasForeignKey(d => d.IdProduct)
                .HasConstraintName("FK__tbBillDet__idPro__656C112C");
        });

        modelBuilder.Entity<TbCustomer>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__tbCustom__3213E83FBF61D265");

            entity.ToTable("tbCustomer");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Address)
                .HasMaxLength(500)
                .HasColumnName("address");
            entity.Property(e => e.Description)
                .HasMaxLength(500)
                .HasColumnName("description");
            entity.Property(e => e.Name)
                .HasMaxLength(500)
                .HasColumnName("name");
            entity.Property(e => e.Phone)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("phone");
            entity.Property(e => e.Status).HasColumnName("status");
        });

        modelBuilder.Entity<TbGroupProduct>(entity =>
        {
            entity.HasKey(e => e.IdGr).HasName("PK__tbGroupP__9DB891D5F32B8B90");

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
            entity.HasKey(e => e.Id).HasName("PK__tbGroupT__3213E83F2D9E3871");

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
            entity.HasKey(e => e.Id).HasName("PK__tbProduc__3213E83F63BA7925");

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
                .HasConstraintName("FK__tbProduct__idGro__5812160E");
        });

        modelBuilder.Entity<TbStore>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__tbStore__3213E83F6C30265F");

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
            entity.HasKey(e => e.Id).HasName("PK__tbTable__3213E83F7B37AB10");

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
                .HasConstraintName("FK__tbTable__idGroup__5CD6CB2B");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
