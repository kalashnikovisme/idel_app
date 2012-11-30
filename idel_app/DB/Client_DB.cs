using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using idel_app.BisnessLogic;

namespace idel_app.DB {
	static public class Client_DB {
		static public List<Client> GetAllClientsFromDB() {
			List<Client> list = new List<Client>();
			list.Add(new Client());
			return list; 
		}
	}
}