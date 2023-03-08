using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace Calculator.VMs
{
    class MainVM : INotifyPropertyChanged
    {
        private string _value;
        private string _selectedOperation;
        private string _firstNumber;
        private bool _error;

        public RelayCommand Reset { get; set; }
        public RelayCommand Point { get; set; }
        public RelayCommand Sign { get; set; }
        public ParametrizedRelayCommand Number { get; set; }
        public ParametrizedRelayCommand Operation { get; set; }


        public MainVM()
        {
            Value = "0";
            Number = new ParametrizedRelayCommand(
                (param) =>
                {
                    string newChar = param.ToString();
                    if ((Value == "0" && newChar == "0")|| Error)
                    {
                        Value = "0";
                        Error = false;
                    }
                    if (Value == "0" || Error)
                    {
                        Value = newChar;
                        Error = false;
                    }
                    else if (Value.EndsWith(".0") && newChar != "0")
                    {
                        Value = Value.Remove(Value.Length - 1, 1) + newChar;
                    }
                    else
                    {
                        Value += newChar;
                    }
                },
                (param) => true
                );
            Point = new RelayCommand(
                () =>
                {
                    Value += ".0";
                    Point.RaiseCanExecuteChanged();
                },
                () => (!Value.Contains("."))
                );
            Sign = new RelayCommand(
                () =>
                {
                    if (Value.StartsWith("-"))
                    {
                        Value = Value.Substring(1, Value.Length - 1);
                    }
                    else
                    {
                        Value = "-" + Value;
                    }
                },
                () => true
                );
            Operation = new ParametrizedRelayCommand(
                (param) =>
                {
                    SelectedOperation = param.ToString();
                    FirstNumber = Value;
                    Value = "0";
                    Point.RaiseCanExecuteChanged();
                },
                (param) => true
                );
            Play = new RelayCommand(
                () =>
                {
                    float number0;
                    float number1;
                    try
                    {
                        number0 = float.Parse(FirstNumber);
                        number1 = float.Parse(Value);
                    }
                    catch
                    {
                        Value = "I don't know how, but something went wrong with the values";
                        Error = true;
                        return;
                    }
                    switch (SelectedOperation)
                    {
                        case "+":
                            Value = (number0 + number1).ToString();
                            return;
                        case "-":
                            Value = (number0 - number1).ToString();
                            return;
                        case "*":
                            Value = (number0 * number1).ToString();
                            return;
                        case "/":
                            if (number1 == 0)
                            {
                                Value = "You can't divide by zero";
                                Error = true;
                                return;
                            }
                            Value = (number0 / number1).ToString();
                            return;
                        case "x!":
                            try
                            {
                                bool possible = int.TryParse(FirstNumber, out int number);
                                int result = number;
                                number--;
                                if (!possible)
                                {
                                    Value = "Not an integer";
                                    Error = true;
                                    return;
                                }
                                for (; number > 0; number--)
                                {
                                    result *= number;
                                }
                                Value = result.ToString();
                            }
                            catch
                            {
                                Value = "Something went wrong";
                                Error = true;
                            }
                            return;
                        case "x^y":
                            try
                            {
                                Value = Math.Pow(number0, number1).ToString();
                            }
                            catch
                            {
                                Value = "Too much number.";
                                Error = true;
                            }
                            return;
                        case "sin(x)":
                            Value = Math.Sin(number0).ToString();
                            return;
                        case "log(x)":
                            Value = Math.Log10(number0).ToString();
                            return;
                        default:
                            Value = "What did you press?";
                            Error = true;
                            return;
                    }

                },
                () => true
                );
            Reset = new RelayCommand(
                () =>
                {
                    SelectedOperation = "";
                    Value = "0";
                    FirstNumber = "";
                    Error = false;
                },
                () => true
            );
        }

        public string Value
        {
            get
            {
                return _value;
            }
            set
            {
                _value = value;
                NotifyPropertyChanged();
            }
        }
        public string SelectedOperation
        {
            get
            {
                return _selectedOperation;
            }
            set
            {
                _selectedOperation = value;
                NotifyPropertyChanged();
            }
        }
        public string FirstNumber
        {
            get
            {
                return _firstNumber;
            }
            set
            {
                _firstNumber = value;
                NotifyPropertyChanged();
            }
        }
        public bool Error
        {
            get
            {
                return _error;
            }
            set
            {
                _error = value;
                NotifyPropertyChanged();
            }
        }

        public RelayCommand Play { get; set; }

        public event PropertyChangedEventHandler PropertyChanged;

        private void NotifyPropertyChanged([CallerMemberName] string propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
