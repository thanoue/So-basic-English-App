   M S B A M L   `   `   `  ����I  ESoBasicEnglish, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null;"clr-namespace:SoBasicEnglish.ViewsSoBasicEnglish.Views  5   	   f bMahApps.Metro.SimpleChildWindow, Version=1.4.0.0, Culture=neutral, PublicKeyToken=bbbfdff7f500a992zVclr-namespace:MahApps.Metro.SimpleChildWindow;assembly=MahApps.Metro.SimpleChildWindowMahApps.Metro.SimpleChildWindow 5   	   U QMahApps.Metro, Version=1.5.0.23, Culture=neutral, PublicKeyToken=f4fb5a3c4d1e5b4fV;clr-namespace:MahApps.Metro.Controls;assembly=MahApps.MetroMahApps.Metro.Controls 5   	   '    !SoBasicEnglish.Views.SignUpWindow   5      \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35^ ZWindowsFormsIntegration, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   4local"clr-namespace:SoBasicEnglish.Views     5   	   tsimpleChildWindowVclr-namespace:MahApps.Metro.SimpleChildWindow;assembly=MahApps.Metro.SimpleChildWindow     5   	   PControls;clr-namespace:MahApps.Metro.Controls;assembly=MahApps.Metro     5   	   -   5        (  "MahApps.Metro.Controls.MetroWindow   Name$  signUpWindow��5	       � WindowStartupLocation$ CenterScreen=�5
   	   
������� � Title$ SignUpScreen��5   
   $��552.348��6   $��586.179��60   .��5      � .+�5   
   �  � ColumnDefinitions 5      �� 5      $��* �6#   �� 5      $
��1.5* �6#   .+�5      � $��0��6   $��0 60 0 0q�6#    � RowDefinitions 5      �� 5      �� 5      $
��0.8* �6$   .+�5      � $��0,10,0,0q�6    5      �� 5      $��1* �6(   �� 5      $	��.2* �6(   .+�5      �� $
��Fill=�6D   ��6   �� *	UserAvt+ �� Mode$ OneWay=� J� Clip 5       @� 5!       @� Center$ 120,100&�6.    @� RadiusX$	 100Q�6?   	 @� RadiusY$		 100Q�6M   5#      �� 5$      -   5        
 ��Name$
 btnAVT��5&      $��1��5$      $��Choose your AVT..��5&   )   $��22,9.6,18,0.4q�6E    J� RenderTransformOrigin$ 0.495,0.902&�6\   ��5%      �� *SelectIimage+5&   \   5'      5(      � 5)      $��1��6   $��	10 0 10 0q�6#   .+�5*      �� -   5         ��Name$ 	stpSignUp��5*      .+�5+      �� $��	20 40 0 0q�6    $��Login name: ...��63   �� 5,      -   5         ��Name$ txtUserLoginName��5.      $��	10 5 10 0q�5-      $��20(�50      ��5/      �� *UserLoginName+$ OneWayToSource=�.��50   #   ��51      O Kmscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e0891&clr-namespace:System;assembly=mscorlibSystem 52   9   �� 6   6sys&clr-namespace:System;assembly=mscorlib     69    �� 
TargetType" ��6$    �� 	Resources 53   "      CueBannerBrush j� 
AlignmentX j� 
AlignmentY j� Stretch !� Visual��%�   &	        !� 54   &   $
 Left=�6I   $ Center=�6[   $
 None=�6o    55   *   �� 56   .   $��Italic$�68   $��10(�6K   $��At least 8 chars...��6Y   
�������6t   58   '   59   #    �� Triggers 5:   "   P� 5;   &    P� Property � �� �� Empty P� Value# Z 6>   . �5<   *   ��  �� Property � �� �� Value# [  6G   5=   '   P� 5>   &    � �� 6>   Z� . �5?   *   ��  � ��# [  6G   5@   '   P� 5A   &    �@J�IsKeyboardFocused . . �5B   *   ��  � ��
 �����6G   5C   '   5E      .��5F   )   5G      �� 5H      $��	20 10 0 0q�6    $��Password: ...��63   G� 5I      -   5         G�Name$ txtUserLoginpPassword��5L      *  $MahApps.Metro.Controls.TextBoxHelper   	Watermark$% make it diffcult to remember...��5J      $��	10 5 10 0q�5K      $��20(�5M      5O      �� 5P      -   5         ��Name$ tbRetypePassWord��5P   3   $��	20 10 0 0q�6    $��Retype your password: ...��6M   G� 5Q      -   5        $! txtUserLoginpPasswordRetype��5S      $��	10 5 10 0q�5R      $��20(�5T      �� 5U      $��	20 10 0 0q�6    $��
Email: ...��63   �� 5V      -   5        $ txtEmail��5X      $��	10 5 10 0q�5W      $��20(�5Y      .��6#   ��5Z      1&clr-namespace:System;assembly=mscorlibSystem 5[   $   �� 6   6sys&clr-namespace:System;assembly=mscorlib     6$   " ��6W    5\   "   ��%�   &	        !� 5]   &   $
 Left=�6I   $ Center=�6[   $
 None=�6o    5^   *   �� 5_   .   $��Italic$�68   $��10(�6K   $��Sould be a Gmail...��6Y   
�������6t   5a   '   5b   #    5c   "   P� 5d   &    � ��# Z 6>   . �5e   *   ��  � ��# [  6G   5f   '   P� 5g   &    � �� 6>   Z� . �5h   *   ��  � ��# [  6G   5i   '   P� 5j   &    �@J�IsKeyboardFocused . . �5k   *   ��  � ��
 �����6G   5l   '   5n      .��5o   )   5p      �� 5q      $��	20 10 0 0q�6    $��Full name: ...��63   �� 5r      -	   5        $ txtUserFullName��5t      $��	10 5 10 0q�5s      $��20(�5u      .��6#   ��5v      1&clr-namespace:System;assembly=mscorlibSystem 5w   $   �� 6   6sys&clr-namespace:System;assembly=mscorlib     6$   " ��6W    5x   "   ��%�   &	        !� 5y   &   $
 Left=�6I   $ Center=�6[   $
 None=�6o    5z   *   �� 5{   .   $��Italic$�68   $��10(�6K   $��At least 10 Chars...��6Y   
�������6u   5}   '   5~   #    5   "   P� 5�   &    � ��# Z 6>   . �5�   *   ��  � ��# [  6G   5�   '   P� 5�   &    � �� 6>   Z� . �5�   *   ��  � ��# [  6G   5�   '   P� 5�   &    �@J�IsKeyboardFocused . . �5�   *   ��  � ��
 �����6G   5�   '   5�      .��5�   )   5�      �� 5�      $��20 8 0 0q�6    $��Detail infomation: ...��62   �� 5�      -
   5        $ txtBasicInfo��5�      $��	10 5 10 0q�5�       �� TextWrapping$
 Wrap=�5�      $��20(�5�      $	��108��6#   ���	#FF0D183A���	#FF604646 �� 
LineHeight$ 10��6`   .��6v   5�      �� 5�      $��18(�5�      $��	10 7 10 0q�6#   $��
Sign Up!!!��66   ��5�      �� *SignUpCommand+��5�      �� *
Children+ �� ElementName$ 	stpSignUp��5�   6   5�      5�      5�      #  MahApps.Metro.Controls.Flyout  5�   
   -   5           Name$  flChampions��5�   q   $
��auto��6   $y�	Collapsed=�6E   $��0q�6]   
���   �$��245,10,21,450.4q�6�   !   Position$! Right=�6�   "   TitleVisibility$" Hidden=�6�   #   CloseButtonVisibility$# Hidden=�6�   $��Right=�6�   $   IsOpen$ 6'   �� *
IsOpenNoti+��5�      �� 5�      % �� Color$% Transparent��6"   & �� Opacity$	& 0.7Q�66   6F   .��5�      �� $
��auto��6   ' �� CornerRadius$' 20��6"   
����p��$��1q�6I   $��	0,0,0,0.4q�6]   ��5�      �� 5�      $% Indigo��6&   $	& 0.8Q�65   6E   ._�5�      � $
��auto��6   $��10q�6$   .+�5�      �� $
��auto��6!   ( �� Orientation$( 
Horizontal=�6.   .+�5�      �� $��5q�6    $!��..\Images/Assets/Cancel.png��6+   6S   �� 5�      $
��Bold"�6$   $	��250��66   
�������$��5 0 0 0q�6]   $��Center=�6n   $��15(�6�   $4��./SoBasicEnglish;component/Fonts/#Lato Hairline)�6�   $
��Wrap=�6�   ��6�   �� *NotiMessage+6  5�      5�      5�      5�      � 5�   
   .+�5�      1  +MahApps.Metro.SimpleChildWindow.ChildWindow  -   5        )  Name$) child01��5�   ,   *   Title$* TestChild 1��5�   "   +   ShowTitleBar+ .  ,   ShowCloseButton, .  -   CloseByEscape- . .   CloseOnOverlay. . /   IsOpen/ 5�   (   �� *OpenDiaglog+$ OneWay=�.��5�      � 
