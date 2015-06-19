using System;
using System.Collections.Generic;

namespace ExpandableListViewExample
{
	public class NewsSources
	{
		public List<string> NewsSourcesList { get; set; }
		public NewsSources ()
		{
			NewsSourcesList = new List<string>();
			NewsSourcesList.Add ("Vnexpress");
			NewsSourcesList.Add ("ZingVN");
			NewsSourcesList.Add ("GenK");
		}

	}
}

