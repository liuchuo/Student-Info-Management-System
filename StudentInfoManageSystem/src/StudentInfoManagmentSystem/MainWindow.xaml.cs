using StudentInfoManagmentSystem.Model;
using StudentInfoManagmentSystem.Util;
using StudentInfoManagmentSystem.View;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media;

namespace StudentInfoManagmentSystem {
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow {
        public MainWindow() {
            InitializeComponent();
        }

        private void MetroWindow_MouseDown(object sender, MouseButtonEventArgs e) {
            DragMove();
        }

        private void BTNConfirm_Click(object sender, RoutedEventArgs e) {
            TBPrompt.Visibility = Visibility.Hidden;
            TBPrompt.Foreground = Brushes.Red;
            var username = TBName.Text.Trim();
            var pwd = PBPwd.Password;
            if (StringUtil.IsAnyNullOrEmpty(username, pwd)) {
                TBPrompt.Text = "请填写完整信息";
                TBPrompt.Visibility = Visibility.Visible;
                return;
            }
            var collegeName = CBCollege.SelectedItem?.ToString();
            int collgedId = DBHelper.GetCollegeIdByName(collegeName);
            if (!DBHelper.CheckUser(username, pwd, collgedId)) {
                TBPrompt.Text = "用户名或密码错误";
                TBPrompt.Visibility = Visibility.Visible;
                return;
            }
            CurrentUser.SignIn(username, pwd, collgedId);
            new MajorManagementWindow().Show();
            Close();
        }

        private void BTNSignup_Click(object sender, RoutedEventArgs e) {
            TBPrompt.Visibility = Visibility.Hidden;
            TBPrompt.Foreground = Brushes.Red;
            var username = TBName.Text.Trim();
            var pwd = PBPwd.Password;
            if (StringUtil.IsAnyNullOrEmpty(username, pwd)) {
                TBPrompt.Text = "请填写完整信息";
                TBPrompt.Visibility = Visibility.Visible;
                return;
            }
            if (CBCollege.SelectedIndex < 0) {
                TBPrompt.Text = "请选择学院";
                TBPrompt.Visibility = Visibility.Visible;
                return;
            }
            var collegeName = CBCollege.SelectedItem?.ToString();
            int collgedId = DBHelper.GetCollegeIdByName(collegeName);
            if (DBHelper.ExistUser(username, collgedId)) {
                TBPrompt.Text = "已存在该用户";
                TBPrompt.Visibility = Visibility.Visible;
                return;
            }
            DBHelper.AddUser(new SUser {
                Name = username, Pwd = pwd, CollegeId = collgedId
            });
            TBPrompt.Foreground = Brushes.SeaGreen;
            TBPrompt.Text = "注册成功";
            TBPrompt.Visibility = Visibility.Visible;
        }
    }
}
