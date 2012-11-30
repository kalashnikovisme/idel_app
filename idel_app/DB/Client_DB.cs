using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using idel_app.BisnessLogic;

namespace idel_app.DB
{
    static public class Client_DB
    {
        static private string intToFormatString(int index)
        {
            string res = index.ToString();
            if (res.Length < 9)
            {
                int count0 = (9 - res.Length);
                for (int i = 0; i < count0; i++)
                {
                    res = "0" + res;
                }
            }
            return res;
        }

        static public List<Client> GetAllClientsFromDB()
        {
            List<List<string>> l = GetAllClientsNames();
            List<Client> list = new List<Client>();
            foreach (List<string> s in l)
            {
                list.Add(new Client(s[0], Int32.Parse(s[1])));
            }
            return list;
        }

        static public void AddNewClient(string name)
        {
            //пример списка:
            //List<string>{"Проект1", DateTime.Today.ToString(), DateTime.Today.ToString(), "пользователь1", "это некий проект 1", "Ложь", "Клиент1"}
            Dictionary<string, object> requisites = new Dictionary<string, object> { 
                { "Наименование", name }, 
                { "Покупатель", "Истина" }
            };
            string index = CommandTo1C.addToThesaurus(Program.v82Base, Program.connector, "Контрагенты", requisites);
        }

        static public List<List<string>> GetAllClientsNames()
        {
            List<string> fields = new List<string>() { "Наименование", "Код" };
            List<List<string>> requests = CommandTo1C.requestToListLists(Program.v82Base, Program.connector, RequestTo1C.RequestGetAllClientsNames, fields);
            return requests;
        }

        static public void DeleteClientById(int index)
        {
            bool good = CommandTo1C.deleteFromThesaurus(Program.v82Base, Program.connector, "Контрагенты", index.ToString());
        }

        static public List<List<string>> GetAllProjectByClient(string code)
        {
            List<string> fields = new List<string>() { "Код", "Наименование", "ДатаНачала", "ДатаОкончания", "Ответственный", "Описание", "СтатусСдачи", "Клиент" };
            Dictionary<string, object> parametrs = new Dictionary<string, object> { 
                { "Клиент", code }
            };
            List<List<string>> requests = CommandTo1C.requestToListLists(Program.v82Base, Program.connector, RequestTo1C.RequestGetAllProjectOfClient, fields, parametrs);
            return requests;
        }

    }
}