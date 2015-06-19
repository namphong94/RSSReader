using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;

namespace ExpandableListViewExample {
	public class NewsAdapter : BaseAdapter<News>{
		readonly Activity context;

		public NewsAdapter(Activity newContext, List<News> newData) : base ()
		{
			context = newContext;
			NewsList = newData;
		}

		public List<News> NewsList { get; set; }

		public override int Count {
			get {
				return NewsList.Count;
			}
		}

		public override View GetView (int position, View convertView, ViewGroup parent)
		{
			View newView = convertView; // re-use an existing view, if one is available

			if (newView == null) // otherwise create a new one
				newView = context.LayoutInflater.Inflate(Resource.Layout.DataListItem, null);
			newView.FindViewById<TextView>(Resource.Id.Category).Text = NewsList[position].Category;

			return newView;
		}

		public override long GetItemId (int position)
		{
			return position;
		}

		public override News this[int index] {
			get {
				return NewsList [index];
			}
		}
	}
}

