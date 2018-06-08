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
using CommunicationManage.Util;
using Windows.UI.Popups;

// https://go.microsoft.com/fwlink/?LinkId=234238 上介绍了“空白页”项模板

namespace CommunicationManage
{
    /// <summary>
    /// 可用于自身或导航至 Frame 内部的空白页。
    /// </summary>
    public sealed partial class AddContactPage : Page
    {
        public AddContactPage()
        {
            this.InitializeComponent();
        }

        private async void confirm_Click(object sender, RoutedEventArgs e)
        {
            if(await Management.CreateContact(name.Text, province.Text, city.Text, adress.Text, postalcode.Text, phone.Text, email.Text) == 0)
            {
                var dialog = new MessageDialog("");
                dialog.Content = "在向本地文件写入新数据时出现异常";
                await dialog.ShowAsync();
            }
            else
                MainPage.mp.main.Navigate(typeof(ShowPage));
        }

        private void cancel_Click(object sender, RoutedEventArgs e)
        {
            addpage.GoBack();
        }
    }
}
