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
using Android.Graphics;


namespace Reader
{
	class RssAdapter: BaseAdapter<RssFeed>
	{
		private List<RssFeed> feeds;
		Activity _activity;

		public Bitmap GetImageBitmapFromUrl(string url)
		{
			Bitmap imageBitmap = null;

			using (var webClient = new WebClient())
			{
				var imageBytes = webClient.DownloadData(url);
				if (imageBytes != null && imageBytes.Length > 0)
				{
					imageBitmap = BitmapFactory.DecodeByteArray(imageBytes, 0, imageBytes.Length);
				}
			}

			return imageBitmap;		
			}

		public RssAdapter(Activity activity, List<RssFeed> feed)
		{
			_activity = activity;
			feeds = feed;
		}

		public void setData(List<RssFeed> theFeed)
		{
			feeds = theFeed;
			notifyDataSetChanged ();
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
			ViewHolder holder;

			if (convertView == null)
			{
				convertView = _activity.LayoutInflater.Inflate(Resource.Layout.Rss_layout, null);
				holder = new ViewHolder();
				holder.newsImageView = (ImageView) convertView.FindViewById(Resource.Id.NewsImage);
				holder.titleTextView = (TextView) convertView.FindViewById(Resource.Id.TitleText);
				convertView.Tag = holder;
			}

			else
			{
				holder = (ViewHolder) convertView.Tag;
			}

			RssFeed news = (RssFeed) GetItem(position);
			holder.titleTextView.Text = news.Title;
			holder.newsImageView.SetImageBitmap (GetImageBitmapFromUrl (news.Image));

			return convertView;
		}
		public void notifyDataSetChanged() // Create this function in your adapter class
				{
			base.NotifyDataSetChanged ();
		}
	}
	
}

