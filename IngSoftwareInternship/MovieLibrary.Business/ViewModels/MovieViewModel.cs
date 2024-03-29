﻿using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MovieLibrary.Business.ViewModels
{
    public class MovieViewModel
    {

        public MovieViewModel()
        {
            Directors = new HashSet<DirectorViewModel>();
            Genres = new HashSet<GenreViewModel>();
            Users = new HashSet<UserViewModel>();
            Rates = new HashSet<RateViewModel>();
        }
        public int MovieId { get; set; }
        [Required(ErrorMessage = "Caption is required!")]
        [StringLength(50, MinimumLength = 4)]
        public string Caption { get; set; } = null!;
        [Display(Name = "Release Year")]
        [Range(1960, 2022)]
        [Required(ErrorMessage = "Release Year is required!")]
        public int ReleaseYear { get; set; }
        [Display(Name = "Submitted By")]
        [Required(ErrorMessage = "Submitted By is required!")]
        [StringLength(100, MinimumLength = 4)]
        public string SubmittedBy { get; set; } = null!;
        public int[] SelectedDirectors { get; set; } = null!;
        public int[] SelectedGenres { get; set; } = null!;
        public List<GenreViewModel>? GenreViewModels { get; set; } = null!;
        public List<DirectorViewModel>? DirectorViewModels { get; set; } = null!;
        public virtual ICollection<DirectorViewModel> Directors { get; set; }
        public virtual ICollection<GenreViewModel> Genres { get; set; }
        public virtual ICollection<UserViewModel> Users { get; set; }
        public virtual ICollection<RateViewModel> Rates { get; set; }
        public string? ImagePath { get; set; }= null!;
        [NotMapped]
        [Required]
        public IFormFile? Image { get; set; }

        public int? Rate { get; set; }
        public decimal AvgRate { get; set; }
        public string DirectorCsv { get
            {
                return string.Join(",", Directors.Select(x=> string.Join(" ",x.FirstName,x.LastName)));
            } }
        public string GenresCsv
        {
            get
            {
                return string.Join(",", Genres.Select(x => x.Caption));
            }
        }
        public string UsersCsv
        {
            get
            {
                return string.Join(",", Users.Select(x => x.Address));
            }
        }
    }
}
