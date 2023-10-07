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
        CreateMap<Subject, SubjectDto>()
            .ReverseMap();
        CreateMap<Notes, NotesDto>()
        .ForMember(des => des.NameSubject, org => org.MapFrom(org => org.Subject.NameSubject))
            .ReverseMap();
    }

}
