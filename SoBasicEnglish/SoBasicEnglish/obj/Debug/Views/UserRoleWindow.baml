   M S B A M L   `   `   `  ����I  ESoBasicEnglish, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null;"clr-namespace:SoBasicEnglish.ViewsSoBasicEnglish.Views  5   	   U QMahApps.Metro, Version=1.5.0.23, Culture=neutral, PublicKeyToken=f4fb5a3c4d1e5b4fV;clr-namespace:MahApps.Metro.Controls;assembly=MahApps.MetroMahApps.Metro.Controls 5
   	   f bMahApps.Metro.SimpleChildWindow, Version=1.4.0.0, Culture=neutral, PublicKeyToken=bbbfdff7f500a992zVclr-namespace:MahApps.Metro.SimpleChildWindow;assembly=MahApps.Metro.SimpleChildWindowMahApps.Metro.SimpleChildWindow 5      )    #SoBasicEnglish.Views.UserRoleWindow   5      \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35^ ZWindowsFormsIntegration, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   4local"clr-namespace:SoBasicEnglish.Views     5   	   PControls;clr-namespace:MahApps.Metro.Controls;assembly=MahApps.Metro     5
   	   tsimpleChildWindowVclr-namespace:MahApps.Metro.SimpleChildWindow;assembly=MahApps.Metro.SimpleChildWindow     5        � WindowStartupLocation$  CenterScreen=�5   	    � Title$ UserRoleScreen��5	      $��506.956��6   $��769.928��60   .��5      � .+�5   
   � 
����ح� � RowDefinitions 5      �� 5      $	��.7* �6    �� 5      .+�5      � $��0��6   .+�5      �� $��Selected user profile��6   .��5      �  � ColumnDefinitions 5      �� 5      $	��.3* �6/   �� 5      .+�5      � $��0��6   .+�5      �� $
��Fill=�6g   ��6%   �� *SelectedItem.UserAvatar+ �� ElementName$ 
gvUserList�� J� Clip 5   "   @� 5   &    @� Center$ 85,75&�66    @� RadiusX$ 70Q�6E    @� RadiusY$ 70Q�6R   5      5      � 5       $��1��6   .+�5!      �� .+�5"   "   �� $��30(�6j   $��	20 10 0 0q�6x   $��	ExtraBold"�6�   $0��*/SoBasicEnglish;component/Fonts/#VNI-Tubes)�6�   ��6,   �� *SelectedItem.UserName+$ 
gvUserList��6�   �� 5#   "   	 �� Orientation$	 
Horizontal=�6-   $��	20 10 0 0q�6F   .+�5$   &   �� $��20(�60   
���  ��Level:6W   �� 5%   &   $��15 0 0 0q�60   $��20(�6�   $
��Bold"�6�   $��Italic$�6�   
���  ����6B   �� *SelectedItem.UserLevel+$ 
gvUserList��6�   5&   #   �� 5'   "   $	 
Horizontal=�6-   $
��auto��6F   $��20,10,8.4,0q�6S   $��53��6h   .+�5(   &   �� $��Center=�60   $
��auto��6K   $��20(�6X   
��� ���Role:6�   �� 5)   &   $��Center=�61   $��15 0 0 0q�6L   $
��auto��6^   $��20(�6�   $
��Bold"�6�   $��Italic$�6�   
���  ����6k   �� *SelectedItem.Role+$ 
gvUserList��6�   !  MahApps.Metro.Controls.Tile  5*   &   -   5        
  Name$
 btnChangeRole��5*   �   $��45��6�   $
��Auto��6�   $��21,3,26.6,0q�6�   ��64   �� *Click_ChangRole+��6X   �� *SelectedItem+$ 
gvUserList�� J� Effect 5+   *   3  -System.Windows.Media.Effects.DropShadowEffect  5,   .      ShadowDepth$ 6Q�6?      
BlurRadius$ 7Q�6O   6`   .��5.   *   �� $"��..\Images/Assets/Edit_04.png��60   $��25��6V   $��30��6b   $
��Auto=�6m   $
��Auto=�6�   {�.  6�   5/   '     50   &   
���**��$��45��6�   $
��Auto��6�   $��0,3,0,0q�6�   ��6H   �� *Click_Delete+��6i   �� *SelectedItem+$ 
gvUserList�� 51   *     52   .   $ 6Q�6?   $ 7Q�6O   6`   .��54   *   �� $)��#..\Images/Assets/Garbage-Closed.png��60   $��25��6]   $��30��6i   $
��Auto=�6t   $
��Auto=�6�   {�.  6�   55   '   56   #   58      59      5;      5<      5=      � 5>      $��1��6   .+�5?      &   System.Windows.Controls.DataGrid  -   5          Name$ 
gvUserList��5?      $��10,20,10,10q�6P      AutoGenerateColumns . ��6/   �� *
Userlist+5B      5C      5D      � 5E   
   .+�5F      1  +MahApps.Metro.SimpleChildWindow.ChildWindow  -   5          Name$ child01��5F   ,      Title$ TestChild 1��5G   "      ShowTitleBar .     ShowCloseButton .     CloseByEscape .    CloseOnOverlay .    IsOpen 5L   (   �� *OpenConfirmDiaglog+ �� Mode$ TwoWay=�.��5N      � $	��150��6   $	��310��6%   ��5O      �� 5P       �� Color$ 	#FFDA9595��6*    �� Opacity$	 0.9Q�6<   6M   .+�5R      �� .+�5S      �� $��5 20 0 0q�6%   $	 
Horizontal=�67   .+�5T      �� $��5q�6$   $$��..\Images/Assets/Help - 02.png��6/   6Z   �� 5U      $��Center=�6)   $��17(�6D    �� TextAlignment$ Center=�6R   $	��241��6i   $��Showcard Gothic)�6u   You Really want?6�   5V      �� 5W      $��0 5 0 0q�6%   $	 
Horizontal=�66   .+�5X      �� $��90 0 0 0q�5Z      $
��Left=�6/   ���	#FF53E453$��60��6a   $��60��6l      MetroCircleButtonStyle#���   6x   ��5Y      �� *Click_ConfirmChangeRole+��6I   �� *SelectedItem+$ 
gvUserList��.��5[   "   �� $#��..\Images/Assets/Check-01.png��6(   $��30��6O   $��30��6[   6h   5\      �� 5]      $��20 0 0 0q�5_      $
��Left=�60   
���  ��$��60��6]   $��60��6h   #���   6t   ��5^      �� *CLick_CancelChangeRole+$ TwoWay=�.��5`   "   �� $!��..\Images/Assets/Cancel.png��6(   $��30��6M   $��30��6Y   6f   5a      5b      5c      5d      5e      5f      � 5g   
   .+�5h        $ TestChild 1��5i   "    .   .   .  .  5n   (   �� *OpenConfirmDeleteUserDiaglog+$ TwoWay=�.��5p      � $	��150��6   $	��310��6%   ��5q      �� 5r      $ 	#FFDA9595��6*   $	 0.9Q�6<   6M   .+�5t      �� .+�5u      �� $��5 20 0 0q�6%   $	 
Horizontal=�67   .+�5v      �� $��5q�6$   $$��..\Images/Assets/Help - 02.png��6/   6Z   �� 5w      $��Center=�6)   $��17(�6D   $ Center=�6R   $	��241��6i   $��Showcard Gothic)�6u   You Really want?6�   5x      �� 5y      $��0 5 0 0q�6%   $	 
Horizontal=�66   .+�5z      �� $��90 0 0 0q�5|      $
��Left=�6/   ���	#FF53E453$��60��6a   $��60��6l   #���   6x   ��5{      �� *Click_ConfirmChangeRole+��6I   �� *SelectedItem+$ 
gvUserList��.��5}   "   �� $#��..\Images/Assets/Check-01.png��6(   $��30��6O   $��30��6[   6h   5~      �� 5      $��20 0 0 0q�5�      $
��Left=�60   
���  ��$��60��6]   $��60��6h   #���   6t   ��5�      �� *CLick_CancelChangeRole+$ TwoWay=�.��5�   "   �� $!��..\Images/Assets/Cancel.png��6(   $��30��6M   $��30��6Y   6f   5�      5�      5�      5�      5�      5�      5�      5�      