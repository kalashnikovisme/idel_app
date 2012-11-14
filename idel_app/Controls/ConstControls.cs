using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SpecialControls {
  static public class ConstControls {
    /// <summary>
    /// Языки
    /// </summary>
    public const int ENGLISH_LANGUAGE = 0;

    /// <summary>
    /// Длины вводимых данных
    /// </summary>
    public const int LOGIN_LENGHT = 3;
    public const int PASSWORD_LENGHT = 3;

    /// <summary>
    /// Типы сообщений интерфейса
    /// </summary>
    public const int BAD_MESSAGE = 0;
    public const int GOOD_MESSAGE = 1;

    /// <summary>
    /// Параметры пользователя
    /// </summary>
    public const int LOGIN = 0;
    public const int PASSWORD = 1;
    public const int EMAIL = 2;
    public const int NAME = 3;
    public const int POST = 4;
    public const int FULL_NAME = 5;

    public const string adressRememberUserFile = "userRemember.txt";

    /// <summary>
    /// Рабочая переменная.
    /// </summary>
    public const int THEREISNOT = -1;

    /// <summary>
    /// Функции.
    /// </summary>
    public const int VIEW_USER_DATA = 0;
    public const int CREATE_NEW_DELIVERY = 1;
    public const int ONLINE_CONFERENCE = 2;

    /// <summary>
    /// Количество фукнций в приложении
    /// </summary>
    public const int FUNCTIONS_COUNT = 3;
    
    /// <summary>
    /// Функция: VIEW_USER_DATA
    /// </summary>
    public const string BOTTOM_BUTTON_VIEW = "Сменить пользователя";
    public const int FULL_NAME_VIEW = 0;
    public const int POST_VIEW = 1;
    public const int TITLE_LISTBOX_LOCALS = 2;
    public const int TITLE_LISTBOX_MEMBERS = 3;
    public const int TITLE_MEMBERCARD = 4;

    /// <summary>
    /// Функция: CREATE_NEW_DELIVERY
    /// </summary>
    public const string BOTTOM_BUTTON_CREATE_NEW_DELIVERY = "Сделать рассылку";

    public const string BOTTOM_BUTTON_ADD_TASK = "Добавить задачу";
    /// <summary>
    /// Названия МИЦ в разных падежах
    /// </summary>
    public const string FULL_MIC_GENITIVE = "УОМОО \"МИЦ\"";

    /// <summary>
    /// Имеет или не имеет пользователь функцию
    /// </summary>
    public const bool GOT = true;
    public const bool NOT_GOT = false;

    public const bool YES = true;
    public const bool NO = false;

    public const string NULL = "0";

    public const int IDEA = 0;
    public const int TASK = 1;

    public const int LOCALS = 0;
    public const int MEMBERS = 1;

    public const int LISTS = 0;
    public const int MEMBERS_LIST = 1;

    public const float WIDTH_LEFT_FUNCTION_PANEL = 230F;

    public const int FUNCTION_PANEL_INDENT = 20;
    public const int CONTROL_INDENT_SMALL = 5;
    public const int CONTROL_INDENT_MIDDLE = 10;
    public const int CONTROL_INDENT_BIG = 15;
    public const int CONTROL_INDENT_VERY_BIG = 30;

    public const int MEMBERCARD_SIZE = 500;

    public const int TITLE_LISTBOX_SENDLISTS = 0;
    public const int TITLE_LISTBOX_MEMBERS_SENDLIST = 1;

    public const int LIST_DELIVERY = 0;
    public const int ONCE_MAIL = 1;
  }
}