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
			SetContentView (Resource.Layout.Main);

			feeds = rssReader.getRSSData(channel);

			var rssAdapter = new RssAdapter (this, feeds);
			var rssListView = FindViewById<ListView> (Resource.Id.RssView);
			rssListView.Adapter = rssAdapter;


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
				intent.PutExtra("feed",feeds[e.Position].Link);
				StartActivity(intent);
			};

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


