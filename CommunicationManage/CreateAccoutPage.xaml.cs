using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;
using Windows.Storage;
using System.Collections.ObjectModel;
using Windows.UI.Popups;
using CommunicationManage.Util;

// https://go.microsoft.com/fwlink/?LinkId=234238 上介绍了“空白页”项模板

namespace CommunicationManage
{
    /// <summary>
    /// 可用于自身或导航至 Frame 内部的空白页。
    /// </summary>
    public sealed partial class CreateAccoutPage : Page
    {
        ApplicationDataContainer localSettings = Windows.Storage.ApplicationData.Current.LocalSettings;  //获取本地应用设置容器(单例)
        public CreateAccoutPage()
        {
            this.InitializeComponent();
        }

        private async void createbutton_Click(object sender, RoutedEventArgs e)
        {
            var dialog = new MessageDialog("");
            if (!localSettings.Containers.ContainsKey(newaccount.Text))
            {
                if (newpassword.Password == checkpassword.Password && newpassword.Password != null)
                {
                    /*example: localSettings.Containers["accountContainer"].Values["exampleSetting"] = "Hello Windows";*/
                    /*此处为方便处理，违规地将应用程序数据容器API ApplicationDataContainer 作为存储用户账号密码以及头像地址的存储容器使用*/
                    Windows.Storage.ApplicationDataContainer container = localSettings.CreateContainer(newaccount.Text, Windows.Storage.ApplicationDataCreateDisposition.Always); //创建新账号容器
                    if (localSettings.Containers.ContainsKey(newaccount.Text))
                    {
                        localSettings.Containers[newaccount.Text].Values["password"] = newpassword.Password;
                        localSettings.Containers[newaccount.Text].Values["avatar"] = "null";
                        if (await Management.NewAccount(newaccount.Text) == 1)
                        {
                            dialog.Content = "注册成功";
                            await dialog.ShowAsync();
                        }
                        else
                        {
                            localSettings.DeleteContainer(newaccount.Text);
                            dialog.Content = "在创建本地文件时出现错误,未能成功创建账号";
                            await dialog.ShowAsync();
                        }
                    }
                    else
                        throw new Exception(String.Format("Key {0} was not found", newaccount.Text));
                }
                else
                {
                    dialog.Content = "两次密码输入不一致";
                    await dialog.ShowAsync();
                }
            }
            else
            {
                dialog.Content = "账号已存在";
                await dialog.ShowAsync();
            }
        }
    }
}
