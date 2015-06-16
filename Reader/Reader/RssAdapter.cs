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


namespace Reader
{
	class RssAdapter: BaseAdapter<RssFeed>
	{
		private List<RssFeed> feeds;
		private Context mContext;
		public RssAdapter(Context Context, List<RssFeed> feed)
		{
			mContext = Context;
			feeds = feed;
		}
		public override int Count
		{
			get
			{
				return feeds.Count;
			}
		}
		public override long GetItemId(int position)
		{
			return position;
		}

		public override RssFeed this[int position]
		{
			get {return feeds [position]; }
		}
		public override View GetView (int position, View convertView, ViewGroup parent)
		{
			View row = convertView;
			if (row == null) {
				row = LayoutInflater.From (mContext).Inflate (Resource.Layout.Rss_layout,null,false);
			}
			TextView txtTitle = row.FindViewById<TextView> (Resource.Id.TitleText);
			txtTitle.Text = feeds [position].Title;

			return row;
		}
	}
}

