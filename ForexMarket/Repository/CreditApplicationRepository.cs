using ForexMarket.Data;
using ForexMarket.Models;
using ForexMarket.Repository.IRepository;

namespace ForexMarket.Repository {
    internal class CreditApplicationRepository : Repository<CreditApplication>, ICreditApplicationRepository {
        private readonly ApplicationDbContext db;

        public CreditApplicationRepository(ApplicationDbContext db) : base(db) {
            this.db = db;
        }

        public void Update(CreditApplication obj) {
            db.CreateApplicationModel.Update(obj);
        }
    }
}