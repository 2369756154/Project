   M S B A M L   `   `   `  ����O  Kmscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e0891&clr-namespace:System;assembly=mscorlibSystem  5   	   M ISystem, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089A-clr-namespace:System.IO.Ports;assembly=SystemSystem.IO.Ports 6?   a ]PresentationFramework.Aero, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35gJclr-namespace:Microsoft.Windows.Themes;assembly=PresentationFramework.AeroMicrosoft.Windows.Themes 6{    
Mvvm-test1   Mvvm_test1.Start   5   	   \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   0ignorehttp://www.galasoft.ch/ignore     5   	   9System&clr-namespace:System;assembly=mscorlib     5   	   ?Ports-clr-namespace:System.IO.Ports;assembly=System     6?   ]ThemesJclr-namespace:Microsoft.Windows.Themes;assembly=PresentationFramework.Aero     6{     � Title$  参数设定��5   !   $	��400��6.   $	��300��6:   ���	#FF2C3AC5 � 
ResizeMode$ CanMinimize=�6^    � Icon$ Image/SerialPort.png��6w   .��5	      �  � RowDefinitions 5
   
   �� 5      $��31 �6   �� 5      $��31 �6   �� 5      $��31 �6   �� 5      $��31 �6   �� 5      $��31 �6   �� 5      $��* �6    � ColumnDefinitions 5   
   �� 5      $	��100 �6   �� 5      $	��100 �6   �� 5      $��* �6   .+�5   
   �� -   5         ��Name$ tb_PortName��5      $��5q�6)   $��	通讯口��65   $
��Left=�6@   $	��Top=�6[   $��16(�6s    �� TextAlignment$ Center=�6�   �� 5   
   -   5        $ tb_BaudRate��5      $��5q�6)   $��1��64   $��16(�6B   $��	波特率��6P   �� 5   
   -   5        $ tb_DataBits��5      $��5q�6)   $��2��64   $��16(�6B   $��	校验位��6P   �� 5   
   -   5        $ 	tb_Parity��5      $��5q�6'   $��3��62   $��16(�6@   $��奇偶校验��6N   �� 5   
   -   5        $ tb_StopBits��5      $��5q�6)   $��4��64   $��16(�6B   $��	停止位��6P   �� 5   
   -   5         ��Name$ comboBox��5      $��1��6%   $	��7,4q�65    �� SelectedIndex$ 0��6B   	 �� SelectedItem	 6T   �� *
portName+
 �� Mode$
 OneWayToSource=�.@�5      ��COM1��6"   ��5      COM2��6"   ��5      COM3��6"   ��5       COM4��6"   ��5!      COM5��6"   5#      �� 5$   
   -   5        $ 	comboBox1��5$      $��1��6&   $��1��66   $	��7,4q�6C   $ 0��6P   	 6b   �� *
baudRate+$
 OneWayToSource=�.@�5%      G�9600F�6"   G�5&      	19200F�6#   G�5'      	38400F�6#   G�5(      
115200F�6$   5)      �� 5*   
   -   5        $ 	comboBox2��5*      $��1��6&   $��2��66   $	��7,4q�6C   $ 0��6P   	 6b   �� *
dataBits+$
 OneWayToSource=�.@�5+      I�8H�6   I�5,      7H�6   5-      �� 5.   
   -	   5        $ 	comboBox3��5.      $��1��6&   $��3��66   $	��7,4q�6C   $ 1��6P   	 6b   �� *parity+$
 OneWayToSource=�.@�5/        System.IO.Ports.Parity None=�6!    50      Even=�6!    51      Odd=�6    52      �� 53   
   -
   5        $ 	comboBox4��53      $��1��6&   $��4��66   $	��7,4q�6C   $ 0��6P   
�������	 6u   �� *
stopBits+$
 OneWayToSource=�.@�54        System.IO.Ports.StopBits 1=�6     55      2=�6    57      �� 58   
   -   5         ��Name$ button��58      $��2��6!   $��Right=�61   $��34��6M   $��	0,0,10,10q�6Y   $��5��6l   $��确认��6y   $��Bottom=�6�   $��83��6�   ���	#FF1D9E2E���	#FFCFA929���	#FF080808��6�   �� *btn_openPort+6�   5:      5=      