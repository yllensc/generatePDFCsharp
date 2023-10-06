using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using API.Dtos;
using AutoMapper;
using Domain.Entities;

namespace API.Profiles;

public class MappingProfiles : Profile
{

    public MappingProfiles()
    {
        CreateMap<Student, StudentDto>()
            .ReverseMap();
        CreateMap<Notes, NotesDto>()
            .ReverseMap();
            
    }

}
