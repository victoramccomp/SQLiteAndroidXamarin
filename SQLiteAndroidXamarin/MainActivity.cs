using System.IO;
using SQLite;
using Android.App;
using Android.Widget;
using Android.OS;
using Android.Content;

namespace SQLiteAndroidXamarin
{
    [Activity(MainLauncher = true, Icon = "@drawable/usaicon")]
    public class MainActivity : Activity
    {
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.Main);


            var path = System.Environment.GetFolderPath(System.Environment.SpecialFolder.Personal);
            path = Path.Combine(path, "Base.db3");
            var conn = new SQLiteConnection(path);
            conn.CreateTable<Information>();

            Button btnCalc = FindViewById<Button>(Resource.Id.btncalc);
            EditText txtIncoming = FindViewById<EditText>(Resource.Id.txtincoming);
            EditText txtExpenditures = FindViewById<EditText>(Resource.Id.txtexpenditures);

            double result, incoming, expenditures;

            btnCalc.Click += delegate
            {
                try
                {
                    incoming = double.Parse(txtIncoming.Text == "" ? "0" : txtIncoming.Text);
                    expenditures = double.Parse(txtExpenditures.Text == "" ? "0" : txtExpenditures.Text);
                    result = expenditures - incoming;

                    var information = new Information();
                    information.IncomingUSA = incoming;
                    information.ExpenditureUSA = expenditures;

                    conn.Insert(information);

                    Calc(result);
                }
                catch (System.Exception ex)
                {
                    Toast.MakeText(this, ex.Message, ToastLength.Short).Show();
                    throw;
                }
            };
        }

        public void Calc(double result)
        {
            Intent objIntent = new Intent(this, typeof(CalcStockCapital));
            objIntent.PutExtra("result", result);
            StartActivity(objIntent);
        }
    }

    public class Information
    {
        public double IncomingUSA { get; set; }
        public double ExpenditureUSA { get; set; }
    }
}

