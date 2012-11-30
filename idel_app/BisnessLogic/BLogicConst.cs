using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace idel_app.BisnessLogic {
	static public class BLogicConst {
		public const bool REQUEST_ENTERED_WARE = true;
		public const bool REQUEST_WAIT_WARE = false;
		public const bool REQUEST_PASSED = true;
		public const bool REQUEST_WAIT = false;

		private const char NEW_STRING_CHAR = '\n';
		private const char TAB_CHAR = '\t';
		static public List<List<string>> ConvertToListList(string s) {
			List<List<string>> returnList = new List<List<string>>();
			List<string> list = s.Split(NEW_STRING_CHAR).ToList<string>();
			foreach (string l in list) {
				returnList.Add(l.Split(TAB_CHAR).ToList<string>());
			}
			return returnList;
		}
		static public string ConvertToString(List<List<string>> list) {
			string s = "";
			foreach (List<string> l in list) {
				foreach (string str in l) {
					s += str + TAB_CHAR;
				}
				s += NEW_STRING_CHAR;
			}
			return s;
		}
	}
}