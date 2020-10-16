using System.Threading.Tasks;
using XiansInitiatives.Data.Repository;
using XiansInitiatives.DataContract.IRepository;
using XiansInitiatives.DataContracts.IRepository;

namespace XiansInitiatives.Data.DataContracts.Repository
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly IdentityDataContext _db;
        public IUserRepository User { get; private set; }
        public IInitiativeYearRepository InitiativeYear { get; private set; }
        public IInitiativeRepository Initiative { get; private set; }
        public IInitiativeMemberRepository InitiativeMember { get; private set; }
        public IActionItemRepository ActionItem { get; private set; }
        public IItemAssigneeRepository ItemAssignee { get; private set; }
        public ICommentItemRepository CommentItem { get; private set; }
        public IReviewCycleRepository ReviewCycle { get; private set; }
        public IEvaluationCriteriaRepository EvaluationCriteria { get; private set; }
        public IInitiativeMeetingRepository InitiativeMeeting { get; private set; }

        public UnitOfWork(IdentityDataContext db)
        {
            _db = db;
            User = new UserRepository(_db);
            InitiativeYear = new InitiativeYearRepository(_db);
            Initiative = new InitiativeRepository(_db);
            InitiativeMember = new InitiativeMemberRepository(_db);
            ActionItem = new ActionItemRepository(_db);
            ItemAssignee = new ItemAssigneeRepository(_db);
            ReviewCycle = new ReviewCycleRepository(_db);
            CommentItem = new CommentItemRepository(_db);
            EvaluationCriteria = new EvaluationCriteriaRepository(_db);
            InitiativeMeeting = new InitiativeMeetingRepository(_db);
        }

        public void Dispose()
        {
            _db.Dispose();
        }

        public async Task<bool> Save()
        {
            return await _db.SaveChangesAsync() > 0;
        }
    }
}