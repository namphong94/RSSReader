using System;

using Android.App;
using Android.Content;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.OS;
using Android.Net;

namespace InternetCheck
{
	[Activity (Label = "InternetCheck", MainLauncher = true, Icon = "@drawable/icon")]
	public class MainActivity : Activity
	{


		protected override void OnCreate (Bundle bundle)
		{
			base.OnCreate (bundle);
			SetContentView (Resource.Layout.Main);

		}

		protected override void OnStart ()
		{
			base.OnStart ();

			var connectivityManager = (ConnectivityManager)GetSystemService(ConnectivityService);
			var activeConnection = connectivityManager.ActiveNetworkInfo;
			if ((activeConnection == null)  || !activeConnection.IsConnected)
			{
				AlertDialog.Builder builder = new AlertDialog.Builder(this);
				builder.SetTitle("Connection Error");
				builder.SetMessage("Cannot connect to the internet!");
				builder.SetCancelable(false);
				builder.SetPositiveButton("Exit", delegate { Finish(); });
				builder.Show();
			}

		}
	}
}


