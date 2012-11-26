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
        #region RequestMethods
        /// <summary>
        /// Получает все запросы
        /// </summary>
        /// <returns></returns>
        public List<List<string>> AllRequests() {
            List<Request> requests = Request_DB.GetAllRequestFromDB();
            List<List<string>> list = new List<List<string>>();
            foreach (Request r in requests) {
                List<string> l = new List<string>();
                object[] obj = r.Properites();
                for (int i = 0; i < obj.Length; i++) {
                    l.Add(obj[i].ToString());
                }
                list.Add(l);
            }
            return list;
        }

        /// <summary>
        /// Запрашивает все поля заявки. Рефлексивно.
        /// </summary>
        public List<string> RequestFields() {
            Request r = new Request();
            return r.ProperitesNames();
        }

        public void AddNewRequest(List<string> newAdd) {
            List<List<string>> list = AllRequests();
            list.Add(newAdd);
        }

        public void DeleteRequestByIndex(int index) {

        }

        public void DeleteAll() {

        }

        public void DeletePassedRequests() {

        }

        public void SaveChanges(List<List<string>> changes) {

        }

        public void MarkRequestPassed(int index) {

        }

        public void MarkRequestUnPassed(int index) {

        }

        #endregion

        #region Provider Methods

        public List<string> ProviderFields() {
            Provider p = new Provider();
            return p.ProperitesNames();
        }

        public List<List<string>> AllProviders() {
            List<Provider> providers = Provider_DB.GetAllProviderFromDB();
            List<List<string>> list = new List<List<string>>();
            foreach (Provider p in providers) {
                List<string> l = new List<string>();
                object[] obj = p.Properites();
                for (int i = 0; i < obj.Length; i++) {
                    l.Add(obj[i].ToString());
                }
                list.Add(l);
            }
            return list;
        }

        public void AddNewProvider(List<string> newAdd) { }


        #endregion

        #region Product Methods

        public List<string> ProductFields() {
            Product p = new Product();
            return p.ProperitesNames();
        }

        public List<List<string>> AllProducts() {
            List<Product> providers = Product_DB.GetAllProductFromDB();
            List<List<string>> list = new List<List<string>>();
            foreach (Product p in providers) {
                List<string> l = new List<string>();
                object[] obj = p.Properites();
                for (int i = 0; i < obj.Length; i++) {
                    l.Add(obj[i].ToString());
                }
                list.Add(l);
            }
            return list;
        }

        #endregion

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