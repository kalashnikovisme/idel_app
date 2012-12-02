using System;
using System.Text;
using System.Reflection;
using System.Windows.Forms;
using System.Collections.Generic;
using V82;

namespace idel_app.DB
{
    class CommandTo1C
    {
        /// <summary>
        /// Метод для подключения к 1С.
        /// </summary>
        /// <param name="filename">путь к базе</param>
        /// <param name="user">логин</param>
        /// <param name="password">пароль</param>
        /// <param name="v82Base">объект подключения к базе</param>
        /// <param name="connector">COM объект для соединения с 1С</param>
        /// <returns></returns>
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

        // Фомирует строку подключения
        static private string GetConnectionString(string filename, string user, string password)
        {
            StringBuilder ConnectionString = new StringBuilder(100);
            ConnectionString.Append(@"File=""" + filename + @""";");
            ConnectionString.Append(@"Usr=" + (user == null ? @";" : @"""" + user + @""";"));
            ConnectionString.Append(@"Pwd=" + (password == null ? @";" : @"""" + password + @""";"));
            return ConnectionString.ToString();
        }

        /// <summary>
        /// Получает из базы 1С список записей по запросу.
        /// </summary>
        /// <param name="v82Base">Объект подключения к базе 1С</param>
        /// <param name="connector">COM объект для соединения с 1С</param>
        /// <param name="requestText">Текст запроса</param>
        /// <param name="fieldsSelection">Названия полей в полученной выборке (в запросе после слова КАК идут)</param>
        /// <param name="parametrs">Ассоциативный массив Название параметра - Значение</param>
        /// <returns>Возвращает список списков строк, в котором каждый внутренний список - запись</returns>
        static public List<List<string>> requestToListLists(object v82Base, COMConnectorClass connector, string requestText, List<string> fieldsSelection,
                                                            Dictionary<string, object> parametrs = null)
        {
            object storage = RequestTo1C.ExecuteCreateObject(v82Base, "NewObject", new object[] { "Запрос" });
            RequestTo1C.SetProperty(storage, "Текст", new object[] { requestText });
            if (parametrs != null)
            {
                foreach (KeyValuePair<string, object> kvp in parametrs)
                    RequestTo1C.ExecuteFunction(storage, "УстановитьПараметр", new object[] { kvp.Key, kvp.Value });
            }
            object result = RequestTo1C.ExecuteFunction(storage, "Выполнить", null);
            object selection = RequestTo1C.ExecuteFunction(result, "Выбрать", null);
            int count;
            int.TryParse(RequestTo1C.ExecuteFunction(selection, "Количество", null).ToString(), out count);
            List<List<string>> list = new List<List<string>>();
            //string [][] list = new string[count][];
            for (int i = 0; i < count; i++)
            {
                //list[i] = new string[fieldsSelection.Count];
                list.Add(new List<string>());
            }
            for (int k = 0; (bool)RequestTo1C.ExecuteFunction(selection, "Следующий", null); k++)
            {
                for (int i = 0; i < fieldsSelection.Count; i++)
                {
                    object record = RequestTo1C.GetProperty(selection, fieldsSelection[i]);
                    //list[k][i] = (((DBNull.Value.Equals(record) || record == null) ? "NULL" : record).ToString());
                    list[k].Add(((DBNull.Value.Equals(record) || record == null) ? "NULL" : record).ToString());//проверка, что в ячейке значение не DBNull или null
                }
            }
            return list;
        }

        /// <summary>
        /// Функция добавления записи в регистр базы 1С.
        /// </summary>
        /// <param name="v82Base">объект подключения к базе</param>
        /// <param name="connector">COM объект для соединения с 1С</param>
        /// <param name="registrType">Тип регистра (РегистрНакопления, РегистрСведений и т.п.)</param>
        /// <param name="registrName">Название регистра</param>
        /// <param name="registratorName">Название регистратора</param>
        /// <param name="dimension">Ассоциативный массив Название измерения - Значение</param>
        /// <param name="resource">Ассоциативный массив Название ресурса - Значение</param>
        static public void addToRegistr(object v82Base, COMConnectorClass connector, string registrType, string registrName, string registratorName,
                        Dictionary<string, object> dimension, Dictionary<string, object> resource)
        {
            object registrs = RequestTo1C.GetProperty(v82Base, registrType);
            object registr = RequestTo1C.GetProperty(registrs, registrName);
            object storage = RequestTo1C.ExecuteFunction(registr, "СоздатьНаборЗаписей", null);
            object registrator = RequestTo1C.GetProperty(RequestTo1C.GetProperty(storage, "Отбор"), "Регистратор");
            object document = RequestTo1C.GetProperty(RequestTo1C.GetProperty(v82Base, "Документы"), registratorName);
            object linkDocument = RequestTo1C.ExecuteFunction(document, "ПолучитьСсылку", null);
            RequestTo1C.ExecuteFunction(registrator, "Установить", new object[] { linkDocument });

            object str = RequestTo1C.ExecuteFunction(storage, "Добавить", null);
            DateTime d = DateTime.Today;
            RequestTo1C.SetProperty(str, "Период", new object[] { d });

            foreach (KeyValuePair<string, object> kvp in dimension)
                RequestTo1C.SetProperty(str, kvp.Key, new object[] { kvp.Value });

            foreach (KeyValuePair<string, object> kvp in resource)
                RequestTo1C.SetProperty(str, kvp.Key, new object[] { kvp.Value });

            RequestTo1C.ExecuteFunction(storage, "Записать", null);
        }

        /// <summary>
        /// Функция добавления записи в справочник базы 1С.
        /// </summary>
        /// <param name="v82Base">объект подключения к базе</param>
        /// <param name="connector">COM объект для соединения с 1С</param>
        /// <param name="thesaurusName">название справочника</param>
        /// <param name="requisites">ассоциативный массив Название реквизита - Значение</param>
        /// <returns>Возвращает код добавленной записи в формате ХХХХХХХХХ(если этот код храниться в базе как строка)</returns>
        static public string addToThesaurus(object v82Base, COMConnectorClass connector, string thesaurusName, Dictionary<string, object> requisites)
        {
            object thesauruses = RequestTo1C.GetProperty(v82Base, "Справочники");
            object thesaurus = RequestTo1C.GetProperty(thesauruses, thesaurusName);
            object record = RequestTo1C.ExecuteFunction(thesaurus, "СоздатьЭлемент", null);

            foreach (KeyValuePair<string, object> kvp in requisites)
                RequestTo1C.SetProperty(record, kvp.Key, new object[] { kvp.Value });

            RequestTo1C.ExecuteFunction(record, "Записать", null);
            return RequestTo1C.GetProperty(record, "Код").ToString();
        }

        /// <summary>
        /// Функция удаления записи из регистра базы 1С.(функция не доделана!)
        /// </summary>
        /// <param name="v82Base">объект подключения к базе</param>
        /// <param name="connector">COM объект для соединения с 1С</param>
        /// <param name="registrType">Тип регистра (РегистрНакопления, РегистрСведений и т.п.)</param>
        /// <param name="registrName">Название регистра</param>
        /// <param name="registratorName">Название регистратора</param>
        /// <param name="index">Индекс удаляемой строки</param>
        /// <returns>Возвращает, прошло ли удаление или нет</returns>
        static public bool deleteFromRegistr(object v82Base, COMConnectorClass connector, string registrType, string registrName, string registratorName, int index)
        {
            try
            {
                object registrs = RequestTo1C.GetProperty(v82Base, registrType);
                object registr = RequestTo1C.GetProperty(registrs, registrName);
                object storage = RequestTo1C.ExecuteFunction(registr, "СоздатьНаборЗаписей", null);
                object registrator = RequestTo1C.GetProperty(RequestTo1C.GetProperty(storage, "Отбор"), "Регистратор");
                object document = RequestTo1C.GetProperty(RequestTo1C.GetProperty(v82Base, "Документы"), registratorName);
                object linkDocument = RequestTo1C.ExecuteFunction(document, "ПолучитьСсылку", null);
                RequestTo1C.ExecuteFunction(registrator, "Установить", new object[] { linkDocument });

                object str = RequestTo1C.ExecuteFunction(storage, "Удалить", new object[] { index });

                RequestTo1C.ExecuteFunction(storage, "Записать", null);
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        /// <summary>
        /// Функция удаления записи из справочника базы 1С.
        /// </summary>
        /// <param name="v82Base">объект подключения к базе</param>
        /// <param name="connector">COM объект для соединения с 1С</param>
        /// <param name="thesaurusName">Название справочника</param>
        /// <param name="index">Индекс удаляемой строки( в формате ХХХХХХХХХ если код хранится как строка)</param>
        /// <returns>Возвращает, прошло ли удаление или нет</returns>
        static public bool deleteFromThesaurus(object v82Base, COMConnectorClass connector, string thesaurusName, string index)
        {
            try
            {
                object thesauruses = RequestTo1C.GetProperty(v82Base, "Справочники");
                object thesaurus = RequestTo1C.GetProperty(thesauruses, thesaurusName);
                object deletingRecord = RequestTo1C.ExecuteFunction(thesaurus, "НайтиПоКоду", new object[] { index });
                object deletingObject = RequestTo1C.ExecuteFunction(deletingRecord, "ПолучитьОбъект", null);
                RequestTo1C.ExecuteFunction(deletingObject, "Удалить", null);
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        /// <summary>
        /// Функция удаления всех записей из справочника базы 1С.
        /// </summary>
        /// <param name="v82Base">объект подключения к базе</param>
        /// <param name="connector">COM объект для соединения с 1С</param>
        /// <param name="thesaurusName">Название справочника</param>
        /// <returns>Возвращает, прошло ли удаление или нет</returns>
        static public bool deleteAllFromThesaurus(object v82Base, COMConnectorClass connector, string thesaurusName)
        {
            try
            {
                object thesauruses = RequestTo1C.GetProperty(v82Base, "Справочники");
                object thesaurus = RequestTo1C.GetProperty(thesauruses, thesaurusName);
                object selection = RequestTo1C.ExecuteFunction(thesaurus, "Выбрать", null);
                while ((bool)RequestTo1C.ExecuteFunction(selection, "Следующий", null))
                {
                    object deletingObject = RequestTo1C.ExecuteFunction(selection, "ПолучитьОбъект", null);
                    RequestTo1C.ExecuteFunction(deletingObject, "Удалить", null);
                }
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        /// <summary>
        /// Функция изменения записи в базе 1С.
        /// </summary>
        /// <param name="v82Base">объект подключения к базе</param>
        /// <param name="connector">COM объект для соединения с 1С</param>
        /// <param name="thesaurusName">Название справочника</param>
        /// <param name="index">Индекс изменяемой записи в формате ХХХХХХХХХ</param>
        /// <param name="updateValues">ассоциативный массив Название реквизита - Значение</param>
        /// <returns>Возвращает удачно завершилось обновление или нет</returns>
        static public bool updatingRecordFromThesaurus(object v82Base, COMConnectorClass connector, string thesaurusName, string index, Dictionary<string, object> updateValues)
        {
            try
            {
                object thesauruses = RequestTo1C.GetProperty(v82Base, "Справочники");
                object thesaurus = RequestTo1C.GetProperty(thesauruses, thesaurusName);
                object record = RequestTo1C.ExecuteFunction(thesaurus, "НайтиПоКоду", new object[] { index });
                object updatingObject = RequestTo1C.ExecuteFunction(record, "ПолучитьОбъект", null);

                foreach (KeyValuePair<string, object> kvp in updateValues)
                    RequestTo1C.SetProperty(updatingObject, kvp.Key, new object[] { kvp.Value });

                RequestTo1C.ExecuteFunction(updatingObject, "Записать", null);
                return true;
            }
            catch (Exception ex)
            {
                string str = ex.Message;
                return false;
            }
        }

        static public object getObjectFromThesaurusByName(object v82Base, COMConnectorClass connector, string thesaurusName, string name)
        {
            object thesauruses = RequestTo1C.GetProperty(v82Base, "Справочники");
            object thesaurus = RequestTo1C.GetProperty(thesauruses, thesaurusName);
            object record = RequestTo1C.ExecuteFunction(thesaurus, "НайтиПоНаименованию", new object[] { name });
            //return RequestTo1C.ExecuteFunction(record, "ПолучитьОбъект", null);
            return record;
        }
    }

    class RequestTo1C
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

        public static string RequestIdel = @"ВЫБРАТЬ 
                     ТоварыНаСкладах.Номенклатура.Артикул КАК Артикул,
                     ТоварыНаСкладах.Номенклатура.НаименованиеПолное КАК НаименованиеТовара,
	               	 ТоварыНаСкладах.Номенклатура.Комментарий КАК Комментарий,
	                 ТоварыНаСкладах.Номенклатура.ОсновнойПоставщик.Наименование КАК ОсновнойПоставщик
	               ИЗ
	                 РегистрНакопления.ТоварыНаСкладах КАК ТоварыНаСкладах";
        public static string RequestTest = @"ВЫБРАТЬ 
                     ТестРегистр.Измерение1 КАК Артикул,
                     ТестРегистр.Измерение2 КАК НаименованиеТовара,
	               	 ТестРегистр.Измерение3 КАК Комментарий,
	                 ТестРегистр.Измерение4 КАК ОсновнойПоставщик
	               ИЗ
	                 РегистрНакопления.ТестРегистр КАК ТестРегистр";
        public static string RequestProvider = @"ВЫБРАТЬ 
                     Контрагенты.НаименованиеПолное КАК Наименование
	               ИЗ
	                 Справочник.Контрагенты КАК Поставщики";
        public static string RequestTestAdd = @"ВЫБРАТЬ
               	ТестСправочник.Реквизит1,
               	ТестСправочник.Реквизит2,
               	ТестСправочник.Реквизит3,
                ТестСправочник.Код
               ИЗ
               	Справочник.ТестСправочник КАК ТестСправочник";
        public static string RequestGetAllProjects = @"ВЫБРАТЬ
                                 	Проекты.Код,
                                 	Проекты.Наименование,
                                 	Проекты.ДатаНачала,
                                 	Проекты.ДатаОкончания,
                                 	Проекты.Ответственный.Наименование КАК Ответственный,
                                 	Проекты.Описание,
                                 	Проекты.СтатусСдачи, 
                                 	Проекты.Клиент
                                  ИЗ
                                 	Справочник.Проекты КАК Проекты";
        public static string RequestGetDescriptionProjectById = @"ВЫБРАТЬ
                                 	Проекты.Описание
                                 ИЗ
                                 	Справочник.Проекты КАК Проекты
                                 ГДЕ
                                 	Проекты.Код = &Код";
        public static string RequestGetAllProvidersNames = @"ВЫБРАТЬ
                                 	Контрагенты.Наименование КАК Наименование,
                                    Контрагенты.Код КАК Код
                                 ИЗ
                                 	Справочник.Контрагенты КАК Контрагенты
                                 ГДЕ
                                 	Контрагенты.Поставщик = Истина";
        public static string RequestGetAllClientsNames = @"ВЫБРАТЬ
                                 	Контрагенты.Наименование КАК Наименование,
                                    Контрагенты.Код КАК Код
                                 ИЗ
                                 	Справочник.Контрагенты КАК Контрагенты
                                 ГДЕ
                                 	Контрагенты.Покупатель = Истина";
        public static string RequestGetAllProjectOfClient = @"ВЫБРАТЬ
	                             	Проекты.Код,
                                 	Проекты.Наименование,
                                 	Проекты.ДатаНачала,
                                 	Проекты.ДатаОкончания,
                                 	Проекты.Ответственный.Наименование КАК Ответственный,
                                 	Проекты.Описание,
                                 	Проекты.СтатусСдачи, 
                                 	Проекты.Клиент
                                 ИЗ
                                 	Справочник.Проекты КАК Проекты
                                 ГДЕ
                                 	Проекты.Клиент ПОДОБНО &Клиент";
        public static string RequestGetProductNameByArticle = @"ВЫБРАТЬ
			                	Номенклатура.Наименование
			                  ИЗ
			                	Справочник.Номенклатура КАК Номенклатура
			                  ГДЕ
			                	Номенклатура.Артикул = &Артикул";

        public static string RequestGetAllUsers = @"ВЫБРАТЬ
				                      	Пользователи.Наименование
				                      ИЗ
				                      	Справочник.Пользователи КАК Пользователи";
        public static string RequestGetProductsFromRegistr(string nameRegistr)
        {
            return @"ВЫБРАТЬ
            " + nameRegistr + @".Номенклатура.Наименование КАК Наименование,
            " + nameRegistr + @".Номенклатура.Артикул КАК Артикул                    
            ИЗ
            РегистрНакопления." + nameRegistr + " КАК " + nameRegistr;
        }
    }
}
