namespace DTO.Models.DataResponse
{
    public class DataResponse
    {
        public string Message { get; set; }
        public string? Description { get; set; }
        public bool IsSucceeded { get; set; }
        public DataResponse(string message, bool isSucceeded, string? description = null)
        {
            Message = message;
            Description = description;
            IsSucceeded = isSucceeded;
        }
    }
}
