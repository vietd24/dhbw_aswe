﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace mvvm.Model
{
    public class StudentCollection
    {
        public string Name { get; set; }
        public Teacher Teacher { get; set; }
        public IList<Student> Students { get; }
        public IList<Student> FullAgeStudents
        {
            get
            {
                return this.Students.Where(x => x.Age >= 18).ToList();
            }
        }

        public StudentCollection()
        {
            Students = new List<Student>();
        }
    }
}
