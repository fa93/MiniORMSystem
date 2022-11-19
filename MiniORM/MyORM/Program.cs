// See https://aka.ms/new-console-template for more information
using MyORM;
using MyORM.Entities;

Console.WriteLine("======================MY MINI ORM=====================");

Course course = new()
{
    Id = 6,
    Title = "Data Structure",
    Fees = 25000,
    Teacher = new Instructor
    {
        Id = 6,
        CourseId = 6,
        Name = "Rafiq",
        Email = "rafiq@gmail.com",
        PresentAddress = new Address
        {
            //Id = 3,//for update query
            InstructorId = 6,
            Street = "10/F",
            City = "Dhaka",
            Country = "Bangladesh"
        },

        PermanentAddress = new Address
        {
            //Id = 3,//for update query
            InstructorId = 6,
            Street = "9/D",
            City = "Dhaka",
            Country = "Bangladesh"
        },

        PhoneNumbers = new List<Phone>
        {
            new Phone(){Number = "1234678", Extension = "439", CountryCode = "088"/*,InstructorId=6*/},//Id for update
            new Phone(){Number = "7865434", Extension = "440", CountryCode = "088"/*,InstructorId=6*/}//Id for update
        }

    },

    Topics = new List<Topic>
    {
        new Topic
        {
            Id = 6,
            CourseId = 6,
            Title="Data Structure",
            Description="a linear data structure, in which the elements are not stored at contiguous memory locations",
            Sessions=new List<Session>()
            {
                new Session{DurationInHour = 2, LearningObjective = "Types of Linked List"/*, TopicId=6*/},//Id for update
                new Session{DurationInHour = 3, LearningObjective="Usage of Linked List"/*, TopicId=6*/}//Id for update
            }
        }

    },

    Tests = new List<AdmissionTest>
    {
        new AdmissionTest
        {
            //Id =3,//for update query
            CourseId = 6,
            StartDateTime = new DateTime(2022,03,02,10,30,00),
            EndDateTime = new DateTime(2022,04,03,11,30,00),
            TestFees = 1500
        }
    }
};

ORM<Course> orm = new();

orm.Insert(course);
//orm.Update(course);
//orm.Delete(course);
//orm.Delete(course.Id);
//orm.GetById(course.Id);
//orm.GetAll()
