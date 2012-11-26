using System;
using System.Text;
using System.Reflection;
using System.Collections.Generic;
using V82;

namespace idel_app.DB
{
    class RequestTo1C
    {
        // Метод для подключения к 1С
        static public bool Connect1C(string filename, string user, string password, ref object v82Base, ref COMConnectorClass connector)
        {
            try
            {
                v82Base = connector.Connect(GetConnectionString(filename, user, password));
                return true;
            }
            catch (Exception ex)
            {
                string error = ("Ошибка подключения!\n" + ex.Message);
                return false;
            }
        }

        // Метод для подключения к 1С
        static public bool Connect1C(ref object v82Base, ref COMConnectorClass connector)
        {
            try
            {
                v82Base = connector.Connect(GetConnectionString(@"d:\programming\1c\идель\", "", ""));
                return true;
            }
            catch (Exception ex)
            {
                string error = ("Ошибка подключения!\n" + ex.Message);
                return false;
            }
        }

        // Фомирует строку подключения
        static private string GetConnectionString(string filename, string user, string password)
        {
            StringBuilder ConnectionString = new StringBuilder(100);
            ConnectionString.Append(@"File=""" + filename + @""";");
            ConnectionString.Append(@"Usr=" + (user == null ? @";" : @"""" + user + @""";"));
            ConnectionString.Append(@"Pwd=" + (password == null ? @";" : @"""" + password + @""";"));
            return ConnectionString.ToString();
        }

        //получает из базы значения и формирует лист листов заявок
        static public List<List<string>> FillListListRequests(object v82Base, COMConnectorClass connector)
        {
            List<List<string>> list = new List<List<string>>();
            List<string> id = new List<string>();
            List<string> title = new List<string>();
            List<string> createDate = new List<string>();
            List<string> passDate = new List<string>();
            List<string> employee = new List<string>();
            List<string> product = new List<string>();
            List<string> provider = new List<string>();
            List<string> count = new List<string>();
            List<string> wareHouseStatus = new List<string>();
            List<string> requestStatus = new List<string>();
            List<string> comment = new List<string>();
            object storage = CommandTo1C.ExecuteCreateObject(v82Base, "NewObject", new object[] { "Запрос" });
            CommandTo1C.SetProperty(storage, "Текст", new object[] { CommandTo1C.RequestIdel });
            object result = CommandTo1C.ExecuteFunction(storage, "Выполнить", new object[] { });
            object selection = CommandTo1C.ExecuteFunction(result, "Выбрать", null);
            while ((bool)CommandTo1C.ExecuteFunction(selection, "Следующий", null))
            {
                id.Add((string)CommandTo1C.GetProperty(selection, "Артикул"));
                title.Add("Заголовок заявки");
                createDate.Add("12.12.2012 7:23:00");
                passDate.Add("12.12.2012 7:23:00");//собственный парсер даты скорее всего нужен будет
                employee.Add("сотрудник");
                product.Add((string)CommandTo1C.GetProperty(selection, "НаименованиеТовара"));
                provider.Add((string)CommandTo1C.GetProperty(selection, "ОсновнойПоставщик"));
                count.Add("3");//(string)CommandTo1C.GetProperty(selection, "Количество"));
                wareHouseStatus.Add("Истина");//будет по русски
                requestStatus.Add("Ложь");//будеть по руски Ложь
                comment.Add((string)CommandTo1C.GetProperty(selection, "Комментарий"));
            }

            try
            {
                for (int i = 0; i < id.Count; i++)
                {
                    list.Add(new List<string> {id[i], title[i], createDate[i], passDate[i], employee[i], product[i], provider[i], count[i], wareHouseStatus[i],
                            requestStatus[i], comment[i]});
                }
            }
            catch (Exception ex)
            {
                string error = ex.Message;
            }
            return list;
        }

        //получает из базы значения и формирует лист листов поставщиков
        static public List<List<string>> FillListListProvider(object v82Base, COMConnectorClass connector)
        {
            List<List<string>> list = new List<List<string>>();
            List<string> id = new List<string>();
            List<string> title = new List<string>();
            List<string> phone = new List<string>();
            List<string> email = new List<string>();
            object storage = CommandTo1C.ExecuteCreateObject(v82Base, "NewObject", new object[] { "Запрос" });
            CommandTo1C.SetProperty(storage, "Текст", new object[] { CommandTo1C.RequestProvider });
            object result = CommandTo1C.ExecuteFunction(storage, "Выполнить", new object[] { });
            object selection = CommandTo1C.ExecuteFunction(result, "Выбрать", null);
            while ((bool)CommandTo1C.ExecuteFunction(selection, "Следующий", null))
            {
                id.Add((string)CommandTo1C.GetProperty(selection, "Артикул"));
                title.Add("Заголовок заявки");
                phone.Add("+9637201212");//будеть по руски Ложь
                email.Add("DarthVader@LordSith.com");
            }

            try
            {
                for (int i = 0; i < id.Count; i++)
                {
                    list.Add(new List<string> {id[i], title[i], phone[i], email[i]});
                }
            }
            catch (Exception ex)
            {
                string error = ex.Message;
            }

            return list;
        }

        //получает из базы значения и формирует лист листов товаров
        static public List<List<string>> FillListListProduct(object v82Base, COMConnectorClass connector)
        {
            List<List<string>> list = new List<List<string>>();
            List<string> id = new List<string>();
            List<string> title = new List<string>();
            List<string> description = new List<string>();

            object storage = CommandTo1C.ExecuteCreateObject(v82Base, "NewObject", new object[] { "Запрос" });
            CommandTo1C.SetProperty(storage, "Текст", new object[] { CommandTo1C.RequestProduct });
            object result = CommandTo1C.ExecuteFunction(storage, "Выполнить", new object[] { });
            object selection = CommandTo1C.ExecuteFunction(result, "Выбрать", null);
            while ((bool)CommandTo1C.ExecuteFunction(selection, "Следующий", null))
            {
                id.Add((string)CommandTo1C.GetProperty(selection, "Артикул"));
                title.Add("Заголовок заявки");
                description.Add("Это описание");
            }

            try
            {
                for (int i = 0; i < id.Count; i++)
                {
                    list.Add(new List<string> { id[i], title[i], description[i] });
                }
            }
            catch (Exception ex)
            {
                string error = ex.Message;
            }

            return list;
        }
    }

    class CommandTo1C
    {
        /// <summary>
        /// Перечисления. Определяют выбор для извлекаемых действий.
        /// </summary>
        private static BindingFlags CREATE_OBJECT = BindingFlags.Public | BindingFlags.InvokeMethod | BindingFlags.Static | BindingFlags.CreateInstance;
        private static BindingFlags INVOKE_METHOD = BindingFlags.Public | BindingFlags.InvokeMethod | BindingFlags.Static;
        private static BindingFlags GET_PROPERTY = BindingFlags.Public | BindingFlags.GetProperty | BindingFlags.Static;
        private static BindingFlags SET_PROPERTY = BindingFlags.Public | BindingFlags.SetProperty | BindingFlags.Static;

        /// <summary>
        /// Статический метод для создания объекта в 1С
        /// </summary>
        /// <param name="Object1C">Объект 1С, в котором создается новый объект</param>
        /// <param name="NameObject">Наименование нового объекта</param>
        /// <param name="Arguments">Аргументы, необходимые для создания нового объекта</param>
        /// <returns>возвращает созданный объект</returns>
        public static object ExecuteCreateObject(object Object1C, string NameObject, object[] Arguments)
        {
            return Object1C.GetType().InvokeMember(NameObject, CREATE_OBJECT, null, Object1C, Arguments);
        }

        /// <summary>
        /// Статический метод для выполннения процедуры или функции в 1С
        /// </summary>
        /// <param name="Object1C">Объект 1С, для которого необходимо выполнить процедуру или функцию</param>
        /// <param name="NameObject">Наименование процедуры или функции</param>
        /// <param name="Arguments">Аргументы, необходимые для выполнения функции</param>
        /// <returns>Возвращает объект. сформированный в результате выполнения функции</returns>
        public static object ExecuteFunction(object Object1C, string NameObject, object[] Arguments)
        {
            return Object1C.GetType().InvokeMember(NameObject, INVOKE_METHOD, null, Object1C, Arguments);
        }

        /// <summary>
        /// Статический метод для установки свойства объекта 1С
        /// </summary>
        /// <param name="Object1C">Объект 1С, для которого необходимо задать свойство</param>
        /// <param name="NameObject">Наименование свойства</param>
        /// <param name="Arguments">Аргументы, необходимые для установки свойства</param>
        public static void SetProperty(object Object1C, string NameObject, object[] Arguments)
        {
            Object1C.GetType().InvokeMember(NameObject, SET_PROPERTY, null, Object1C, Arguments);
        }

        /// <summary>
        /// Статический метод для получения какого-либо значения объекта 1С
        /// </summary>
        /// <param name="Object1C">Объект 1С, у которого необходимо получить свойство</param>
        /// <param name="NameObject">Наименование свойства</param>
        /// <returns>Возвращает полученное значение</returns>
        public static object GetProperty(object Object1C, string NameObject)
        {
            return Object1C.GetType().InvokeMember(NameObject, GET_PROPERTY, null, Object1C, null);
        }
        /// <summary>
        /// Статический метод. Формирует текст запроса для получения прайса
        /// </summary>
        /// <param name="only_availability">True - получить прайс только по товарам, которые
        /// имеются на складе, False - получение прайса по всей номенклатуре</param>
        /// <returns></returns>
        public static string CreateRequest(bool only_availability) 
        {
            string keyword = "";
            if (only_availability)
                keyword = "ЛЕВОЕ";
            else
                keyword = "ПРАВОЕ";
            string date = "ДАТАВРЕМЯ(" + DateTime.Now.Year + "," + DateTime.Now.Month + "," + DateTime.Now.Day + ")";
            string request =
        @"ВЫБРАТЬ
			ЦеныНоменклатурыСрезПоследних.Номенклатура.Наименование КАК Номенклатура,
			ЦеныНоменклатурыСрезПоследних.Цена,
			ЦеныНоменклатурыСрезПоследних.Валюта.Наименование КАК Валюта,
			ЦеныНоменклатурыСрезПоследних.ЕдиницаИзмерения.Наименование КАК ЕдиницаИзмерения
		ИЗ
			РегистрНакопления.ТоварыВРознице.Остатки(" + date + @", Склад.Наименование = &Склад) КАК ТоварыВРозницеОстатки
				" + keyword + " СОЕДИНЕНИЕ РегистрСведений.ЦеныНоменклатуры.СрезПоследних(" + date + @", ТипЦен.Наименование = &ТипыЦен) КАК ЦеныНоменклатурыСрезПоследних
				ПО ТоварыВРозницеОстатки.Номенклатура = ЦеныНоменклатурыСрезПоследних.Номенклатура";
            return request;
        }

        /// <summary>
        /// Строка запроса для получения списка складов
        /// </summary>
        public static string RequestStorage = @"ВЫБРАТЬ
	 	                Склады.Наименование
	                 ИЗ
	 	                Справочник.Склады КАК Склады";
        /// <summary>
        /// Строка запроса для получения типов цен номенклатуры
        /// </summary>
        public static string RequestTypePrice = @"ВЫБРАТЬ
	                	ТипыЦенНоменклатуры.Ссылка,
	                	ТипыЦенНоменклатуры.Наименование,
	                	ТипыЦенНоменклатуры.ВалютаЦены.Наименование
	                 ИЗ
	                	Справочник.ТипыЦенНоменклатуры КАК ТипыЦенНоменклатуры";
        public static string RequestIdel = @"ВЫБРАТЬ 
                     ТоварыНаСкладах.Номенклатура.Артикул КАК Артикул,
                     ТоварыНаСкладах.Номенклатура.НаименованиеПолное КАК НаименованиеТовара,
	               	 ТоварыНаСкладах.Номенклатура.Комментарий КАК Комментарий,
	                 ТоварыНаСкладах.Номенклатура.ОсновнойПоставщик.Наименование КАК ОсновнойПоставщик
	               ИЗ
	                 РегистрНакопления.ТоварыНаСкладах КАК ТоварыНаСкладах";
        public static string RequestProvider = @"ВЫБРАТЬ 
                     Контрагенты.НаименованиеПолное КАК Наименование
	               ИЗ
	                 Справочник.Контрагенты КАК Поставщики";
        public static string RequestProduct = @"ВЫБРАТЬ 
                     ТоварыНаСкладах.Номенклатура.Артикул КАК Артикул,
                     ТоварыНаСкладах.Номенклатура.НаименованиеПолное КАК НаименованиеТовара,
	               	 ТоварыНаСкладах.Номенклатура.Комментарий КАК Комментарий,
	                 ТоварыНаСкладах.Номенклатура.ОсновнойПоставщик.Наименование КАК ОсновнойПоставщик
	               ИЗ
	                 РегистрНакопления.ТоварыНаСкладах КАК ТоварыНаСкладах";
    }
}
