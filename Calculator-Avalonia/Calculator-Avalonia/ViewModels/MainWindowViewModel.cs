using ReactiveUI;

namespace Calculator_Avalonia.ViewModels
{
    public class MainWindowViewModel : ViewModelBase
    {
        private string _value = string.Empty;
        private string _selectedOperation = string.Empty;
        private double? _number0;
        private double? _number1;

        public string Value
        {
            get { if (string.IsNullOrEmpty(_value)) { return "0"; } return _value; }
            set { this.RaiseAndSetIfChanged(ref _value, value); }
        }

        public string SelectedOperation
        {
            get { return _selectedOperation; }
            set { this.RaiseAndSetIfChanged(ref _selectedOperation, value); }
        }

        public double? Number0
        {
            get { return _number0; }
            set { this.RaiseAndSetIfChanged(ref _number0, value); }
        }

        public double? Number1
        {
            get { return _number1; }
            set { this.RaiseAndSetIfChanged(ref _number1, value); }
        }
    }
}