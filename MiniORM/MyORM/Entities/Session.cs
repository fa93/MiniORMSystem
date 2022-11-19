using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyORM.Entities
{
    internal class Session
    {
        //public int Id { get; set; }
        public int TopicId { get; set; }
        public int DurationInHour { get; set; }
        public string? LearningObjective { get; set; }
    }
}
