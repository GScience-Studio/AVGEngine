using System;
using Xamarin.Forms;

namespace AVGEngine.Page
{
    public class MainMenuPage : ContentPage
    {
        private readonly StackLayout mMainLayout;

        private readonly Style mButtonStyle;
        private readonly Label mAboutButton;
        private readonly Label mStartGameButton;

        private readonly StackLayout mSubLayout;
        private readonly Image mTitleImage;

        public MainMenuPage()
        {
            //主菜单按钮风格
            mButtonStyle = new Style(typeof(Button))
            {
                Setters =
                {
                    //居中
                    new Setter
                    {
                        Property = View.HorizontalOptionsProperty,
                        Value = LayoutOptions.Center
                    },
                    new Setter
                    {
                        Property = View.VerticalOptionsProperty,
                        Value = LayoutOptions.Center
                    },
                    //文字颜色
                    new Setter
                    {
                        Property = Button.TextColorProperty,
                        Value = Color.White
                    },
                    //黑边框
                    new Setter
                    {
                        Property = Button.BorderColorProperty,
                        Value = Color.Black
                    },
                    //颜色
                    new Setter
                    {
                        Property = BackgroundColorProperty,
                        Value = new Color(0.0,0.0,0.0,0.3)
                    }
                }
            };
            //标题
            mTitleImage = new Image
            {
                Source = InterApplication.Resource.Title,

                HorizontalOptions = LayoutOptions.Center,
                VerticalOptions = LayoutOptions.Center
            };

            //两个按钮
            mStartGameButton = Control.createLabelButton("Start Game", StartGame);
            mAboutButton = Control.createLabelButton("About Us", StartGame);

            //两个按钮的布局
            mSubLayout = new StackLayout
            {
                Children =
                {
                    mStartGameButton,
                    mAboutButton
                },
                HorizontalOptions = LayoutOptions.Center,
                VerticalOptions = LayoutOptions.Center
            };

            //主布局
            mMainLayout = new StackLayout
            {
                Children =
                {
                    mTitleImage,
                    mSubLayout
                },
                HorizontalOptions = LayoutOptions.Center,
                VerticalOptions = LayoutOptions.Center
            };

            Content = mMainLayout;
            BackgroundColor = Color.Blue;
        }

        protected override void OnSizeAllocated(double width, double height)
        {
            base.OnSizeAllocated(width, height);

            //main layout
            mMainLayout.Margin = new Thickness(Height / 5);
            mMainLayout.Spacing = Height / 5;

            //title
            mTitleImage.HeightRequest = Height / 4;

            //two buttons
            mStartGameButton.WidthRequest = mTitleImage.Width / 1.5;
            mAboutButton.WidthRequest = mTitleImage.Width / 1.5;
            mStartGameButton.FontSize = Height / 15;
            mAboutButton.FontSize = Height / 15;
            mStartGameButton.HeightRequest = mStartGameButton.FontSize * 2.5;
            mAboutButton.HeightRequest = mStartGameButton.FontSize * 2.5;
        }

        private void StartGame(object sender, EventArgs e)
        {
            InterApplication.InterApp.MainPage = InterApplication.InterApp.inGamePage;
        }
    }
}