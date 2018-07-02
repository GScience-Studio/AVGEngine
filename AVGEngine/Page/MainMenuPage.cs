using System;
using AVGEngine.Control;
using AVGEngine.GameEvent;
using Xamarin.Forms;
using Button = Xamarin.Forms.Button;

namespace AVGEngine.Page
{
    public class MainMenuPage : ContentPage
    {
        private readonly AbsoluteLayout mMainLayoutWithBackgroundImage;
        private readonly RelevantImage mBackgroundImage;

        private readonly StackLayout mMainLayout;

        private readonly Button mAboutButton;
        private readonly Button mStartGameButton;

        private readonly StackLayout mSubLayout;
        private readonly Image mTitleImage;

        public MainMenuPage()
        {
            //标题
            mTitleImage = new Image
            {
                Source = InterApplication.Resource.Title,

                HorizontalOptions = LayoutOptions.Center,
                VerticalOptions = LayoutOptions.Center
            };

            //两个按钮
            mStartGameButton = new Control.Button("开始游戏");
            mAboutButton = new Control.Button("关于");

            mStartGameButton.Clicked += StartGame;

           //两个按钮的布局
           mSubLayout = new StackLayout
            {
                Children =
                {
                    mStartGameButton,
                    mAboutButton
                },
                HorizontalOptions = LayoutOptions.Center,
                VerticalOptions = LayoutOptions.Center,
            };

            //主布局
            mMainLayout = new StackLayout
            {
                Children =
                {
                    mTitleImage,
                    mSubLayout,
                },
                HorizontalOptions = LayoutOptions.Center,
                VerticalOptions = LayoutOptions.Center,
            };

            //背景
            mBackgroundImage = new RelevantImage(this)
            {
                Source = InterApplication.Resource.TitleBackground,
                RelevantX = 0.5,
                RelevantY = 0.5,
                Aspect = Aspect.AspectFill
            };

            mMainLayoutWithBackgroundImage = new AbsoluteLayout
            {
                Children =
                {
                    mBackgroundImage,
                    mMainLayout
                }
            };
            Content = mMainLayoutWithBackgroundImage;

            BackgroundColor = Color.Blue;
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            new GameEventTextPrinterButton(mStartGameButton, "开始游戏").Do();
            new GameEventTextPrinterButton(mAboutButton, "关于").Do();
        }

        protected override void OnSizeAllocated(double width, double height)
        {
            base.OnSizeAllocated(width, height);

            //main layout
            mMainLayout.Margin = new Thickness(Height / 15);
            mMainLayout.Spacing = Height / 10;
            mMainLayout.WidthRequest = Width - Height / 15 * 2;
            mMainLayout.HeightRequest = Height - Height / 15 * 2;

            //title
            mTitleImage.HeightRequest = Height / 3;

            //two buttons layout
            mSubLayout.Spacing = Height / 25;

            //two buttons
            mStartGameButton.WidthRequest = mTitleImage.Width / 2;
            mAboutButton.WidthRequest = mTitleImage.Width / 2;
            mStartGameButton.HeightRequest = height / 9;
            mAboutButton.HeightRequest = height / 9;

            //背景
            mBackgroundImage.HeightRequest = Height;
        }

        private void StartGame(object sender, EventArgs e)
        {
            InterApplication.InterApp.StartGame();
        }
    }
}