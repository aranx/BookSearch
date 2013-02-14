using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Xml;
using System.Collections.ObjectModel;
using System.Collections;

namespace BookSearch
{
    /// <summary>
    /// MainWindow.xaml 的交互逻辑
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void btSearch_Click(object sender, RoutedEventArgs e)
        {
            
            ObservableCollection<Book> result = GetData(Getpage(tbName.Text));
            //lvDatas.DataContext = this;
            //lvDatas.Items.Clear();
            lvDatas.ItemsSource = result;
        }
        public XmlDocument Getpage(string param)
        {
            string requestUrl = "http://api.douban.com/book/subjects?tag=" + param + "&start-index=1";
            System.Net.WebRequest wReq = System.Net.WebRequest.Create(requestUrl);
            System.Net.WebResponse wResp = wReq.GetResponse();
            System.IO.Stream respStream = wResp.GetResponseStream();
            string reader = (new System.IO.StreamReader(respStream, System.Text.Encoding.UTF8)).ReadToEnd();
            XmlDocument docXml = new XmlDocument();
            docXml.LoadXml(reader);
            //string str = docXml.ChildNodes[1].InnerText;
            return docXml;

        }
        public ObservableCollection<Book> GetData(XmlDocument docXml)
         {
                    
             ObservableCollection<Book> books = new ObservableCollection<Book>();
             foreach (XmlNode node in docXml.ChildNodes)
             {
                 if (node.Name == "feed")
                 {
                     foreach (XmlNode childnode in node.ChildNodes)
                     {
                         //string name = docXml.DocumentElement.Attributes[0].Value;
                         if (childnode.Name == "entry")
                         {
                             Book book = new Book();
                             foreach (XmlNode childNode in childnode.ChildNodes)
                             {

                                 if (childNode.Name == "title")
                                 {
                                     book.BookName = childNode.InnerText;

                                 }
                                 else if (childNode.Name == "author")
                                 {
                                     book.Author = childNode.InnerText;

                                 }
                                 else if (childNode.Name=="db:attribute")
                                 {
                                     XmlElement element =(XmlElement)childNode;
                                     string name = element.GetAttribute("name");
                                     if (name == "isbn10")
                                     {
                                         book.ISBN = childNode.InnerText;
                                     }
                                     else if (name == "price")
                                     {
                                         book.Price = childNode.InnerText;
                                     }
                                     else if (name == "publisher")
                                     {

                                         book.Publisher = childNode.InnerText;
                                     }
                                     else if (name == "pubdate")
                                     {

                                         book.PubDate = childNode.InnerText;
                                     }

                                 }

                             }
                             books.Add(book);

                         }
                     }

                 }
     
             }
             return books;
             
         }
    }
   
}
