using ForexMarket.Models;

namespace ForexMarket.Services {
    public interface IValidationChecker {
        bool validatorLogic(CreditApplication model);
        string ErrorMessage { get; }
    }
}