����+��$	��150��60   $	��220��6=   .+�5�      �� .+�5�      �� $��5,20,10.4,0q�6%   $( 
Horizontal=�6:   .+�5�      �� $��5q�6$   $$��..\Images/Assets/Help - 02.png��6/   6Z   �� 5�      $��Center=�6)   $��25(�6D   0 �� TextAlignment$0 Center=�6R   $	��152��6i   $��Showcard Gothic)�6u   $��27��6�   	Success!!6�   5�      �� 5�      $��0 5 0 0q�6%   $( 
Horizontal=�66   .+�5�      �� 1 � ToolTip$1 Go to the Login Page��6%   $��40 0 0 0q�5�      $
��Left=�6/   
���"�"�$��60��6c   $��60��6n     MetroCircleButtonStyle#���  6z   ��5�      �� *CloseWindowCommand+$ OneWay=���5�   %   �� $ signUpWindow��.��5�   "   �� $#��..\Images/Assets/Check-01.png��6(   $��30��6O   $��30��6[   6h   5�      �� 5�      $1 Create others User��5�      $��20 0 0 0q�5�      $
��Left=�60   
���  ��$��60��6]   $��60��6h   #���  6t   ��5�      �� *	CLickNo+.��5�   "   �� $!��..\Images/Assets/Cancel.png��6(   $��30��6M   $��30��6Y   6f   5�      5�      5�      5�      5�      5�      5�      5�      