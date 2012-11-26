using System;
using System.Text;
using System.Reflection;

namespace idel_app.DB
{
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
                     ТоварыНаСкладах.Количество КАК Количество,
	               	 ТоварыНаСкладах.Номенклатура.Комментарий КАК Комментарий,
	                 ТоварыНаСкладах.Номенклатура.ОсновнойПоставщик.Наименование КАК ОсновнойПоставщик
	               ИЗ
	                 РегистрНакопления.ТоварыНаСкладах КАК ТоварыНаСкладах";
    }
}
