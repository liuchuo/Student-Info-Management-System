   M S B A M L   `   `   `  ����U  QStudentInfoManagmentSystem, Version=1.0.0.0, Culture=neutral, PublicKeyToken=nullG(clr-namespace:StudentInfoManagmentSystemStudentInfoManagmentSystem  5      T PMahApps.Metro, Version=1.3.0.0, Culture=neutral, PublicKeyToken=f4fb5a3c4d1e5b4fV;clr-namespace:MahApps.Metro.Controls;assembly=MahApps.MetroMahApps.Metro.Controls 5      +    %StudentInfoManagmentSystem.MainWindow   5      \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5      R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      @d2http://schemas.microsoft.com/expression/blend/2008     5      Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5      :local(clr-namespace:StudentInfoManagmentSystem     5      PControls;clr-namespace:MahApps.Metro.Controls;assembly=MahApps.Metro     5      U QMaterialDesignThemes.Wpf, Version=2.2.1.750, Culture=neutral, PublicKeyToken=nullW SMaterialDesignThemes.MahApps, Version=0.0.7.0, Culture=neutral, PublicKeyToken=null<md1http://materialdesigninxaml.net/winfx/xaml/themes   5      -   5           MaterialDesignBody#��[  5
      $��Microsoft YaHei)�5        MaterialDesignPaper#��[ 5        HighlightBrush(  "MahApps.Metro.Controls.MetroWindow    	GlowBrush#  [ 5       � 
ResizeMode$ CanMinimize=�6C    � Title$ 学生信息管理系统��5      $	��450��6(   $	��500��65    � WindowStartupLocation$ CenterScreen=�6A   .��5      �� $��16q�6   $��Center=�6   $��Center=�68   .+�5      #  MaterialDesignThemes.Wpf.Card  $��32q�6   $	��400��6   +  %MaterialDesignThemes.Wpf.ShadowAssist   ShadowDepth$ Depth3=�6'   .��5      �  � RowDefinitions 5   
   �� 5      �� 5      �� 5      �� 5      �� 5      �� 5       � ColumnDefinitions 5   
   �� 5      $��1* �6   �� 5      $��5* �6    � 	Resources 5   
   '  !MaterialDesignThemes.Wpf.PackIcon �� 
TargetType	 �� Property
 �� Value��%�   '        �� 5       "  6   .�5!      �� 	 � ��$
 28��6&   �� 5"      	 � ��$
 28��6'   �� 5#      	 � ��$
 Center=�64   5$      5%      .+�5'   
   �� $��2��6    �� TextAlignment$ Center=�6(   $��用户登录��6?    # MaterialDesignHeadlineTextBlock#��[ 5(      $��0 0 0 32q�6M     5)   
   �  �ControlzEx.PackIconBase`1[[MaterialDesignThemes.Wpf.PackIconKind, MaterialDesignThemes.Wpf, Version=2.2.1.750, Culture=neutral, PublicKeyToken=null]]   Kind$ Account=�6   $��1��6%   �� 5*   
   -   5         ��Name$ TBName��5+   3   $��1��5*      $��1��6   )  #MaterialDesignThemes.Wpf.HintAssist   Hint$ 	用户名��6/   $��14(�6H    �� 	MaxLength$ 20��5+      $��0 0 16 0q�6!   $��test1��6C     5-   
   $	 Key=�6   $��2��6!   $��0 16 0 0q�6.   G� 5.   
   -   5         G�Name$ PBPwd��5/   F    G� PasswordChar$ *��5.      $��2��6'   $��1��64   $ 密码��6D   $��14(�5/      $��	0 16 16 0q�6$    G� 	MaxLength$ 20��67    G� Password$	 123��6U     51   
   $
 City=�6   $��3��6"   $��0 16 0 0q�6/   �� 52   
   -   5         ��Name$ 	CBCollege��52      $��2��6&   $��3��66   $��14(�6C   $ 选择学院��53      $��	0 16 16 0q�6-   ��6@   �� *
Colleges+ � DataContext 54      4  .StudentInfoManagmentSystem.MainWindowViewModel  55      57      �� 59   
   -   5         ��Name$ TBPrompt��59      $��2��6&   $��4��6:   $
��0 16q�6G   $ Center=�5:      $y�Hidden=�6+   $��14(�6?   � 5<   
   $��2��6   $��5��6#   $
��16 0q�60    5=      �� 5>      �� 5?      .+�5A      �� -   5         ��Name$ 	BTNSignup��5A      $��注册��6&   $	��8 0q�6K   �� 5B      -   5        $ 
BTNConfirm��5B      $��1��6'   $��确定��67   $	��8 0q�6]   5C      5D   	   5E      5F      5G      