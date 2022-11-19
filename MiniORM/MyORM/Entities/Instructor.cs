using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyORM.Entities
{
    internal class Instructor
    {
        public int Id { get; set; }
        public int CourseId { get; set; }
        public string? Name { get; set; }
        public string? Email { get; set; }
        public Address? PresentAddress { get; set; }
        public Address? PermanentAddress { get; set; }
        public List<Phone>? PhoneNumbers { get; set; }
    }
}
