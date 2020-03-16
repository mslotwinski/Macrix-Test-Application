using System.ComponentModel;

namespace ViewModels
{
    /* 
     * It is a base view model class. 
     * By adding PropertyChanged.Fody nuget it is no longer necessary to rise PropertyChanged event individually in each property setter.
     */

    public abstract class BaseViewModel : INotifyPropertyChanged
    {        
        /// <summary>
        /// Default property changed event handler
        /// </summary>
        public event PropertyChangedEventHandler PropertyChanged = (sender, e) => { };
    }
}
