   M S B A M L   `   `   `  ����B  >Project, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null1clr-namespace:Project.ControlProject.Control  5      -    'Project.Control.ObjectManagementControl   5      W SPresentationCore, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089R NWindowsBase, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5      R NSystem.Xaml, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5      @d2http://schemas.microsoft.com/expression/blend/2008     5      /localclr-namespace:Project.Control     5      .��5
      � $��10q�6     � RowDefinitions  5   
   �� 5      $
��Auto �6   �� 5      $��* �6   �� 5      $
��Auto �6   .+�5   
   �� $��0��6   $��Quản lý Vật tư��6!   $��24(�67   $
��Bold"�6E   $��0,0,0,15q�6W   &   System.Windows.Controls.DataGrid  5   
   -   5          Name$ dgObjectDetails��5      $��1��5         AutoGenerateColumns$ False��5         
IsReadOnly$
 True��5         HeadersVisibility$ Column=�5         SelectionMode$ Single=�5      $��0,0,0,15q�5         Columns 5      0  *System.Windows.Controls.DataGridTextColumn  5      ,  &System.Windows.Controls.DataGridColumn   Header$ Id��6%      Width5  /System.Windows.Controls.DataGridLengthConverter$
 Auto 6H   1  +System.Windows.Controls.DataGridBoundColumn	   Binding	 61   �� *Id+6H     5      $ Tên vật tư��6%   $ * 6Y   	 69   �� *DisplayName+6Y     5!      $ Đơn vị đo��6%   $ * 6i   	 68   �� *IdUnitNavigation.DisplayName+6i     5#      $ Nhà cung cấp��6%   $ * 6p   	 6;   �� *" IdSupplierNavigation.DisplayName+6p     5%      $ Mã vật tư��6%   $
 Auto 6S   	 68   �� *QrCode+6S     5'      $ 
Mã vạch��6%   $
 Auto 6R   	 66   �� *	BarCode+6R     5)      $ Số lượng tồn��6%   $
 Auto 6X   	 6;   �� *
Quantity+6X   5,      �� 5.   
   $��2��6   ���#CCCCCC$��1q�64   
 �� CornerRadius$
 5��6H    �� Padding$ 15q�6Y   
�������._�5/      ��  �� Orientation$ Vertical=�6   .+�50      � $��0,0,0,10q�6    � ColumnDefinitions 51      �� 52      $��* �6+   �� 53      $
��Auto �6+   �� 54      $��* �6+     56      �� 57      $
��Auto �6(   �� 58      $
��Auto �6(   .+�5;      �� $��0��6!   $��0��6.   $ 
Horizontal=�6>   $��	0,0,10,10q�6W   .+�5<      �� $��Tên vật tư:��6$   $��Center=�67   $��80��6R   �� 5=      -   5         ��Name$ txtDisplayName��5=   "    � ToolTip$ Tên của vật tư��6N   $	��200��6g   5>      �� 5@      $��0��6!   $��1��6.   $ 
Horizontal=�6>   $��	0,0,10,10q�6W   .+�5A      �� $��Đơn vị đo:��6$   $��Center=�66   $��80��6Q   �� 5B      -   5         ��Name$ cmbUnit��5B   #    �� DisplayMemberPath$ DisplayName��5C   #    �� SelectedValuePath$ Id��5D   #   $ Chọn đơn vị đo��5E   #   $	��120��6<   5F      �� 5H      $��0��6!   $��2��6.   $ 
Horizontal=�6>   $��	0,0,10,10q�6W   .+�5I      �� $��Nhà cung cấp:��6$   $��Center=�69   $	��100��6T   �� 5J      -   5        $ cmbSupplier��5J   #   $ DisplayName��5K   #   $ Id��5L   #   $ Chọn nhà cung cấp��5M   #   $	��150��6?   5N      �� 5P      $��1��6!   $��0��6.   $ 
Horizontal=�6>   $��0,0,10,0q�6W   .+�5Q      �� $��Mã vật tư:��6$   $��Center=�66   $��80��6Q   �� 5R      -   5        $ 	txtQrCode��5R   "   $ Mã QR của vật tư��6D   $	��120��6_   5S      �� 5U      $��1��6!   $��1��6.   $ 
Horizontal=�6>   $��0,0,10,0q�6W   .+�5V      �� $��Mã vạch:��6$   $��Center=�64   $��80��6O   �� 5W      -   5        $ 
txtBarCode��5W   "   $  Mã vạch của vật tư��6G   $	��120��6d   5X      �� 5Z      $��1��6!   $��2��6.   $ 
Horizontal=�6>   $��0,0,10,0q�6W   .+�5[      �� $��Số lượng:��6$   $��Center=�65   $��80��6P   �� 5\      -   5        $ txtQuantity��5\   "   $ Số lượng tồn kho��6I   $	��100��6d   6p   5]      5a      �� 5c      $ 
Horizontal=�6   $
��Left=�66   $��0,10,0,0q�6Q   .+�5d      �� -   5        $��Thêm��5d      $��0,0,10,0q�6B   �� 5e      -	   5        $��Sửa��5e      $��0,0,10,0q�6D   �� 5f      -
   5        $
��Xóa��5f      6+   �� 5g      -   5        $��
Làm mới��5g      $��20,0,0,0q�6G   5h      5i      5j      5k      5l      