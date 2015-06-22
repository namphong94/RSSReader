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
using System.Text.RegularExpressions;

namespace Reader
{
	public class RSS
	{
		private RssFeed rssFeed;
		private string title;
		private string description;
		private string link;
		private string image;
		public List<RssFeed> Feeds = new List<RssFeed> ();

		public List<RssFeed> getRSSData(string channel)
		{
			XmlDocument rssDoc = new XmlDocument ();
			rssDoc.Load (channel);
			XmlNodeList rssItems = rssDoc.SelectNodes ("rss/channel/item");

			for (int i = 0; i < rssItems.Count; i++)
			{
				XmlNode rssNode;

				rssNode = rssItems.Item (i).SelectSingleNode ("title");
				if (rssNode != null)
					title = rssNode.InnerText;
				else
					title = "";


				rssNode = rssItems.Item(i).SelectSingleNode("description");

				description = rssNode.InnerText;
				int start = description.IndexOf ("src=") + 5;

				Regex regex = new Regex ("src=\"(.*(jpg|jpeg|png))");
				var result = regex.Match (description);

				try {
					image = result.Groups [1].ToString ();
				}
				catch(Exception)
				{
					Console.WriteLine (description);
					XmlNodeList rssLogo = rssDoc.SelectNodes ("rss/channel/image");
					XmlNode rssNodeLogo = rssLogo.Item (i).SelectSingleNode ("url");
					image = rssNodeLogo.InnerText;
				}


				rssNode = rssItems.Item(i).SelectSingleNode("link");
				if (rssNode != null) 
					link = rssNode.InnerText;
				else 
					link = "";


				rssFeed = new RssFeed (title, description, link, image);
				Feeds.Add (rssFeed);
			}
			return Feeds;
		}
	}
}

