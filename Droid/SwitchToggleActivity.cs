using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;

namespace Droid
{
    [Activity(Label = "SwitchToggleActivity")]
    public class SwitchToggleActivity : Activity
    {
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            // Create your application here
            SetContentView(Resource.Layout.SwitchToggle);

            ToggleButton toggleButton = FindViewById<ToggleButton>(Resource.Id.toggleButton1);

            Switch _switch = FindViewById<Switch>(Resource.Id.switch1);

            TextView msg = FindViewById<TextView>(Resource.Id.textView1);

            string template = "{0} Button的状态是：{1}";

            toggleButton.Click += (s, e) =>
            {
                string txtStatus = "";
                if (toggleButton.Checked)
                {
                    txtStatus = toggleButton.TextOn;
                }
                else
                {
                    txtStatus = toggleButton.TextOff;
                }

                msg.Text = string.Format(template, "Toggle", txtStatus);
            };

            _switch.Click += (s, e) =>
            {
                string txtStatus = "";
                if (_switch.Checked)
                {
                    txtStatus = _switch.TextOn;
                }
                else
                {
                    txtStatus = _switch.TextOff;
                }

                msg.Text = string.Format(template, "Switch", txtStatus);
            };
        }

        DateTime? lastBackKeyDownTime;

        public override bool OnKeyDown([GeneratedEnum] Keycode keyCode, KeyEvent e)
        {
            if (keyCode == Keycode.Back && e.Action == KeyEventActions.Down)
            {
                if (!lastBackKeyDownTime.HasValue || DateTime.Now - lastBackKeyDownTime.Value > new TimeSpan(0, 0, 2))
                {
                    Toast.MakeText(this, "再按一次退出程序", ToastLength.Short).Show();

                    lastBackKeyDownTime = DateTime.Now;
                }
                else
                {
                    Intent intent = new Intent();

                    intent.SetClass(this, typeof(MainActivity));

                    StartActivity(intent);
                }

                return true;
            }

            return base.OnKeyDown(keyCode, e);
        }
    }
}