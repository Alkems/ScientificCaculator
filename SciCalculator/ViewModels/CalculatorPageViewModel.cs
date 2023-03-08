﻿namespace SciCalculator.ViewModels
{
    [INotifyPropertyChanged]
    internal partial class CalculatorPageViewModel //: INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        [ObservableProperty]
        private string inputText = string.Empty;

        [ObservableProperty]
        private string calculatedResult = "0";

        private bool isSciOpWaiting = false;

        public CalculatorPageViewModel()
        {

        }

        [RelayCommand]
        private void Reset()
        {
            CalculatedResult = "0";
            inputText = string.Empty;
            isSciOpWaiting = false;
        }

        [RelayCommand]
        private void Calculate()
        {
            if (inputText.Length == 0)
            {
                return;
            }

            if (isSciOpWaiting)
            {
                inputText += ")";
                isSciOpWaiting = false;
            }

            try
            {
                var inputString = NormalizeInputString();
            }
            catch (Exception)
            {
                throw;
            }
        }

        private string NormalizeInputString()
        {
            Dictionary<string, string> _opMapper = new()
            {
                {"×", "*"},
                {"÷", "/"},
                {"SIN", "Sin"},
                {"COS", "Cos"},
                {"TAN", "Tan"},
                {"ASIN", "Asin"},
                {"ACOS", "Acos"},
                {"ATAN", "Atan"},
                {"LOG", "Log"},
                {"EXP", "Exp"},
                {"LOG10", "Log10"},
                {"POW", "Pow"},
                {"SQRT", "Sqrt"},
                {"ABS", "Abs"},
            };
            var retString = InputText;

            foreach (var key in _opMapper.Keys)
            {
                retString = retString.Replace(key, _opMapper[key]);
            }

            return retString;
        }

        [RelayCommand]
        private void Backspace()
        {
            if (InputText.Length > 0)
            {
                InputText = InputText.Substring(0, InputText.Length - 1);
            }
        }

        [RelayCommand]
        private void NumberInput(string key)
        {
            inputText += key;
        }

        [RelayCommand]
        private void MathOperator(string op)
        {
            if (isSciOpWaiting)
            {
                inputText += ")";
                isSciOpWaiting = false;
            }

            inputText += $" {op} ";
        }

        [RelayCommand]
        private void RegionOperator(string op)
        {
            if (op == ")")
            {
                isSciOpWaiting = false;
            }

            InputText += op;
        }

        [RelayCommand]
        private void ScientificOperator(string op)
        {
            InputText += $"{op}(";
            isSciOpWaiting = true;
        }
    }
}