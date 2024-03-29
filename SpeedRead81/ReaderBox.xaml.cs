﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Documents;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// The User Control item template is documented at http://go.microsoft.com/fwlink/?LinkId=234236

namespace SpeedRead81
{
    public partial class ReaderBox : UserControl
    {
        List<string> data;
        int amount = 60;
        int font = 19;

        public ReaderBox()
        {
            InitializeComponent();
            this.Loaded += ReaderBox_Loaded;
        }

        SolidColorBrush old = new SolidColorBrush(Colors.Gray);
        SolidColorBrush selected = Application.Current.Resources["ButtonPressedBackgroundThemeBrush"] as SolidColorBrush;
        public void init(List<String> data , int currentlyAt)
        {
            box.Inlines.Clear();
            this.data = data;
            last = currentlyAt - currentlyAt % amount;
            current = currentlyAt % amount;
            for (int x = 0; x < Math.Min(amount, data.Count - currentlyAt); x++)
            {
                box.Inlines.Add(new Run { Text =  (last + x >= data.Count)
                    ? "" :data[last+x] + " ", FontSize = font,
                                          Foreground = (last + x <= currentlyAt) ? ((last + x == currentlyAt) ? selected : old) : new SolidColorBrush((Color)Application.Current.Resources["PhoneForegroundColor"])
                });
                blocks.Add(box.Inlines[box.Inlines.Count - 1] as Run);
            }
        }

        List<Run> blocks = new List<Run>();
        void ReaderBox_Loaded(object sender, RoutedEventArgs e)
        {

        }

        int current = 0;
        int last = 0;
        public void next()
        {
            /* if (last < data.Count - 1 && current > 25)
             {
                 box.Children.RemoveAt(0);
                 box.Children.Add(new TextBlock { Text = data[++last] + " ", FontSize = font, Foreground = new SolidColorBrush(Colors.LightGray) });
                 current -=1;
             }

             if (current < data.Count - 1)
             {
                 ((box.Children[current] as TextBlock).Foreground as SolidColorBrush).Color = Colors.Gray;
                 ((box.Children[++current] as TextBlock).Foreground as SolidColorBrush).Color = (Color)Application.Current.Resources["PhoneAccentColor"];
             }
             */
            if (current == amount - 1)
            {
                foreach (Run c in blocks) c.Foreground = new SolidColorBrush((Color)Application.Current.Resources["PhoneForegroundColor"]);
                last += amount;
                current = 0;
                for (int x = 0; x < amount; x++)
                {
                    if (last + x >= data.Count)
                    {
                        blocks[x].Text = "";
                    }
                    else
                    {
                        blocks[x].Text = data[last + x] + " ";
                    }
                }
            }
            if (last + current < data.Count - 1)
            {
                (box.Inlines[current] as Run).Foreground = old;
                (box.Inlines[++current] as Run).Foreground = selected;
            }

        }
    }
}
