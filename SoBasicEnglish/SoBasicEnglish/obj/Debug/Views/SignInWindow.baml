   M S B A M L   `   `   `  ����I  ESoBasicEnglish, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null;"clr-namespace:SoBasicEnglish.ViewsSoBasicEnglish.Views  5   	   f bMahApps.Metro.SimpleChildWindow, Version=1.4.0.0, Culture=neutral, PublicKeyToken=bbbfdff7f500a992zVclr-namespace:MahApps.Metro.SimpleChildWindow;assembly=MahApps.Metro.SimpleChildWindowMahApps.Metro.SimpleChildWindow 5   	   c _System.Windows.Interactivity, Version=4.5.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35qPclr-namespace:System.Windows.Interactivity;assembly=System.Windows.InteractivitySystem.Windows.Interactivity 5      U QMahApps.Metro, Version=1.5.0.23, Culture=neutral, PublicKeyToken=f4fb5a3c4d1e5b4fV;clr-namespace:MahApps.Metro.Controls;assembly=MahApps.MetroMahApps.Metro.Controls 5	   	   '    !SoBasicEnglish.Views.SignInWindow   5   	   \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35^ ZWindowsFormsIntegration, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   4local"clr-namespace:SoBasicEnglish.Views     5   	   tsimpleChildWindowVclr-namespace:MahApps.Metro.SimpleChildWindow;assembly=MahApps.Metro.SimpleChildWindow     5   	   ^iPclr-namespace:System.Windows.Interactivity;assembly=System.Windows.Interactivity     5      PControls;clr-namespace:MahApps.Metro.Controls;assembly=MahApps.Metro     5	   	   -   5          �Name$  signInWindow��5   	    � WindowStyle$
 None=�5
   	    � AllowsTransparency . 
������  � WindowStartupLocation$ CenterScreen=�5   	    � Title$ SignInWindow��5   	   $	��523��6   $��329.856��6-    � 	Resources 5       
  Color1��%+   &	        ��5   
   	#FFEAD8A5��6*   5      .��5      �� $	��Top=�6    �� CornerRadius$ 30��6%   ��67   �� *HeighOfWindow+ �� Mode$ OneWay=���5   
   �� 5       �� EndPoint$ 0.5,1&�6"   	 � MappingMode$	 RelativeToBoundingBox=�63   
 �� 
StartPoint$
 0.5,0&�6W   .��5      �  � Color$ SaddleBrown��6    � Offset$ 0.826Q�63   � 5      $ 	#FFAA9867��6   $ 0.248Q�61   5      ._�5   
   � $��10q�6   .+�5      �� $	��Top=�6   $��Right=�6-   $��40��5   \   $��40��6h   
���**��  MetroCircleButtonStyle#���  6�   ��5   J   �� *CloseWindowCommand+$ OneWay=���5   %   ��  �� ElementName$ signInWindow��.��5       �� $!��..\Images/Assets/Cancel.png��6   $��20��6=   $��20��6I    J� RenderTransformOrigin$ 0.5,0.62&�6T   5"      �� 5#      $	��380��6   .+�5$      �� $��60 10q�6   $	��150��6'   $7��1/SoBasicEnglish;component/Images/Avt/customer.png��64   5%      � 5&      -   5         �Name$ stpLoginInfo��5&      $��90��6-   $	��230��69   $��0 10 0 5q�6E    � RowDefinitions 5'      �� 5(      �� 5)      $
��.09* �6(   �� 5*      �� 5+      $
��.09* �6(    � ColumnDefinitions 5-      �� 5.      $	��.1* �6+   69   �� 5/      .+�51      �� $��Bottom=�6   $"��..\Images/Assets/Edit_04.png��67   $	��0 5q�6]   6m   �� 52      -   5         ��Name$ txtUserLoginName��52      
�������$��khoideptrai��6K   $��Bottom=�6_   *  $MahApps.Metro.Controls.TextBoxHelper   ClearTextButton . $��0q�6�   
������ $	��5 2q�6�   $��1��6�      	Watermark$ Your login name...��6�   $1��+/SoBasicEnglish;component/Fonts/#Lato Heavy)�6(  .��6b  53      �� 54      $��2 0 2 0q�6   $��2��6/   $��1��6C   
�������6h   �� 55      $��0 0 0 5q�6   $��0��6-   $��2��6=   $��Bottom=�6J   $+��%..\Images/Assets/Password-Text-01.png��6e   6�   G� 56      -   5         G�Name$ txtUserLoginpassWord��56   E    G� Password$
 1234��6"   
�������$��1��6c   $��2��6s   $��Bottom=�6�    . $��0q�6�   
������ $	��5 2q�6�   $ Your password...��6  $1��+/SoBasicEnglish;component/Fonts/#Lato Heavy)�67  57      �� 58      $��3��6   $	��2 0q�6*   $��2��67   
�������6c   59      �� 5:       �� Orientation$ 
Horizontal=�6   .+�5;      ��  �� 	IsDefault . )  #MahApps.Metro.Controls.ButtonHelper   CornerRadius$ 20��5>   *   $��77��6R   $��30��6u   $��	73 0 10 0q�6�   
�������$��Sign In��6�   $��10(�6�   +  %MahApps.Metro.Controls.ControlsHelper   ContentCharacterCasing$ Upper=�6�   ��5<      �� *Login+��5=      �� *
Children+$ stpLoginInfo����5>   ]   Z� ��5?      �� 5@       �� 
TargetType" ��6$   .�5A   "   ��  �� Property � �� �� Value
 ���� 6?   �� 5B   "    � �� 5C   &   �� 5D   *    �� 
TargetType" ��6:   .R�5E   .   �� #��z��65   ._�5F   2   �� $��Center=�6C   $��Center=�6`   5G   /   5H   +   5J   #    �� Triggers 5K   "   P� 5L   &     P� Property  �@J�IsMouseOver! P� Value! . . �5M   *   ��  � ��
 �**��6G   5N   '   5P      5R      �� 5S      $��10q�6C   $ 20��6P   $��30��6�   $
