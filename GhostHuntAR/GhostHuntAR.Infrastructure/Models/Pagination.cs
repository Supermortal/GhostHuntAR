namespace GhostHuntAR.Infrastructure.Models
{
    public class Pagination
    {
        public string Action { get; set; }
        public string Controller { get; set; }
        public int TotalPostsCount { get; set; }
        public int CurrentPage { get; set; }
        public int PageSize { get; set; }
        public string Type { get; set; }
    }
}