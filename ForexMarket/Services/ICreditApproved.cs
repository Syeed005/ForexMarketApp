using ForexMarket.Models;

namespace ForexMarket.Services {
    public interface ICreditApproved {
        double GetCreditApproved(CreditApplication creditApplication);
    }
}
