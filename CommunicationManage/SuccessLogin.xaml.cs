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
using Windows.UI.Xaml.Media.Imaging;
using Windows.Storage;
using System.Threading.Tasks;

// https://go.microsoft.com/fwlink/?LinkId=234238 上介绍了“空白页”项模板

namespace CommunicationManage
{
    /// <summary>
    /// 可用于自身或导航至 Frame 内部的空白页。
    /// </summary>
    public sealed partial class SuccessLogin : Page
    {
        ApplicationDataContainer localSettings = Windows.Storage.ApplicationData.Current.LocalSettings;
        private static string imgpath;

        public SuccessLogin()
        {
            this.InitializeComponent();
            this.username.Text = Management.getid();
            this.count.Text = Management.getcount();
            avatarpaint();
        }


        private async Task avatarpaint()
        {
            if ((string)localSettings.Containers[username.Text].Values["avatar"] != "null")               //判断是否有头像,有则填充头像图像
            {
                imgpath = localSettings.Containers[username.Text].Values["avatar"].ToString();
                ImageBrush brush = new ImageBrush();
                BitmapImage bitmap = new BitmapImage();
                Windows.Storage.StorageFolder storageFolder = Windows.Storage.ApplicationData.Current.LocalFolder;
                Windows.Storage.StorageFile sf =(StorageFile)await storageFolder.GetItemAsync(imgpath);
                using(var stream = await sf.OpenAsync(FileAccessMode.ReadWrite))
                {
                   bitmap.SetSource(stream);
                }
                brush.ImageSource = bitmap;
                Avator.Fill = brush;
            }
        }

        private void logout_Click(object sender, RoutedEventArgs e)
        {
            Management.Logout();
            MainPage.mp.main.Navigate(typeof(PleaseLogin));
        }

        private void edit_Click(object sender, RoutedEventArgs e)
        {
            MainPage.mp.main.Navigate(typeof(SettingPage));
        }

        private async void Button_Click(object sender, RoutedEventArgs e)
        {
            /*参考文档: https://docs.microsoft.com/zh-cn/windows/uwp/files/quickstart-using-file-and-folder-pickers */
            var picker = new Windows.Storage.Pickers.FileOpenPicker();         
            picker.ViewMode = Windows.Storage.Pickers.PickerViewMode.Thumbnail;
            picker.SuggestedStartLocation =
                Windows.Storage.Pickers.PickerLocationId.PicturesLibrary;
            picker.FileTypeFilter.Add("*");
            Windows.Storage.StorageFolder storageFolder = Windows.Storage.ApplicationData.Current.LocalFolder;
            Windows.Storage.StorageFile imagefile = await storageFolder.CreateFileAsync("avatar", CreationCollisionOption.ReplaceExisting);
            imagefile = await picker.PickSingleFileAsync();
            if(imagefile != null)                                            //若选中了图片，则将该图片填充头像，并保存图像地址
            {
                imgpath = imagefile.Path;
                localSettings.Containers[username.Text].Values["avatar"] = imgpath;
                ImageBrush brush = new ImageBrush();
                BitmapImage bitmap = new BitmapImage();
                using (var stream = await imagefile.OpenAsync(FileAccessMode.ReadWrite))         //需要使用文件流才能访问storagefile图片路径
                {
                    bitmap.SetSource(stream);
                }
                brush.ImageSource = bitmap;
                Avator.Fill = brush;
            }
        }
    }
}
