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

    public DbSet<Prescription> Prescriptions { get; set; }
    public DbSet<HourAvailable> HoursAvailable { get; set; }
    public DbSet<MedicalInformation> MedicalInformations { get; set; }

    public DbSet<Date> Dates { get; set; }

    public DbSet<Review> Reviews { get; set; }


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

        //Reviews
        builder.Entity<Review>().ToTable("Reviews");
        builder.Entity<Review>().HasKey(p => p.Id);
        builder.Entity<Review>().Property(p => p.Id).IsRequired().ValueGeneratedOnAdd();
        builder.Entity<Review>().Property(p => p.ProfilePhotoUrl).IsRequired();
        builder.Entity<Review>().Property(p => p.CustomerReview).IsRequired();
        builder.Entity<Review>().Property(p => p.CustomerName).IsRequired();
        builder.Entity<Review>().Property(p => p.CustomerScore).IsRequired();
        builder.Entity<Review>()
               .HasOne(p => p.CPatient)
               .WithMany(q => q.CReviews)
               .HasForeignKey(id => id.IdPatient);
        builder.Entity<Review>()
               .HasOne(p => p.CDoctor)
               .WithMany(q => q.CReviews)
               .HasForeignKey(id => id.IdDoctor);

        builder.Entity<Review>().HasData
         (new Review
         {

             Id = 1,
             ProfilePhotoUrl = "https://upload.wikimedia.org/wikipedia/commons/thumb/d/d8/Emblem-person-blue.svg/2048px-Emblem-person-blue.svg.png",
             CustomerReview = "Very good specialist. Help me with every question I had. I would definetely recommen it!!!",
             CustomerName = "Juan Perez",
             CustomerScore = 4,
             IdPatient = 1,
             IdDoctor = 2         }
         );



        //HourAvailable
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
             Booked = false,
             DoctorId = 2
         }
         );

        //MedicalInformation
        builder.Entity<MedicalInformation>().ToTable("MedicalHistories");
        builder.Entity<MedicalInformation>().HasKey(p => p.Id);
        builder.Entity<MedicalInformation>().Property(p => p.Id).IsRequired().ValueGeneratedOnAdd();
        builder.Entity<MedicalInformation>().Property(p => p.weight).IsRequired();
        builder.Entity<MedicalInformation>().Property(p => p.height).IsRequired();
        builder.Entity<MedicalInformation>().Property(p => p.bodyMass).IsRequired();
        builder.Entity<MedicalInformation>().Property(p => p.allergy).IsRequired();
        builder.Entity<MedicalInformation>().Property(p => p.pathological).IsRequired();

        builder.Entity<MedicalInformation>()
               .HasOne(p => p.CPatient)
               .WithMany(q => q.CMedicalInformation)
               .HasForeignKey(id => id.IdPatient);



        //Prescription
        builder.Entity<Prescription>().ToTable("Prescriptions");
        builder.Entity<Prescription>().HasKey(p => p.Id);
        builder.Entity<Prescription>().Property(p => p.Id).IsRequired().ValueGeneratedOnAdd();
        builder.Entity<Prescription>().Property(p => p.DateIssue).IsRequired();
        builder.Entity<Prescription>().Property(p => p.DateExpiration).IsRequired();
        builder.Entity<Prescription>().Property(p => p.MedicalSpeciality).IsRequired();
        builder.Entity<Prescription>().Property(p => p.RecipCode).IsRequired();
        builder.Entity<Prescription>().Property(p => p.Condition).IsRequired();
        builder.Entity<Prescription>().Property(p => p.Rest).IsRequired();
        builder.Entity<Prescription>().Property(p => p.Drink).IsRequired();
        builder.Entity<Prescription>().Property(p => p.Food).IsRequired();
        builder.Entity<Prescription>().Property(p => p.Medicines).IsRequired();
        builder.Entity<Prescription>().Property(p => p.NumberDose).IsRequired();
        builder.Entity<Prescription>().Property(p => p.Meals).IsRequired();
        builder.Entity<Prescription>().Property(p => p.Hours).IsRequired();

        builder.Entity<Prescription>()
               .HasOne(p => p.CPatient)
               .WithMany(q => q.CPrescription)
               .HasForeignKey(id => id.IdPatient);


        builder.Entity<Prescription>().HasData
         (new Prescription
         {

             Id = 1,
             IdPatient = 1,
             DateIssue = "10/05/2018",
             DateExpiration = "09/06/2018",
             MedicalSpeciality = "Dermatology",
             RecipCode = "123456789",
             Condition = "Valid",
             Rest = ". Sleep a lot Rest your voice too.",
             Drink = ". Liquids keep the throat hydrated and prevent dehydration. Avoid caffeine and alcohol, which can dehydrate you.",
             Food = ". Hot beverages, such as broth, non-caffeinated tea, or warm water with honey, and cold snacks, such as popsicle sticks, can soothe a sore throat. You should never give honey to babies younger than 1 month old.",
             NumberDose = 3,
             Medicines = "Cetirizina,Paracetamol,Migralivia",
             Meals = "3 times per week,2 times per nigth",
             Hours = "03:00,04:00"
         }
         );

        // Apply Snake Case Naming Convention

        builder.UseSnakeCaseNamingConvention();
    }
}
