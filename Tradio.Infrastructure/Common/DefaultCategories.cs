using Tradio.Domain;

namespace Tradio.Infrastructure.Common
{
    public class DefaultCategories
    {
        public static readonly Category[] Categories = new Category[]
        {
            new Category { Id = 1, Name = "Education" },
            new Category { Id = 2, Name = "Tutoring", ParentId = 1 },
            new Category { Id = 3, Name = "Music Lessons", ParentId = 1 },
            new Category { Id = 4, Name = "Language Lessons", ParentId = 1 },
            new Category { Id = 5, Name = "Coding & Tech", ParentId = 1 },

            new Category { Id = 6, Name = "Home & Repair" },
            new Category { Id = 7, Name = "Plumbing", ParentId = 6 },
            new Category { Id = 8, Name = "Electrical Work", ParentId = 6 },
            new Category { Id = 9, Name = "Furniture Assembly", ParentId = 6 },
            new Category { Id = 10, Name = "Painting & Decorating", ParentId = 6 },

            new Category { Id = 11, Name = "Online Services" },
            new Category { Id = 12, Name = "Graphic Design", ParentId = 11 },
            new Category { Id = 13, Name = "Writing & Translation", ParentId = 11 },
            new Category { Id = 14, Name = "Web Development", ParentId = 11 },
            new Category { Id = 15, Name = "Digital Marketing", ParentId = 11 },

            new Category { Id = 16, Name = "Entertainment" },
            new Category { Id = 17, Name = "Gaming Help", ParentId = 16 },
            new Category { Id = 18, Name = "Event Hosting", ParentId = 16 },
            new Category { Id = 19, Name = "Music Performance", ParentId = 16 },

            new Category { Id = 20, Name = "Health & Wellness" },
            new Category { Id = 21, Name = "Fitness Training", ParentId = 20 },
            new Category { Id = 22, Name = "Nutrition Advice", ParentId = 20 },
            new Category { Id = 23, Name = "Mental Health Support", ParentId = 20 },

            new Category { Id = 24, Name = "Transport & Delivery" },
            new Category { Id = 25, Name = "Courier Services", ParentId = 24 },
            new Category { Id = 26, Name = "Ride Sharing", ParentId = 24 },

            new Category { Id = 27, Name = "Pet Services" },
            new Category { Id = 28, Name = "Pet Sitting", ParentId = 27 },
            new Category { Id = 29, Name = "Dog Walking", ParentId = 27 },

            new Category { Id = 30, Name = "Other Services" },
            new Category { Id = 31, Name = "Consulting", ParentId = 30 },
            new Category { Id = 32, Name = "Miscellaneous Help", ParentId = 30 },
        };
    }
}
