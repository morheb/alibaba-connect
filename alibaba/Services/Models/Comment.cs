using System;

namespace alibaba.Services.Models
{
    public class Comment
    {
        public int Id { get; set; }
        public int Type { get; set; }
        public string Content { get; set; }
        public int TargetId { get; set; }
        public string TargetName { get; set; }
        public string UserName { get; set; }
        public DateTime Date { get; set; }
    }
}
