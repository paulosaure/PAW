   M S B A M L   `   `   `  ����   PaintSurface!    PaintSurface.SurfaceWindow1   W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     e aMicrosoft.Surface.Presentation, Version=2.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e351s)http://schemas.microsoft.com/surface/2008    � Title$  PaintSurface��$
��1920��$
��1080��.��� -    �Name$
 main��$
��1920��$
��1080������  �� ImageSource$ /Resources/backgroud.jpg��.+�� -   $ myGrid��$y�Visible=�$��2��.+��� $��/Resources/rond.png��$	��360��$	��360��z���  �� CenterX$	 180Q� �� CenterY$	 180Q�����  �� Triggers P�  P� Property � {� P� Value .  N� EnterActions �� .���� .��W� 	 �� TargetProperty$	 RenderTransform.Angle�
 W� From$
 0X�	 W� To$	 360X� g� Duration$ 0:0:10D� g� RepeatBehavior$ Forever���� $��/Resources/roue.png��$	��500��$	��560��z��� $	 250Q�$	 250Q�����  P�  � {� .  �� .���� .��W� $	 RenderTransform.Angle�$	
 360X�$ 0X�$ 0:0:5D�$ Forever���� -   $��button��$	��400��$	��400�� J� RenderTransformOrigin$ 0.5,0.5&����� .R��� $��touchez��$��/Resources/text.png��$	��400��$	��400��z��� 
ImageScale�� �� ScaleX$ 1Q� �� ScaleY$ 1Q� �� CenterX$	 200Q� �� CenterY$	 150Q� � Triggers :�  :� RoutedEvent$ Image.Loaded��.[��� .���� .��W�  �� 
TargetName$ 
ImageScale��$	 (ScaleTransform.ScaleX)�$	 1.5X�$ 0:0:1D� g� AutoReverse . $ Forever��W� $ 
ImageScale��$	 (ScaleTransform.ScaleY)�$	 1.5X�$ 0:0:1D� . $ Forever��� -   $��maison��$y�Hidden=�$��2��.+��� -   $��t��$	��100��$	��100��$��42,20,1778,960q����� .R��� $��rotateFleche2��$��Resources/rotateImg.png��$ 0.5, 0.5&�z���  �� Angle$	 180Q��� -   $	��100��$	��100��$��1780,931,40,49q����� .R��� $��t2��$��Resources/rotateImg.png��$ 0.5, 0.5&��� -   $��consigneMaison��$��5,0,5,0q�
�������$��50(�$��Center=�Choisir une pièce��  �� Orientation$ 
Horizontal=�$
��auto��$
��auto��.+��� -   $��buttonCuisine��$	��350��$	��350��$��150,100,150,100q�z��� -	   cuisineScale��$ 1Q�$ 1Q�$	 200Q�$	 150Q����� .R��� $��/Resources/cuisine.jpg��$	��350��$	��350���� -
   $��buttonSalon��$	��350��$	��350��$��150,100,150,100q�z��� -   
salonScale��$ 1Q�$ 1Q�$	 200Q�$	 150Q����� .R��� $��/Resources/salon.jpg��$	��350��$	��350���� -   $��buttonSalledebain��$	��350��$	��350��$��150,100,150,100q�z��� -   salledebainScale��$ 1Q�$ 1Q�$	 200Q�$	 150Q����� .R��� $ ��/Resources/salledebain.jpg��$	��350��$	��350��� -   $��atelier��$y�Hidden=�$��10,54,-10,-54q�.+��� -   $	��100��$	��100��$��42,20,1778,960q����� .R��� $��Resources/rotateImg.png��$ 0.5, 0.5&�z��� $	 180Q��� -   $	��100��$	��100��$��1775,884,45,96q����� .R��� $��t2��$��Resources/rotateImg.png��$ 0.5, 0.5&��� .+��� -   $��consigneAtelier��$��	679,0,0,0q�
�������$��50(�$ 0.5,0.5&�$	��643��z�V� .~��� ��  �� AngleY$ 0.658Q��� T�  T� Y$ 2.974Q��� $�Que voulez-vous faire ?���  � RowDefinitions �� $	��300 ��� $	��300 � � ColumnDefinitions �� $
��Auto ��� $
��Auto �.+��� -    ��Name$ brosseadent��$	��350��$	��350��$��0��$��0��$��Center=�$��200,67,202,183q�$��2��z��� -   brosseadentScale��$ 1Q�$ 1Q�$	 200Q�$	 150Q����� .R��� $!��/Resources/brosse_grand.png���� -   $ brosseacheveux��$	��350��$	��350��$��1��$��0��$��546,67,-541,183q�$��2��z��� -   brosseacheveuxScale��$ 1Q�$ 1Q�$	 200Q�$	 150Q����� .R��� $#��/Resources/brosse_cheveux.png���� -   $ rasoir��$	��350��$	��350��$��0��$��1��$��200,184,203,-234q�z��� -   rasoirScale��$ 1Q�$ 1Q�$	 200Q�$	 150Q����� .R��� $��/Resources/rasoir.png���� -   $ douche��$	��350��$	��350��$��1��$��1��$��547,184,-541,-234q�z��� -   doucheScale��$ 1Q�$ 1Q�$	 200Q�$	 150Q����� .R��� $��/Resources/douche.png���� -   $��canvas��$
��1080��$
��1920��.+�� -   $��objet��$y�Hidden=�$��2��.+��� .+�� -    �Name$ aideTop��$	��200�� � Orientation$ 
Horizontal=�$��0,0,0,0q�.+��� -     ��Name$  borderVerre��$��4q�$��200,0,100,0q�._��� -   ! ��Name$! verre��$	��186��$	��186��$ 0.5, 0.5&�z��� $	 180Q��� -   $  borderDentifrice��$��4q�$��100,0,100,0q�._��� -    $! 
dentifrice��$	��186��$	��186��$ 0.5, 0.5&�z��� $	 180Q��� -!   $  borderAideBrosseDent��$��4q�$��100,0,100,0q�._��� -"   $! 
brosseDent��$	��186��$	��186��$ 0.5, 0.5&�z��� $	 180Q��� $��100,0,100,0q�
�������$��50(�$��Aide :��$	��200��$��Center=�$ 0.5, 0.5&�z��� $	 180Q��� -#   $  borderObjet��$��0,5,0,0q�
���   �$��5q�._�� $	��635��$
��1920��.+��� -$   " ��Name$
" text��
�������$ 0.5, 0.5&�$��Center=�$
��Left=�$��50(�$��Placer les objets ��$��24,23,0,560q��� -%   $" text2��
�������$ 0.5, 0.5&�$��Center=�$��Right=�$��50(�$��Placer les objets ��$��0,546,150,37q�z��� $	 180Q��  �� $	��81* ��� $
��244* �.+�;  5Microsoft.Surface.Presentation.Controls.TagVisualizer  -&   #  Name$# tagBrosseaDent��$	��649��$
��1920��$��2��$   Definitions$ H  BMicrosoft.Surface.Presentation.Controls.TagVisualizationDefinition  %   Source$% dentifrice.xaml?�&   Value<  6Microsoft.Surface.Presentation.Input.TagValueConverter$
& 0x31 '   OrientationOffsetFromTag$' 0Q�"(   PhysicalCenterOffsetFromTag$	( 0,0-�)   TagRemovedBehavior$) 	Disappear=�*   UsesTagOrientation* .   $
& 0x05 $% dentifrice.xaml?�$) 	Disappear=�$' 0Q�$	( 0,0-�* .   $
& 0x24 $% dentifrice.xaml?�$) 	Disappear=�$' 0Q�$	( 0,0-�* . � -'   $ aideBot��$	��200��$ 
Horizontal=�$��0,0,0,0q�.+��� 
�������$��50(�$	��200��$��Center=�$��200,0,100,0q��� $�Aide :���� -(   $  borderAideBrosseDent2��$��4q�$��100,0,100,0q�._��� -)   $! brosseDent2��$	��186��$	��186���� -*   $  borderDentifrice2��$��4q�$��100,0,100,0q�._��� -+   $! dentifrice2��$	��186��$	��186���� -,   $  borderVerre2��$��4q�$��100,0,100,0q�._��� --   $! verre2��$	��186��$	��186��� -.   $��ordonnancement��$y�Hidden=�+ �� Left$+ 10��
, �� Top$, 7�� �� $
��auto ��� $
��auto ��� $
��auto � �� $
��Auto �.+�� -/   $��	videoHaut��$y�Hidden=�$��0��$��0��$��127,0,-127,0q�.+��� -0   $��
QrcodeHaut��$	��138��$	��125��$��50,0,0,0q�r� -1   - r�Name$- media1��. r� LoadedBehavior$. Manual=�$	��200��$	��200��$��650,0,100,0q�/ � LayoutTransform/ V� .~��� $
 -180Q��� $��100,0,100,0q�
�������$��50(�$��Vidéo :��$	��200��$��Center=�$ 0.5, 0.5&�$	��168��z��� $	 180Q�� -2   $��	friseHaut��$��0��$��0��$��
0,-10,0,10q�.+��� -3   $��rotatefecheHaut��$	��100��$	��100������ .R��� $��rotateFleche��$��Resources/rotateImg.png��$ 0.5, 0.5&�z��� $	 180Q��� -4   $! fleche��$��0,0,30,0q�$	��200��$	��200��$��/Resources/flecheTop.png���� -5   $  borderbloc1��$��4q�$��	30,0,30,0q�._��� -6   $! bloc1��$��0,0,0,0q�$	��200��$	��200��$��/Resources/cubeTop.png��$
��Left=��� -7   $  borderbloc2��$��4q�$��	30,0,30,0q�._��� -8   $��bloc2��$	��200��$	��200��$��/Resources/cubeTop.png���� -9   $  borderbloc3��$��4q�$��	30,0,30,0q�._��� -:   $��bloc3��$	��200��$	��200��$��/Resources/cubeTop.png���� -;   $  borderbloc4��$��4q�$��	30,0,30,0q�._��� -<   $��bloc4��$	��200��$	��200��$��/Resources/cubeTop.png���� -=   $  borderbloc5��$��4q�$��	30,0,30,0q�._��� ->   $��bloc5��$	��200��$	��200��$��/Resources/cubeTop.png���� -?   $  borderbloc6��$��4q�$��	30,0,30,0q�._��� -@   $��bloc6��$	��200��$	��200��$ ��/Resources/cubeTopCoin.png��� -A   $��friseBas��$��0��$��2��$��0,638,-30,19q�.+��� -B   $  borderbloc7��$��4q�$��0,0,30,0q�._��� -C   $! bloc1B��$	��200��$	��200��$ ��/Resources/cubeBotCoin.png���� -D   $  borderbloc8��$��4q�$��	20,0,30,0q�._��� -E   $! bloc2B��$	��200��$	��200��$��/Resources/cubeBot.png���� -F   $  borderbloc9��$��4q�._��� -G   $! bloc3B��$	��200��$	��199��$��/Resources/cubeBot.png���� -H   $  borderbloc10��$��4q�$��	20,0,30,0q�._��� -I   $! bloc4B��$	��200��$	��200��$��/Resources/cubeBot.png���� -J   $  borderbloc11��$��4q�$��	20,0,30,0q�._��� -K   $! bloc5B��$	��200��$	��200��$��/Resources/cubeBot.png���� -L   $  borderbloc12��$��4q�$��	20,0,30,0q�._��� -M   $��bloc6B��$	��200��$	��200��$��/Resources/cubeBot.png���� -N   $! flecheB��$	��200��$	��200��$��/Resources/flecheBot.png��$��20,0,0,0q��� -O   $ rotatefecheBas��$	��100��$	��124������ .R��� $! rotateFleche��$��Resources/rotateImg.png��$ 0.5, 0.5&�� -P   $��videoBas��$y�Hidden=�$��0��$��2��$��57,638,-57,-88q�.+��� $��200,0,200,0q�
�������$��50(�$��Vidéo :��$	��200��$��Center=�$ 0.5, 0.5&�r� -Q   $- media2��$��200,0,200,0q�$. Manual=�$	��200��$	��200���� -R   $! 	QrcodeBas��$	��138��$	��125��$��	50,0,0,10q