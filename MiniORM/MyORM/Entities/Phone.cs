﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyORM.Entities
{
    internal class Phone
    {
        //public int Id { get; set; }
        public int InstructorId { get; set; }
        public string? Number { get; set; }
        public string? Extension { get; set; }
        public string? CountryCode { get; set; }
    }
}
