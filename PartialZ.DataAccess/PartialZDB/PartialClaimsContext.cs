using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace PartialZ.DataAccess.PartialZDB;

public partial class PartialClaimsContext : DbContext
{
    public PartialClaimsContext()
    {
    }

    public PartialClaimsContext(DbContextOptions<PartialClaimsContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Claim> Claims { get; set; }

    public virtual DbSet<DropdownCitizen> DropdownCitizens { get; set; }

    public virtual DbSet<DropdownCity> DropdownCities { get; set; }

    public virtual DbSet<DropdownEducation> DropdownEducations { get; set; }

    public virtual DbSet<DropdownEthnicity> DropdownEthnicities { get; set; }

    public virtual DbSet<DropdownGender> DropdownGenders { get; set; }

    public virtual DbSet<DropdownHandicap> DropdownHandicaps { get; set; }

    public virtual DbSet<DropdownNameSuffix> DropdownNameSuffixes { get; set; }

    public virtual DbSet<DropdownOtherWage> DropdownOtherWages { get; set; }

    public virtual DbSet<DropdownRace> DropdownRaces { get; set; }

    public virtual DbSet<DropdownVeteran> DropdownVeterans { get; set; }

    public virtual DbSet<DropdownWithholding> DropdownWithholdings { get; set; }

    public virtual DbSet<DropdownZip> DropdownZips { get; set; }

    public virtual DbSet<EmailTemplate> EmailTemplates { get; set; }

    public virtual DbSet<EmployeeDirectory> EmployeeDirectories { get; set; }

    public virtual DbSet<EmployeeWorkHistory> EmployeeWorkHistories { get; set; }

    public virtual DbSet<Employer> Employers { get; set; }

    public virtual DbSet<Filer> Filers { get; set; }

    public virtual DbSet<State> States { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Data Source=APPDEVDB19;Initial Catalog=PartialClaims;Persist Security Info=True;TrustServerCertificate=True;User ID=PartialEmp_User;Password=BtFItmFmnX04clyyrIGu");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.HasDefaultSchema("Data");

        modelBuilder.Entity<Claim>(entity =>
        {
            entity.ToTable("Claims", "dbo");

            entity.Property(e => e.AuthorizedAlienNumber)
                .HasMaxLength(20)
                .IsFixedLength();
            entity.Property(e => e.ClaimantFirstName).HasMaxLength(100);
            entity.Property(e => e.ClaimantLastName).HasMaxLength(100);
            entity.Property(e => e.ClaimantMiddleName).HasMaxLength(100);
            entity.Property(e => e.CreatedDate).HasColumnType("datetime");
            entity.Property(e => e.DateOfBirth).HasColumnType("datetime");
            entity.Property(e => e.Earnings).HasColumnType("decimal(18, 0)");
            entity.Property(e => e.HolidayPay).HasColumnType("decimal(18, 0)");
            entity.Property(e => e.LastDateWorked).HasMaxLength(50);
            entity.Property(e => e.LastModifedDate).HasColumnType("datetime");
            entity.Property(e => e.MailingCity).HasMaxLength(150);
            entity.Property(e => e.MailingStreetAddress).HasMaxLength(200);
            entity.Property(e => e.Occupation).HasMaxLength(150);
            entity.Property(e => e.OtherPay).HasColumnType("decimal(18, 0)");
            entity.Property(e => e.SocialSecurityNumber)
                .HasMaxLength(15)
                .IsFixedLength();
            entity.Property(e => e.TelephoneNumber).HasMaxLength(15);
            entity.Property(e => e.VacationPay).HasColumnType("decimal(18, 0)");
            entity.Property(e => e.WeekEndingDate).HasColumnType("datetime");
            entity.Property(e => e.ZipCode)
                .HasMaxLength(10)
                .IsFixedLength();

            entity.HasOne(d => d.Filer).WithMany(p => p.Claims)
                .HasForeignKey(d => d.FilerId)
                .HasConstraintName("FK__Claims__FilerId__37703C52");
        });

        modelBuilder.Entity<DropdownCitizen>(entity =>
        {
            entity.HasKey(e => e.CitizenId);

            entity.ToTable("Dropdown_Citizen", "dbo");

            entity.Property(e => e.CitizenId).HasColumnName("CitizenID");
            entity.Property(e => e.Code)
                .HasMaxLength(50)
                .IsUnicode(false);
        });

        modelBuilder.Entity<DropdownCity>(entity =>
        {
            entity.HasKey(e => e.CityId);

            entity.ToTable("Dropdown_City", "dbo");

            entity.Property(e => e.CityId).HasColumnName("CityID");
            entity.Property(e => e.Code)
                .HasMaxLength(50)
                .IsUnicode(false);
        });

        modelBuilder.Entity<DropdownEducation>(entity =>
        {
            entity.HasKey(e => e.EducationId);

            entity.ToTable("Dropdown_Education", "dbo");

            entity.Property(e => e.EducationId).HasColumnName("EducationID");
            entity.Property(e => e.Code)
                .HasMaxLength(50)
                .IsUnicode(false);
        });

        modelBuilder.Entity<DropdownEthnicity>(entity =>
        {
            entity.HasKey(e => e.EthnicityId);

            entity.ToTable("Dropdown_Ethnicity", "dbo");

            entity.Property(e => e.EthnicityId).HasColumnName("EthnicityID");
            entity.Property(e => e.Code)
                .HasMaxLength(50)
                .IsUnicode(false);
        });

        modelBuilder.Entity<DropdownGender>(entity =>
        {
            entity.HasKey(e => e.GenderId);

            entity.ToTable("Dropdown_Gender", "dbo");

            entity.Property(e => e.GenderId).HasColumnName("GenderID");
            entity.Property(e => e.Code)
                .HasMaxLength(50)
                .IsUnicode(false);
        });

        modelBuilder.Entity<DropdownHandicap>(entity =>
        {
            entity.HasKey(e => e.HandicapId);

            entity.ToTable("Dropdown_Handicap", "dbo");

            entity.Property(e => e.HandicapId).HasColumnName("HandicapID");
            entity.Property(e => e.Code)
                .HasMaxLength(50)
                .IsUnicode(false);
        });

        modelBuilder.Entity<DropdownNameSuffix>(entity =>
        {
            entity.HasKey(e => e.NameSuffixId);

            entity.ToTable("Dropdown_NameSuffix", "dbo");

            entity.Property(e => e.NameSuffixId).HasColumnName("NameSuffixID");
            entity.Property(e => e.Code)
                .HasMaxLength(50)
                .IsUnicode(false);
        });

        modelBuilder.Entity<DropdownOtherWage>(entity =>
        {
            entity.HasKey(e => e.OtherWagesId);

            entity.ToTable("Dropdown_OtherWages", "dbo");

            entity.Property(e => e.OtherWagesId).HasColumnName("OtherWagesID");
            entity.Property(e => e.Code)
                .HasMaxLength(50)
                .IsUnicode(false);
        });

        modelBuilder.Entity<DropdownRace>(entity =>
        {
            entity.HasKey(e => e.RaceId);

            entity.ToTable("Dropdown_Race", "dbo");

            entity.Property(e => e.RaceId).HasColumnName("RaceID");
            entity.Property(e => e.Code)
                .HasMaxLength(50)
                .IsUnicode(false);
        });

        modelBuilder.Entity<DropdownVeteran>(entity =>
        {
            entity.HasKey(e => e.VeteranId);

            entity.ToTable("Dropdown_Veteran", "dbo");

            entity.Property(e => e.VeteranId).HasColumnName("VeteranID");
            entity.Property(e => e.Code)
                .HasMaxLength(50)
                .IsUnicode(false);
        });

        modelBuilder.Entity<DropdownWithholding>(entity =>
        {
            entity.HasKey(e => e.WithholdingsId);

            entity.ToTable("Dropdown_Withholdings", "dbo");

            entity.Property(e => e.WithholdingsId).HasColumnName("WithholdingsID");
            entity.Property(e => e.Code)
                .HasMaxLength(50)
                .IsUnicode(false);
        });

        modelBuilder.Entity<DropdownZip>(entity =>
        {
            entity.HasKey(e => e.Zipid).HasName("PK_Dropdown_");

            entity.ToTable("Dropdown_ZIP", "dbo");

            entity.Property(e => e.Zipid).HasColumnName("ZIPID");
            entity.Property(e => e.City)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.State)
                .HasMaxLength(2)
                .IsUnicode(false);
            entity.Property(e => e.Zip)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("ZIP");
        });

        modelBuilder.Entity<EmailTemplate>(entity =>
        {
            entity.ToTable("EmailTemplates", "dbo");

            entity.Property(e => e.Id).HasColumnName("ID");
            entity.Property(e => e.CreatedDate).HasColumnType("datetime");
            entity.Property(e => e.LastModifedDate).HasColumnType("datetime");
            entity.Property(e => e.Subject).HasMaxLength(500);
        });

        modelBuilder.Entity<EmployeeDirectory>(entity =>
        {
            entity.ToTable("EmployeeDirectories", "dbo");

            entity.Property(e => e.AuthorizedAlienNumber).HasMaxLength(20);
            entity.Property(e => e.ClaimantFirstName).HasMaxLength(100);
            entity.Property(e => e.ClaimantLastName).HasMaxLength(100);
            entity.Property(e => e.ClaimantMiddleName).HasMaxLength(100);
            entity.Property(e => e.CreatedDate).HasColumnType("datetime");
            entity.Property(e => e.DateOfBirth).HasColumnType("datetime");
            entity.Property(e => e.LastModifedDate).HasColumnType("datetime");
            entity.Property(e => e.MailingCity).HasMaxLength(150);
            entity.Property(e => e.MailingStreetAddress).HasMaxLength(200);
            entity.Property(e => e.Occupation).HasMaxLength(50);
            entity.Property(e => e.SocialSecurityNumber).HasMaxLength(20);
            entity.Property(e => e.TelephoneNumber).HasMaxLength(15);
            entity.Property(e => e.ZipCode).HasMaxLength(10);

            entity.HasOne(d => d.Filer).WithMany(p => p.EmployeeDirectories)
                .HasForeignKey(d => d.FilerId)
                .HasConstraintName("FK__EmployeeD__Filer__3493CFA7");
        });

        modelBuilder.Entity<EmployeeWorkHistory>(entity =>
        {
            entity.ToTable("EmployeeWorkHistory", "dbo");

            entity.Property(e => e.CreatedDate).HasColumnType("datetime");
            entity.Property(e => e.EmployeeId).HasColumnName("EmployeeID");
            entity.Property(e => e.LastModifedDate).HasColumnType("datetime");
            entity.Property(e => e.PayrollEndDay).HasMaxLength(50);
        });

        modelBuilder.Entity<Employer>(entity =>
        {
            entity.ToTable("Employer", "dbo");

            entity.Property(e => e.City).HasMaxLength(100);
            entity.Property(e => e.CreatedDate).HasColumnType("datetime");
            entity.Property(e => e.Eannumber)
                .HasMaxLength(100)
                .HasColumnName("EANNumber");
            entity.Property(e => e.EmployerEmail).HasMaxLength(100);
            entity.Property(e => e.Feinnumber)
                .HasMaxLength(100)
                .HasColumnName("FEINNumber");
            entity.Property(e => e.LastModifedDate).HasColumnType("datetime");
            entity.Property(e => e.Name).HasMaxLength(500);
            entity.Property(e => e.State).HasMaxLength(100);
            entity.Property(e => e.ZipCode).HasMaxLength(100);
        });

        modelBuilder.Entity<Filer>(entity =>
        {
            entity.HasKey(e => e.EmployeeId).HasName("PK_Employee");

            entity.ToTable("Filer", "dbo");

            entity.Property(e => e.BusinessTitle).HasMaxLength(500);
            entity.Property(e => e.CreatedDate).HasColumnType("datetime");
            entity.Property(e => e.Email).HasMaxLength(150);
            entity.Property(e => e.FirstName).HasMaxLength(100);
            entity.Property(e => e.LastModifedDate).HasColumnType("datetime");
            entity.Property(e => e.LastName).HasMaxLength(100);
            entity.Property(e => e.LoginOtp).HasColumnName("LoginOTP");
            entity.Property(e => e.PhoneNumber).HasMaxLength(15);
        });

        modelBuilder.Entity<State>(entity =>
        {
            entity.HasKey(e => e.StateCode);

            entity.ToTable("States", "dbo");

            entity.Property(e => e.StateCode)
                .HasMaxLength(2)
                .IsUnicode(false);
            entity.Property(e => e.Code)
                .HasMaxLength(4)
                .IsUnicode(false);
            entity.Property(e => e.Fipscode)
                .HasMaxLength(2)
                .IsUnicode(false)
                .HasColumnName("FIPSCode");
            entity.Property(e => e.HighZip)
                .HasMaxLength(3)
                .IsUnicode(false);
            entity.Property(e => e.LowZip)
                .HasMaxLength(3)
                .IsUnicode(false);
            entity.Property(e => e.StateId)
                .ValueGeneratedOnAdd()
                .HasColumnName("StateID");
            entity.Property(e => e.StateName)
                .HasMaxLength(128)
                .IsUnicode(false);
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
