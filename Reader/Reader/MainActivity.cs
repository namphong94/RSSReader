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
using Android.Support.V4;
using Android.Support.V4.Widget;
using Android.Support.V4.App;

namespace Reader
{
	[Activity (Label = "Reader", MainLauncher = true, Icon = "@drawable/icon",Theme ="@style/CustomActionBarTheme")]
	public class MainActivity : Activity
	{
		private ListView rssListView;
		private string channel = "http://vnexpress.net/rss/tin-moi-nhat.rss";
		private RSS rssReader= new RSS();
		List<RssFeed> feeds =new List<RssFeed>();

		DrawerLayout mDrawerLayout;
		List<string> mLeftitem = new List<string>();
		ListView mLeftDrawer;
		ArrayAdapter mLeftAdapter;
		ActionBarDrawerToggle mDrawerToggle;

		protected override void OnCreate (Bundle bundle)
		{
			base.OnCreate (bundle);
			// Set our view from the "main" layout resource
			SetContentView (Resource.Layout.Main);

			rssListView = FindViewById<ListView>(Resource.Id.RssView);
			feeds = rssReader.getRSSData(channel);
			RssAdapter adapter = new RssAdapter (this, feeds);
			rssListView.Adapter = adapter;	


			mDrawerLayout = FindViewById<DrawerLayout> (Resource.Id.myDrawer);
			mLeftDrawer = FindViewById<ListView> (Resource.Id.leftListView);

			mLeftitem.Add ("VnExpress");
			mLeftitem.Add ("ZingVN");
			mLeftitem.Add ("Genk");

			mDrawerToggle = new MyActionBarDrawerToggle (this, mDrawerLayout, Resource.Drawable.ic_navigation_drawer, Resource.String.open_drawer, Resource.String.close_drawer);

			mLeftAdapter = new ArrayAdapter (this, Android.Resource.Layout.SimpleListItem1, mLeftitem);
			mLeftDrawer.Adapter = mLeftAdapter;

			mDrawerLayout.SetDrawerListener(mDrawerToggle);
			ActionBar.SetDisplayHomeAsUpEnabled (true);
			ActionBar.SetHomeButtonEnabled (true);	
			ActionBar.SetDisplayShowTitleEnabled (true);


			rssListView.ItemClick += (object sender, AdapterView.ItemClickEventArgs e) => {
				Intent intent = new Intent(this,typeof(webViewActivity));
				intent.PutExtra("feed",JsonConvert.SerializeObject(feeds[e.Position]));
				StartActivity(intent);
			};

			/*rssListView.ItemLongClick += (object sender, AdapterView.ItemLongClickEventArgs e) => {
				var uri = Android.Net.Uri.Parse (feeds[e.Position].Link);
				var intent = new Intent (Intent.ActionView, uri); 
				StartActivity (intent);   
			};
			*/
		}

		protected override void OnPostCreate (Bundle savedInstanceState)
		{
			base.OnPostCreate (savedInstanceState);
			mDrawerToggle.SyncState ();
		}

		public override bool OnOptionsItemSelected (IMenuItem item)
		{
			if (mDrawerToggle.OnOptionsItemSelected (item)) 
			{
				return true;
			}

			return base.OnOptionsItemSelected (item);
		}
	}
}


