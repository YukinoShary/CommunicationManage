using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.Storage;
using Windows.UI.Popups;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;
using CommunicationManage.Util;

// https://go.microsoft.com/fwlink/?LinkId=234238 上介绍了“空白页”项模板

namespace CommunicationManage
{
    /// <summary>
    /// 可用于自身或导航至 Frame 内部的空白页。
    /// </summary>
    public sealed partial class LoginPage : Page
    {
        ApplicationDataContainer localSettings = Windows.Storage.ApplicationData.Current.LocalSettings;
        public LoginPage()
        {
            this.InitializeComponent();
        }

        private async void loginbutton_Click(object sender, RoutedEventArgs e)
        {
            var dialog = new MessageDialog("");
            if(localSettings.Containers.ContainsKey(account.Text))
            {
                if((string)localSettings.Containers[account.Text].Values["password"] == password.Password)
                {
                    if (await Management.LoadContactsList(account.Text) == 0)
                    {
                        dialog.Content = "在读取文件时出现错误";
                        await dialog.ShowAsync();
                    }
                    else if(await Management.LoadContactsList(account.Text) == 1)
                    {
                        MainPage.mp.main.Navigate(typeof(SuccessLogin));
                        MainPage.islogin = true;
                    }
                }
                else
                {
                    dialog.Content = "密码错误";
                    await dialog.ShowAsync();
                }
            }
            else
            {
                dialog.Content = "该账号不存在";
                await dialog.ShowAsync();
            }
        }
    }
}
