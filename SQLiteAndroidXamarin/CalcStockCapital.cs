using System;
using System.IO;
using Android.App;
using Android.Content;
using Android.OS;
using Android.Widget;
using SQLite;

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

                var path = System.Environment.GetFolderPath(System.Environment.SpecialFolder.Personal);
                path = Path.Combine(path, "Base.db3");
                var conn = new SQLiteConnection(path);

                var elements = from s in conn.Table<Information>()
                               select s;

                foreach (var item in elements)
                {
                    Toast.MakeText(this, item.IncomingUSA.ToString(), ToastLength.Short).Show();
                    Toast.MakeText(this, item.ExpenditureUSA.ToString(), ToastLength.Short).Show();
                }

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