namespace ForexMarket.Repository.IRepository {
    public interface IUnitOfWork:IDisposable {
        ICreditApplicationRepository CreditApplication { get; }
        void Save();
    }
}
