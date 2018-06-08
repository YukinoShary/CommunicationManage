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

// https://go.microsoft.com/fwlink/?LinkId=234238 上介绍了“空白页”项模板

namespace CommunicationManage
{
    /// <summary>
    /// 可用于自身或导航至 Frame 内部的空白页。
    /// </summary>
    public sealed partial class ShowPage : Page
    {
        public ShowPage()
        {
            this.InitializeComponent();
            Management.Display(contactslist);          //获取联系人列表，此方法在页面构造方法中直接生成完整的页面
        }

        private void addcontact_Click(object sender, RoutedEventArgs e)
        {
            MainPage.mp.main.Navigate(typeof(AddContactPage));
        }

        private void contactslist_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Management.Showdetail(contactslist.SelectedItem.ToString());               //点击列表中的一项，查看详细信息
        }
    }
}
