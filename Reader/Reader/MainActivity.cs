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
using Android.Net;
using System.Threading.Tasks;

namespace Reader
{
	[Activity (Label = "Reader", MainLauncher = true, Icon = "@drawable/icon",Theme ="@style/CustomActionBarTheme")]
	public class MainActivity : Activity
	{
		private static int groupPosition;
		private string channel = "http://vnexpress.net/rss/thoi-su.rss";
		private RSS rssReader= new RSS();
		List<RssFeed> feeds =new List<RssFeed>();
		DrawerLayout mDrawerLayout;
		List<string> mLeftitem = new List<string>();
		ExpandableListView mLeftDrawer;
		//Adapter mLeftAdapter;
		ListView rssListView;
		RssAdapter rssAdapter;
		//ActionBarDrawerToggle mDrawerToggle;
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

		protected override void OnCreate (Bundle bundle)
		{
			RequestWindowFeature (WindowFeatures.NoTitle);
			base.OnCreate (bundle);
			SetContentView (Resource.Layout.Main);

			feeds = rssReader.getRSSData(channel);

			rssAdapter = new RssAdapter (this, feeds);
			rssListView = FindViewById<ListView> (Resource.Id.RssView);
			rssListView.Adapter = rssAdapter;


			mDrawerLayout = FindViewById<DrawerLayout> (Resource.Id.myDrawer);
			mLeftDrawer = FindViewById<ExpandableListView> (Resource.Id.leftListView);

			//mLeftitem.Add ("VnExpress");
			//mLeftitem.Add ("ZingVN");
			//mLeftitem.Add ("Genk");

			//mDrawerToggle = new MyActionBarDrawerToggle (this, mDrawerLayout, Resource.Drawable.ic_navigation_drawer, Resource.String.open_drawer, Resource.String.close_drawer);

			/*mLeftAdapter = new ArrayAdapter (this, Android.Resource.Layout.SimpleListItem1, mLeftitem);
			mLeftDrawer.Adapter = mLeftAdapter;
			*/

			mLeftDrawer.SetAdapter (new ExpandableDataAdapter (this, News.CategoryList()));

			//mDrawerLayout.SetDrawerListener(mDrawerToggle);
			//ActionBar.SetDisplayHomeAsUpEnabled (true);
			//ActionBar.SetHomeButtonEnabled (true);	
			//ActionBar.SetDisplayShowTitleEnabled (true);

			var menuButton = FindViewById<ImageButton> (Resource.Id.menuButton); // menu button

			menuButton.Click += (sender, e) => {      // open menu click
				if (mDrawerLayout.IsShown == true) {
					mDrawerLayout.OpenDrawer(mLeftDrawer);
				} else {
					mDrawerLayout.CloseDrawer(mLeftDrawer);
				}

			};

			rssListView.ItemClick += (object sender, AdapterView.ItemClickEventArgs e) => {
				Intent intent = new Intent(this,typeof(webViewActivity));
				intent.PutExtra("feed",feeds[e.Position].Link);
				intent.PutExtra("title",feeds[e.Position].Title);
				StartActivity(intent);
			};

			mLeftDrawer.ChildClick += (object sender, ExpandableListView.ChildClickEventArgs e) => {
				groupPosition = e.GroupPosition;
				this.channel = getChannel(e.GroupPosition,e.ChildPosition);
				Console.WriteLine("group: " + e.GroupPosition + "child:" + e.ChildPosition);
				Refresh();
			};

		}

		protected override void OnPostCreate (Bundle savedInstanceState)
		{
			base.OnPostCreate (savedInstanceState);
			//mDrawerToggle.SyncState ();
		}

		async public void Refresh()
		{
			feeds.Clear ();
			feeds = rssReader.getRSSData(this.channel);
			rssAdapter.setData (feeds);
			await Task.Delay (100);
			//rssListView.Invalidate ();
		}

		public override bool OnOptionsItemSelected (IMenuItem item)
		{
			//if (mDrawerToggle.OnOptionsItemSelected (item)) 
			//{
			//	return true;
			//}

			return base.OnOptionsItemSelected (item);
		}


		public string getChannel(int groupPosition,int childPosition)
		{
			if(childPosition == 0  && groupPosition == 0)
				return "http://vnexpress.net/rss/thoi-su.rss";

			if (childPosition == 1 && groupPosition == 0)
					return "http://vnexpress.net/rss/kinh-doanh.rss";

			if(childPosition == 2 && groupPosition == 0)
					return "http://vnexpress.net/rss/du-lich.rss";

			if(childPosition == 3 && groupPosition == 0)
					return "http://vnexpress.net/rss/the-thao.rss";

			if(childPosition == 0 && groupPosition == 1)
					return "http://vietnamnet.vn/rss/chinh-tri.rss";

			if (childPosition == 1 && groupPosition == 1)
					return "http://vietnamnet.vn/rss/kinh-te.rss";

			if(childPosition == 2 && groupPosition == 1)
				return "http://vietnamnet.vn/rss/ban-doc-phap-luat.rss";

			if(childPosition == 3 && groupPosition == 1)
				return  "http://vietnamnet.vn/rss/cong-nghe-thong-tin-vien-thong.rss";

			if(childPosition == 0 && groupPosition == 2)
				return "http://www.thanhnien.com.vn/rss/chinh-tri-xa-hoi.rss";

			if (childPosition == 1 && groupPosition == 2)
				return "http://www.thanhnien.com.vn/rss/kinh-te.rss";

			if(childPosition == 2 && groupPosition == 2)
				return "http://www.thanhnien.com.vn/rss/du-lich.rss";

			if(childPosition == 3 && groupPosition == 2)
				return "http://www.thanhnien.com.vn/rss/the-thao.rss";
			return null;
		}
	}
}


