namespace MailSenderTest.MVVM.Models
{
    public class ToSetting : NotifyPropertyChanged
    {
        private string _to;
        private string _subject;
        private string _folderPath;

        public string To
        {
            get => _to;
            set
            {
                _to = value;
                OnPropertyChanged("To");
            }
        }

        public string Subject
        {
            get => _subject;
            set
            {
                _subject = value;
                OnPropertyChanged("Subject");
            }
        }

        public string FolderPath
        {
            get => _folderPath;
            set
            {
                _folderPath = value;
                OnPropertyChanged("FolderPath");
            }
        }
    }
}
