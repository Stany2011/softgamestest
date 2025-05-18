using UnityEngine;
using UnityEngine.UI;

namespace PhoenixFlame
{
    public class FireColorController : MonoBehaviour
    {
        public Animator fireAnimator;
        public Image buttonImage;

        private enum FireColor { Orange, Green, Blue }
        private FireColor currentColor = FireColor.Orange;

        public void SwitchToNextColor()
        {
            // Determine next color (previewed on the button)
            FireColor nextColor = currentColor switch
            {
                FireColor.Orange => FireColor.Green,
                FireColor.Green => FireColor.Blue,
                FireColor.Blue => FireColor.Orange,
                _ => FireColor.Orange
            };

            // Trigger Animator to change to the next color
            string stateName = nextColor.ToString(); // assumes state names are "Orange", "Green", "Blue"
            fireAnimator.CrossFade(stateName, 0.2f);

            // Update current color tracker
            currentColor = nextColor;

            // Preview *next* color on the button
            UpdateButtonColor(GetNextColor(currentColor));
        }


        private FireColor GetNextColor(FireColor color)
        {
            return color switch
            {
                FireColor.Orange => FireColor.Green,
                FireColor.Green => FireColor.Blue,
                FireColor.Blue => FireColor.Orange,
                _ => FireColor.Orange
            };
        }


        private void UpdateButtonColor(FireColor nextColor)
        {
            if (buttonImage == null) return;

            Color newColor = nextColor switch
            {
                FireColor.Orange => new Color32(255, 110, 0, 255),   // #FF6E00
                FireColor.Green  => new Color32(0, 255, 0, 255),     // #00FF00
                FireColor.Blue   => new Color32(0, 123, 255, 255),   // #007BFF
                _ => Color.white
            };

            buttonImage.color = newColor;
        }


    }
}
