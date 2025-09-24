using Microsoft.EntityFrameworkCore;

namespace Cinema.Models
{
    [PrimaryKey(nameof(ApplicationUserId),nameof(MovieId))]
    public class Ticket
    {
        public string ApplicationUserId { get; set; }
        public ApplicationUser ApplicationUser { get; set; }

        public int MovieId { get; set; }
        public  Movies Movie { get; set; }

        public  int Count { get; set; }
    }
}
