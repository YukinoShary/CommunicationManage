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

// https://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x804 上介绍了“空白页”项模板

namespace CommunicationManage
{
    /// <summary>
    /// 可用于自身或导航至 Frame 内部的空白页。
    /// </summary>
    public sealed partial class MainPage : Page
    {

        public static bool islogin = false;   
        public static MainPage mp;

        public MainPage()
        {
            this.InitializeComponent();
            mp = this;
        }

        /// <summary>
        /// listview页面导航
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void IconListBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if(loginpage.IsSelected)
            {
                if(islogin == false)
                {
                    main.Navigate(typeof(LoginPage));
                }
                else
                {
                    main.Navigate(typeof(SuccessLogin));        
                }
            }
            else if(showpage.IsSelected)
            {
                if(islogin == false)
                {
                    main.Navigate(typeof(PleaseLogin));
                }
                else
                {
                    main.Navigate(typeof(ShowPage));
                }
            }
            else if(addcontactpage.IsSelected)
            {
                if(islogin == false)
                {
                    main.Navigate(typeof(PleaseLogin));
                }
                else
                {
                    main.Navigate(typeof(AddContactPage));
                }
            }
            else if(createaccountpage.IsSelected)
            {
                main.Navigate(typeof(CreateAccoutPage));
            }
            else if(settingpage.IsSelected)
            {
                if(islogin == false)
                {
                    main.Navigate(typeof(PleaseLogin));
                }
                else
                {
                    main.Navigate(typeof(SettingPage));
                }
            }
        }

        private void Hamburger_Click(object sender, RoutedEventArgs e)
        {
            sv.IsPaneOpen = !sv.IsPaneOpen;
        }
    }
}
