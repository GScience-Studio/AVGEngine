using AVGEngine.Control;
using Xamarin.Forms;

namespace AVGEngine.GameEvent
{
    public class GameEventTextPrinterDialogLabel : GameEventTextPrinter
    {
        private readonly DialogLabel mDialogLabel;

        public GameEventTextPrinterDialogLabel(DialogLabel label, string message) : base(message)
        {
            mDialogLabel = label;
        }

        protected override string GetText()
        {
            return mDialogLabel.Text;
        }

        protected override void SetText(string str)
        {
            mDialogLabel.Text = str;
        }

        //是否打印满了
        private bool mIsFull;
        protected override bool CanPrint()
        {
            if (mDialogLabel.Realheight + mDialogLabel.FontSize / 2 >= mDialogLabel.Height && !mIsFull)
            {
                mIsFull = true;
                new GameEventWaitInput(() =>
                    mDialogLabel.clean(() =>
                    mIsFull = false
                )).Do();
            }

            return !mIsFull;
        }
    }

    public class GameEventTextPrinterButton : GameEventTextPrinter
    {
        private readonly Xamarin.Forms.Button mButton;

        public GameEventTextPrinterButton(Xamarin.Forms.Button button, string message) : base(message)
        {
            mButton = button;
        }

        public override void Do()
        {
            base.Do();
            mButton.IsVisible = true;
        }

        protected override string GetText()
        {
            return mButton.Text;
        }

        protected override void SetText(string str)
        {
            mButton.Text = str;
        }

        protected override bool CanPrint()
        {
            return true;
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

        protected abstract bool CanPrint();

        public override void Do()
        {
            Text = "";

            mTask = TimedTask.createTask((task) =>
            {
                if (mTotalText.Length == 0)
                {
                    task.Stop();
                    OnFinish();
                }
                else
                {
                    if (!CanPrint())
                        return;
                    Text += mTotalText[0];
                    //如果修改后不能再显示则说明上次的被丢弃了
                    if (CanPrint())
                        mTotalText = mTotalText.Substring(1, mTotalText.Length - 1);
                    else
                        Text = Text.Remove(Text.Length - 1);
                }
            }, 0.01, true);

            mTask.Start(); ;
        }
    }
}
