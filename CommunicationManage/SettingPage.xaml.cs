using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.Storage;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;
using CommunicationManage.Util;
using Windows.UI.Popups;

// https://go.microsoft.com/fwlink/?LinkId=234238 上介绍了“空白页”项模板

namespace CommunicationManage
{
    /// <summary>
    /// 可用于自身或导航至 Frame 内部的空白页。
    /// </summary>
    public sealed partial class SettingPage : Page
    {
        ApplicationDataContainer localSettings = Windows.Storage.ApplicationData.Current.LocalSettings;
        public SettingPage()
        {
            this.InitializeComponent();
        }

        private async void Button_Click(object sender, RoutedEventArgs e)
        {
            var dialog = new MessageDialog("");
            if(localSettings.Containers["accountContainers"].Values[Management.getid()] == oldpsw)
            {
                if(newpsw.Text == confirmpsw.Text)
                {
                    localSettings.Containers["accountContainers"].Values[Management.getid()] = newpsw;
                    dialog.Content = "密码修改成功";
                    await dialog.ShowAsync();
                }
                else
                {
                    dialog.Content = "两次密码不一致";
                    await dialog.ShowAsync();
                }
            }
            else
            {
                dialog.Content = "原密码输入错误";
                await dialog.ShowAsync();
            }
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            Frame.GoBack();
        }
    }
}
