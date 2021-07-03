using System;
using System.Collections.Generic;
using RockContent.Shared.Entities;

namespace RockContent.Domain.Entities
{
    public class Article : Entity, IAggregateRoot
    {
        protected Article() { }

        public Article(string title, string text, string author, DateTime datePublished)
        {
            Title = title;
            Text = text;
            Author = author;
            DatePublished = datePublished;
        }

        public string Title { get; private set; }
        public string Text { get; private set; }
        public string Author { get; private set; }
        public DateTime DatePublished { get; private set; }

        public ICollection<Like> Likes { get; private set; } = new List<Like>();
    }
}