using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.Dtos
{
    public class NotesDto
    {
        public int Id {get;set;}
        public string Subject { get; set; }
        public double Note { get; set; }
        public int IdStudent { get; set; }
    }
}