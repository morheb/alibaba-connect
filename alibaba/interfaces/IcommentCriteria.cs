using System.Collections.Generic;
using System.Threading.Tasks;
using alibaba.Data;

namespace alibaba.interfaces
{
    public interface ICommentRepo
    {
        Task<bool> PostCommentAsync(DbComment comment);
        Task<IEnumerable<DbComment>> FilterComments(DbCommentCriteria criteria);
        Task<string> DeleteComment(int id);

    }
}
