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

        private BusinessProperity id = new BusinessProperity(-1, "Код");
        public int Id
        {
            get
            {
                return Int32.Parse(id.Properity.ToString());
            }
            set
            {
                id.Properity = value;
            }
        }

		public object[] ProperitesWithoutDescription() {
			return new object[] { Name, Id };
		}

		private void construct(string _name, int _id) {
			Name = _name;
            Id = _id;
		}

		public Client(string _name, int _id) {
			construct(_name, _id);
		}
		
		public Client() {
			construct("Microsoft", 0);
		}
	}
}