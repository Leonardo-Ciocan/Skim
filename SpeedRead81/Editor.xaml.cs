using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Threading.Tasks;
using Windows.Devices.Sensors;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Media.Animation;
using Windows.UI.Xaml.Navigation;
using Windows.UI.Xaml.Shapes;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkID=390556

namespace SpeedRead81
{
    public partial class Editor : Page
    {
        public Editor()
        {
            InitializeComponent();
            this.Loaded += Editor_Loaded;
            NavigationCacheMode = Windows.UI.Xaml.Navigation.NavigationCacheMode.Required;
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            if (!loaded)
            {
                if (e.Parameter == "")
                {
                    s = new Story
                    {
                        Name = "New title",
                        WPM = 350,
                        Text = @"Zack Snyder's sequel to Man of Steel is shaping up to be a who's who of DC characters. Jesse Eisenberg stars as Lex Luthor, Gal Gadot will play Wonder Woman — and of course Ben Affleck will be Batman. Other than casting announcements and a rescheduled release date, however, official news on the movie has been relatively quiet, but Snyder unveiled the first image of Ben Affleck as Batman today, right alongside his new Batmobile. The black-aIt follows a tease the director dropped on Twitter yesterday, which made the vehicle look like a cross between the Tumbler from The Dark Knight films and the swooping, long-nosed car from Tim Burton's Batman. Now that we can see the new design in all its glory, it certainly seems to edge more towards the industrial side of things. The as-of-yet-untitled Man of Steel sequel is slated for release in May of 2016, but we imagine we'll be learning quite a bit about the new film this July at San Diego Comic-Con. As for Affleck as Batman himself, the costume design definitely seems to hearken back to the caped crusader's look in Frank Miller's 1986 classic The Dark Knight Returns, which depicted an aged Bruce Wayne at the end of his career. Since Affleck is rumored to be playing an older Batman in the upcoming film, it may not be surprising that Snyder would draw on Miller's grittier designs."
                    };
                    st = new AppSettings
                    {
                        FontSize = 20,
                        ShowText = true,
                        Background = Colors.White,
                        Foreground = Colors.Black
                    };
                }
                else
                {
                    s = (e.Parameter as List<object>)[0] as Story;
                    st = (e.Parameter as List<object>)[1] as AppSettings;
                }
            }

            if (box != null && loaded)
            {
                box.init(s.Words, s.CurrentWord);
            }

        //    base.OnNavigatedTo(e);
        }

        bool loaded;
        Story s;
        AppSettings st;
        void Editor_Loaded(object sender, RoutedEventArgs e)
        {
            loaded = true;
            details.DataContext = s;
            text.DataContext = s;
            settings.DataContext = st;

            Color[] cColors = new Color[] { 
                Colors.Red, Colors.Blue, Colors.Yellow, Colors.White,
                Colors.Green, Colors.Orange , Colors.Purple , Colors.Brown ,
                Colors.LightGoldenrodYellow , Colors.Black ,Colors.BlanchedAlmond , Colors.Black , 
            Colors.Aqua , Colors.Violet , Colors.DarkCyan , Colors.DarkOliveGreen , Colors.DarkOrange};
            
            ForegroundList.ItemsSource = cColors;
            ForegroundList.Tapped += (a, b) =>
            {
                ForegroundFlyout.Hide();
            };

            BackgroundList.ItemsSource = cColors;
            BackgroundList.Tapped += (a, b) =>
            {
                BackgroundFlyout.Hide();
            };

            s.Words = s.Text.Split(' ').ToList();


            SkimBtn.PointerPressed += (a, b) =>
            {
                PointerDown.Stop();
                Storyboard.SetTargetName(PointerDown.Children[0], "SkimBtn");
                PointerDown.Begin();
                b.Handled = true;
            };
            SkimBtn.PointerReleased += (a, b) =>
            {
                PointerDown.Stop();
                Storyboard.SetTargetName(PointerUp.Children[0], "SkimBtn");
                PointerUp.Begin();
            };

            /*DispatcherTimer dt = new DispatcherTimer { Interval = TimeSpan.FromSeconds(0.01) };
            dt.Tick += (a, b) =>
            {

            };*/


            foreach (Ellipse el in background.Children)
            {
                el.RenderTransform = new ScaleTransform();
            }

            

        }

        private void paste(object sender, System.EventArgs e)
        {
            //s.Text = Clipboard.GetText();
            
        }

        private void play(object sender, PointerRoutedEventArgs e)
        {
            Frame.Navigate(typeof(PlayingPage), new List<object> { s, st });
        }

        private void ListBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            switch ((sender as ComboBox).SelectedIndex)
            {
                case 0 :
                    RequestedTheme = ElementTheme.Default;
                    break;
                case 1:
                    RequestedTheme = ElementTheme.Light;
                    break;
                case 2:
                    RequestedTheme = ElementTheme.Dark;
                    break;
            }
        }




       


    }
}
