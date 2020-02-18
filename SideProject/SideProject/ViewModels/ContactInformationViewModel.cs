using SideProject.Common;
using SideProject.ViewModels.Common;

namespace SideProject.ViewModels
{
    public class ContactInformationViewModel : ViewModelBase
    {
        public ContactInformationViewModel()
        {
            name = "Prueba";
        }

        private string name;
        public string Name
        {
            get { return name; }
            set
            {
                name = value;
                NotifyPropertyChanged();
            }
        }

        private int counter;
        public int Counter
        {
            get { return counter; }
            set
            {
                counter = value;
                NotifyPropertyChanged();
            }
        }

        private string nameInput;
        public string NameInput
        {

            get { return nameInput; }
            set
            {
                nameInput = value;
                NotifyPropertyChanged();
                Name = value;
            }
        }

        #region Commands

        public RelayCommand IncreaseCounterCommand
        {
            get
            {
                return new RelayCommand(x =>
            {
                Counter++;
            });
            }
        }

        #endregion
    }
}
