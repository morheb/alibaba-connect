using System;
using System.Collections.Generic;

namespace alibaba.Data
{
    public class DbCommentCriteria
    {

        public int TargetId { get; set; }

        public int TargetType { get; set; }
        public int PageSize { get; set; }
        public int PageNumber{ get; set; }
    }


}
