using System;

namespace RockContent.Domain.Dtos
{
    public class ArticleToShowDto
    {
        public Guid Id { get; set; }
        public string Title { get; set; }
        public string Text { get; set; }
        public string Author { get; set; }
        public DateTime DatePublished { get; set; }
        public int CountLikes { get; set; }
        public bool UserMarkLiked { get; set; }
    }
}