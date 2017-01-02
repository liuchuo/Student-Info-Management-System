using StudentInfoManagmentSystem.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudentInfoManagmentSystem.ViewModel {
    class StudentViewModel : INotifyPropertyChanged {

        private bool _isSelected;
        private Student _student;

        public bool IsSelected {
            get { return _isSelected; }
            set {
                _isSelected = value;
                onPropertyChange("IsSelected");
            }
        }

        public Student Student {
            get { return _student; }
            set {
                _student = value;
                onPropertyChange("Student");
            }
        }

        public StudentViewModel() { }

        public StudentViewModel(Student s) {
            Student = s;
        }

        public event PropertyChangedEventHandler PropertyChanged;

        private void onPropertyChange(string propName) {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propName));
        }

        public string[] ToInfoArray() => new string[] {
            Student.Id, Student.Name, Student.Sex, Student.Age.ToString() };
            
    }
}
