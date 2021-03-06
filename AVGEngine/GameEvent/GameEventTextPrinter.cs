﻿using AVGEngine.Control;
using Xamarin.Forms;

namespace AVGEngine.GameEvent
{
    public class GameEventTextPrinterDialogLabel : GameEventTextPrinter
    {
        private readonly DialogLabel mDialogLabel;
        private readonly bool mDoNextAfterClick;

        public GameEventTextPrinterDialogLabel(DialogLabel label, string message, bool doNextAfterClick = true) : base(message)
        {
            mDialogLabel = label;
            mDoNextAfterClick = doNextAfterClick;
        }

        protected override string GetText()
        {
            return mDialogLabel.Text;
        }

        protected override void SetText(string str)
        {
            mDialogLabel.Text = str;
        }

        public override void Do()
        {
            mDialogLabel.Clean(() => base.Do());
        }

        protected override void OnFinish()
        {
            if (mDoNextAfterClick)
                new GameEventWaitInput(base.OnFinish).Do();
            else
                base.OnFinish();
        }

        //是否打印满了
        private bool mIsFull;
        protected override bool CanPrint()
        {
            if (mDialogLabel.IsFull && !mIsFull)
            {
                mIsFull = true;
                new GameEventWaitInput(() =>
                    mDialogLabel.Clean(() =>
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
            mTotalText = message + " ";
        }

        protected abstract bool CanPrint();

        public override void Do()
        {
            mTask = TimedTask.createTask((task) =>
            {
                if (mTotalText.Length == 0)
                {
                    task.Stop();
                    OnFinish();
                }
                else
                {
                    //不能再显示了
                    if (!CanPrint())
                        return;

                    //禁止首位为回车
                    if (Text != "" || mTotalText[0] != '\n')
                        Text += mTotalText[0];

                    if (!CanPrint())
                    {
                        Text = Text.Remove(Text.Length - 1);
                    }
                    else
                        mTotalText = mTotalText.Substring(1, mTotalText.Length - 1);
                }
            }, 0.025, true);

            mTask.Start();
        }
    }
}
