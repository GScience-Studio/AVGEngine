using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace AVGEngine.Control
{
    //以锚点为中心可设定位置的Image
    public class Image : Xamarin.Forms.Image
    {
        private double mRelevantX;
        public double RelevantX
        {
            get => mRelevantX;
            set
            {
                UpdatePos();
                mRelevantX = value;
            }
        }

        private double mRelevantY;
        public double RelevantY
        {
            get => mRelevantY;
            set
            {
                UpdatePos();
                mRelevantY = value;
            }
        }

        private double ContainerWidth;
        private double ContainerHeight;

        protected override void OnSizeAllocated(double width, double height)
        {
            base.OnSizeAllocated(width, height);
            UpdatePos();
        }

        private void UpdatePos()
        {
            var x = -Width * AnchorX + mRelevantX * ContainerWidth;
            var y = -Height * AnchorY + mRelevantY * ContainerHeight;

            Margin = new Thickness(
                x,
                y,
                ContainerWidth - x - Width,
                ContainerHeight - y - Height);
        }

        public Image(VisualElement rootPage)
        {
            ContainerWidth = rootPage.Width;
            ContainerHeight = rootPage.Height;

            rootPage.SizeChanged += (sender, args) =>
            {
                ContainerWidth = rootPage.Width;
                ContainerHeight = rootPage.Height;
            };
        }
    }
}
