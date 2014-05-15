using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using Windows.UI;

namespace SpeedRead81
{
    public class AppSettings
    {
        private double _fontSize;
        public double FontSize
        {
            get
            {
                return _fontSize;
            }
            set
            {
                _fontSize = value;
                RaisePropertyChanged();
            }
        }

        private bool _showText;
        public bool ShowText
        {
            get
            {
                return _showText;
            }
            set
            {
                _showText = value;
                RaisePropertyChanged();
            }
        }


        private Color _background;
        public Color Background
        {
            get
            {
                return _background;
            }
            set
            {
                _background = value;
                RaisePropertyChanged();
            }
        }

        private Color _foreground;
        public Color Foreground
        {
            get
            {
                return _foreground;
            }
            set
            {
                _foreground = value;
                RaisePropertyChanged();
            }
        }


        public event PropertyChangedEventHandler PropertyChanged;
        private void RaisePropertyChanged([CallerMemberName] string caller = "")
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(caller));

            }
        }
    }
}
