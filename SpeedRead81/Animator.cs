using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Media.Animation;

namespace SpeedRead81
{
    public class Animator
    {
        /// <summary>
        /// This method allows to bind a small reaction animation to an element.
        ///The element will move when pressed to show it responds to touch.
        /// </summary>
        /// <param name="element">The element to add the animation to</param>
        public static void AddPointerAnimation(FrameworkElement element , string NAME)
        {
            //animation to shrink the element when mouse is pressed
            Storyboard anim_down = new Storyboard();
            
            anim_down.Children.Add(new PointerDownThemeAnimation());
            Storyboard.SetTargetName(anim_down.Children[0], NAME);
            //animation to enlarge to original size when mouse is not pressed
            Storyboard anim_up = new Storyboard();
            
            anim_up.Children.Add(new PointerUpThemeAnimation());
            Storyboard.SetTargetName(anim_up.Children[0], NAME);
            bool down = false;
            //when the pointer enters the animation should be shrinking unless the mouse is not pressed.
            element.PointerEntered += (e, ev) =>
            {
                if (down)
                    anim_down.Begin();
            };
            //if mouse button is down then begin shrinking animation and set down to true
            element.PointerPressed += (e, ev) =>
            {
                anim_down.Begin();
                down = true;
            };
            //if the pointer leaves the bounds of the element the released event will not be detected. Therefore stop the nimation and reverse value of down. 
            element.PointerExited += (e, ev) =>
            {
                anim_up.Begin();
                down = false;
            };
            //if the mouse button is released resize and set down to false.
            element.PointerReleased += (e, ev) =>
            {
                anim_up.Begin();
                down = false;
            };
        }

    }
}
