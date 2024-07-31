namespace ForexMarket.Services.LifeTimeServices {
    public class TransientService {
        private readonly Guid guid;

        public TransientService() {
            this.guid = Guid.NewGuid();
        }
        public string GetGuid() => guid.ToString();
    }
}
