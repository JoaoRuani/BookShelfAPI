using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Http;

namespace BookShelfAPI.Dtos
{
    public class UpdateBookDto
    {
        [Required]
        public string Title {get; set;}
    }
}