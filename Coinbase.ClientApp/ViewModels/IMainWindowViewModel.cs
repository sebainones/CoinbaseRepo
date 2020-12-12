using System.ComponentModel;

namespace Coinbase.ClientApp.ViewModels
{
    public interface IMainWindowViewModel : INotifyPropertyChanged
    {
        string Eur { get; set; }
    }
}