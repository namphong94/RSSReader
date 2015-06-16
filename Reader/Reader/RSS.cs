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
using System.Collections.Generic;

namespace Reader
{
	public class RSS
	{
		private RssFeed rssFeed;
		private string title;
		private string description;
		private string link;
		public List<RssFeed> Feeds = new List<RssFeed> ();

		public List<RssFeed> getRSSData(string channel)
		{
			
			/*WebRequest myRequest = WebRequest.Create ("http://vnexpress.net/rss/khoa-hoc.rss");
			WebResponse myReponse = myRequest.GetResponse ();
			Stream rssStream = myReponse.GetResponseStream ();
			XmlDocument rssDoc = new XmlDocument ();
			XmlNodeList rssItems = rssDoc.SelectNodes("rss/channel/item");
			rssDoc.Load (rssStream);*/
			XmlDocument rssDoc = new XmlDocument ();
			rssDoc.Load (channel);
			XmlNodeList rssItems = rssDoc.SelectNodes ("rss/channel/item");

			for (int i = 0; i < rssItems.Count; i++) {
				XmlNode rssNode;
				rssNode = rssItems.Item (i).SelectSingleNode ("title");
				if (rssNode != null) {
					title = rssNode.InnerText;
					//this.Feeds[i].Title = rssNode.InnerText;
				} else {
					//this.Feeds[i].Title = "";
					title = "";
				}
				rssNode = rssItems.Item(i).SelectSingleNode("description");
				if (rssNode != null) {
					description = rssNode.InnerText;
					//this.Feeds[i].Description = rssNode.InnerText;
				} else {
					description="";
					//this.Feeds[i].Description = "";
				}
				rssNode = rssItems.Item(i).SelectSingleNode("link");
				if (rssNode != null) {
					link = rssNode.InnerText;
					//this.Feeds[i].Link= rssNode.InnerText;
				} else {
					link = "";
					//this.Feeds[i].Link = "";
				}
				rssFeed = new RssFeed (title, description, link);
				Feeds.Add (rssFeed);
			}
			return Feeds;
		}
	}
}

