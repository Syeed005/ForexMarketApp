using ForexMarket.Models;

namespace ForexMarket.Services {
    public interface ICreditValidator {
        Task<(bool, IEnumerable<string>)> PassAllValidations(CreditApplication model);
    }
}
