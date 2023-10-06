using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.Dtos
{
    public class SubjectDto
    {
        public int Id {get;set;}
        public string Subject {get;set;}
        public IEnumerable<NotesDto> Notes{get;set;}
    }
}