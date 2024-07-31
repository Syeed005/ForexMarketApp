namespace ForexMarket.Services.LifeTimeServices {
    public class ScopedService {
        private readonly Guid guid;

        public ScopedService() {
            this.guid = Guid.NewGuid();
        }
        public string GetGuid() => guid.ToString();
    }
}
