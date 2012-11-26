using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using idel_app.Middle;

namespace idel_app.BisnessLogic {
  public class Product {
    private BusinessProperity id = new BusinessProperity(Const.THERE_IS_NOT, "Id");
    public int Id {
      get {
        return Int32.Parse(id.Properity.ToString());
      }
      set {
        id.Properity = value;
      }
    }

    private BusinessProperity title = new BusinessProperity("", "Название");
    public string Title {
      get {
        return description.Properity.ToString();
      }
      set {
        description.Properity = value;
      }
    }

    private BusinessProperity description = new BusinessProperity("", "Описание");
    public string Description {
      get {
        return description.Properity.ToString();
      }
      set {
        description.Properity = value;
      }
    }

    public Product(int _id, string _title, string _description) {
      Id = _id;
      Title = _title;
      Description = _description;
    }

    public Product() {
      Id = 0;
      Title = "Death Star";
      Description = "tamtamtamtamtamtaaaaaaaaaaaaam";
    }

    public object[] Properites() {
      return new object[] { Id, Title, Description };
    }

    public List<string> ProperitesNames() {
      return new List<string>() { id.Name, title.Name, description.Name };
    }
  }
}