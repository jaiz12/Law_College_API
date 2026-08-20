namespace DTO.Models.Student_Life
{
    public class LibraryDTO
    {
        public int Id { get; set; }

        public string? Title { get; set; }

        public string? ExternalLink { get; set; } = null;

        public DateTime? CreatedOn { get; set; }

        public string? CreatedBy { get; set; }

        public DateTime? UpdatedOn { get; set; }

        public string? UpdatedBy { get; set; }
    }
}
