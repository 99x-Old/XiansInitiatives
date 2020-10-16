using System.Collections.Generic;
using System.Threading.Tasks;
using XiansInitiatives.Shared.Dtos;
using XiansInitiatives.Shared.Models;

namespace XiansInitiatives.DataContract.IRepository
{
    public interface ICommentItemRepository
    {
        Task<bool> AddComment(CommentItem commentItem);

        Task<List<CommentItemDto>> GetComments(string itemId);
    }
}