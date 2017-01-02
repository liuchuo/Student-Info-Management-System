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
    class MajorClassManagementViewModel : INotifyPropertyChanged {
        
        private int _count;
        private ObservableCollection<MajorClass> _majorclasses;

        public int Count {
            get { return _count; }
            set {
                _count = value;
                onPropertyChange("Count");
            }
        }
        public ObservableCollection<MajorClass> MajorClasses {
            get { return _majorclasses; }
            set {
                _majorclasses = value;
                onPropertyChange("MajorClasses");
                Count = _majorclasses.Count;
            }
        }

        public MajorClassManagementViewModel() {
            MajorClasses = new ObservableCollection<MajorClass>();
            var list = DBHelper.GetAllClassesInMajor(CurrentMajor.ID);
            foreach (var item in list) {
                MajorClasses.Add(item);
            }
            updateCount();
        }

        public event PropertyChangedEventHandler PropertyChanged;

        private void onPropertyChange(string propName) {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propName));
        }

        private void updateCount() {
            Count = MajorClasses.Count;
        }

        public DataTable ToDataTable() {
            var dt = new DataTable("班级");
            dt.Columns.Add("班级名");
            foreach (var item in MajorClasses) {
                dt.Rows.Add(dt.NewRow());
                dt.Rows[dt.Rows.Count - 1][0] = item.Name;
            }
            return dt;
        }

        public void Add(MajorClass mClass) {
            MajorClasses.Add(mClass);
            updateCount();
            DBHelper.AddMajorClass(mClass);
        }

        public void Remove(int id) {
            foreach (var item in MajorClasses) {
                if (item.Id == id) {
                    MajorClasses.Remove(item); break;
                }
            }
            DBHelper.DeleteMajorClass(id);
            updateCount();
        }
    }
}
