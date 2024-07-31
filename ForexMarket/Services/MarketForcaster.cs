using ForexMarket.Models;

namespace ForexMarket.Services {
    public class MarketForcaster : IMarketForcaster {
        public MarketResult GetMarketPrediction() {
            return new MarketResult { MarketCondition = MarketCondition.StableUp };
        }
    }
}
