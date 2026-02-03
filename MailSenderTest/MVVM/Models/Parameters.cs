namespace MailSenderTest.MVVM.Models
{
    public class Parameters : NotifyPropertyChanged
    {
        private decimal _progress;

        public decimal Progress
        {
            get => _progress;
            set
            {
                _progress = value;
                OnPropertyChanged("Progress");
            }
        }
    }
}
