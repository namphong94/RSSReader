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
		string title;
		TextView webTitle;
		//RssFeed rssFeed;
		protected override void OnCreate (Bundle bundle)
		{
			base.OnCreate (bundle);
			// Create your application here
			RequestWindowFeature(WindowFeatures.NoTitle);
			SetContentView (Resource.Layout.webviewLayout);
			link = Intent.GetStringExtra("feed");
			title = Intent.GetStringExtra ("title");

			webTitle = FindViewById<TextView> (Resource.Id.titleWebView);
			webTitle.Text = title;

			rssWebview = FindViewById<WebView> (Resource.Id.rssWebview);
			rssWebview.Settings.JavaScriptEnabled = true;
			rssWebview.LoadUrl (link);

		}
	}
}

