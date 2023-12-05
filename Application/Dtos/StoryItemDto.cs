namespace Application.Dtos
{
    public record StoryItemDto
    {
        public int id {get; set;}
        public string url { get; set; }
        public string title { get; set; }
    }
}
