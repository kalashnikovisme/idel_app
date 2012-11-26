using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using idel_app.Middle;

namespace idel_app.BisnessLogic {
    public class Provider {
        private BusinessProperity id = new BusinessProperity(Const.THERE_IS_NOT, "Id");
        public int Id {
            get {
                return Int32.Parse(id.Properity.ToString());
            }
            set {
                id.Properity = value;
            }
        }

        private BusinessProperity title = new BusinessProperity("", "Наименование");
        public string Title {
            get {
                return title.Properity.ToString();
            }
            set {
                title.Properity = value;
            }
        }

        private BusinessProperity phone = new BusinessProperity("", "Телефон");
        public string Phone {
            get {
                return phone.Properity.ToString();
            }
            set {
                phone.Properity = value;
            }
        }

        private BusinessProperity email = new BusinessProperity("", "Email");
        public string Email {
            get {
                return email.Properity.ToString();
            }
            set {
                email.Properity = value;
            }
        }

        public Provider(int _Id, string _Title, string _phone, string _email) {
            Id = _Id;
            Title = _Title;
            Phone = _phone;
            Email = _email;
        }

        public Provider() {
            Id = 0;
            Title = "Darth Vader";
            Phone = "+9637201212";
            Email = "DarthVader@LordSith.com";
        }

        public object[] Properites() {
            return new object[] { Id, Title, Phone, Email };
        }

        public List<string> ProperitesNames() {
            return new List<string>() { id.Name, title.Name, phone.Name, email.Name };
        }
    }
}