��0 10q�6�   
�������$��Sigh Up��6�   $��10(�6�   $ Upper=�6�   $��77��6  ��6   �� *ShowSignUpCommand+��6x   Z� ��5T      �� 5U      " ��6$   .�5V   "   ��  � ��
 ���� 6?   �� 5W   "    � �� 5X   &   �� 5Y   *   " ��6:   .R�5Z   .   �� #��z��65   ._�5[   2   �� $��Center=�6C   $��Center=�6`   5\   /   5]   +   5_   #    5`   "   P� 5a   &     �@J�IsMouseOver! . . �5b   *   ��  � ��
 �**��6G   5c   '   5e      5g      5h      9� 5i      -   5        $��ExpanderControl��5k   )   
������ $��
10,0,10,10q�6   $��Center=�5l      $��You forgot your password?��63   " 9� ExpandDirection$
" Down=�5m      # 9� 
IsExpanded# .  $��10(�5n      $
��Bold"�6!   
��� � �$	��290��6F   $	��168��6R   .  (System.Windows.Interactivity.Interaction$   Triggers$ 5o      /  )System.Windows.Interactivity.EventTrigger  5p      %   	EventName$% Expanded��6)   .  (System.Windows.Interactivity.TriggerBase&   Actions.& 5q      6  0System.Windows.Interactivity.InvokeCommandAction  '   Command' 5r   !   �� *ExpanderControl_Expanded+5s   $   5t        5u      $% 	Collapsed��6)   .& 5v        ' 5w   !   �� *ExpanderControl_Collapsed+5x   $   5y      .��5|      �� .+�5}      � -   5        $ gForgetPass��5}   +   $��90��6    5~      �� 5   "   �� 5�   "   $
��.09* �60   �� 5�   "   �� 5�   "   $
��.09* �60    5�      �� 5�   "   $	��.1* �63   6A   �� 5�   "   .+�5�      �� $��Bottom=�6$   $ ��..\Images/Assets/GMail.png��6?   $	��0 5q�6c   6s   �� 5�      -   5        $ txtEmailToResetPassword��5�   B   $
��auto��6&   $	��200��63   $��Bottom=�6c    . $��0q�6�   
������ $	��5 2q�6�   $��1��6�   $ Your Email...��6�   
�������$1��+/SoBasicEnglish;component/Fonts/#Lato Heavy)�6:  .��6t  5�      �� 5�      $��2 0 2 0q�6%   $��2��67   $��1��6K   
�������6p   �� 5�      $��0 0 0 5q�6$   $��0��65   $��2��6E   $��Bottom=�6R   $"��..\Images/Assets/Edit_04.png��6m   6�   �� 5�      -   5        $! txtLoginNameToReSetPassWord��5�   '   $��1��6L   $��2��6\   $��Bottom=�6i    . $��0q�6�   
������ $	��5 2q�6�   
�������$ Your login name...��6�   $1��+/SoBasicEnglish;component/Fonts/#Lato Heavy)�65  .��6o  5�      �� 5�      $��3��6%   $	��2 0q�62   $��2��6?   
�������6k   5�      �� 5�      $ 20��6!   $��30��6�   $��70 10q�6�   
�������$��Get old password��6�   $��10(�6  $ Upper=�6   ��6I   �� *Click_ForgetPassword+��6r   �� *
Children+$ gForgetPass����6�   Z� ��5�      �� 5�   "   " ��6(   .�5�   &   ��  � ��
 ���� 6C   �� 5�   &    � �� 5�   *   �� 5�   .   " ��6>   .R�5�   2   �� #��z��69   ._�5�   6   �� $��Center=�6G   $��Center=�6d   5�   3   5�   /   5�   '    5�   &   P� 5�   *     �@J�IsMouseOver! . . �5�   .   ��  � ��
 �**��6K   5�   +   5�   #   5�      5�      5�      5�      #  MahApps.Metro.Controls.Flyout  5�      $y�	Collapsed=�6=   $��0q�6U   
���   �(   Position$
( Left=�6}   )   TitleVisibility$) Hidden=�6�   *   CloseButtonVisibility$* Hidden=�6�   $��Right=�6�   + �� Left$	+ 245��6�   
, �� Top$, 10��6�   $��
0,0,95,437q�6
  -   IsOpen- 6   �� *IsOpenError+��5�      �� 5�      . �� Color$. Transparent��6&   / �� Opacity$	/ 0.7Q�6:   6J   .��5�      �� $
��auto��6   $ 15��6&   
����p��$��1q�6M   $��60��6b   $��	0,0,0.4,0q�6n   ��5�      �� 5�      $. Indigo��6*   $	/ 0.7Q�69   6I   ._�5�      � $
��auto��6   $��10.2,9.2,9.4,9.6q�6(   .+�5�      �� $
��auto��6%   $ 
Horizontal=�62   $��0,0,-0.6,-0.2q�6K   .+�5�      �� $��Center=�6$   $!��..\Images/Assets/Cancel.png��6?   $��29��6d   �� 5�      $
��Bold"�6(   $	��229��6:   
�������$��	5,0,0,0.2q�6a   $��Center=�6t   $��13(�6�   $4��./SoBasicEnglish;component/Fonts/#Lato Semibold)�6�   $
��Wrap=�6  $
��auto��6  ��6�   �� *ErrorMessage+$ OneWay=�6'  5�      5�      5�      5�      )	  #MahApps.Metro.Controls.ProgressRing	  5�      -
  'MahApps.Metro.Controls.VisibilityHelper0 
  IsCollapsed$
0 TrueX�6$   
������$	��150��6�   $	��150��6�   1 
  	IsVisible1 6Q   �� *
IsActive+5�      � 5�      $	��150��6   $	��310��6!   .+�5�      1  +MahApps.Metro.SimpleChildWindow.ChildWindow  -	   5        2  Name$2 child01��5�   0   3   Title$3 TestChild 1��5�   "   4   ShowTitleBar4 .  
������ 5   ShowCloseButton5 .  6   CloseByEscape6 . 7   CloseOnOverlay7 . 8   IsOpen8 5�   (   �� *OpenDiaglog+$ TwoWay=�.��5�      � $	��150��6   $	��310��6)   ��5�      �� 5�      $. Brown��6.   $	/ 0.4Q�6<   6M   .+�5�      �� .+�5�      �� $��5 20 0 0q�6)   $ 
Horizontal=�6;   .+�5�   "   �� $��5q�6(   $$��..\Images/Assets/Help - 02.png��63   6^   �� 5�   "   $��Center=�6-   $��17(�6H   9 �� TextAlignment$9 Center=�6V   $	��241��6m   $��Showcard Gothic)�6y   You wanna go to system ?6�   5�      �� 5�      $��0 5 0 0q�6)   $ 
Horizontal=�6:   .+�5�   "   �� $��90 0 0 0q�5�      $
��Left=�6/   ���	#FF53E453$��60��6a   $��60��6l   #���  6x   ��5�      �� *ShowMenuCommand+.��5�   &   �� $#��..\Images/Assets/Check-01.png��6,   $��30��6S   $��30��6_   6l   5�   #   �� 5�   "   $��20 0 0 0q�5�      $
��Left=�60   
���  ��$��60��6]   $��60��6h   #���  6t   ��5�      �� *	CLickNo+.��5�   &   �� $!��..\Images/Assets/Cancel.png��6,   $��30��6Q   $��30��6]   6j   5�   #   5�      5�      5�      5�      5�      5�      5�      5�      