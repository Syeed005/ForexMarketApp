using ForexMarket.Models;

namespace ForexMarket.Repository.IRepository {
    public interface ICreditApplicationRepository :IRepository<CreditApplication> {
        void Update(CreditApplication obj);
    }
}
