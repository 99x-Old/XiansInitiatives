using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using XiansInitiatives.DataContract.IRepository;
using XiansInitiatives.Shared.Dtos;
using XiansInitiatives.Shared.Models;
using Microsoft.EntityFrameworkCore;

namespace XiansInitiatives.Data.Repository
{
    public class CommentItemRepository : Repository<CommentItem>, ICommentItemRepository
    {
        private readonly IdentityDataContext _db;

        public CommentItemRepository(IdentityDataContext db) : base(db)
        {
            _db = db;
        }

        public async Task<bool> AddComment(CommentItem commentItem)
        {
            await _db.CommentItem.AddAsync(commentItem);
            return _db.SaveChanges() > 0;
        }

        public Task<List<CommentItemDto>> GetComments(string itemId)
        {
            var result = from commentItem in _db.CommentItem
                         join user in _db.ApplicationUser
                         on commentItem.CommenterId equals user.Id
                         where commentItem.ItemId.ToString() == itemId

                         select new CommentItemDto
                         {
                             ItemId = commentItem.ItemId.ToString(),
                             CommenterId = commentItem.CommenterId,
                             Comment = commentItem.Comment,
                             CommenterName = user.FullName,
                             CreatedAt = commentItem.CreatedAt,
                             ProfileImageUrl = user.ProfileImageUrl
                         };
            return result.OrderBy(x => x.CreatedAt).ToListAsync();
        }
    }
}