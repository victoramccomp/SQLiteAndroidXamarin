using System;
using Android.App;
using Android.Content;
using Android.OS;
using Android.Widget;

namespace SQLiteAndroidXamarin
{
    [Activity(Label = "CalcStockCapital")]
    public class CalcStockCapital : Activity
    {
        double defaultValue;

        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);
            SetContentView(Resource.Layout.CalcStockCapital);

            EditText txtResult = FindViewById<EditText>(Resource.Id.txtresult);
            ImageView imgFlag = FindViewById<ImageView>(Resource.Id.imgflag);
            Button btnExit = FindViewById<Button>(Resource.Id.btnexit);

            try
            {
                txtResult.Text = Intent.GetDoubleExtra("result", defaultValue).ToString();
                imgFlag.SetImageResource(Resource.Drawable.usa);
            }
            catch (Exception ex)
            {
                Toast.MakeText(this, ex.Message, ToastLength.Short).Show();
                throw;
            }

            btnExit.Click += delegate
            {
                Process.KillProcess(Process.MyPid());
            };
        }
    }
}