using ForexMarket.Models;

namespace ForexMarket.Services {
    public class AddressValidationChecker : IValidationChecker {
        public string ErrorMessage => "Address is valid";

        public bool validatorLogic(CreditApplication model) {
            if (model.PostalCode <= 0 || model.PostalCode >= 99999) {
                return false;
            }
            return true;
        }
    }
}
