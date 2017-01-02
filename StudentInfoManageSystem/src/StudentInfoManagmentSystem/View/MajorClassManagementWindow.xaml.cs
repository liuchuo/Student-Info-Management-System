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
    /// Interaction logic for MajorClassMagementWindow.xaml
    /// </summary>
    public partial class MajorClassManagementWindow {

        MajorClassManagementViewModel _mcmvm;

        public MajorClassManagementWindow() {
            InitializeComponent();
            _mcmvm = Grid.DataContext as MajorClassManagementViewModel;
        }

        private void MetroWindow_MouseDown(object sender, MouseButtonEventArgs e) {
            DragMove();
        }

        private void BtnReSelect_Click(object sender, RoutedEventArgs e) {
            CurrentMajor.ID = -1;
            new MajorManagementWindow().Show();
            Close();
        }

        private void BtnDelete_Click(object sender, RoutedEventArgs e) {
            var ret = MessageBox.Show("确定删除？", "提示", MessageBoxButton.YesNo);
            if (ret == MessageBoxResult.No) return;
            _mcmvm.Remove(int.Parse((sender as Button).Tag.ToString()));
        }

        private void BtnSelect_Click(object sender, RoutedEventArgs e) {
            int id = int.Parse((sender as Button).Tag.ToString());
            CurrentMajorClass.ID = id;
            new StudentManagementWindow().Show();
            Close();
        }

        private void BtnAdd_Click(object sender, RoutedEventArgs e) {
            var newMajorClassName = TBNewMajorClass.Text.Trim();
            if (string.IsNullOrEmpty(newMajorClassName)) return;
            _mcmvm.Add(new MajorClass { Name = newMajorClassName, MajorId = CurrentMajor.ID });
        }

        private void BtnExport_Click(object sender, RoutedEventArgs e) {
            var sfd = Util.FileDialogFactory.GetSFD("Excel 97-2003|*.xls|Excel 2007|*.xlsx");
            if (!(bool) sfd.ShowDialog()) return;
            var dt = _mcmvm.ToDataTable();
            Util.ExcelIO.Write(sfd.FileName, dt);
        }
    }
}
