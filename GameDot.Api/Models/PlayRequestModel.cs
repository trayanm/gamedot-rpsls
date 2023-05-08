using System.ComponentModel.DataAnnotations;

namespace GameDot.Api.Models
{
    public class PlayRequestModel
    {
        [Required]
        public int Player { get; set; }
    }
}
