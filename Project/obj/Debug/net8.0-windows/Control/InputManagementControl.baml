   M S B A M L   `   `   `  ����B  >Project, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null1clr-namespace:Project.ControlProject.Control  5      ,    &Project.Control.InputManagementControl   5      W SPresentationCore, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089R NWindowsBase, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5      R NSystem.Xaml, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5      @d2http://schemas.microsoft.com/expression/blend/2008     5      /localclr-namespace:Project.Control     5      .��5
      � $��10q�6     � RowDefinitions  5   
   �� 5      $
��Auto �6   �� 5      $��* �6   �� 5      $
��Auto �6   .+�5   
   �� $��0��6   $��Quản lý Nhập Kho��6!   $��24(�69   $
��Bold"�6G   $��0,0,0,15q�6Y   &   System.Windows.Controls.DataGrid  5   
   -   5          Name$ dgInputDetails��5      $��1��5         AutoGenerateColumns$ False��5         
IsReadOnly$
 True��5         HeadersVisibility$ Column=�5         SelectionMode$ Single=�5      $��0,0,0,15q�5         Columns 5      0  *System.Windows.Controls.DataGridTextColumn  5      ,  &System.Windows.Controls.DataGridColumn   Header$ Id��6%      Width5  /System.Windows.Controls.DataGridLengthConverter$
 Auto 6H   1  +System.Windows.Controls.DataGridBoundColumn	   Binding	 61   �� *Id+6H     5      $ 	Vật tư��6%   $ * 6h   	 65   �� * IdObjectNavigation.DisplayName+6h   4  .System.Windows.Controls.DataGridTemplateColumn  5!      $ Ngày nhập��6)   $
 Auto 6<   
   CellTemplate
 5"      �� 5#      .R�5$      �� $��Center=�6p   ��6(   �� *IdInputNavigation.DateInput+ �� StringFormat$ 
dd/MM/yyyy��6p   5%      5'        5)      $ Số lượng��6%   $
 Auto 6Q   	 67   �� *Count+6Q     5*      $ Giá nhập��6%   $
 Auto 6q   	 67   �� *
InputPrice+$ 	{0:N0} đ��6q     5-      $ Trạng thái nhập��6%   $ * 6Y   	 6>   �� *Status+6Y   5/      �� 51   
   $��2��6   ���#CCCCCC$��1q�64    �� CornerRadius$ 5��6H    �� Padding$ 15q�6Y   
�������._�52      ��  �� Orientation$ Vertical=�6   .+�53      � $��0,0,0,10q�6    � ColumnDefinitions 54      �� 55      $��* �6+   �� 56      $
��Auto �6+   �� 57      $
��Auto �6+   �� 58      $
��Auto �6+   �� 59      $��* �6+     5;      �� 5<      $
��Auto �6(   .+�5?      �� -   5         ��Name$ 	cmbObject��5?   /   $��0��6    �� DisplayMemberPath$ DisplayName��5@       �� SelectedValuePath$ Id��5A       � ToolTip$ Chọn vật tư��5B      $��0,0,10,0q�65   (  "System.Windows.Controls.DatePicker  5E      -   5          Name$ dpDateInput��5E   1   $��1��6!   $��0,0,10,0q�5F   >   $ Ngày nhập kho��5G   "   �� 5I      -   5         ��Name$ txtCount��5I   .   $��2��6   $ Số lượng nhập��5J   0   $��0,0,10,0q�6H   5K      �� 5M      -   5        $ txtInputPrice��5M   .   $��3��6   $! Giá nhập của vật tư��5N   0   $��0,0,10,0q�6N   5O      �� 5R      -   5        $ 	txtStatus��5R   .   $��5��6   $( "Trạng thái của phiếu nhập��5S   7   5U      �� 5W      $ 
Horizontal=�6   $
��Left=�66   .+�5X      �� -   5        $��Thêm��5X      $��0,0,10,0q�6B   �� 5Y      -   5        $��Sửa��5Y      $��0,0,10,0q�6D   �� 5Z      -	   5        $
��Xóa��5Z      6+   �� 5[      -
   5        $��
Làm mới��5[      $��20,0,0,0q�6G   5\      5]      5^      5_      5`      