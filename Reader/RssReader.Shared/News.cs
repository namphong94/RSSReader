using System;
using System.Collections.Generic;

namespace Reader {
	public class News {

		public News ()
		{
		}

		public static List<News> CategoryList()
		{
			var newDataList = new List<News>();
			newDataList.Add (new News ("Thoi su"));
			newDataList.Add (new News ("Kinh doanh"));
			newDataList.Add (new News ("Du lich"));
			newDataList.Add (new News ("The thao"));

			return newDataList;
		}

		public News (string newCategory = "Temporary Id")
		{
			Category = newCategory;
		}

		public string Category { get; set; }
	}
}

