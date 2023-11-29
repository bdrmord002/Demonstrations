using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Text.RegularExpressions;
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

namespace Demonstrations.View
{
    /// <summary>
    /// Interaction logic for Calculator.xaml
    /// </summary>
    public partial class Calculator : UserControl, INotifyPropertyChanged
    {
        private enum Operation
        {
            eInvalid,
            eEq,
            eMinus,
            ePLus,
            eMultiply,
            eDivide,
            eClear,
            eClearAll,
            eSign
        }

        private double _result;
        private double _input;

        public event PropertyChangedEventHandler? PropertyChanged;

        // Create the OnPropertyChanged method to raise the event
        // The calling member's name will be used as the parameter.
        protected void OnPropertyChanged([CallerMemberName] string name = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }
        public double Result { get => _result; set { _result = value; OnPropertyChanged(); } }

        public double Input 
        { 
            get => _input; 
            set 
            { 
                if(IsValidInput(value))
                    _input = value; 
                OnPropertyChanged(); 
            } 
        }

        private bool IsValidInput(double value)
        {
            Regex rx = new Regex("^(d+.?d+){0,8}$");
            return rx.IsMatch(value.ToString());
        }

        public Calculator()
        {
            InitializeComponent();
        }

        private void OpBtn(Operation op)
        {
            switch(op)
            {
                case Operation.eEq:
                    break;
                case Operation.eSign:
                    break;
                case Operation.eMinus:
                    break;
                case Operation.ePLus:
                    break;
                case Operation.eMultiply:
                    break;
                case Operation.eDivide:
                    break;
                case Operation.eClear:
                    break;
                case Operation.eClearAll:
                    break;
                default:
                    break;
            }
        }

        private void BtnMediator(object sender, RoutedEventArgs e)
        {
            var tag = ((Button)sender).Tag.ToString();
            if (tag.Contains("Op:"))
            {
                var toks = tag.Split(':');
                Operation result;
                if(Enum.TryParse<Operation>(toks[1], out result))
                {
                    OpBtn(result);
                }
                
            }
            else if(tag.Contains("Decimal"))
            {

            }
            else
            {
                Result *= 10;
                Result += int.Parse(tag);
            }
        }
    }
}
