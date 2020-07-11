using System;
using System.ComponentModel.DataAnnotations;
using BookShelfAPI.Dtos;

namespace BookShelfAPI.Models
{
    public class Book
    {
        public int Id {get; set;}
        [Required]
        public string Title {get; set;}
        public string Capa {get; set;}
        [Required]
        public string Arquivo {get; set;}

        public static explicit operator Book(CreateBookDto v)
        {
            throw new NotImplementedException();
        }
    }
}