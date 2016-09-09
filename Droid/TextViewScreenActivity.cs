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
    [Activity(Label = "TextViewScreenActivity")]
    public class TextViewScreenActivity : Activity
    {
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            // Create your application here
            SetContentView(Resource.Layout.TextView);

            string phone = Intent.GetStringExtra("phone");

            TextView phoneText = FindViewById<TextView>(Resource.Id.textView4);
            phoneText.Text += string.Format("，新传入电话：{0}", phone);
        }
    }
}