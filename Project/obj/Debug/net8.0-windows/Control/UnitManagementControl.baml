   M S B A M L   `   `   `  ����B  >Project, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null1clr-namespace:Project.ControlProject.Control  5      +    %Project.Control.UnitManagementControl   5      W SPresentationCore, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089R NWindowsBase, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5      R NSystem.Xaml, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5      @d2http://schemas.microsoft.com/expression/blend/2008     5      /localclr-namespace:Project.Control     5      .��5
      � $��10q�6     � RowDefinitions  5   
   �� 5      $
��Auto �6   �� 5      $
��Auto �6   �� 5      $��* �6   �� 5      $
��Auto �6   .+�5   
   �� $��0��6   $��Quản lý Đơn vị đo��6!   $��24(�6:   $
��Bold"�6H   $��0,0,0,15q�6Z   �� 5   
   $��1��6   ���#DDDDDD$��1q�64    �� CornerRadius$ 5��6H    �� Padding$ 10q�6Y   $��0,0,0,15q�6f   ���#F8F8F8._�5      �  � ColumnDefinitions 5      �� 5      $
��Auto �6'   �� 5      $��* �6'   �� 5      $
��Auto �6'   .+�5      �� $��0��6   $��Tìm kiếm:��6,   $��Center=�6=   $��0,0,10,0q�6X   �� 5      -   5         ��Name$ 	txtSearch��5   *   $��1��6   $��30��6=   $��Center=�6I   6k   �� 5      -   5         ��Name$ 	btnSearch��5   <   $��2��6   $��Tìm kiếm��6)   $��10,0,0,0q�6g   $��80��6y   5      5      &   System.Windows.Controls.DataGrid  5!   
   -   5          Name$ dgUnitDetails��5!   !   $��2��6      AutoGenerateColumns$ False��5"         
IsReadOnly$
 True��5#      	   HeadersVisibility$	 Column=�5$      
   SelectionMode$
 Single=�5%      $��0,0,0,15q�5&         Columns 5)      0  *System.Windows.Controls.DataGridTextColumn  5*      ,  &System.Windows.Controls.DataGridColumn   Header$ Id��6%      Width5  /System.Windows.Controls.DataGridLengthConverter$
 Auto 6H   1  +System.Windows.Controls.DataGridBoundColumn   Binding 61   �� *Id+6H     5,      $ Đơn vị đo��6%   $ * 6X    68   �� *DisplayName+6X     5.      $ Mô tả��6%   $ 2* 6T    64   �� *Description+6T   50      �� 52   
   $��3��6   ���#CCCCCC$��1q�64   $ 5��6H   $ 15q�6Y   
�������._�53      ��  �� Orientation$ Vertical=�6   .+�54      � $��0,0,0,10q�6    55      �� 56      $
��Auto �6+   �� 57      $��* �6+   �� 58      $
��Auto �6+   �� 59      $��* �6+     5;      �� 5<      $
��Auto �6(   .+�5?      �� $��0��6    $��Đơn vị đo:��60   $��Center=�6B   $��0,0,10,0q�6]   �� 5@      -   5        $ txtDisplayName��5@   .   $��1��6    � ToolTip$ Tên của đơn vị đo��6]   $��0,0,20,0q�6y   �� 5B      $��2��6    $��	Mô tả:��60   $��Center=�6>   $��0,0,10,0q�6Y   �� 5C      -   5        $ txtDescription��5C   .   $��3��6   $( "Mô tả chi tiết đơn vị đo��6_   5D      �� 5F      $ 
Horizontal=�6   $
��Left=�66   .+�5G      �� -   5        $��Thêm��5G      $��0,0,10,0q�6B   �� 5H      -   5        $��Sửa��5H      $��0,0,10,0q�6D   �� 5I      -   5        $
��Xóa��5I      6+   �� 5J      -	   5        $��
Làm mới��5J      $��20,0,0,0q�6G   5K      5L      5M      5N      5O      