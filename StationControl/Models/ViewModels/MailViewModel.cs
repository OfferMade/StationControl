using StationControl.Data;
using StationControl.Models.Entities;
using System.ComponentModel.DataAnnotations;

namespace StationControl.Models.ViewModels
{
    public class MailViewModel
    {
       [Required(ErrorMessage = "Lütfen mail girin.")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Lütfen başlık girin.")]
        public string Title { get; set; }

        [Required(ErrorMessage = "Lütfen mesaj girin.")]
        public string Message { get; set; }

        public int DeputyId { get; set; } 
    }
}
