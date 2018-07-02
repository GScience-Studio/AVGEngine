using AVGEngine.Control;
using Xamarin.Forms;

namespace AVGEngine.GameEvent
{
    public class GameEventTextPrinterLabel : GameEventTextPrinter
    {
        private readonly Label mLabel;

        public GameEventTextPrinterLabel(Label label, string message) : base(message)
        {
            mLabel = label;
        }

        protected override string GetText()
        {
            return mLabel.Text;
        }

        protected override void SetText(string str)
        {
            mLabel.Text = str;
        }
    }

    public class GameEventTextPrinterButton : GameEventTextPrinter
    {
        private readonly Xamarin.Forms.Button mButton;

        public GameEventTextPrinterButton(Xamarin.Forms.Button button, string message) : base(message)
        {
            mButton = button;
        }

        protected override string GetText()
        {
            return mButton.Text;
        }

        protected override void SetText(string str)
        {
            mButton.Text = str;
        }
    }

    public abstract class GameEventTextPrinter : GameEventBase
    {
        private TimedTask mTask;
        private string mTotalText;

        protected abstract string GetText();
        protected abstract void SetText(string str);

        private string Text
        {
            get => GetText();
            set => SetText(value);
        }

        protected GameEventTextPrinter(string message)
        {
            mTotalText = message;
        }

        public override void Do()
        {
            Text = "";

            mTask = TimedTask.createTask(() =>
            {
                if (mTotalText.Length == 0)
                {
                    mTask.Stop();
                    OnFinish();
                }
                else
                {
                    Text += mTotalText[0];
                    mTotalText = mTotalText.Substring(1, mTotalText.Length - 1);
                }
            }, 0.05, true);

            mTask.Start(); ;
        }
    }
}
