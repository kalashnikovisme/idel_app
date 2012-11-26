using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using idel_app.BisnessLogic;
using V82;

namespace idel_app.DB {
  static public class Request_DB {
    /// <summary>
    /// Если в БД будет сущность Request, у которой будут все те же поля, что у меня в классе и они будут расположены в том же порядке, что у 
    /// у меня в классе, тогда твоя задача просто сделать функцию (она ниже), которая будет переделывать из БД в список списков стрингов, дальше всё должно само по себе произойти.
    /// Если нет, буди звонком.
    /// </summary>
    /// <returns></returns>
    static private List<List<string>> GetAllRequestToListList() {

        // создание COM объекта для соединения с 1С
        COMConnectorClass connector = new COMConnectorClass();//мб лучше сделать полем класса
        // объект подключения к базе
        object v82Base = null;//мб лучше сделать полем класса

        bool connect = Connect1C(GetConnectionString(), ref v82Base, ref connector);

        List<List<string>> requests = new List<List<string>>();

        List<string> oneRequest = new List<string>();
        string id = "0";
        string title = "Заголовок заявки";
        string createDate = "12.12.2012 7:23:00";
        string passDate = "12.12.2012 07:23:00";//собственный парсер даты скорее всего нужен будет
        string employee = FillcBoxStorage(v82Base, connector)[0];
        string product = "товар";
        string provider = "поставщик";
        string count = "1";
        string wareHouseStatus = "true";//будет по русски
        string requestStatus = "false";//будеть по руски Ложь
        string comment = "комментарий";
        oneRequest.AddRange(new List<string> {id, title, createDate, passDate, employee, product, provider, count, wareHouseStatus,
                            requestStatus, comment});
        requests.Add(oneRequest);

      return requests;
    }

    // Метод для подключения к 1С
    static private bool Connect1C(string connectionString, ref object v82Base, ref COMConnectorClass connector)
    {
        try
        {
            v82Base = connector.Connect(connectionString);
            return true;
        }
        catch (Exception ex)
        {
            string error = ("Ошибка подключения!\n" + ex.Message);
            return false;
        }
    }

    // Фомирует строку подключения
    static private string GetConnectionString()
    {
        StringBuilder ConnectionString = new StringBuilder(100);
        ConnectionString.Append(@"File=""" + @"d:\programming\1c\идель\"/*здесь путь к файлу*/ + @""";");
        ConnectionString.Append(@"Usr=" + (""/*здесь имя пользователя*/ == null ? @";" : @"""" + ""/*здесь имя пользователя*/ + @""";"));
        ConnectionString.Append(@"Pwd=" + (""/*здесь пароль*/ == null ? @";" : @"""" + ""/*здесь пароль*/ + @""";"));
        return ConnectionString.ToString();
    }

    static private List<string> FillcBoxStorage(object v82Base, COMConnectorClass connector)
    {
        List<string> list = new List<string>();
        object storage = CommandTo1C.ExecuteCreateObject(v82Base, "NewObject", new object[] { "Запрос" });
        CommandTo1C.SetProperty(storage, "Текст", new object[] { CommandTo1C.RequestStorage });
        object result = CommandTo1C.ExecuteFunction(storage, "Выполнить", null);
        object selection = CommandTo1C.ExecuteFunction(result, "Выбрать", null);
        while ((bool)CommandTo1C.ExecuteFunction(selection, "Следующий", null))
        {
            list.Add((string)CommandTo1C.GetProperty(selection, "Наименование"));
        }
        return list;
    }

    static private int PInt(string s) {
      return Int32.Parse(s);
    }

    static private DateTime PDT(string s) {
      return DateTime.Parse(s);
    }

    static private bool PB(string s) {
      return Boolean.Parse(s);//не будет работать, там же не true-false, а скорее всего чтото по русски(Истина-Ложь, например) 
    }

    static public List<Request> GetAllRequestFromDB() {
      List<List<string>> workList = GetAllRequestToListList();
      List<Request> list = new List<Request>();
      foreach (List<string> l in workList) {
        list.Add(new Request(PInt(l[0]), l[1], PDT(l[2]), PDT(l[3]), l[4], l[5], l[6], PInt(l[7]), PB(l[8]), PB(l[9]), l[10]));
      }
      return list;
    }
  }
}