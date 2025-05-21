using System;
using System.Collections.Generic;

namespace TaskManagerAPI.Domain.Entities
{
    public class Category
    {
        public int Id { get; private set; }
        public string Name { get; private set; }
        public string Description { get; private set; }
        public string Color { get; private set; }
        public int CreatedById { get; private set; }
        public DateTime CreatedAt { get; private set; }
        public ICollection<Task> Tasks { get; private set; } = new List<Task>();

        public Category(string name, string description, string color, int createdById)
        {
            ValidateName(name);

            Name = name;
            Description = description;
            Color = color;
            CreatedById = createdById;
            CreatedAt = DateTime.UtcNow;
        }

        public void Update(string name, string description, string color)
        {
            ValidateName(name);

            Name = name;
            Description = description;
            Color = color;
        }

        private void ValidateName(string name)
        {
            if (string.IsNullOrWhiteSpace(name))
            {
                throw new ArgumentException("Category name cannot be empty", nameof(name));
            }
        }
    }
}
