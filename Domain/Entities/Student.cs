using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class Student : BaseEntity
    {
        public string NameStudent { get; set; }
        public string StudentIdentification { get; set;}
        public string Profile {get; set;}
        public IEnumerable<Notes> Notes { get; set;}
    }
}