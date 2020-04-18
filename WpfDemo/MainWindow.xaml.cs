using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace WpfDemo
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private async void BtnInsert_Click(object sender, RoutedEventArgs e)
        {
            string strFName = txtFName.Text.Trim();
            string strLName = txtLName.Text.Trim();
            int intAge = Convert.ToInt32(txtAge.Text.Trim());

            string httpBody = createJsonBody(strFName, strLName, intAge);

            await insertData(httpBody);
        }

        private static string createJsonBody(string fname, string lname, int age)
        {
            JsonBody jsonBody = new JsonBody()
            {
                FName = fname,
                LName = lname,
                Age = age
            };
            var body = JsonConvert.SerializeObject(jsonBody);
            return body.ToString();
        }

        private async Task<string> insertData(string httpBody)
        {
            {
                try
                {
                    using (var getclient = new HttpClient())
                    {
                        string apiurl;
                        apiurl = "http://localhost:port#/api/Function1"; //ConfigurationSettings.AppSettings["GetFnAppUrl"];
                        getclient.BaseAddress = new Uri(apiurl);
                        string address = getclient.BaseAddress.AbsoluteUri;
                        var body = new StringContent(httpBody, Encoding.UTF8, "application/json");
                        var response = await getclient.PostAsync(address, body);
                        if (response.IsSuccessStatusCode)
                        {
                            return response.Content.ReadAsStringAsync().Result;
                        }
                        else
                        {
                            return "Connectivity Issue";
                        }
                    }
                }
                catch (Exception e)
                {
                    return null;
                }
            }
        }

    }

    public class JsonBody
    {
        public string FName { get; set; }
        public string LName { get; set; }
        public int Age { get; set; }
    }
}
