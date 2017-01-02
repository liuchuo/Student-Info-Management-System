using StudentInfoManagmentSystem.Model;
using StudentInfoManagmentSystem.View;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace StudentInfoManagmentSystem.ViewModel {
    class MajorManagementViewModel : INotifyPropertyChanged {

        private int _count;
        private ObservableCollection<Major> _majors;

        public int Count {
            get { return _count; }
            set {
                _count = value;
                onPropertyChange("Count");
            }
        }
        public ObservableCollection<Major> Majors {
            get { return _majors; }
            set {
                _majors = value;
                onPropertyChange("Majors");
                Count = _majors.Count;
            }
        }

        public MajorManagementViewModel() {
            Majors = new ObservableCollection<Major>();
            var list = DBHelper.GetAllMajorsInCollege(CurrentUser.GetCollegeId());
            foreach (var item in list) {
                Majors.Add(item);
            }
            updateCount();
        }
        
        public event PropertyChangedEventHandler PropertyChanged;

        private void onPropertyChange(string propName) {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propName));
        }

        public void Add(Major major) {
            Majors.Add(major);
            updateCount();
            DBHelper.AddMajor(major);
        }

        public void Remove(int id) {
            foreach (var item in Majors) {
                if (item.Id == id) {
                    Majors.Remove(item); break;
                }
            }
            DBHelper.DeleteMajor(id);
            updateCount();
        }

        public DataTable ToDataTable() {
            var dt = new DataTable("专业");
            dt.Columns.Add("专业名");
            foreach (var item in Majors) {
                dt.Rows.Add(dt.NewRow());
                dt.Rows[dt.Rows.Count - 1][0] = item.Name;
            }
            return dt;
        }
        
        private void updateCount() {
            Count = Majors.Count;
        }
    }
}
