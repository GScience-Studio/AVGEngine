
namespace AVGGame.UWP
{
    public sealed partial class MainPage
    {
        public MainPage()
        {
            InitializeComponent();

            LoadApplication(AVGGameCore.Program.AvgApp);
        }
    }
}