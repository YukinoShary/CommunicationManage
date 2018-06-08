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
    public sealed partial class EditPage : Page
    {
        private static string oldname;                                      //用于记录原有的姓名，以防止改变姓名后无法查找字典
        public EditPage()
        {
            this.InitializeComponent();
        }

        /// <summary>
        /// 重写该方法接收导航传来的参数
        /// 参考文档 https://blog.csdn.net/maple__leaves/article/details/50565229
        /// </summary>
        /// <param name="e"></param>
        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);
            Contacts contact = (Contacts)e.Parameter;
            this.edname.Text = contact.Name;
            this.edphone.Text = contact.Phone;
            this.edpostalcode.Text = contact.PostalCode;
            this.edprovince.Text = contact.Province;
            this.edcity.Text = contact.City;
            this.edadress.Text = contact.Adress;
            this.edemail.Text = contact.Email;
            oldname = contact.Name; 
        }

        private async void edconfirm_Click(object sender, RoutedEventArgs e)
        {
            Contacts edcontact = new Contacts();
            edcontact.Name = edname.Text;
            edcontact.Phone = edphone.Text;
            edcontact.Email = edemail.Text;
            edcontact.PostalCode = edpostalcode.Text;
            edcontact.Province = edprovince.Text;
            edcontact.City = edcity.Text;
            edcontact.Adress = edadress.Text;
            if(await Management.EditContact(edcontact, oldname)==0)
            {
                var dialog = new MessageDialog("");
                dialog.Content = "在修改本地数据时出现异常";
                await dialog.ShowAsync();
            }
            MainPage.mp.main.Navigate(typeof(ShowPage));
        }

        private void edcancel_Click(object sender, RoutedEventArgs e)
        {
            Frame.GoBack();
        }
    }
}
