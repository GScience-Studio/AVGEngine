using System;
using System.Collections.Generic;
using AVGEngine.GameEvent;
using Xamarin.Forms;

namespace AVGEngine.Page
{
    public abstract class GamePage : ContentPage
	{
        //对话框
	    protected Label DialogLabel;
        //角色布局
	    private AbsoluteLayout mActorLayout;
        //主布局
	    private AbsoluteLayout mMainLayout;

        //角色列表
	    private readonly List<Tuple<GameActor, Control.RelevantImage>> mActorList = new List<Tuple<GameActor, Control.RelevantImage>>();

        //事件相关
	    protected GameEventList eventList = new GameEventList();

        //初始化
	    protected abstract void OnInit();
	    protected abstract void OnDestory();

        protected override void OnAppearing()
	    {
	        base.OnAppearing();
	        OnInit();
	        eventList.Run();

        }

	    protected override void OnDisappearing()
	    {
	        base.OnDisappearing();
	        OnDestory();
        }

	    //初始化游戏页
        protected GamePage()
	    {
            //对话框
	        DialogLabel = new Label
	        {
                BackgroundColor = Color.White
            };

            //角色等
	        mActorLayout = new AbsoluteLayout
	        {
	            HorizontalOptions = LayoutOptions.Fill,
	            VerticalOptions = LayoutOptions.Fill
            };

	        //主布局
	        mMainLayout = new AbsoluteLayout
	        {
	            Children =
	            {
	                mActorLayout,
	                DialogLabel
                },
	            HorizontalOptions = LayoutOptions.Fill,
	            VerticalOptions = LayoutOptions.Fill,
                BackgroundColor = Color.Blue
	        };

            //大小变化
            SizeChanged += OnSizeChanged;
            Content = mMainLayout;
	    }

	    private void OnSizeChanged(object sender, EventArgs e)
	    {
            //对话框
	        DialogLabel.HeightRequest = Height / 3;
	        DialogLabel.WidthRequest = Width - 6;
	        DialogLabel.Margin = new Thickness(3, Height / 3 * 2 - 3, 0, 0);
	        DialogLabel.FontSize = DialogLabel.HeightRequest / (1.5 * 4);
        }

	    //切换游戏页
        protected void SwitchTo<T>() where T : GamePage, new()
        {
	        InterApplication.InterApp.MainPage = new T();
        }

        //添加新Actor
	    protected void AddActor(GameActor actor)
	    {
	        var image = new Control.RelevantImage(this)
	        {
	            Source = actor.ActorSource,
                RelevantX = actor.RelevantX,
                RelevantY = actor.RelevantY,
                RelevantScale = actor.RelevantScale,
	            ImageRelevantType = actor.ImageRelevantType
            };

            mActorList.Add(new Tuple<GameActor, Control.RelevantImage>(actor, image));
	        mActorLayout.Children.Add(image);
        }
    }
}