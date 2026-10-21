using System;
using System.ComponentModel.DataAnnotations;

namespace ArtifyAI.Models
{
    public class AIImage
    {
        public int Id { get; set; }

        [Required]
        public string Prompt { get; set; } = string.Empty;

        [Required]
        [Display(Name = "Image Generator")]
        public string ImageGenerator { get; set; } = string.Empty;

        [Display(Name = "Upload Date")]
        public DateTime UploadDate { get; set; } = DateTime.Now;

        [Display(Name = "Filename")]
        public string Filename { get; set; } = string.Empty;

        [Display(Name = "Likes")]
        public int Likes { get; set; } = 0;

        [Display(Name = "Can Increase Like")]
        public bool canIncreaseLike { get; set; } = true;
    }
}