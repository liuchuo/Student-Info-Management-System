using NPOI.Util;
using StudentInfoManagmentSystem.Model;
using StudentInfoManagmentSystem.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace StudentInfoManagmentSystem.View {
    /// <summary>
    /// Interaction logic for StudentManagementWindow.xaml
    /// </summary>
    public partial class StudentManagementWindow {

        StudentManagementViewModel _smvm;

        public StudentManagementWindow() {
            InitializeComponent();
            _smvm = Grid.DataContext as StudentManagementViewModel;
            for (int i = 12; i < 51; i++) {
                CBAge.Items.Add(i);
            }
        }

        private void Window_MouseDown(object sender, MouseButtonEventArgs e) {
            DragMove();
        }

        private void BtnReSelect_Click(object sender, RoutedEventArgs e) {
            CurrentMajorClass.ID = -1;
            new MajorClassManagementWindow().Show();
            Close();
        }

        private void BtnExport_Click(object sender, RoutedEventArgs e) {
            var sfd = Util.FileDialogFactory.GetSFD("Excel 97-2003|*.xls|Excel 2007|*.xlsx");
            if (!(bool) sfd.ShowDialog()) return;
            var dt = _smvm.ToDataTable();
            Util.ExcelIO.Write(sfd.FileName, dt);
        }

        private void BtnSave_Click(object sender, RoutedEventArgs e) {
            _smvm.Save();
        }

        private void BtnAdd_Click(object sender, RoutedEventArgs e) {
            TBProm.Visibility = Visibility.Hidden;
            TBProm.Foreground = Brushes.Red;
            var id = TBId.Text.Trim();
            var name = TBName.Text.Trim();
            if (Util.StringUtil.IsAnyNullOrEmpty(id, name)) {
                TBProm.Text = "请填写完整信息";
                TBProm.Visibility = Visibility.Visible;
                return;
            }
            if (DBHelper.ExistStudent(id)) {
                TBProm.Text = "已存在相同学号学生";
                TBProm.Visibility = Visibility.Visible;
                return;
            }
            if (CBAge.SelectedIndex < 0) {
                TBProm.Text = "请选择年龄";
                TBProm.Visibility = Visibility.Visible;
                return;
            }
            var age = (int) (CBAge.SelectedItem);
            var sex = (bool) RBMale.IsChecked ? "男" : "女";
            var s = new Student {
                Id = id, Age = age, Name = name, Sex = sex, MajorClassId = CurrentMajorClass.ID
            };
            _smvm.Add(s);
            TBProm.Foreground = Brushes.SeaGreen;
            TBProm.Text = "添加成功";
            TBProm.Visibility = Visibility.Visible;
        }

        private void BtnDelete_Click(object sender, RoutedEventArgs e) {
            _smvm.Remove();
        }

        private void CBAll_Checked(object sender, RoutedEventArgs e) {
            foreach (var item in _smvm.Students) {
                item.IsSelected = true;
            }
        }

        private void CBAll_Unchecked(object sender, RoutedEventArgs e) {
            foreach (var item in _smvm.Students) {
                item.IsSelected = false;
            }
        }
    }
}
