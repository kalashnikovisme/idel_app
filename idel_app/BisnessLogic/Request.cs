using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using idel_app.Middle;

namespace idel_app.BisnessLogic {
	public class Request {

		#region ProperitesWithOutDescription

		public List<string> DescriptionHeader = new List<string>() { "Id", "Name" };

		private BusinessProperity id = new BusinessProperity(Const.THERE_IS_NOT, "Id");
		public int Id {
			get {
				return Int32.Parse(id.Properity.ToString());
			}
			set {
				id.Properity = value;
			}
		}

		private BusinessProperity createDate = new BusinessProperity(new DateTime(), "Дата создания");
		public DateTime CreateDate {
			get {
				return DateTime.Parse(createDate.Properity.ToString());
			}
			set {
				createDate.Properity = value;
			}
		}

		private BusinessProperity passDate = new BusinessProperity(new DateTime(), "Дата сдачи");
		public DateTime PassDate {
			get {
				return DateTime.Parse(passDate.Properity.ToString());
			}
			set {
				passDate.Properity = value;
			}
		}

		private BusinessProperity employee = new BusinessProperity("", "Ответственный");
		public string Employee {
			get {
				return employee.Properity.ToString();
			}
			set {
				employee.Properity = value;
			}
		}

		private BusinessProperity requestPassStatus = new BusinessProperity(BLogicConst.REQUEST_WAIT, "Статус");
		public bool RequestPassStatus {
			get {
				return Boolean.Parse(requestPassStatus.Properity.ToString());
			}
			set {
				requestPassStatus.Properity = value;
			}
		}

		private BusinessProperity description = new BusinessProperity("", "Описание");
		public DescriptionRequestTable Description {
			get {
				DescriptionRequestTable table = new DescriptionRequestTable(DescriptionHeader);
				table.ChangeAllDatas(description.Properity.ToString());
				return table;
			}
			set {
				description.Properity = value;
			}
		}

        private BusinessProperity title = new BusinessProperity("", "Название");
        public string Title
        {
            get
            {
                return title.Properity.ToString();
            }
            set
            {
                title.Properity = value;
            }
        }

		#endregion

		public Request(int _id, string _title, DateTime _createDate, DateTime _passDate, string _employee,
					   bool _requestStatus, string _description) {
			Id = _id;
			CreateDate = _createDate;
			PassDate = _passDate;
			Employee = _employee;
			RequestPassStatus = _requestStatus;
			Description = new DescriptionRequestTable(DescriptionHeader, BLogicConst.ConvertToListList(_description));
            Title = _title;
        }

		public Request(int _id, string _title, DateTime _createDate, DateTime _passDate, string _employee,
                       bool _requestStatus, List<List<string>> _description)
        {
			Id = _id;
			CreateDate = _createDate;
			PassDate = _passDate;
			Employee = _employee;
			RequestPassStatus = _requestStatus;
			Description = new DescriptionRequestTable(DescriptionHeader, _description);
            Title = _title;
        }

		public Request() {
			Id = 0;
			CreateDate = DateTime.Today;
			PassDate = DateTime.Today + new TimeSpan(1, 0, 0, 0);
			Employee = "Sidius";
			RequestPassStatus = BLogicConst.REQUEST_WAIT;
			Description = new DescriptionRequestTable(DescriptionHeader);
            Title = "Build of Death Star";
		}

		public object[] ProperitesWithOutDescription() {
			return new object[] { Id, Title, CreateDate, PassDate, Employee, RequestPassStatus };
		}

        public List<string> ProperitesForAdding()
        {
            return new List<string> { title.Name, createDate.Name, passDate.Name };
        }


		public List<string> ProperitesNamesWithOutDescription() {
			return new List<string>() { id.Name, title.Name, createDate.Name, passDate.Name, employee.Name };
		}
	}
}