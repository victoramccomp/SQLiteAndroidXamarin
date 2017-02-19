using Android.App;
using Android.Widget;
using Android.OS;
using Android.Content;

namespace PatrinomioAndroidXamarin
{
    [Activity(MainLauncher = true, Icon = "@drawable/usaicon")]
    public class MainActivity : Activity
    {
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.Main);

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
}

