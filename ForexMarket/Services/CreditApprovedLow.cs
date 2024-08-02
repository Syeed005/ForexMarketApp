using ForexMarket.Models;

namespace ForexMarket.Services {
    public class CreditApprovedLow : ICreditApproved {
        public double GetCreditApproved(CreditApplication creditApplication) {
            return creditApplication.Salary * .3;
        }
    }
}
