
namespace AVGGame.UWP
{
    public sealed partial class MainPage
    {
        public MainPage()
        {
            InitializeComponent();

            LoadApplication(AVGEngine.InterApplication.Create(new AVGGameCore.Laucher()));
        }
    }
}