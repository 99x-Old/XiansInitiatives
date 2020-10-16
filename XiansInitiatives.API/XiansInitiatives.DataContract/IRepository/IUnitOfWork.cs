using System;
using System.Threading.Tasks;
using XiansInitiatives.DataContract.IRepository;

namespace XiansInitiatives.DataContracts.IRepository
{
    public interface IUnitOfWork : IDisposable
    {
        IUserRepository User { get; }
        IInitiativeYearRepository InitiativeYear { get; }
        IInitiativeRepository Initiative { get; }
        IInitiativeMemberRepository InitiativeMember { get; }
        IActionItemRepository ActionItem { get; }
        IItemAssigneeRepository ItemAssignee { get; }
        ICommentItemRepository CommentItem { get; }
        IReviewCycleRepository ReviewCycle { get; }
        IEvaluationCriteriaRepository EvaluationCriteria { get; }
        IInitiativeMeetingRepository InitiativeMeeting { get; }

        Task<bool> Save();
    }
}