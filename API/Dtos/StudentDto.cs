using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.Dtos
{
    public class StudentDto
    {
        public int Id {get;set;}
        public string NameStudent { get; set; }
        public string StudentIdentification { get; set;}
        public string Profile {get; set;}
        public IEnumerable<NotesDto> Notes { get; set;}
    }

     public class StudentAverageTotalDto
    {
        public string NameStudent { get; set; }
        public string AverageTotal { get; set; }
        
    }
}