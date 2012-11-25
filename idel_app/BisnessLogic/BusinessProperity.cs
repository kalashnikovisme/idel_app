using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using idel_app.Middle;

namespace idel_app.BisnessLogic {
  public class BusinessProperity {
    public object Properity;
    public string Name;
    public BusinessProperity(object prop, string name) {
      Properity = prop;
      Name = name;
    }
  }
}