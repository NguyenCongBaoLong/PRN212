   M S B A M L   `   `   `  ����B  >Project, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null1clr-namespace:Project.ControlProject.Control  5      /    )Project.Control.SupplierManagementControl   5      W SPresentationCore, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089R NWindowsBase, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5      R NSystem.Xaml, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5      @d2http://schemas.microsoft.com/expression/blend/2008     5      /localclr-namespace:Project.Control     5      .��5
      � $��10q�6     � RowDefinitions  5   
   �� 5      $
��Auto �6   �� 5      $
��Auto �6   �� 5      $��* �6   �� 5      $
��Auto �6   .+�5   
   �� $��0��6   $ ��Quản lý Nhà cung cấp��6!   $��24(�6=   $
��Bold"�6K   $��0,0,0,15q�6]   �� 5   
   $��1��6   ���#DDDDDD$��1q�64    �� CornerRadius$ 5��6H    �� Padding$ 10q�6Y   $��0,0,0,15q�6f   ���#F8F8F8._�5      �  � ColumnDefinitions 5      �� 5      $
��Auto �6'   �� 5      $��* �6'   �� 5      $
��Auto �6'   .+�5      �� $��0��6   $��Tìm kiếm:��6,   $��Center=�6=   $��0,0,10,0q�6X   �� 5      -   5         ��Name$ 	txtSearch��5   *   $��1��6   $��30��6=   $��Center=�6I   6k   �� 5      -   5         ��Name$ 	btnSearch��5   <   $��2��6   $��Tìm kiếm��6)   $��10,0,0,0q�6g   $��80��6y   5      5      &   System.Windows.Controls.DataGrid  5!   
   -   5          Name$ dgSupplierDetails��5"      $��2��5!         AutoGenerateColumns$ False��5#         
IsReadOnly$
 True��5$      	   HeadersVisibility$	 Column=�5%      
   SelectionMode$
 Single=�5&      $��0,0,0,15q�5'         Columns 5)      0  *System.Windows.Controls.DataGridTextColumn  5*      ,  &System.Windows.Controls.DataGridColumn   Header$ Id��6%      Width5  /System.Windows.Controls.DataGridLengthConverter$
 Auto 6H   1  +System.Windows.Controls.DataGridBoundColumn   Binding 61   �� *Id+6H     5+      $ Tên nhà cung cấp��6%   $ * 6_    6?   �� *DisplayName+6_     5,      $ Địa chỉ��6%   $
 1.5* 6R    66   �� *	Address+6R     5-      $ Điện thoại��6%   $
 Auto 6S    69   �� *Phone+6S     5.      $ Email��6%   $ * 6N    64   �� *Email+6N     5/      $ Thông tin thêm��6%   $ * 6Z    6=   �� *
MoreInfo+6Z   4  .System.Windows.Controls.DataGridTemplateColumn  50      $ Ngày hợp tác��6)   $
 Auto 6?      CellTemplate 51      �� 52      .R�53      �� $��Center=�6a   ��6(   �� *ContractDate+ �� StringFormat$ 
dd/MM/yyyy��6a   54      56      58      �� 5:   
   $��3��6   ���#CCCCCC$��1q�64   $ 5��6H   $ 15q�6Y   
�������._�5;      ��  �� Orientation$ Vertical=�6   .+�5<      � $��0,0,0,10q�6    5=      �� 5>      $
��Auto �6+   �� 5?      $��* �6+   �� 5@      $
��Auto �6+   �� 5A      $��* �6+   �� 5B      $
��Auto �6+   �� 5C      $��* �6+     5E      �� 5F      $
��Auto �6(   �� 5G      $
��Auto �6(   �� 5H      $
��Auto �6(   .+�5K      �� $��0��6    $��0��6-   $��	Tên NCC:��6=   $��Center=�6M   $��0,0,10,5q�6h   �� 5L      -   5        $ txtDisplayName��5L   ;   $��0��6   $��1��6+    � ToolTip$  Tên của nhà cung cấp��6m   $��0,0,20,5q�6�   �� 5N      $��0��6    $��2��6-   $��Địa chỉ:��6=   $��Center=�6M   $��0,0,10,5q�6h   �� 5O      -   5        $ 
txtAddress��5O   ;   $��0��6   $��3��6+   $" Địa chỉ nhà cung cấp��6`   $��0,0,20,5q�6   �� 5Q      $��0��6    $��4��6-   $��Điện thoại:��6=   $��Center=�6P   $��0,0,10,5q�6k   �� 5R      -   5        $ txtPhone��5R   ;   $��0��6   $��5��6+   $* $Số điện thoại nhà cung cấp��6d   6�   �� 5T      $��1��6    $��0��6-   $��Email:��6=   $��Center=�6K   $��0,0,10,5q�6f   �� 5U      -   5        $ txtEmail��5U   ;   $��1��6   $��1��6+   $ Email nhà cung cấp��6\   $��0,0,20,5q�6y   �� 5W      $��1��6    $��2��6-   $��Ngày hợp tác:��6=   $��Center=�6R   $��0,0,10,5q�6m   (  "System.Windows.Controls.DatePicker  5X      -   5          Name$ dpContractDate��5X   >   $��1��6!   $��3��6.   $# Ngày bắt đầu hợp tác��5Y   =   $��0,0,20,5q�6\   �� 5[      $��1��6    $��4��6-   $��Thông tin thêm:��6=   $��Center=�6T   $��0,0,10,5q�6o   �� 5\      -	   5        $ txtMoreInfo��5\   ;   $��1��6   $��5��6+   $ Thông tin bổ sung��6h    �� TextWrapping$
 Wrap=�6�    �� AcceptsReturn$
 True��6�   $��40��6�   5^      �� 5`      $ 
Horizontal=�6   $
��Left=�66   $��0,10,0,0q�6Q   .+�5a      �� -
   5        $��Thêm��5a      $��0,0,10,0q�6B   �� 5b      -   5        $��Sửa��5b      $��0,0,10,0q�6D   �� 5c      -   5        $
��Xóa��5c      6+   �� 5d      -   5        $��
Làm mới��5d      $��20,0,0,0q�6G   5e      5f      5g      5h      5i      