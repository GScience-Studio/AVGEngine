using System;
using System.Collections.Generic;
using Xamarin.Forms;

namespace AVGEngine.Page
{
	public class GamePage : ContentPage
	{
        //对话框
	    private Label mDialogLabel;
        //角色布局
	    private AbsoluteLayout mActorLayout;
        //主布局
	    private AbsoluteLayout mMainLayout;

        //角色列表
	    private readonly List<Tuple<GameActor, Image>> mActorList = new List<Tuple<GameActor, Image>>();

        //设置消息
	    public void setDialogMessage(string dialogMessage)
	    {
	        mDialogLabel.Text = dialogMessage;
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

            Content = mMainLayout;
	    }

	    protected override void OnSizeAllocated(double width, double height)
	    {
	        base.OnSizeAllocated(width, height);
            //对话框
	        mDialogLabel.HeightRequest = Height / 3;
	        mDialogLabel.WidthRequest = Width - 6;
	        mDialogLabel.Margin = new Thickness(3, Height / 3 * 2 - 3, 0, 0);
            mDialogLabel.FontSize = mDialogLabel.HeightRequest / (1.5 * 4);

            //角色
	        foreach (var actorPair in mActorList)
	        {
	            var actor = actorPair.Item1;
	            var image = actorPair.Item2;

                //大小
	            if (actor.RelevantHeight > 0)
	                image.HeightRequest = actor.RelevantHeight * Height;
	            else if (actor.RelevantWidth > 0)
                    image.WidthRequest = actor.RelevantWidth * Height;

	            //位置
                image.SizeChanged += (sender, args) =>
	            {
	                image.Margin = new Thickness(
	                    -image.Width * image.AnchorX + actor.RelevantX * Width,
	                    -image.Height * image.AnchorY + actor.RelevantY * Height,
	                    0, 0);
	            };
	        }
	    }

	    //切换游戏页
        protected void SwitchTo<T>() where T : GamePage, new()
        {
	        InterApplication.InterApp.MainPage = new T();
        }

        //添加新Actor
	    protected void AddActor(GameActor actor)
	    {
	        var image = new Image {Source = actor.ActorSource};
            mActorList.Add(new Tuple<GameActor, Image>(actor, image));
	        mActorLayout.Children.Add(image);
        }
    }
}