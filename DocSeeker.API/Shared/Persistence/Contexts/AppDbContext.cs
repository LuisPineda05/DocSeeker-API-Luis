using DocSeeker.API.DocSeeker.Domain.Models;
using DocSeeker.API.Security.Domain.Models;
using DocSeeker.API.Shared.Extensions;
using Microsoft.EntityFrameworkCore;

namespace DocSeeker.API.Shared.Persistence.Contexts;

public class AppDbContext : DbContext
{
    

    public DbSet<User> Users { get; set; }
    public DbSet<Patient> Patients { get; set; }
    public DbSet<Doctor> Doctors { get; set; }
    public DbSet<New> News { get; set; }
    public DbSet<HourAvailable> HoursAvailable { get; set; }

    public DbSet<Date> Dates { get; set; }

    public AppDbContext(DbContextOptions options) : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);

        // Users

        // Constraints
        builder.Entity<User>().ToTable("Users");
        builder.Entity<User>().HasKey(p => p.Id);
        builder.Entity<User>().Property(p => p.Id).IsRequired().ValueGeneratedOnAdd();
        builder.Entity<User>().Property(p => p.Dni).IsRequired().HasMaxLength(30);
        builder.Entity<User>().Property(p => p.FirstName).IsRequired();
        builder.Entity<User>().Property(p => p.LastName).IsRequired();
        builder.Entity<User>().Property(p => p.Email).IsRequired().HasMaxLength(100);
        builder.Entity<User>().Property(p => p.cell1).IsRequired();
        builder.Entity<User>().Property(p => p.Birthday).IsRequired();
        builder.Entity<User>().Property(p => p.Genre).IsRequired();
        builder.Entity<User>()
                .HasDiscriminator<int>("Type")
                .HasValue<Patient>(1)
                .HasValue<Doctor>(2);

        //Patients
        builder.Entity<Patient>().ToTable("Users").HasBaseType<User>();
        //builder.Entity<Patient>().HasKey(p => p.Id);
        //builder.Entity<Patient>().Property(p => p.Id).IsRequired().ValueGeneratedOnAdd();
        /* builder.Entity<Patient>().Property(p => p.Dni).IsRequired().HasMaxLength(30);
         builder.Entity<Patient>().Property(p => p.FirstName).IsRequired();
         builder.Entity<Patient>().Property(p => p.LastName).IsRequired();
         builder.Entity<Patient>().Property(p => p.Email).IsRequired().HasMaxLength(100);
         builder.Entity<Patient>().Property(p => p.cell1).IsRequired();
         builder.Entity<Patient>().Property(p => p.Birthday).IsRequired();
         builder.Entity<Patient>().Property(p => p.Genre).IsRequired();*/
        builder.Entity<Patient>().HasData(
               new Patient
               {
                   Id = 1,
                   Dni = "74747474",
                   Password = "contra123456",
                   FirstName = "Juan",
                   LastName = "Perez",
                   Email = "juanperez@gmail.com",
                   cell1 = "968745123",
                   Birthday = "22/02/1985",
                   Genre = "male",
                   photo = " https://images.pexels.com/photos/3831612/pexels-photo-3831612.jpeg?auto=compress&cs=tinysrgb&w=600"
               });

   


        //Doctors
        builder.Entity<Doctor>().ToTable("Users").HasBaseType<User>(); ;
        //builder.Entity<Doctor>().HasKey(p => p.Id);
        //builder.Entity<Doctor>().Property(p => p.Id).IsRequired().ValueGeneratedOnAdd();
        builder.Entity<Doctor>().Property(p => p.Area).IsRequired();
        builder.Entity<Doctor>().Property(p => p.Description).IsRequired();
        builder.Entity<Doctor>().Property(p => p.Patients).IsRequired();
        builder.Entity<Doctor>().Property(p => p.Years).IsRequired();
        builder.Entity<Doctor>().Property(p => p.Age).IsRequired();
        builder.Entity<Doctor>().Property(p => p.Cost).IsRequired();

        builder.Entity<Doctor>().HasData(
               new Doctor
               {
                   Id = 2,
                   Dni = "43434343",
                   Password = "contra987",
                   FirstName = "Pedro",
                   LastName = "Camones",
                   Email = "pedrocamones@gmail.com",
                   cell1 = "999888777",
                   Birthday = "22/02/1972",
                   Genre = "male",
                   photo = "https://tse3.mm.bing.net/th?id=OIP.m5GEWxP3vt6Z24SMIJ5z5AHaKX&pid=Api&P=0",
                   Area = "Dermatology",
                   Description = "A responsible doctor who always tries to help those who need it.",
                   Patients = 345,
                   Years = 15,
                   Age = 45,
                   Cost = 70.23
               });
   

        //News
        builder.Entity<New>().ToTable("News");
        builder.Entity<New>().HasKey(p => p.Id);
        builder.Entity<New>().Property(p => p.Id).IsRequired().ValueGeneratedOnAdd();
        builder.Entity<New>().Property(p => p.Image).IsRequired();
        builder.Entity<New>().Property(p => p.Title).IsRequired();
        builder.Entity<New>().Property(p => p.Info).IsRequired();
        builder.Entity<New>().Property(p => p.Description).IsRequired();
        builder.Entity<New>().Property(p => p.Views).IsRequired();

        builder.Entity<New>().HasData(
               new New
               {
                   Id = 1,
                   Image = "https://image.ondacero.es/clipping/cmsimages02/2022/10/17/BF222101-B7D9-4A98-9C28-9F4B1DBF53F3/gente-hace-cola-hacerse-prueba-pcr-coronavirus-china_97.jpg?crop=1920,1080,x0,y53&width=1600&height=900&optimize=high&format=webply",
                   Title = "China confines almost a million people in the east of the country and increases restrictions",
                   Description = "Almost a million residents of the Zhongyuan district, one of the most populous in the city of Zhengzhou, have been ordered to stay at home from Monday.",
                   Info = "The city of Zhengzhou, in eastern China, joins the increase in restrictions that China is imposing in the face of coronavirus outbreaks. Nearly a million residents of the Zhongyuan district have been ordered to stay home from Monday, except for times when they have to undergo coronavirus tests, according to local media.",
                   Views = 12742
               });



        //Dates
        builder.Entity<Date>().ToTable("Dates");
        builder.Entity<Date>().HasKey(p => p.Id);
        builder.Entity<Date>().Property(p => p.Id).IsRequired().ValueGeneratedOnAdd();
        builder.Entity<Date>().Property(p => p.CDate).IsRequired();
        builder.Entity<Date>()
               .HasOne(p => p.CPatient)
               .WithMany(q => q.CDates)
               .HasForeignKey(id => id.IdPatient);
        builder.Entity<Date>()
               .HasOne(p => p.CDoctor)
               .WithMany(q => q.CDates)
               .HasForeignKey(id => id.DoctorId);


        builder.Entity<Date>().HasData
         (new Date
         {

               Id = 1,
               IdPatient  = 1,
               DoctorId = 2,
               CDate = "2022-10-14"
         }
         );


        builder.Entity<HourAvailable>().ToTable("HourAvailables");
        builder.Entity<HourAvailable>().HasKey(p => p.Id);
        builder.Entity<HourAvailable>().Property(p => p.Id).IsRequired().ValueGeneratedOnAdd();
        builder.Entity<HourAvailable>().Property(p => p.Hours).IsRequired();
        builder.Entity<HourAvailable>().Property(p => p.Booked).IsRequired();

        builder.Entity<HourAvailable>()
               .HasOne(p => p.CDoctor)
               .WithMany(q => q.CHoursAvailable)
               .HasForeignKey(id => id.DoctorId);

        builder.Entity<HourAvailable>().HasData
         (new HourAvailable
         {
             Id = 1,
             Hours = "3:00 - 4:00",
             Booked = true,
             DoctorId = 2
         }
         );

        // Apply Snake Case Naming Convention

        builder.UseSnakeCaseNamingConvention();
    }
}
