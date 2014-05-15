using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Media.Animation;

namespace SpeedRead81
{
    public partial class Reader : UserControl
    {

        public Storyboard intro = new Storyboard();
        DoubleAnimation anim = new DoubleAnimation
        {
             From = 1,
             To = 0,
             Duration = TimeSpan.FromSeconds(1)
        };

        public List<string> words = new List<string>();
        public Reader()
        {
            InitializeComponent();

            this.Loaded += Reader_Loaded;
            
        }

        void Reader_Loaded(object sender, RoutedEventArgs e)
        {
            shade.RenderTransform = new CompositeTransform
            {
                   ScaleX = 1, ScaleY = 1
            };
            intro.Children.Add(anim);
            Storyboard.SetTarget(anim, shade);
            Storyboard.SetTargetProperty(anim, "(UIElement.RenderTransform).(CompositeTransform.ScaleX)");
        }




        ReaderBox box;
        

        int wpm = 350;
        public int current = 0;
        DispatcherTimer dt = new DispatcherTimer();

        public void init(String data, ReaderBox box, int wpm)
        {
            this.wpm = wpm;
            this.box = box;
            words = data.Split(' ').ToList();
            txt.Text = words[0];
            dt.Tick += (a, b) =>
            {
                if (current < words.Count - 1)
                {
                    txt.Text = words[++current];
                    if (box != null) box.next();
                }
                else
                {
                    dt.Stop();
                }
            };
            dt.Interval = TimeSpan.FromMinutes(1.0 / wpm);

            box.init(words);
        }



        public void setPlaying(bool play)
        {
            if (play)
            {
                dt.Start();
            }
            else
            {
                dt.Stop();
            }
        }

        public void shiftBy(int d)
        {
            current += d;
            if (current < words.Count - 1)
            {
                txt.Text = words[current];
            }
        }
    }
}
