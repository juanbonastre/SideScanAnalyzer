   M S B A M L   `   `   `  ����K  GSideScanAnalyzer, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null3clr-namespace:SideScanAnalyzerSideScanAnalyzer  5   	   I)clr-namespace:SideScanAnalyzer.ViewModelsSideScanAnalyzer.ViewModels  5   	   =#clr-namespace:SideScanAnalyzer.CoreSideScanAnalyzer.Core  5   ?   !    SideScanAnalyzer.MainWindow   5   	   W SPresentationCore, Version=6.0.2.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=6.0.2.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=6.0.2.0, Culture=neutral, PublicKeyToken=b77a5c561934e089R NWindowsBase, Version=6.0.2.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5   	   R NSystem.Xaml, Version=6.0.2.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   0localclr-namespace:SideScanAnalyzer     5   	   8VM)clr-namespace:SideScanAnalyzer.ViewModels     5   	   _ [Microsoft.Xaml.Behaviors, Version=1.1.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a3i+http://schemas.microsoft.com/xaml/behaviors  5   	   .core#clr-namespace:SideScanAnalyzer.Core  6?   $	��450��5
   3   $	��800��6@     � Title  6	   �� *ProjectInfo.ProjectName+ � DataContext 5      5   /SideScanAnalyzer.ViewModels.MainWindowViewModel  5   
    � 	Resources 5         BoolConverter3   -SideScanAnalyzer.Core.InverseBooleanConverter  InverseBoolConverter��%:   &	        &	      �� 5   
   6'     5   
   6'   5      *  $Microsoft.Xaml.Behaviors.Interaction   Triggers 5      +  %Microsoft.Xaml.Behaviors.EventTrigger  5   
      	EventName$ Closing��6   *  $Microsoft.Xaml.Behaviors.TriggerBase   Actions. 5      2  ,Microsoft.Xaml.Behaviors.InvokeCommandAction     PassEventArgsToCommand$
 True��6E      Command 6$   �� *CloseCommand+6E   5       J� InputBindings 5      �� 5   
   
	 �� Key$	 N��6   
 �� 	Modifiers$

 Ctrlf�6    �� Command 6.   �� *NewProjectCommand+�� 5   
   $	 O��6   $

 Ctrlf�6    6.   �� *OpenProjectCommand+�� 5   
   $	 X��6   $

 Ctrlf�6    6.   �� *LoadFileCommand+�� 5   
   $	 X��6   $
 Ctrl+Altf�6    62   �� *LoadFolderCommand+�� 5    
   $	 S��6   $

 Ctrlf�6    6.   �� *SaveCommand+�� 5!   
   $	 S��6   $
 Ctrl+Altf�6    62   �� *SaveAsCommand+�� 5"   
   $	 R��6   $

 Ctrlf�6    6.   �� *AnalyzeCommand+.��5%      ]� .+�5&   
   `� $	��Top=�6   .�5'      c� .@�5(      o� 
������ .@�5)      m� $��Archivo��6   .@�5*      m� $��Nuevo proyecto (Ctrl+N)��6#    m� Command 6D   �� *NewProjectCommand+m� 5+      $��Abrir proyecto (Ctrl+O)��6#    6D   �� *OpenProjectCommand+m� 5,      $��Guardar proyecto (Ctrl+S)��6#    6F   �� *SaveCommand+m� 5-      $(��"Guardar proyecto como (Ctrl+Alt+S)��6#    6O   �� *SaveAsCommand+m� 5.      $!��Cargar archivo XTF (Ctrl+X)��6#    6I   �� *LoadFileCommand+m� 5/      $"��Cargar acarpeta (Ctrl+Alt+X)��6#    6I   �� *LoadFolderCommand+51      m� 52      $��Proyecto��6   .@�53      m� $��Analizar Todo (Ctrl+R)��6#    6C   �� *AnalyzeCommand+m� 54      $��Exportar Análisis��6#    6>   �� *ExportAllAnalysisCommand+m� 55      $'��!Mostrar información del proyecto��6#    6M   �� *ShowProjectInfoCommand+56      57      �� 58      -   5        $
��TEST��58      $
��TEST��6%   ��64   �� *TestCommmand+59      5:      � 5;   
   $	��Top=�6    � ColumnDefinitions 5<      �� 5=      $��0.25* �6#   �� 5>       � RowDefinitions 5@      �� 5A      �� 5B      $
��30px �6    .+�5D      �� $��0��6   $��Disabled=�6+   $
��Auto=�6T   y�6w   �� *ProjectInfo.ProjectLoaded+ �� 	Converter# [  .��5E      �� -   5        $��XTFItemsControl��5E      ��66   �� *XTFFilesList+5F      �� 5G      $��1��6   $��Disabled=�6+   $
��Auto=�6T   y�6w   �� *ProjectInfo.ProjectLoaded+# [  .��5H      �� -   5        $��XTFImageViewer��5H      ��6.   �� *XTFImageSource+5I      �� 5J      -   5        $��Label��5J   A   $��Bottom=�6   $��2��6,   $��Center=�6N   $��Center=�6k   y�6�   �� *ProjectInfo.ProjectLoaded+# [ .��6�   CACrea un nuevo proyecto (Ctrl+N) o abra uno (Ctrl+O) para comenzar5L      � 5M      ���#EEEE$��1��6'   $��0��64   $��2��6D   .+�5N      �� -   5        $��StatusLabel��5N      $��Center=�6L   ��6+   �� *StatusString+6L   5O      5P      5Q      5R      