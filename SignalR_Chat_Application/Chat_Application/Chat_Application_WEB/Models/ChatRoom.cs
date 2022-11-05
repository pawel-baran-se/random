using System.ComponentModel.DataAnnotations;

namespace Chat_Application_WEB.Models
{
    public class ChatRoom
    {
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }
    }
}
