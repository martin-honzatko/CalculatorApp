using Avalonia.Data;
using Avalonia.Data.Converters;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Calculator_Avalonia.Converters
{
    internal class MathConverter : IMultiValueConverter
    {
        public object? Convert(IList<object?> values, Type targetType, object? parameter, CultureInfo culture)
        {
            if (values.Count != 3) //Validation check
            {
                Trace.WriteLine("Exactly three values expected");
                return BindingOperations.DoNothing;
            }

            string operation = values[0] as string ?? "+";
            double value0 = values[1] as double? ?? 0;
            double value1 = values[2] as double? ?? 0;


            //Depending on the operator calculate the result.
            switch (operation)
            {
                case "+":
                    return value0 + value1;

                case "-":
                    return value0 - value1;

                case "*":
                    return value0 * value1;

                case "/":
                    if (value1 == 0) //We cannot divide by zero. If value1 is '0', return an error.
                    {
                        return new BindingNotification(new DivideByZeroException("Don't do this!"), BindingErrorType.Error);
                    }

                    return value0 / value1;
            }

            //If we reach this line, something was wrong. So we return an error notification
            return new BindingNotification(new InvalidOperationException("Something went wrong"), BindingErrorType.Error);
        }
    }
}
