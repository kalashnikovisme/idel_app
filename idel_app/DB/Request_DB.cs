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
        static private List<List<string>> GetAllRequestToListList() {
            /*
              // создание COM объекта для соединения с 1С
              //COMConnectorClass connector = new COMConnectorClass();//мб лучше сделать полем класса
              // объект подключения к базе
              object v82Base = null;//мб лучше сделать полем класса

              bool connect = Connect1C(GetConnectionString(), ref v82Base, ref connector);

              List<List<string>> requests = FillListList(v82Base, connector);
            */
            //return requests;
            return new List<List<string>>();
        }

        // Метод для подключения к 1С
        //static private bool Connect1C(string connectionString, ref object v82Base, ref COMConnectorClass connector)
        //{
        //    try
        //    {
        //        v82Base = connector.Connect(connectionString);
        //        return true;
        //    }
        //    catch (Exception ex)
        //    {
        //        string error = ("Ошибка подключения!\n" + ex.Message);
        //        return false;
        //    }
        //}

        // Фомирует строку подключения
        static private string GetConnectionString() {
            StringBuilder ConnectionString = new StringBuilder(100);
            ConnectionString.Append(@"File=""" + @"d:\programming\1c\идель\"/*здесь путь к файлу*/ + @""";");
            ConnectionString.Append(@"Usr=" + (""/*здесь имя пользователя*/ == null ? @";" : @"""" + ""/*здесь имя пользователя*/ + @""";"));
            ConnectionString.Append(@"Pwd=" + (""/*здесь пароль*/ == null ? @";" : @"""" + ""/*здесь пароль*/ + @""";"));
            return ConnectionString.ToString();
        }

        //получает из базы значения и формирует лист листов
        //static private List<List<string>> FillListList(object v82Base, COMConnectorClass connector)
        //{
        //    List<List<string>> list = new List<List<string>>(); 
        //     List<string> id = new List<string>();
        //     List<string> title = new List<string>();
        //     List<string> createDate = new List<string>();
        //     List<string> passDate = new List<string>();
        //     List<string> employee = new List<string>();
        //     List<string> product = new List<string>();
        //     List<string> provider = new List<string>();
        //     List<string> count = new List<string>();
        //     List<string> wareHouseStatus = new List<string>(); 
        //     List<string> requestStatus = new List<string>();
        //     List<string> comment = new List<string>();
        //    object storage = CommandTo1C.ExecuteCreateObject(v82Base, "NewObject", new object[] { "Запрос" });
        //    CommandTo1C.SetProperty(storage, "Текст", new object[] { CommandTo1C.RequestIdel });
        //    object result = CommandTo1C.ExecuteFunction(storage, "Выполнить", new object[]{});
        //    object selection = CommandTo1C.ExecuteFunction(result, "Выбрать", null);
        //    while ((bool)CommandTo1C.ExecuteFunction(selection, "Следующий", null))
        //    {
        //        id.Add((string)CommandTo1C.GetProperty(selection, "Артикул"));
        //        title.Add("Заголовок заявки");
        //        createDate.Add("12.12.2012 7:23:00");
        //        passDate.Add("12.12.2012 7:23:00");//собственный парсер даты скорее всего нужен будет
        //        employee.Add("сотрудник");
        //        product.Add((string)CommandTo1C.GetProperty(selection, "НаименованиеТовара"));
        //        provider.Add((string)CommandTo1C.GetProperty(selection, "ОсновнойПоставщик"));
        //        count.Add("3");//(string)CommandTo1C.GetProperty(selection, "Количество"));
        //        wareHouseStatus.Add("true");//будет по русски
        //        requestStatus.Add("false");//будеть по руски Ложь
        //        comment.Add((string)CommandTo1C.GetProperty(selection, "Комментарий"));
        //    }

        //    try
        //    {
        //        for (int i = 0; i < id.Count; i++)
        //        {
        //            list.Add(new List<string> {id[i], title[i], createDate[i], passDate[i], employee[i], product[i], provider[i], count[i], wareHouseStatus[i],
        //                        requestStatus[i], comment[i]});
        //        }
        //    }
        //    catch(Exception ex)
        //    {
        //        string error = ex.Message;
        //    }

        //    return list;
        //}

        static private int PInt(string s) {
            return Int32.Parse(s);
        }

        static private DateTime PDT(string s) {
            return DateTime.Parse(s);
        }

        static private bool Parse1CString(string s) {
            if (s == "Истина") {
                return true;
            }
            if (s == "Ложь") {
                return false;
            }
            return false;
        }

        static public List<Request> GetAllRequestFromDB() {
            List<List<string>> workList = GetAllRequestToListList();
            List<Request> list = new List<Request>();
            foreach (List<string> l in workList) {
                list.Add(new Request(PInt(l[0]), l[1], PDT(l[2]), PDT(l[3]), l[4], l[5], l[6], PInt(l[7]), Parse1CString(l[8]), Parse1CString(l[9]), l[10]));
            }
            return list;
        }
    }
}