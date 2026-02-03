using MailDemo.Services;
using MailSender.Entities;
using MailSenderTest.MVVM.Commands;
using MailSenderTest.MVVM.Models;
using Microsoft.Win32;
using System.Collections.ObjectModel;
using System.IO;

namespace MailSenderTest.MVVM.ViewModel
{
    public class ApplicationViewModel
    {
        private readonly MailService _service;

        private ToSetting _toSetting;
        private Parameters _parameter;

        private RelayCommand _chooseFolder;
        private RelayCommand _sentMail;

        private ObservableCollection<string> Files;
        public ObservableCollection<StatusSent> Statuses { get; set; } = [];

        public ApplicationViewModel(MailService service)
        {
            _service = service;
            _toSetting = new ToSetting();
            _parameter = new Parameters();
        }

        public ToSetting ToSetting { get => _toSetting; }
        public Parameters Parameter { get => _parameter; }

        public RelayCommand ChooseFolder
        {
            get
            {
                return _chooseFolder ?? new RelayCommand(obj =>
                {
                    var openFolderDialog = new OpenFolderDialog();
                    bool? result = openFolderDialog.ShowDialog();

                    if (result == true)
                    {
                        string folderPath = openFolderDialog.FolderName;
                        Files = [.. Directory.GetFiles(folderPath)];
                        _toSetting.FolderPath = folderPath;
                    }
                });
            }
        }

        public RelayCommand SentMail { get => _sentMail ?? new RelayCommand(obj => OnSentMail()); }

        private async void OnSentMail()
        {
            await _service.ConnectAsync();

            decimal incrementValue = (decimal)100 / Files.Count;
            foreach (string file in Files)
            {
                MailData message = new()
                {
                    DisplayName = _toSetting.To,
                    To = _toSetting.To,
                    Subject = $"{_toSetting.Subject} ({file.Split('\\').Last()})",
                    Attachment = new AttachmentData(file)
                };
                var status = await _service.SendAsync(message);
                _parameter.Progress += incrementValue;
                Statuses.Insert(0, status);

                await Task.Delay(1000);
            }
            await _service.DisconnectAsync();
        }
    }
}
