using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace AVGEngine.Control
{
    //以锚点为中心可设定位置的Image
    public class RelevantImage : Xamarin.Forms.Image
    {
        public enum RelevantType { Width, Height }

        private RelevantType mImageRelevantType = RelevantType.Width;
        public RelevantType ImageRelevantType
        {
            get => mImageRelevantType;
            set
            {
                mImageRelevantType = value;
                UpdateSize();
            }
        }

        private double mRelevantScale = 1;
        public double RelevantScale
        {
            get => mRelevantScale;
            set
            {
                mRelevantScale = value;
                UpdateSize();
            }
        }

        private double mRelevantX = 0;
        public double RelevantX
        {
            get => mRelevantX;
            set
            {
                mRelevantX = value;
                UpdateSize();
            }
        }

        private double mRelevantY = 0;
        public double RelevantY
        {
            get => mRelevantY;
            set
            {
                mRelevantY = value;
                UpdateSize();
            }
        }

        private double ContainerWidth;
        private double ContainerHeight;

        private void UpdateSize()
        {
            if (mImageRelevantType == RelevantType.Height)
                HeightRequest = ContainerHeight * RelevantScale;
            else
                WidthRequest = ContainerWidth * RelevantScale;
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

        public RelevantImage(VisualElement rootPage)
        {
            ContainerWidth = rootPage.Width;
            ContainerHeight = rootPage.Height;

            rootPage.SizeChanged += (sender, args) =>
            {
                ContainerWidth = rootPage.Width;
                ContainerHeight = rootPage.Height;
                UpdateSize();
            };
            SizeChanged += (sender, args) => UpdatePos();
        }
    }
}
