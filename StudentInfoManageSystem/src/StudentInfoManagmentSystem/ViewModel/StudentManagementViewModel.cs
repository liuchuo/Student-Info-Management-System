using StudentInfoManagmentSystem.Model;
using System; 
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudentInfoManagmentSystem.ViewModel {
    class StudentManagementViewModel : INotifyPropertyChanged {

        private int _count;
        private ObservableCollection<StudentViewModel> _students;

        public int Count {
            get { return _count; }
            set {
                _count = value;
                onPropertyChange("Count");
            }
        }

        public ObservableCollection<StudentViewModel> Students {
            get { return _students; }
            set {
                _students = value;
                onPropertyChange("Students");
                updateCount();
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        private void onPropertyChange(string propName) {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propName));
        }

        public StudentManagementViewModel() {
            Students = new ObservableCollection<StudentViewModel>();
            var list = DBHelper.GetAllStudentsInMajorClass(CurrentMajorClass.ID);
            foreach (var item in list) {
                Students.Add(new StudentViewModel(item));
            }
            updateCount();
        }

        public void Remove() {
            for (int i = Students.Count - 1; i >= 0; i--) {
                if (Students[i].IsSelected) {
                    DBHelper.DeleteStudent(Students[i].Student.Id);
                    Students.RemoveAt(i);
                }
            }
            updateCount();
        }

        public void Add(Student s) {
            Students.Add(new StudentViewModel(s));
            updateCount();
            DBHelper.AddStudent(s);
        }

        public void Save() {
            foreach (var item in Students) {
                DBHelper.UpdateStudent(item.Student.Id, item.Student);
            }
        }

        public DataTable ToDataTable() {
            var dt = new DataTable("学生信息");
            dt.Columns.Add("学号");
            dt.Columns.Add("姓名");
            dt.Columns.Add("性别");
            dt.Columns.Add("年龄");
            foreach (var item in Students) {
                dt.Rows.Add(dt.NewRow());
                dt.Rows[dt.Rows.Count - 1].ItemArray = item.ToInfoArray();
            }
            return dt;
        }

        private void updateCount() {
            Count = Students.Count;
        }
    }
}
