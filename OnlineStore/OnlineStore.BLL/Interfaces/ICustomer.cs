namespace OnlineStore.BLL.Interfaces
{
    public interface ICustomer
    {
        string FirstName { get; }
        string LastName { get; }
        void DisplayInfo();
    }
}