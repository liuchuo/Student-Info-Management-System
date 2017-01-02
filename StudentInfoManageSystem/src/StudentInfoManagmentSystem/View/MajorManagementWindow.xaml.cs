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
    /// Interaction logic for MajorManagementWindow.xaml
    /// </summary>
    public partial class MajorManagementWindow {

        MajorManagementViewModel _mmvm;

        public MajorManagementWindow() {
            InitializeComponent();
            _mmvm = Grid.DataContext as MajorManagementViewModel;
        }

        private void MetroWindow_MouseDown(object sender, MouseButtonEventArgs e) {
            DragMove();
        }

        private void BTNSignOff_Click(object sender, RoutedEventArgs e) {
            CurrentUser.SignOff();
            new MainWindow().Show();
            Close();
        }

        private void BtnDelete_Click(object sender, RoutedEventArgs e) {
            var ret = MessageBox.Show("确定删除？", "提示", MessageBoxButton.YesNo);
            if (ret == MessageBoxResult.No) return;
            _mmvm.Remove(int.Parse((sender as Button).Tag.ToString()));
        }
        
        private void BtnSelect_Click(object sender, RoutedEventArgs e) {
            int id = int.Parse((sender as Button).Tag.ToString());
            CurrentMajor.ID = id;
            new MajorClassManagementWindow().Show();
            Close();
        }

        private void BtnExport_Click(object sender, RoutedEventArgs e) {
            var sfd = Util.FileDialogFactory.GetSFD("Excel 97-2003|*.xls|Excel 2007|*.xlsx");
            if (!(bool) sfd.ShowDialog()) return;
            var dt = _mmvm.ToDataTable();
            Util.ExcelIO.Write(sfd.FileName, dt);
        }

        private void BtnAdd_Click(object sender, RoutedEventArgs e) {
            var newMajorName = TBNewMajor.Text.Trim();
            if (string.IsNullOrEmpty(newMajorName)) return;
            _mmvm.Add(new Major { Name = newMajorName, CollegeId = CurrentUser.GetCollegeId() });
        }
    }
}
