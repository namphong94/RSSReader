using System;
using System.Net;
using Android.App;
using Android.Content;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.OS;
using System.IO;
using System.Xml;
using Reader;
using System.Collections.Generic;
using System.Runtime.Serialization;
using Newtonsoft.Json;

namespace Reader
{
	[Activity (Label = "Reader", MainLauncher = true, Icon = "@drawable/icon")]
	public class MainActivity : Activity
	{
		private ListView rssListView;
		private string channel = "http://vnexpress.net/rss/tin-moi-nhat.rss";
		private RSS rssReader= new RSS();
		List<RssFeed> feeds =new List<RssFeed>();
		protected override void OnCreate (Bundle bundle)
		{
			base.OnCreate (bundle);
			// Set our view from the "main" layout resource
			SetContentView (Resource.Layout.Main);
			// Get our button from the layout resource,
			// and attach an event to it
			rssListView = FindViewById<ListView>(Resource.Id.RssView);
			//string[,] rssData = rssReader.getRSSData(channel);
			//rssListView.Adapter = new ArrayAdapter (this, Android.Resource.Layout.SimpleListItem1, rssData);
			//rssReader = rssReader.getRSSData(channel);
			feeds = rssReader.getRSSData(channel);
			RssAdapter adapter = new RssAdapter (this, feeds);
			rssListView.Adapter = adapter;
			//foreach(RssFeed feed in rssReader.Feeds)
			//{
			//	feedTitle.AddRange (rssReader.Feeds.);
			//	//rssListView.Adapter = new ArrayAdapter (this, Android.Resource.Layout.SimpleListItem1, feed.Title);
			//}
			//for (int i = 0; i < rssReader.Length; i++) {
			//	feedTitle.Add (rssReader[i].Title);
			//}
			//rssListView.Adapter = new ArrayAdapter (this, Android.Resource.Layout.SimpleListItem1, feedTitle);
			rssListView.ItemClick += (object sender, AdapterView.ItemClickEventArgs e) => {
				//string selectedfromlist = rssListView.GetItemAtPosition (e.Position).ToString ();
				string show = feeds[e.Position].Description.Replace("\\<.*?>","");
				//Toast.MakeText(this,show,ToastLength.Short).Show();

				Intent intent = new Intent(this,typeof(webViewActivity));
				//intent.PutExtra(feeds[e.Position].Description,"Data");
				//intent.PutExtra(feeds[e.Position].Link,"Link");
				intent.PutExtra("feed",JsonConvert.SerializeObject(feeds[e.Position]));
				StartActivity(intent);
			};

			rssListView.ItemLongClick += (object sender, AdapterView.ItemLongClickEventArgs e) => {
				var uri = Android.Net.Uri.Parse (feeds[e.Position].Link);
				var intent = new Intent (Intent.ActionView, uri); 
				StartActivity (intent);   
			};
		}
	}
}


