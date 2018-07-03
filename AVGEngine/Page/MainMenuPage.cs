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

        //渐变图层
        private FadeLayer mFadeLayer;

        public MainMenuPage()
        {
            //渐变图层
            mFadeLayer = new FadeLayer(this);

            //标题
            mTitleImage = new Image
            {
                Source = InterApplication.Resource.Title,

                HorizontalOptions = LayoutOptions.Center,
                VerticalOptions = LayoutOptions.Center
            };

            //两个按钮
            mStartGameButton = new Control.Button("")
            {
                IsVisible = false
            };
            mAboutButton = new Control.Button("")
            {
                IsVisible = false
            };

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
                    mMainLayout,
                    mFadeLayer
                }
            };
            Content = mMainLayoutWithBackgroundImage;

            BackgroundColor = Color.Black;
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            mFadeLayer.ShowAll(() =>
            {
                new GameEventTextPrinterButton(mStartGameButton, "开始游戏").Do();
                new GameEventTextPrinterButton(mAboutButton, "关于").Do();
            }, 0.005);

            //监听交互
            GameEventWaitInput.MainListenedContent = Content;
        }

        protected override void OnSizeAllocated(double width, double height)
        {
            base.OnSizeAllocated(width, height);

            //main layout
            mMainLayout.Margin = new Thickness(Height / 15);
            mMainLayout.Spacing = Height / 8;
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
        }

        private void StartGame(object sender, EventArgs e)
        {
            mFadeLayer.HideAll(InterApplication.InterApp.StartGame, 0.005);
        }
    }
}