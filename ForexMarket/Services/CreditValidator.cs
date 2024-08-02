using ForexMarket.Models;

namespace ForexMarket.Services {
    public class CreditValidator : ICreditValidator {
        private readonly IEnumerable<IValidationChecker> validations;

        public CreditValidator(IEnumerable<IValidationChecker> validations) {
            this.validations = validations;
        }

        public async Task<(bool, IEnumerable<string>)> PassAllValidations(CreditApplication model) {
            var validateAllCondition = true;
            List<string> errorMessage = new List<string>();
            foreach (var item in validations) {
                if (!item.validatorLogic(model)) {
                    //error
                    validateAllCondition = false;
                    errorMessage.Add(item.ErrorMessage);
                }
            }
            return (validateAllCondition, errorMessage);
        }
    }
}
