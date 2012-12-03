using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using idel_app.BisnessLogic;
using idel_app.DB;
using System.Reflection;

namespace idel_app.Middle {
	/// <summary>
	/// Этот класс будет организовывать передачу данных между интерфейсом и "внутренностями"
	/// </summary>
	public class MiddleClass {

		private Client curClient = new Client();
		public Client CurrentClient {
			get {
				return curClient;
			}
			set {
				curClient = value;
			}
		}

		private Request curRequest = new Request();

		#region RequestMethods
		/// <summary>
		/// Получает все запросы
		/// </summary>
		/// <returns></returns>
		public List<List<string>> AllRequestsOfClient(string clientName) {
			List<Request> requests = Request_DB.GetAllRequestFromDBByClient(clientName);
			List<List<string>> list = new List<List<string>>();
			foreach (Request r in requests) {
				List<string> l = new List<string>();
				object[] obj = r.ProperitesWithOutDescription();
				for (int i = 0; i < obj.Length; i++) {
					l.Add(obj[i].ToString());
				}
				list.Add(l);
			}
			return list;
		}

		public List<List<string>> GetDescriptionByIndexOfRequest(int index) {
			return Request_DB.GetAllRequestFromDBByClient(Program.mainMiddleClass.CurrentClient.Name)[index].Description.Data;
		}

		/// <summary>
		/// Запрашивает все поля заявки. Рефлексивно.
		/// </summary>
		public List<string> RequestFields() {
			Request r = new Request();
			return r.ProperitesNamesWithOutDescription();
		}

        public List<string> RequestAddingFields()
        {
            Request r = new Request();
            return r.ProperitesForAdding();
        }

		public void AddNewRequest(List<string> newAdd, string clientName) {
            List<string> add = newAdd;
            add.Add("");
            add.Add("Ложь");
            add.Add(clientName);
            Request_DB.AddNewProject(add);
		}

		public void DeleteRequestByIndex(int index) {

		}

		public void DeleteAll() {

		}

		public void DeletePassedRequests() {

		}

		public void SaveChanges(List<List<string>> changes) {
            foreach (List<string> l in changes)
            {
                int id = Int32.Parse(l[0]);
                l.RemoveAt(0);
                l.Add(Request_DB.GetDescriptionProjectById(id));
                l.Add(CurrentClient.Id.ToString());
                Request_DB.UpdateProjectById(l, id);
            }
		}

		public void MarkRequestPassed(int index) {

		}

		public void MarkRequestUnPassed(int index) {

		}

		public void SaveDescriptionCurrentRequest(List<List<string>> list) {
			
		}

		#endregion

		#region ClientsFunctions

		public List<List<string>> AllClients() {
			List<Client> clients = Client_DB.GetAllClientsFromDB();
			List<List<string>> list = new List<List<string>>();
			foreach (Client c in clients) {
				List<string> l = new List<string>();
				object[] obj = c.ProperitesWithoutDescription();
				for (int i = 0; i < obj.Length; i++) {
					l.Add(obj[i].ToString());
				}
				list.Add(l);
			}
			return list;
		}

        public void DeleteClientByIndex(int index) {
            Client_DB.DeleteClientById(index);   
        }


		public List<string> ClientFields() {
			return TypeFields(typeof(Client));
		}

		public void AddNewClient(List<string> newAdd) {
            Client_DB.AddNewClient(newAdd[0]);
		}

		#endregion

        public List<string> AllUsers()
        {
            return Request_DB.GetAllUsers();
        }
        
        private List<string> TypeFields(Type type) {
			var foo = Activator.CreateInstance(type);
			List<string> list = new List<string>();
			foreach (System.Reflection.PropertyInfo p in foo.GetType().GetProperties()) {
				list.Add(p.Name);
			}
			return list;
		}
	}
}