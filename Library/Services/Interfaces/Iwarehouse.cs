using Library.Services.Delegates;

namespace Library.Services.Interfaces
{
    public interface Iwarehouse
    {
        event ProductRemoveEventHandler ProductRemoved;

        string Manager { get; set; }

        bool HasInventory(string productName, int quantity);
        void Remove(string productName, int quantity);
    }
}
