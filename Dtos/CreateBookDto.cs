using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Http;

namespace BookShelfAPI.Dtos
{
    public class CreateBookDto
    {
        [Required]
        public string Title {get; set;}
        [Required]
        [DataType(DataType.Upload)]
        public IFormFile File {get; set;}
    }
}