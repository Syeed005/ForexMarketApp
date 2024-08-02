using ForexMarket.Models;

namespace ForexMarket.Services {
    public class CreditApprovedHigh : ICreditApproved {
        public double GetCreditApproved(CreditApplication creditApplication) {
            return creditApplication.Salary * .5;
        }
    }
}
