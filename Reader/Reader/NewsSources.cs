using System;
using System.Collections.Generic;

namespace Reader
{
	public class NewsSources
	{
		public List<string> NewsSourcesList { get; set; }
		public NewsSources ()
		{
			NewsSourcesList = new List<string>();
			NewsSourcesList.Add ("Vnexpress");
			NewsSourcesList.Add ("VietnamNet");
			NewsSourcesList.Add ("ThanhNien");
		}

	}
}

