using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class Notes : BaseEntity
    {
        public string Subject { get; set; }
        public double Note { get; set; }
        public int IdStudent { get; set; }
        public Student Student{ get; set; }
    }
}
    
