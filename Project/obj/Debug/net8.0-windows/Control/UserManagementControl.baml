   M S B A M L   `   `   `  ����B  >Project, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null1clr-namespace:Project.ControlProject.Control  5      +    %Project.Control.UserManagementControl   5      W SPresentationCore, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089R NWindowsBase, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5      R NSystem.Xaml, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5      @d2http://schemas.microsoft.com/expression/blend/2008     5      /localclr-namespace:Project.Control     5      .��5
      � $��10q�6     � RowDefinitions  5   
   �� 5      $
��Auto �6   �� 5      $
��Auto �6   �� 5      $��* �6   �� 5      $
��Auto �6   .+�5   
   �� $��0��6   $��Quản lý Người dùng��6!   $��24(�6;   $
��Bold"�6I   $��0,0,0,15q�6[   �� 5   
   $��1��6   ���#DDDDDD$��1q�64    �� CornerRadius$ 5��6H    �� Padding$ 10q�6Y   $��0,0,0,15q�6f   ���#F8F8F8._�5      �  � ColumnDefinitions 5      �� 5      $
��Auto �6'   �� 5      $��* �6'   �� 5      $
��Auto �6'   .+�5      �� $��0��6   $��Tìm kiếm:��6,   $��Center=�6=   $��0,0,10,0q�6X   �� 5      -   5         ��Name$ 	txtSearch��5   *   $��1��6   $��30��6=   $��Center=�6I   6k   �� 5      -   5         ��Name$ 	btnSearch��5   <   $��2��6   $��Tìm kiếm��6)   $��10,0,0,0q�6g   $��80��6y   5      5      &   System.Windows.Controls.DataGrid  5!   
   -   5          Name$ dgUserDetails��5!   !   $��2��6      AutoGenerateColumns$ False��5"         
IsReadOnly$
 True��5#      	   HeadersVisibility$	 Column=�5$      
   SelectionMode$
 Single=�5%      $��0,0,0,15q�5&         Columns 5)      0  *System.Windows.Controls.DataGridTextColumn  5*      ,  &System.Windows.Controls.DataGridColumn   Header$ Id��6%      Width5  /System.Windows.Controls.DataGridLengthConverter$
 Auto 6H   1  +System.Windows.Controls.DataGridBoundColumn   Binding 61   �� *Id+6H     5,      $ Tên hiển thị��6%   $ * 6[    6;   �� *DisplayName+6[     5.      $ Tên đăng nhập��6%   $ * 6Y    6<   �� *
UserName+6Y     50      $ Mật khẩu��6%   $ * 6T    67   �� *
Password+6T     52      $ Quyền��6%   $ * 6e    64   �� *IdRoleNavigation.DisplayName+6e   54      �� 56   
   $��3��6   ���#CCCCCC$��1q�64   $ 5��6H   $ 15q�6Y   
�������._�57      ��  �� Orientation$ Vertical=�6   .+�58      � $��0,0,0,10q�6    59      �� 5:      $
��Auto �6+   �� 5;      $��* �6+   �� 5<      $
��Auto �6+   �� 5=      $��* �6+   �� 5>      $
��Auto �6+   �� 5?      $��* �6+     5A      �� 5B      $
��Auto �6(   �� 5C      $
��Auto �6(   .+�5F      �� $��0��6    $��0��6-   $��Tên hiển thị:��6=   $��Center=�6R   $��0,0,10,5q�6m   �� 5G      -   5        $ txtDisplayName��5G   ;   $��0��6   $��1��6+    � ToolTip$, &Tên đầy đủ của người dùng��6i   $��0,0,20,5q�6�   �� 5I      $��0��6    $��2��6-   $��Tên đăng nhập:��6=   $��Center=�6S   $��0,0,10,5q�6n   �� 5J      -   5        $ txtUserName��5J   ;   $��0��6   $��3��6+   $ Tên đăng nhập��6b   $��0,0,20,5q�6z   �� 5L      $��0��6    $��4��6-   $��Mật khẩu:��6=   $��Center=�6N   $��0,0,10,5q�6i   �� 5M      -   5        $ txtPassword��5M   ;   $��0��6   $��5��6+   $! Mật khẩu người dùng��6b   �� 5O      $��1��6    $��0��6-   $��Quyền:��6=   $��Center=�6K   $��0,0,10,0q�6f   �� 5P      -   5         ��Name$ cmbUserRole��5P   <   $��1��6   $��1��6,    �� DisplayMemberPath$ DisplayName��5Q       �� SelectedValuePath$ Id��5R      $# Chọn quyền người dùng��5S      $��0,0,20,0q�6?   5T      �� 5V      $ 
Horizontal=�6   $
��Left=�66   $��0,10,0,0q�6Q   .+�5W      �� -   5        $��Thêm��5W      $��0,0,10,0q�6A   �� 5X      -	   5        $��Sửa��5X      $��0,0,10,0q�6C   �� 5Y      -
   5        $
��Xóa��5Y      6+   �� 5Z      -   5        $��
Làm mới��5Z      $��20,0,0,0q�6F   5[      5\      5]      5^      5_      