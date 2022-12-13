using System;
using System.Collections.Generic;

namespace alibaba.Data
{
    public class DbComment
    {
        public int Id { get; set; }
        public int Type { get; set; }
        public string TargetId { get; set; }
        public string TargetName { get; set; }

        public string Content { get; set; }
        public string UserName { get; set; }
        public DateTime Date { get; set; }

    }
}
