using Microsoft.AspNetCore.Identity;

namespace MegaHockey.Models
{
    public class HistoryRecord
    {
        public int Id { get; set; }
        public string First { get; set; }
        public string Second { get; set; }
        public string UserId { get; set; }

        public string GameResult { get; set; }

    }
}
