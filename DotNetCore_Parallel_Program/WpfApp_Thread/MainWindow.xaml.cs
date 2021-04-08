using System;
using System.Net.Http;
using System.Windows;

namespace WpfApp_Thread
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

        private async void Button_Click(object sender, RoutedEventArgs e)
        {
            var url = textBox.Text;
            var isValidUrl = Uri.TryCreate(url, UriKind.Absolute, out _);
            if (!isValidUrl)
            {
                textBlock.Text = "Given url is not valid.";
                return;
            }

            var httpClient = new HttpClient();
            var response = await httpClient.GetAsync(url);
            try
            {
                response.EnsureSuccessStatusCode();
                var data = await response.Content.ReadAsStringAsync();
                textBlock.Text = data;
            }
            catch (Exception ex)
            {
                textBlock.Text = ex.Message;
            }
        }
    }
}
