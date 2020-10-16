using System.Collections.Generic;
using System.Threading.Tasks;
using XiansInitiatives.Shared.Models;

namespace XiansInitiatives.DataContract.IRepository
{
    public interface IReviewCycleRepository
    {
        Task<List<ReviewCycle>> GetReviewCycles(string initativeId);

        Task<List<ReviewCycle>> GetUpComingReviewCycles(string initativeId);

        Task<bool> CreateReviewCycle(ReviewCycle reviewCycle);

        Task<bool> RemoveReviewCycle(string id);
    }
}