using AutoMapper;
using MovieLibrary.Business.ViewModels;
using MovieLibrary.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MovieLibrary.Business.Mapping
{
    public class MappingProfile : Profile
    {
        public MappingProfile() 
        {
            CreateMap<User, UserViewModel>().ReverseMap();
            CreateMap<Occupation, OccupationViewModel>().ReverseMap();
            CreateMap<Movie, MovieViewModel>().ReverseMap();
            CreateMap<Genre, GenreViewModel>().ReverseMap();
            CreateMap<Director, DirectorViewModel>().ReverseMap();
        }
    }
}
