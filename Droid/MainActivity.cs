using System;
using Android.App;
using Android.Content;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.OS;

namespace Droid
{
    [Activity(Label = "@string/ApplicationName", MainLauncher = true, Icon = "@drawable/icon")]
    public class MainActivity : Activity
    {
        EditText phoneNumberText;

        Button translateButton;

        Button callButton;

        string translatedNumber;

        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);

            // Set our view from the "main" layout resource
            SetContentView(Resource.Layout.Main);

            // Get our button from the layout resource,
            // and attach an event to it
            phoneNumberText = FindViewById<EditText>(Resource.Id.PhoneNumberText);

            translateButton = FindViewById<Button>(Resource.Id.TranslateButton);

            callButton = FindViewById<Button>(Resource.Id.CallButton);

            callButton.Enabled = false;

            translateButton.Click += new EventHandler(translateButton_Click);

            callButton.Click += new EventHandler(callButton_Click);

            Button postPhoneButton = FindViewById<Button>(Resource.Id.postPhoneButton);
            postPhoneButton.Click += delegate
            {
                Intent intent = new Intent(this, typeof(TextViewScreenActivity));
                intent.PutExtra("phone", phoneNumberText.Text);
                StartActivity(intent);
            };

            Button setSwitchButton = FindViewById<Button>(Resource.Id.setSwitchButton);
            setSwitchButton.Click += delegate
            {
                Intent intent = new Intent(this, typeof(SwitchToggleActivity));
                StartActivity(intent);
            };
        }

        private void translateButton_Click(object sender, EventArgs e)
        {
            translatedNumber = PhoneTranslator.ToNumber(phoneNumberText.Text);

            if (string.IsNullOrWhiteSpace(translatedNumber))
            {
                callButton.Text = "Call";
                callButton.Enabled = false;
                callButton.SetBackgroundColor(Android.Graphics.Color.Red);
            }
            else
            {
                callButton.Text = "Call" + translatedNumber;
                callButton.Enabled = true;
                callButton.SetBackgroundColor(Android.Graphics.Color.Green);
            }
        }

        private void callButton_Click(object sender, EventArgs e)
        {
            var callDialog = new AlertDialog.Builder(this);

            callDialog.SetMessage(callButton.Text + "?");

            callDialog.SetNeutralButton("Call", delegate
            {
                var callIntent = new Intent(Intent.ActionCall);

                callIntent.SetData(Android.Net.Uri.Parse("tel:" + translatedNumber));

                StartActivity(callIntent);
            });

            callDialog.SetNegativeButton("Cancel", delegate { });

            callDialog.Show();
        }

        protected override void OnNewIntent(Intent intent)
        {
            Finish();
        }
    }
}

