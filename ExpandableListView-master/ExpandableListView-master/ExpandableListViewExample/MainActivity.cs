﻿using System;
using Android.App;
using Android.Content;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.OS;

namespace ExpandableListViewExample {
	[Activity (Label = "ExpandableListViewExample", MainLauncher = true)]
	public class MainActivity : Activity {

		protected override void OnCreate (Bundle bundle)
		{
			base.OnCreate (bundle);

			// Set our view from the "main" layout resource
			SetContentView (Resource.Layout.Main);

			var listView = FindViewById<ExpandableListView> (Resource.Id.listView);
			listView.SetAdapter (new ExpandableDataAdapter ( this, News.CategoryList () ) );

		}
	}
}


