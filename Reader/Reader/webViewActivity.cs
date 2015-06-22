using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Android.Webkit;
using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Newtonsoft.Json;

namespace Reader
{
	[Activity (Label = "webViewActivity")]			
	public class webViewActivity : Activity
	{
		WebView rssWebview;
		string link;
		//RssFeed rssFeed;
		protected override void OnCreate (Bundle bundle)
		{
			base.OnCreate (bundle);
			// Create your application here
			SetContentView (Resource.Layout.webviewLayout);
			//rssWebview.Title = Intent.GetStringExtra("title");
		//	rssFeed = JsonConvert.DeserializeObject<RssFeed> (Intent.GetStringExtra("feed"));
			link = Intent.GetStringExtra("feed");
			rssWebview = FindViewById<WebView> (Resource.Id.rssWebview);
			rssWebview.Settings.JavaScriptEnabled = true;
			rssWebview.LoadUrl (link);

		}
	}
}

