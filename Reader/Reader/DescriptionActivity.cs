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
using Newtonsoft.Json;

namespace Reader
{
	[Activity (Label = "DescriptionActivity")]			
	public class DescriptionActivity : Activity
	{
		RssFeed feed;
		protected override void OnCreate (Bundle bundle)
		{
			base.OnCreate (bundle);

			// Create your application here
			SetContentView (Resource.Layout.descriptionLayout);

			TextView descriptionRSS = FindViewById<TextView> (Resource.Id.description);
			feed = JsonConvert.DeserializeObject<RssFeed> (Intent.GetStringExtra ("feed"));
			descriptionRSS.Text = feed.Description.ToString ();

			descriptionRSS.Click +=	delegate {
				Intent intent = new Intent (this, typeof(webViewActivity));
				intent.PutExtra ("link", JsonConvert.SerializeObject (feed));
				StartActivity (intent);
			};
		}
	}
}

