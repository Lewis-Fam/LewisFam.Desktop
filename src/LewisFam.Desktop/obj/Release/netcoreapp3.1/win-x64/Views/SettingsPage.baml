   M S B A M L   `   `   `  ���� L  HLewisFam.App.Core, Version=1.0.0.0, Culture=neutral, PublicKeyToken=nullfEclr-namespace:LewisFam.App.Core.Converters;assembly=LewisFam.App.CoreLewisFam.App.Core.Converters  ^Aclr-namespace:LewisFam.App.Core.Models;assembly=LewisFam.App.CoreLewisFam.App.Core.Models  fEclr-namespace:LewisFam.App.Core.Properties;assembly=LewisFam.App.CoreLewisFam.App.Core.Properties   LewisFam.App%   LewisFam.App.Views.SettingsPage   \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     \
convertersEclr-namespace:LewisFam.App.Core.Converters;assembly=LewisFam.App.Core     @d2http://schemas.microsoft.com/expression/blend/2008     _ [Microsoft.Xaml.Behaviors, Version=1.1.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a3i+http://schemas.microsoft.com/xaml/behaviors  Dmc;http://schemas.openxmlformats.org/markup-compatibility/2006  NmodelsAclr-namespace:LewisFam.App.Core.Models;assembly=LewisFam.App.Core  S OPrism.Wpf, Version=7.2.0.1422, Culture=neutral, PublicKeyToken=40ee6c3a2184dc59Y	 UPrism.Unity.Wpf, Version=7.2.0.1422, Culture=neutral, PublicKeyToken=40ee6c3a2184dc59N
 JPrism.DryIoc.Wpf, Version=7.2.0.1422, Culture=neutral, PublicKeyToken=null(prismhttp://prismlibrary.com/  	 
 Z
propertiesEclr-namespace:LewisFam.App.Core.Properties;assembly=LewisFam.App.Core  	 
 !  Prism.Mvvm.ViewModelLocator    AutoWireViewModel$
  True��   BaseUserControlStyle#��[   � 	Resources ?   9LewisFam.App.Core.Converters.BooleanToVisibilityConverter   BooleanToVisibilityConverter ' #ReverseBooleanToVisibilityConverter   Reverse9   3LewisFam.App.Core.Converters.EnumToBooleanConverter  EnumToBooleanConverter   EnumType'   !LewisFam.App.Core.Models.AppTheme��%=   &	       &	      &	          $
 True��  "  .���� .���   SmallTopBottomMargin#��[  � RowDefinitions �� $��48 ��� $��* �.+���   PageTitleStyle#��[ ,   &LewisFam.App.Core.Properties.Resources   SettingsPageTitle#��Z �� -    ��Name$ ContentArea��$��1��  MediumLeftRightMargin#��[ .+���   SmallTopMargin#��[   SubtitleTextStyle#��[ '    SettingsPagePersonalizationTitle#��Z �� #��[ .+���  	 BodyTextStyle#��[	 "   SettingsPageChooseThemeText#��Z ��  
 XSmallTopMargin#��[
 .+�	� (	   !SettingsPageRadioButtonLightTheme#��Z	   MediumFontSize#��[ 
 	� 	GroupName$
 AppTheme�� d� 	IsChecked �� *Theme+ �� 	Converter# [  �� ConverterParameter$ Light��*  $Microsoft.Xaml.Behaviors.Interaction   Triggers +  %Microsoft.Xaml.Behaviors.EventTrigger     	EventName$ Checked��*  $Microsoft.Xaml.Behaviors.TriggerBase   Actions. 2	  ,Microsoft.Xaml.Behaviors.InvokeCommandAction	   	  CommandParameter$ Light�� 	  Command �� *SetThemeCommand+	� #��[
 '    SettingsPageRadioButtonDarkTheme#��Z #��[ $
 AppTheme�� �� *Theme+# [ $
 Dark��   $ Checked��. 	  $
 Dark�� �� *SetThemeCommand+�� #��[    SettingsPageUserTitle#��Z �� #��[  �� Orientation$ 
Horizontal=�y��� *
IsLoggedIn+# [ .+�A� $��64��$��64������  j� Stretch$ UniformToFill=� �� ImageSource �� *
User.Photo+��   SmallLeftMargin#��[ .+��� #��[	 ���� *	User.Name+�� #��[
 #��[	 ���� *User.UserPrincipalName+�� #��[ y��� *
IsLoggedIn+# [ ��  �� Command �� *LogOutCommand+.�.��� #��[	 #   SettingsPageLogOutButtonText#��Z �� Settings_NoUserLoggedIn��#��[ #��[	 %   SettingsPageNoUserLoggedInText#��Z y��� *
IsLoggedIn+# [ �� #��[ y��� *
IsLoggedIn+# [ ��  �� *LogInCommand+.�.��� #��[	 "   SettingsPageLogInButtonText#��Z �� #��[ #��[    SettingsPageAboutTitle#��Z �� #��[
 #��[	 ���� *VersionDescription+ �� Mode$ OneWay=��� #��[
 #��[	    SettingsPageAboutText#��Z �� #��[ 4
  .System.Windows.Automation.AutomationProperties 
  Name �� *Text+  �� ElementName$  settingsPrivacyStatement����  �� *Text+$  settingsPrivacyStatement�� �� *PrivacyStatementCommand+.�.��� -   ! ��Name$! settingsPrivacyStatement��#��[	 '"    SettingsPagePrivacyStatementText#��Z" 