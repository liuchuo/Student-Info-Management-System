using StudentInfoManagmentSystem.Model;
using System.Collections.ObjectModel;
using System.ComponentModel;

namespace StudentInfoManagmentSystem {
    class MainWindowViewModel : INotifyPropertyChanged {

        private ObservableCollection<string> _colleges;
        public ObservableCollection<string> Colleges {
            get { return _colleges; }
            set {
                _colleges = value;
                onPropertyChanged("Colleges");
            }
        }

        public MainWindowViewModel() {
            Colleges = new ObservableCollection<string>();
            var list = DBHelper.GetAllColleges();
            foreach (var item in list) {
                Colleges.Add(item.Name);
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        private void onPropertyChanged(string propName) {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propName));
        }
    }
}
