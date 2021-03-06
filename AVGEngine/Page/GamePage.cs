﻿using System;
using System.Collections.Generic;
using AVGEngine.Control;
using AVGEngine.GameEvent;
using Xamarin.Forms;

namespace AVGEngine.Page
{
    public abstract class GamePage : ContentPage
	{
        //对话框
	    protected DialogLabel DialogLabel;
        //角色布局
	    private AbsoluteLayout mActorLayout;
        //主布局
	    private AbsoluteLayout mMainLayout;

        //UI背景
	    private Image mMainUiBacnground;

        //角色列表
	    private readonly List<Tuple<GameActor, Control.RelevantImage>> mActorList = new List<Tuple<GameActor, Control.RelevantImage>>();

        //事件相关
	    protected GameEventList eventList = new GameEventList();
        //背景
	    private RelevantImage mGameBackgroundImage;

        //渐变图层
	    private FadeLayer mFadeLayer;

        //初始化
        protected abstract void OnInit();
	    protected abstract void OnDestory();

        protected override void OnAppearing()
	    {
	        base.OnAppearing();
	        OnInit();
            mFadeLayer.ShowAll(() => { eventList.Run(); });

            //监听交互
	        GameEventWaitInput.MainListenedContent = Content;
	    }

	    protected override void OnDisappearing()
	    {
	        base.OnDisappearing();
	        OnDestory();
        }

	    //初始化游戏页
        protected GamePage()
        {
            //渐变图层
            mFadeLayer = new FadeLayer(this);

            //对话框
            DialogLabel = new DialogLabel();

            //角色等
	        mActorLayout = new AbsoluteLayout
	        {
	            HorizontalOptions = LayoutOptions.Fill,
	            VerticalOptions = LayoutOptions.Fill
            };

            //游戏背景
	        mGameBackgroundImage = new RelevantImage(this)
	        {
	            Source = InterApplication.Resource.LoadImageSource("GamePage." + GetType().Name + ".Background"),
	            RelevantX = 0.5,
	            RelevantY = 0.5,
	            Aspect = Aspect.AspectFill
	        };

            //UI背景
            mMainUiBacnground = new Image()
            {
                Source = InterApplication.Resource.GameMainUi,
                Aspect = Aspect.Fill
            };

            //主布局
            mMainLayout = new AbsoluteLayout
	        {
	            Children =
	            {
	                mGameBackgroundImage,
                    mActorLayout,
	                mMainUiBacnground,
                    DialogLabel,
	                mFadeLayer
                },
	            HorizontalOptions = LayoutOptions.Fill,
	            VerticalOptions = LayoutOptions.Fill,
                BackgroundColor = Color.Black
	        };

            //大小变化
            SizeChanged += OnSizeChanged;

            Content = mMainLayout;
	    }

	    private void OnSizeChanged(object sender, EventArgs e)
	    {
            //对话框
	        var dialogBorderWidth = Width / 64;
	        var dialogBorderHeight = Height / 36;

            DialogLabel.HeightRequest = Height / 3 - dialogBorderHeight * 2;
	        DialogLabel.WidthRequest = Width - dialogBorderWidth * 2;
	        DialogLabel.Margin = new Thickness(
	            dialogBorderWidth, 
	            Height / 3 * 2 + dialogBorderHeight, 
	            0,
	            0);
	        DialogLabel.FontSize = DialogLabel.HeightRequest / 6;

            //背景
            mMainUiBacnground.HeightRequest = Height;
	        mMainUiBacnground.WidthRequest = Width;

	    }

	    //切换游戏页
        protected void SwitchTo<T>() where T : GamePage, new()
        {
            mFadeLayer.HideAll(() => { InterApplication.InterApp.MainPage = new T(); });
        }

        //添加新Actor
	    protected Image AddActor(GameActor actor, string actorRes = "MainImage")
	    {
	        var imageSource = (ImageSource) actor.GetType().GetField(actorRes)?.GetValue(actor);

	        if (imageSource == null)
	            throw new ArgumentException("Can't find " + actorRes + " in actor " + actor.GetType().Name);

            var image = new Control.RelevantImage(this)
	        {
	            Source = imageSource,
	            RelevantX = actor.RelevantX,
	            RelevantY = actor.RelevantY,
	            RelevantScale = actor.RelevantScale,
	            ImageRelevantType = actor.ImageRelevantType
	        };

            mActorList.Add(new Tuple<GameActor, Control.RelevantImage>(actor, image));
	        mActorLayout.Children.Add(image);

	        return image;
	    }
    }
}