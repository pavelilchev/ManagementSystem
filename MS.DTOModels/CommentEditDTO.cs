namespace MS.DTOModels
{
    public class CommentEditDTO
    {
        public int Id { get; set; }

        public string Content { get; set; }

        public string[] Types { get; set; }

        public string Type { get; set; }

        public string Date { get; set; }
    }
}
