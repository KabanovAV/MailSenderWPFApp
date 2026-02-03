using MailDemo.Services;
using MailSender.Entities;
using MailSenderTest.MVVM.ViewModel;
using Microsoft.Extensions.Configuration;
using Microsoft.Win32;
using System.IO;
using System.Windows;
using System.Windows.Controls;

namespace MailSender
{
    public partial class MainWindow : Window
    {
        //private readonly MailService? _service;
        //private string _to = string.Empty;
        //private string _subject = string.Empty;
        //private string[]? _files;

        //private bool IsSendMailEnable
        //    => !string.IsNullOrEmpty(_to) && !string.IsNullOrEmpty(_subject) && (_files != null);

        public MainWindow()
        {
            InitializeComponent();

            IConfiguration config = new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile("appsettings.json")
            .Build();

            MailSettings settings = new();
            config.GetSection("MailSettings").Bind(settings);

            DataContext = new ApplicationViewModel(new MailService(settings));            

            //SendMailButton.IsEnabled = IsSendMailEnable;
        }

        private async void SendMailButton_Click(object sender, RoutedEventArgs e)
        {
            //DisableElements();
            //await _service.ConnectAsync();

            //var incrementValue = 100 / _files.Length;
            //foreach (string file in _files)
            //{
            //    MailData message = new()
            //    {
            //        DisplayName = _to,
            //        To = _to,
            //        Subject = _subject,
            //        Attachment = new AttachmentData(file)
            //    };
            //    var status = await _service.SendAsync(message);
            //    LogListView.Items.Add(status);
            //    ProgressSendMail.Value += incrementValue;
            //    await Task.Delay(500);
            //}
            //await _service.DisconnectAsync();
            //EnableElements();
        }

        private void DisableElements()
        {
            //if (ProgressSendMail.Value != 0)
            //{
            //    ProgressSendMail.Value = 0;
            //    LogListView.Items.Clear();
            //}
            //ToTextBox.IsEnabled = false;
            //SubjectTextBox.IsEnabled = false;
            ////ChooseFolderButton.IsEnabled = false;
            //SendMailButton.IsEnabled = false;
        }

        private void EnableElements()
        {
            //ToTextBox.IsEnabled = true;
            //SubjectTextBox.IsEnabled = true;
            //ChooseFolderButton.IsEnabled = true;
            //_files = null;
            //SendMailButton.IsEnabled = IsSendMailEnable;
        }
    }
}