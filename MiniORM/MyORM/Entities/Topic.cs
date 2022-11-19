using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyORM.Entities
{
    internal class Topic
    {
        public int Id { get; set; }
        public int CourseId { get; set; }
        public string? Title { get; set; }
        public string? Description { get; set; }
        public List<Session>? Sessions { get; set; }
    }
}
