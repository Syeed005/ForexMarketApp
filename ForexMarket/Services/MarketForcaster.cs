using ForexMarket.Models;

namespace ForexMarket.Services {
    public class MarketForcaster {
        public MarketResult GetMarketPrediction() {
            return new MarketResult { MarketCondition = MarketCondition.StableUp };
        }
    }
}
