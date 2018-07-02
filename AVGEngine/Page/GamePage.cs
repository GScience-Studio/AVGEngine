using System;
using System.Collections.Generic;
using Xamarin.Forms;

namespace AVGEngine.Page
{
    public abstract class GamePage : ContentPage
	{
        //对话框
	    private Label mDialogLabel;
        //角色布局
	    private AbsoluteLayout mActorLayout;
        //主布局
	    private AbsoluteLayout mMainLayout;

        //角色列表
	    private readonly List<Tuple<GameActor, Control.Image>> mActorList = new List<Tuple<GameActor, Control.Image>>();

        //设置消息
	    public void setDialogMessage(string dialogMessage)
	    {
	        mDialogLabel.Text = dialogMessage;
	    }

        //初始化
	    protected abstract void OnInit();
	    protected abstract void OnDestory();

        protected override void OnAppearing()
	    {
	        base.OnAppearing();
	        OnInit();
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
            mDialogLabel = new Label
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
	                mDialogLabel
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
	        mDialogLabel.HeightRequest = Height / 3;
	        mDialogLabel.WidthRequest = Width - 6;
	        mDialogLabel.Margin = new Thickness(3, Height / 3 * 2 - 3, 0, 0);
	        mDialogLabel.FontSize = mDialogLabel.HeightRequest / (1.5 * 4);

	        //角色
	        foreach (var actorPair in mActorList)
	            UpdateActorPos(actorPair.Item1, actorPair.Item2);
        }

	    //切换游戏页
        protected void SwitchTo<T>() where T : GamePage, new()
        {
	        InterApplication.InterApp.MainPage = new T();
        }

        //添加新Actor
	    protected void AddActor(GameActor actor)
	    {
	        var image = new Control.Image(this)
	        {
	            Source = actor.ActorSource,
                RelevantX = actor.RelevantX,
                RelevantY = actor.RelevantY
	        };

	        UpdateActorPos(actor, image);

            mActorList.Add(new Tuple<GameActor, Control.Image>(actor, image));
	        mActorLayout.Children.Add(image);
        }

	    private void UpdateActorPos(GameActor actor, View image)
	    {
            //大小
            if (actor.RelevantHeight > 0)
	            image.HeightRequest = actor.RelevantHeight * Height;
	        else if (actor.RelevantWidth > 0)
	            image.WidthRequest = actor.RelevantWidth * Height;

            //位置
            image.Margin = new Thickness(
	            actor.RelevantX * Width,
	            actor.RelevantY * Height,
	            0, 0);
	    }
    }
}