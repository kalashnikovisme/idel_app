using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using idel_app.BisnessLogic;

namespace idel_app.DB
{
    public class Product_DB
    {
        static public List<Product> GetAllProductFromDB()
        {
            List<Product> list = new List<Product>();
            for (int i = 0; i < 30; i++)
            {
                list.Add(new Product());
            }
            return list;
        }

        static public string GetNameProviderById(int id)
        {
            if (id == 1)
            {
                return "Dark emperror";
            }
            return "NoBody";
        }


        public string GetProductNameByArticle(string art)
        {
            //SELECT name FROM (все товары) WHERE артикль = art
            List<string> fields = new List<string>() { "Наименование" };
            Dictionary<string, object> parametrs = new Dictionary<string, object> { 
                { "Артикул", art }
            };
            List<List<string>> requests = CommandTo1C.requestToListLists(Program.v82Base, Program.connector, RequestTo1C.RequestGetProductNameByArticle, fields, parametrs);
            return requests[0][0];
        }

        #region getProducts
        /// <summary>
        /// Получает товары из регистра ТоварыВНТТ
        /// </summary>
        /// <returns></returns>
        public List<List<string>> GetProductsFromRegistrVNTT()
        {
            //SELECT names FROM (все товары)
            List<string> fields = new List<string>() { "Наименование", "Артикул" };
            List<List<string>> requests = CommandTo1C.requestToListLists(Program.v82Base, Program.connector, RequestTo1C.RequestGetProductsFromRegistr("ТоварыВНТТ"), fields);
            List<string> result = new List<string>();
            for (int i = 0; i < requests.Count; i++)
                result.Add(requests[i][0]);
            return requests;
        }

        /// <summary>
        /// Получает товары из регистра ТоварыВРезервеНаСкладах
        /// </summary>
        /// <returns></returns>
        public List<List<string>> GetProductsFromRegistrReserveOfStorages()
        {
            //SELECT names FROM (все товары)
            List<string> fields = new List<string>() { "Наименование", "Артикул" };
            List<List<string>> requests = CommandTo1C.requestToListLists(Program.v82Base, Program.connector, RequestTo1C.RequestGetProductsFromRegistr("ТоварыВРезервеНаСкладах"), fields);
            List<string> result = new List<string>();
            for (int i = 0; i < requests.Count; i++)
                result.Add(requests[i][0]);
            return requests;
        }

        /// <summary>
        /// Получает товары из регистра ТоварыВРознице
        /// </summary>
        /// <returns></returns>
        public List<List<string>> GetProductsFromRegistrRoznica()
        {
            //SELECT names FROM (все товары)
            List<string> fields = new List<string>() { "Наименование", "Артикул" };
            List<List<string>> requests = CommandTo1C.requestToListLists(Program.v82Base, Program.connector, RequestTo1C.RequestGetProductsFromRegistr("ТоварыВРознице"), fields);
            List<string> result = new List<string>();
            for (int i = 0; i < requests.Count; i++)
                result.Add(requests[i][0]);
            return requests;
        }

        /// <summary>
        /// Получает товары из регистра ТоварыКПередачеОрганизаций
        /// </summary>
        /// <returns></returns>
        public List<List<string>> GetProductsFromRegistrPeredaCorporation()
        {
            //SELECT names FROM (все товары)
            List<string> fields = new List<string>() { "Наименование", "Артикул" };
            List<List<string>> requests = CommandTo1C.requestToListLists(Program.v82Base, Program.connector, RequestTo1C.RequestGetProductsFromRegistr("ТоварыКПередачеОрганизаций"), fields);
            List<string> result = new List<string>();
            for (int i = 0; i < requests.Count; i++)
                result.Add(requests[i][0]);
            return requests;
        }

        /// <summary>
        /// Получает товары из регистра ТоварыКПередачеСоСкладов
        /// </summary>
        /// <returns></returns>
        public List<List<string>> GetProductsFromRegistrPeredaStorages()
        {
            //SELECT names FROM (все товары)
            List<string> fields = new List<string>() { "Наименование", "Артикул" };
            List<List<string>> requests = CommandTo1C.requestToListLists(Program.v82Base, Program.connector, RequestTo1C.RequestGetProductsFromRegistr("ТоварыКПередачеСоСкладов"), fields);
            List<string> result = new List<string>();
            for (int i = 0; i < requests.Count; i++)
                result.Add(requests[i][0]);
            return requests;
        }

        /// <summary>
        /// Получает товары из регистра ТоварыКПеремещениюВНТТ
        /// </summary>
        /// <returns></returns>
        public List<List<string>> GetProductsFromRegistrPeremeVNTT()
        {
            //SELECT names FROM (все товары)
            List<string> fields = new List<string>() { "Наименование", "Артикул" };
            List<List<string>> requests = CommandTo1C.requestToListLists(Program.v82Base, Program.connector, RequestTo1C.RequestGetProductsFromRegistr("ТоварыКПеремещениюВНТТ"), fields);
            List<string> result = new List<string>();
            for (int i = 0; i < requests.Count; i++)
                result.Add(requests[i][0]);
            return requests;
        }

        /// <summary>
        /// Получает товары из регистра ТоварыКПолучениюНаСклады
        /// </summary>
        /// <returns></returns>
        public List<List<string>> GetProductsFromRegistrGetStorages()
        {
            //SELECT names FROM (все товары)
            List<string> fields = new List<string>() { "Наименование", "Артикул" };
            List<List<string>> requests = CommandTo1C.requestToListLists(Program.v82Base, Program.connector, RequestTo1C.RequestGetProductsFromRegistr("ТоварыКПолучениюНаСклады"), fields);
            List<string> result = new List<string>();
            for (int i = 0; i < requests.Count; i++)
                result.Add(requests[i][0]);
            return requests;
        }

        /// <summary>
        /// Получает товары из регистра ТоварыНаСкладах
        /// </summary>
        /// <returns></returns>
        public List<List<string>> GetProductsFromRegistrStorages()
        {
            //SELECT names FROM (все товары)
            List<string> fields = new List<string>() { "Наименование", "Артикул" };
            List<List<string>> requests = CommandTo1C.requestToListLists(Program.v82Base, Program.connector, RequestTo1C.RequestGetProductsFromRegistr("ТоварыНаСкладах"), fields);
            List<string> result = new List<string>();
            for (int i = 0; i < requests.Count; i++)
                result.Add(requests[i][0]);
            return requests;
        }

        /// <summary>
        /// Получает товары из регистра ТоварыОрганизаций
        /// </summary>
        /// <returns></returns>
        public List<List<string>> GetProductsFromRegistrCorporation()
        {
            //SELECT names FROM (все товары)
            List<string> fields = new List<string>() { "Наименование", "Артикул" };
            List<List<string>> requests = CommandTo1C.requestToListLists(Program.v82Base, Program.connector, RequestTo1C.RequestGetProductsFromRegistr("ТоварыОрганизаций"), fields);
            List<string> result = new List<string>();
            for (int i = 0; i < requests.Count; i++)
                result.Add(requests[i][0]);
            return requests;
        }

        /// <summary>
        /// Получает товары из регистра ТоварыПереданные
        /// </summary>
        /// <returns></returns>
        public List<List<string>> GetProductsFromRegistrPeredan()
        {
            //SELECT names FROM (все товары)
            List<string> fields = new List<string>() { "Наименование", "Артикул" };
            List<List<string>> requests = CommandTo1C.requestToListLists(Program.v82Base, Program.connector, RequestTo1C.RequestGetProductsFromRegistr("ТоварыПереданные"), fields);
            List<string> result = new List<string>();
            for (int i = 0; i < requests.Count; i++)
                result.Add(requests[i][0]);
            return requests;
        }

        /// <summary>
        /// Получает товары из регистра ТоварыПолученные
        /// </summary>
        /// <returns></returns>
        public List<List<string>> GetProductsFromRegistrGets()
        {
            //SELECT names FROM (все товары)
            List<string> fields = new List<string>() { "Наименование", "Артикул" };
            List<List<string>> requests = CommandTo1C.requestToListLists(Program.v82Base, Program.connector, RequestTo1C.RequestGetProductsFromRegistr("ТоварыПолученные"), fields);
            List<string> result = new List<string>();
            for (int i = 0; i < requests.Count; i++)
                result.Add(requests[i][0]);
            return requests;
        }

        #endregion

    }
}