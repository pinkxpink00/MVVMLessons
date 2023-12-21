using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace mvvmLessons.Models.Decanat
{
    public class Group
    {
        public string Name { get; set; }

        public ICollection<Student> Students { get; set; }
    }
}
