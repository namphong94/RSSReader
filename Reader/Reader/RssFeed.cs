﻿using System;
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

namespace Reader
{
	public class RssFeed : Java.Lang.Object
	{
		public string Title {
			get;
			set;
		}
		public string Description {
			get;
			set;
		}
		public string Image {
			get;
			set;
		}
		public string Link {
			get;
			set;
		}
		public RssFeed(string title,string description,string link,string image)
		{
			Title = title;
			Description = description;
			Link = link;
			Image = image;
		}
	}
}

