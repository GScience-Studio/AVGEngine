using Xamarin.Forms;

namespace AVGEngine.Page
{
	public class GamePage : ContentPage
	{
        /* 初始化游戏页
         * message 对话框内的消息
         */
	    protected GamePage(string message)
	    {
	        Content = new StackLayout
	        {
	            Children =
	            {
	                new Label { Text = message }
	            }
	        };
        }

        //切换游戏页
        protected void SwitchTo<T>() where T : GamePage, new()
        {
	        InterApplication.InterApp.MainPage = new T();
        }
    }
}