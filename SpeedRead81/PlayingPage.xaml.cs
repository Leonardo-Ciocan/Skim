using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Threading.Tasks;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.Graphics.Display;
using Windows.UI.ViewManagement;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkID=390556

namespace SpeedRead81
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class PlayingPage : Page
    {
        
        public  PlayingPage()
        {
            this.InitializeComponent(); this.Loaded += MainPage_Loaded;
            StatusBar.GetForCurrentView().HideAsync();
            DisplayInformation.GetForCurrentView().OrientationChanged += PlayingPage_OrientationChanged;
            Windows.Phone.UI.Input.HardwareButtons.BackPressed += (a,b)=>{
                if(Frame.CanGoBack)Frame.GoBack();
                b.Handled = true;
            };
        }

        void PlayingPage_OrientationChanged(DisplayInformation sender, object args)
        {
            if (sender.CurrentOrientation == DisplayOrientations.Landscape || sender.CurrentOrientation == DisplayOrientations.LandscapeFlipped)
            {
                box.Visibility = Windows.UI.Xaml.Visibility.Collapsed;
                reader.VerticalAlignment = Windows.UI.Xaml.VerticalAlignment.Center;
                BottomAppBar.IsSticky = false;
            }
            else
            {
                box.Visibility = Windows.UI.Xaml.Visibility.Visible;
                reader.VerticalAlignment = Windows.UI.Xaml.VerticalAlignment.Top;
                BottomAppBar.IsSticky = true;
            }
        }


        Story s;
        AppSettings st;
        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            s = (e.Parameter as List<object>)[0] as Story;
            st = (e.Parameter as List<object>)[1] as AppSettings;
            base.OnNavigatedTo(e);
        }
       
        void MainPage_Loaded(object sender, RoutedEventArgs e)
        {
            this.Tapped += (a, b) =>
            {
                play(null, null);
            };
            reader.init(s.Text, box, s.WPM);
            this.DataContext = st;
            if (!st.ShowText) box.Visibility = Windows.UI.Xaml.Visibility.Collapsed;
        }

        bool playing = false;
        private async void play(object sender, RoutedEventArgs e)
        {
            playing = !playing;
            if (playing)
            {
                reader.intro.Begin();
                await Task.Delay(1100);
            }
            
            reader.setPlaying(playing);
            reader.Opacity = (playing) ? 1 : 0.8;
            playBtn.Icon = new SymbolIcon( ((!playing) ? Symbol.Play : Symbol.Pause));
        }


        private void before(object sender, RoutedEventArgs e)
        {
            reader.setPlaying(false);
            reader.shiftBy(-1);
        }

        private void after(object sender, RoutedEventArgs e)
        {
            reader.setPlaying(false);
            reader.shiftBy(1);
        }
    }
}
