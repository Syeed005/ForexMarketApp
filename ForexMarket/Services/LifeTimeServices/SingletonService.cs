namespace ForexMarket.Services.LifeTimeServices {
    public class SingletonService {
        private readonly Guid guid;

        public SingletonService() {
            this.guid = Guid.NewGuid();
        }
        public string GetGuid() => guid.ToString();
    }
}
