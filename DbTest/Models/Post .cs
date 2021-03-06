﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DbTest.Models
{
    public class Post
    {
        public int Id { get; set; }
        public int BlogId { get; set; }
        public string Title { get; set; }
        public string Body { get; set; }
        public Blog Blog { get; set; }
        public ICollection<PostCategory> PostCategory { get; set; } = new List<PostCategory>();
    }
}
