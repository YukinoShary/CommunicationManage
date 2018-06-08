using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml.Controls;
using System.IO;
using System.Runtime.Serialization;
using System.Xml;
using Windows.UI.Popups;
using Windows.Storage;
using System.Diagnostics;

namespace CommunicationManage.Util
{
    class Management
    {
        private static Dictionary<string, Contacts> contacts; //联系人泛型列表
        private static string currentaccount = null;

        /// <summary>
        /// 创建新联系人并添加进列表
        /// </summary>
        /// <param name="name"></param>
        /// <param name="province"></param>
        /// <param name="city"></param>
        /// <param name="adress"></param>
        /// <param name="postalcode"></param>
        /// <param name="phone"></param>
        /// <param name="email"></param>
        public static async Task<int> CreateContact(string name, string province, string city,
        string adress, string postalcode, string phone, string email)
        {
            Contacts c = new Contacts();
            c.Name = name;
            c.Province = province;
            c.City = city;
            c.Adress = adress;
            c.PostalCode = postalcode;
            c.Phone = phone;
            c.Email = email;
            contacts.Add(c.Name, c);                                    //向字典中添加数据
            return await SaveSerializeAsync();                         //覆盖存储本地文件
        }

        /// <summary>
        /// 修改类私有字典中的联系人数据并保存到本地
        /// </summary>
        /// <param name="ct"></param>
        /// <param name="old"></param>
        /// <returns></returns>
        public static async Task<int> EditContact(Contacts ct,string old)
        {
            contacts[old] = ct;
            return await SaveSerializeAsync();
        }

        /// <summary>
        /// 调用私有函数
        /// </summary>
        /// <param name="uid"></param>
        /// <returns></returns>
        public static async Task<int> LoadContactsList(string uid)
        {
            return await LoadSerializeAsync(uid);
        }

        /// <summary>
        /// 调用方传递listbox，此方法将字典contacts中的元素遍历输出到listbox中
        /// </summary>
        /// <param name="listBox"></param>
        public static void Display(ListBox listBox)
        {
            foreach(var item in contacts)
            {
                listBox.Items.Add(item.Key.ToString());   
            }
        }

        /// <summary>
        /// 传递账号名
        /// </summary>
        /// <returns></returns>
        public static string getid()
        {
            return currentaccount;
        }

        /// <summary>
        /// 传递联系人计数
        /// </summary>
        /// <returns></returns>
        public static string getcount()
        {
            return contacts.Count.ToString();
        }

        public static void Logout()
        {
            currentaccount = null;
            MainPage.islogin = false;
        }

        /// <summary>
        /// 联系人详细资料展示
        /// </summary>
        /// <param name="str"></param>
        /// <param name="frame"></param>
        public static void Showdetail(string str)
        {
            Debug.Write(contacts[str]);
            MainPage.mp.main.Navigate(typeof(EditPage),contacts[str]);               //将类私有成员作为参数传递到联系人详细资料展示
        }

        /// <summary>
        /// 将账号数据(Dictionary类)序列化并用流写入到硬盘
        /// </summary>
        private static async Task<int> SaveSerializeAsync()
        {
            Windows.Storage.StorageFolder storageFolder = Windows.Storage.ApplicationData.Current.LocalFolder;
            if (currentaccount != null)
            {
                try
                {
                    Stream fs = await Windows.Storage.ApplicationData.Current.LocalFolder.OpenStreamForWriteAsync(currentaccount + ".bat", CreationCollisionOption.ReplaceExisting);
                    using (XmlDictionaryWriter xdw = XmlDictionaryWriter.CreateTextWriter(fs, Encoding.UTF8))
                    {
                        DataContractSerializer dcs = new DataContractSerializer(typeof(Dictionary<string, Contacts>));
                        dcs.WriteObject(xdw, contacts);
                    }
                    return 1;
                }
                catch (Exception e)
                {
                    var dialog = new MessageDialog("");
                    dialog.Content = "Save Serialize Exception";
                    await dialog.ShowAsync();
                    return 0;
                }
            }
            else
                return 0;
        }

        /// <summary>
        /// 从硬盘读取并反序列化账号数据
        /// </summary>
        /// <param name="id"></param>
        private async static Task<int> LoadSerializeAsync(string uid)
        {
            currentaccount = uid;
            Windows.Storage.StorageFolder storageFolder = Windows.Storage.ApplicationData.Current.LocalFolder;
            try
            {
                Stream fs = await storageFolder.OpenStreamForReadAsync(currentaccount + ".bat");                                                   //获取本地文件对象的流写入对象
                using (XmlDictionaryReader reader = XmlDictionaryReader.CreateTextReader(fs, new XmlDictionaryReaderQuotas()))                    //using语句控制其生命周期
                {
                    DataContractSerializer dcs = new DataContractSerializer(typeof(Dictionary<string, Contacts>));
                    contacts = (Dictionary<string, Contacts>)dcs.ReadObject(reader);                                                          //获取到硬盘中的联系人数据
                }   
                return 1;
            }
            catch(Exception e)
            {
                var dialog = new MessageDialog("");
                dialog.Content = "Load Serialize Exception";
                await dialog.ShowAsync();
                return 0;
            }
        }

        /// <summary>
        /// 新账号数据保存
        /// 参考文档 https://jingyan.baidu.com/article/67508eb47a05499cca1ce430.html
        /// </summary> 
        /// <param name="id"></param>
        public async static Task<int> NewAccount(string uid)
        {
            if (uid != null)
            {
                Windows.Storage.StorageFolder storageFolder = Windows.Storage.ApplicationData.Current.LocalFolder;                 //调用API，获取本地存储文件夹对象
                Windows.Storage.StorageFile serializeFile = await storageFolder.CreateFileAsync(uid+".bat",CreationCollisionOption.ReplaceExisting);  //创建本地存储文件对象，模式为覆盖
                try
                {
                    Stream fs = await serializeFile.OpenStreamForWriteAsync();                                    //创建文件写入的数据流对象(异步方法)
                    using (XmlDictionaryWriter xdw = XmlDictionaryWriter.CreateTextWriter(fs, Encoding.UTF8))
                    {
                        DataContractSerializer dcs = new DataContractSerializer(typeof(Dictionary<string, Contacts>));
                        dcs.WriteObject(xdw, new Dictionary<string, Contacts>());
                    } 
                    return 1;                                                                                     //无异常，返回值为1
                }
                catch (Exception e)
                {
                    var dialog = new MessageDialog("");
                    dialog.Content = "Create New Accout Exception";
                    await dialog.ShowAsync();
                    return 0;                                                                 // 出现异常，返回值为0
                }
            }
            else
                return 0;
        }
    }
}
