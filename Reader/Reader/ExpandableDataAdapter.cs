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

namespace Reader {
	public class ExpandableDataAdapter : BaseExpandableListAdapter 
	{
		
		NewsSources NewsSourcesList = new NewsSources();
		readonly Activity Context;
		protected List<News> NewsList { get; set; }

		public ExpandableDataAdapter(Activity newContext, List<News> newList) : base()
		{
			Context = newContext;
			NewsList = newList;
		}



		public override View GetGroupView (int groupPosition, bool isExpanded, View convertView, ViewGroup parent)
		{
			View header = convertView;

			if (header == null) {
				header = Context.LayoutInflater.Inflate (Resource.Layout.ListGroup, null);
			}
			header.FindViewById<TextView> (Resource.Id.DataHeader).Text = NewsSourcesList.NewsSourcesList[groupPosition];

			return header;
		}

		public override View GetChildView (int groupPosition, int childPosition, bool isLastChild, View convertView, ViewGroup parent)
		{
			View row = convertView;

			if (row == null) 
				row = Context.LayoutInflater.Inflate (Resource.Layout.DataListItem, null);

			string newCategory = "";

			GetChildViewHelper (groupPosition, childPosition, out newCategory);
			row.FindViewById<TextView> (Resource.Id.Category).Text = newCategory;

			return row;
		}

		public override int GetChildrenCount (int groupPosition)
		{

			return NewsList.Count;
		}

		public override int GroupCount {
			get {
				return 3;
			}
		}

		private void GetChildViewHelper (int groupPosition, int childPosition, out string Category)
		{
			Category = NewsList [childPosition].Category;
		}

		#region implemented abstract members of BaseExpandableListAdapter

		public override Java.Lang.Object GetChild (int groupPosition, int childPosition)
		{
			throw new NotImplementedException ();
		}

		public override long GetChildId (int groupPosition, int childPosition)
		{
			return childPosition;
		}

		public override Java.Lang.Object GetGroup (int groupPosition)
		{
			throw new NotImplementedException ();
		}

		public override long GetGroupId (int groupPosition)
		{
			return groupPosition;
		}

		public override bool IsChildSelectable (int groupPosition, int childPosition)
		{
			return true;
		}

		public override bool HasStableIds {
			get {
				return true;
			}
		}

		#endregion
	}
}

