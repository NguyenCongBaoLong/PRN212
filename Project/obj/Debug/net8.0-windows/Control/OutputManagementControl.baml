   M S B A M L   `   `   `  ����B  >Project, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null1clr-namespace:Project.ControlProject.Control  5      -    'Project.Control.OutputManagementControl   5      W SPresentationCore, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089R NWindowsBase, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5      R NSystem.Xaml, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5      @d2http://schemas.microsoft.com/expression/blend/2008     5      /localclr-namespace:Project.Control     5      .��5
      � $��10q�6     � RowDefinitions  5   
   �� 5      $
��Auto �6   �� 5      $��* �6   �� 5      $
��Auto �6   .+�5   
   �� $��0��6   $��Quản lý Xuất Kho��6!   $��24(�69   $
��Bold"�6G   $��0,0,0,15q�6Y   &   System.Windows.Controls.DataGrid  5   
   -   5          Name$ dgOutputDetails��5      $��1��5         AutoGenerateColumns$ False��5         
IsReadOnly$
 True��5         HeadersVisibility$ Column=�5         SelectionMode$ Single=�5      $��0,0,0,15q�5         Columns 5      0  *System.Windows.Controls.DataGridTextColumn  5      ,  &System.Windows.Controls.DataGridColumn   Header$ Id��6%      Width5  /System.Windows.Controls.DataGridLengthConverter$
 Auto 6H   1  +System.Windows.Controls.DataGridBoundColumn	   Binding	 61   �� *Id+6H     5      $ 	Vật tư��6%   $ * 6h   	 65   �� * IdObjectNavigation.DisplayName+6h   4  .System.Windows.Controls.DataGridTemplateColumn  5!      $ Ngày xuất��6)   $
 Auto 6<   
   CellTemplate
 5"      �� 5#      .R�5$      �� $��Center=�6r   ��6(   �� *IdOutputNavigation.DateOutput+ �� StringFormat$ 
dd/MM/yyyy��6r   5%      5'        5)      $ Số lượng��6%   $
 Auto 6Q   	 67   �� *Count+6Q     5+      $ Giá xuất��6%   $
 Auto 6r   	 67   �� *OutputPrice+$ 	{0:N0} đ��6r     5-      $ Khách hàng��6%   $ * 6n   	 69   �� *" IdCustomerNavigation.DisplayName+6n     5/      $ Trạng thái xuất��6%   $ * 6Y   	 6>   �� *Status+6Y   51      �� 53   
   $��2��6   ���#CCCCCC$��1q�64    �� CornerRadius$ 5��6H    �� Padding$ 15q�6Y   
�������._�54      ��  �� Orientation$ Vertical=�6   .+�55      � $��0,0,0,10q�6    � ColumnDefinitions 56      �� 57      $��* �6+   �� 58      $
��Auto �6+   �� 59      $
��Auto �6+   �� 5:      $
��Auto �6+   �� 5;      $
��Auto �6+   �� 5<      $
��Auto �6+   �� 5=      $��* �6+     5?      �� 5@      $
��Auto �6(   .+�5C      �� -   5         ��Name$ 	cmbObject��5C   /   $��0��6    �� DisplayMemberPath$ DisplayName��5D       �� SelectedValuePath$ Id��5E       � ToolTip$ Chọn vật tư��5F      $��0,0,10,0q�65   (  "System.Windows.Controls.DatePicker  5I      -   5          Name$ dpDateOutput��5I   1   $��1��6!   $��0,0,10,0q�5J   >   $ Ngày xuất kho��5K   "   �� 5M      -   5         ��Name$ txtCount��5M   .   $��2��6   $ Số lượng xuất��5N   0   $��0,0,10,0q�6H   5O      �� 5Q      -   5        $ txtOutputPrice��5Q   .   $��3��6   $! Giá xuất của vật tư��5R   0   $��0,0,10,0q�6N   5S      �� 5U      -   5        $ cmbCustomer��5U   /   $��4��6   $ DisplayName��5V      $ Id��5W      $ Chọn khách hàng��5X      $��0,0,10,0q�69   �� 5[      -   5        $ 	txtStatus��5[   .   $��5��6   $( "Trạng thái của phiếu xuất��5\   7   5^      �� 5`      $ 
Horizontal=�6   $
��Left=�66   .+�5a      �� -   5        $��Thêm��5a      $��0,0,10,0q�6B   �� 5b      -	   5        $��Sửa��5b      $��0,0,10,0q�6D   �� 5c      -
   5        $
��Xóa��5c      6+   �� 5d      -   5        $��
Làm mới��5d      $��20,0,0,0q�6G   5e      5f      5g      5h      5i      