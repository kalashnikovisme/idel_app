using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace idel_app.BisnessLogic {
	public class Client {
		private BusinessProperity name = new BusinessProperity("", "Имя");
		public string Name {
			get {
				return name.Properity.ToString();
			}
			set {
				name.Properity = value;
			}
		}

		public object[] ProperitesWithoutDescription() {
			return new object[] { Name };
		}

		private void construct(string _name) {
			Name = _name;
		}

		public Client(string _name) {
			construct(_name);
		}
		
		public Client() {
			construct("Microsoft");
		}
	}
}