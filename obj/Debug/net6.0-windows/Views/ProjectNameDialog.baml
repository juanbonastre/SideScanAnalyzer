   M S B A M L   `   `   `  ����K  GSideScanAnalyzer, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null?$clr-namespace:SideScanAnalyzer.ViewsSideScanAnalyzer.Views  5   	   .    (SideScanAnalyzer.Views.ProjectNameDialog   5   	   W SPresentationCore, Version=6.0.2.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=6.0.2.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=6.0.2.0, Culture=neutral, PublicKeyToken=b77a5c561934e089R NWindowsBase, Version=6.0.2.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5   	   R NSystem.Xaml, Version=6.0.2.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   6local$clr-namespace:SideScanAnalyzer.Views     5   	     � Title$$  Ingrese el nombre del proyecto��5   	    � SizeToContent$ WidthAndHeight=�60    � WindowStartupLocation$ CenterScreen=�6O   .��5
      � $��15q�6    � ColumnDefinitions 5   
   �� 5      $
��Auto �6   �� 5      $��* �6    � RowDefinitions 5   
   �� 5      $
��Auto �6   �� 5      $
��Auto �6   �� 5      $
��Auto �6   .+�5   
   �� $��1��6   .��6    Ingrese nombre del proyecto6=   �� 5   
   -   5        $��ProjectNameLabel��5      $��1��6*   $��1��6:   $	��250��6G   6X   � 5   
   $��2��6   $��2��6!   $��Right=�65   $��0,15,0,0q�6Q   .+�5      �� -   5        $��btnDialogOk��5   &    �� 	IsDefault$
 True��6   $��60��6S   $��0,0,10,0q�6a   .��6s   _Ok6x   �� 5      -   5        $��CancelButton��5       �� IsCancel$
 True��6)   $��60��69   .��6f   	_Cancel6o   5      5      5      