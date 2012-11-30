using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace idel_app.BisnessLogic {
	public class DescriptionRequestTable {
		private const int headerIndex = 0;

		List<string> header;
		List<List<string>> table;

		private void initializeHeader(List<string> fields) {
			header = fields;
		}

		public DescriptionRequestTable(List<string> fields) {
			initializeHeader(fields);
		}

		private void addHeaderAndLines(List<string> fields, List<List<string>> datas) {
			initializeHeader(fields);
			table = new List<List<string>>();
			foreach (List<string> l in datas) {
				table.Add(l);
			}
		}

		public DescriptionRequestTable(List<string> fields, List<List<string>> datas) {
			addHeaderAndLines(fields, datas);
		}

		public DescriptionRequestTable(List<string> fields, string datas) {
			addHeaderAndLines(fields, BLogicConst.ConvertToListList(datas));
		}

		public void Push(List<string> datas) {
			table.Add(datas);
		}

		public List<List<string>> Data {
			get {
				return table;
			}
		}

		public string StringData {
			get {
				return BLogicConst.ConvertToString(table);
			}
		}

		public void ChangeAllDatas(List<List<string>> datas) {
			addHeaderAndLines(this.header, datas);
		}

		public void ChangeAllDatas(string datas) {
			addHeaderAndLines(this.header, BLogicConst.ConvertToListList(datas));
		}
	}
}