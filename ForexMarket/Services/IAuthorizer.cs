namespace ForexMarket.Services
{
    public interface IAuthorizer
    {
        bool IsAuthorize();
        bool IsAdmin();
    }
}
