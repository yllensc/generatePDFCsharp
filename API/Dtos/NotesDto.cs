using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.Dtos
{
    public class NotesDto
    {
        public int Id {get;set;}
        public int IdSubject { get; set; }
        public double Note1 { get; set; }
        public double Note2 { get; set; }
        public double Note3 { get; set; }
        public double Average { get; set; }
        public int IdStudent { get; set; }
    }
}