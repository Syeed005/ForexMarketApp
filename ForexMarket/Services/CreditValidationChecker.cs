using ForexMarket.Models;

namespace ForexMarket.Services {
    public class CreditValidationChecker : IValidationChecker {
        public string ErrorMessage => "Age/Salary/Credit requirement does not meet";

        public bool validatorLogic(CreditApplication model) {
            if (DateTime.Now.AddYears(-18) < model.DOB) {
                return false;
            }
            if (model.Salary < 10000) {
                return false;
            }
            return true;
        }
    }
}
