using System;
using System.Collections.Generic;
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
    /// Interaction logic for Bin2Dec.xaml
    /// </summary>
    public partial class Bin2Dec : UserControl, INotifyPropertyChanged
    {
        private enum BinStringErrors
        {
            eNone,
            eInvalidNumber,
            eInvalidLength
        }

        private string _result;
        private string _input;

        public event PropertyChangedEventHandler? PropertyChanged;
        public Bin2Dec()
        {
            InitializeComponent();
            DataContext = this;
        }

        public string Result { 
            get => _result;
            set
            {
                _result = value;
                OnPropertyChanged();
            }
        }

        public string Input { 
            get => _input;
            set 
            {
                var res = IsValidInput(value);
                switch(res)
                {
                    case BinStringErrors.eInvalidNumber:
                    case BinStringErrors.eInvalidLength:
                        MessageBox.Show(res==BinStringErrors.eInvalidLength ? "Entry must be no larger than 8 characters" : 
                            "Entry must be 0 or 1", "WARNING", MessageBoxButton.OK, MessageBoxImage.Error);
                        break;
                    case BinStringErrors.eNone:
                    default:
                        _input = value;
                        break;
                }
                Result = string.Empty;
                OnPropertyChanged();
            }
        }

        // Create the OnPropertyChanged method to raise the event
        // The calling member's name will be used as the parameter.
        protected void OnPropertyChanged([CallerMemberName] string name = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }

        private void btnGenerate_Click(object sender, RoutedEventArgs e)
        {
            int pow = 0;
            int res = 0;
            for(int i = Input.Length-1; i >= 0; i--)
            {
                if (Input[i] == '1')
                    res += (int)Math.Pow(2, pow);
                pow++;
                Result = res.ToString();
            }
        }

        private BinStringErrors IsValidInput(string input)
        {
            Regex rx = new Regex("^[01]{0,8}$");
            if (rx.IsMatch(input))
                return BinStringErrors.eNone;
            if(input.Length>8)
                return BinStringErrors.eInvalidLength;
            return BinStringErrors.eInvalidNumber;
        }
    }
}
