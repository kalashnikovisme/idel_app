using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using idel_app.Middle;

namespace idel_app.BisnessLogic {
  public class Provider {
    private int id = Const.THERE_IS_NOT;
    public int Id {
      get {
        return id;
      }
      set {
        id = value;
      }
    }

    private string title = "";
    public string Title {
      get {
        return title;
      }
      set {
        title = value;
      }
    }

    private string phone = "";
    public string Phone {
      get {
        return phone;
      }
      set {
        phone = value;
      }
    }

    private string email = "";
    public string Email {
      get {
        return email;
      }
      set {
        email = value;
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
  }
}