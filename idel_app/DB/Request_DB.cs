using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using idel_app.BisnessLogic;
//using V82;

namespace idel_app.DB {
    static public class Request_DB {
        /// <summary>
        /// Если в БД будет сущность Request, у которой будут все те же поля, что у меня в классе и они будут расположены в том же порядке, что у 
        /// у меня в классе, тогда твоя задача просто сделать функцию (она ниже), которая будет переделывать из БД в список списков стрингов, дальше всё должно само по себе произойти.
        /// Если нет, буди звонком.
        /// </summary>ок
        /// <returns></returns>
        static private List<List<string>> GetAllRequestToListList(string clientName) {
            return Client_DB.GetAllProjectByClient(clientName);
        }

        static private int PInt(string s) {
            return Int32.Parse(s);
        }

        static private DateTime PDT(string s) {
            return DateTime.Parse(s);
        }

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

        static private bool Parse1CStringToBoolean(string s) {
            if (s == "Истина") {
                return true;
            }
            if (s == "Ложь") {
                return false;
            }
            return false;
        }

        static private string Parse1C(string s)
        {
            if (s == "True")
            {
                return "Истина";
            }
            if (s == "False")
            {
                return "Ложь";
            }
            return "Ложь";
        }

        static public List<Request> GetAllRequestFromDBByClient(string clientName) {
            List<List<string>> workList = GetAllRequestToListList(clientName);
            List<Request> list = new List<Request>();
            foreach (List<string> l in workList) {
                list.Add(new Request(PInt(l[0]), l[1], PDT(l[2]), PDT(l[3]), l[4], Parse1CStringToBoolean(l[5]), l[6]));
            }
            return list;
        }

        static public void AddNewProject(List<string> add)
        {
            //пример списка:
            //List<string>{"Проект1", DateTime.Today.ToString(), DateTime.Today.ToString(), "пользователь1", "это некий проект 1", "Ложь", "Клиент1"}
            Dictionary<string, object> requisites = new Dictionary<string, object> { 
                { "Наименование", add[0] }, 
                { "ДатаНачала", DateTime.Parse(add[1]) }, 
                { "ДатаОкончания", DateTime.Parse(add[2]) },
                { "Ответственный", CommandTo1C.getObjectFromThesaurusByName(Program.v82Base, Program.connector, "Пользователи", add[3]) },
                { "Описание", add[4] },
                { "СтатусСдачи", Parse1C(add[5]) },
                { "Клиент", add[6] }
            };
            string index = CommandTo1C.addToThesaurus(Program.v82Base, Program.connector, "Проекты", requisites);
        }

        static public void updateProjectById(List<string> up, int index)
        {
            //пример списка:
            //List<string>{"Проект1", DateTime.Today.ToString(), DateTime.Today.ToString(), "пользователь1", "это некий проект 1", "Ложь", "Клиент1"}
            Dictionary<string, object> updateValues = new Dictionary<string, object> { 
                { "Наименование", up[0] }, 
                { "ДатаНачала", DateTime.Parse(up[1]) }, 
                { "ДатаОкончания", DateTime.Parse(up[2]) },
                { "Ответственный", CommandTo1C.getObjectFromThesaurusByName(Program.v82Base, Program.connector, "Пользователи", up[3]) },
                { "Описание", up[4] },
                { "СтатусСдачи", Parse1C(up[5]) },
                { "Клиент", up[6] }
            };
            string indexStr = intToFormatString(index);
            bool good = CommandTo1C.updatingRecordFromThesaurus(Program.v82Base, Program.connector, "Проекты", indexStr, updateValues);
        }

        static public string GetDescriptionProjectById(int index)
        {
            List<string> fields = new List<string>() { "Описание" };
            Dictionary<string, object> parametrs = new Dictionary<string, object> { 
                { "Код", intToFormatString(index) }
            };
            List<List<string>> requests = CommandTo1C.requestToListLists(Program.v82Base, Program.connector, RequestTo1C.RequestGetDescriptionProjectById, fields, parametrs);
            return requests[0][0];
        }

        static public List<string> GetAllUsers()
        {
            List<string> fields = new List<string>() { "Наименование" };
            List<List<string>> requests = CommandTo1C.requestToListLists(Program.v82Base, Program.connector, RequestTo1C.RequestGetAllUsers, fields);
            List<string> result = new List<string>();
            for (int i = 0; i < requests.Count; i++)
            {
                result.Add(requests[i][0]);
            }
            return result;
        }
    }
}