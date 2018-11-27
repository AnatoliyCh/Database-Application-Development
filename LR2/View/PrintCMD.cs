using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;



namespace LR2.View
{
    class PrintCMD
    {
        public const int SpaceCellId = 2;
        private const int _spaceCell = 5;
        private const string _lineWrite = "==> ";
        public static PrintGenre Genre { get; } = new PrintGenre();
        public static PrintActor Actor { get; } = new PrintActor();
        //верхний UI
        private static string[] _menuUP =
        {
            "Жанры",
            "Актёры",
            "Фильмы"
        };
        //UI каждой ячейки
        private static string[] _menuItem =
        {
            "Просмотр",
            "Добавление",
            "Редактирование",
            "Удаление записи"
        };
        public static int ReadKey()
        {
            Console.Write("\n" + _lineWrite);
            return Convert.ToInt32(Console.ReadLine());
        }
        public static int ReadKey(string str, bool indent)
        {
            if (indent) Console.Write("\n" + str + " " + _lineWrite);
            else Console.Write("[" + str + "] " + _lineWrite);
            return Convert.ToInt32(Console.ReadLine());
        }
        public static string ReadLine()
        {
            Console.Write("\n" + _lineWrite);
            return Console.ReadLine();
        }
        public static string ReadLine(string str, bool indent)
        {
            if (indent) Console.Write("\n" + str + " " + _lineWrite);
            else Console.Write("[" + str + "] " + _lineWrite);
            return Console.ReadLine();
        }
        public static void MenuUP()
        {
            Console.Clear();
            for (int i = 0; i < _menuUP.Length; i++)
                Console.WriteLine(" {0, " + SpaceCellId + "} {1, " + _spaceCell + "}", "[" + (i + 1) + "]", _menuUP[i]);
            Console.WriteLine(" {0, " + SpaceCellId + "} {1, " + _spaceCell + "}", "[" + 0 + "]", "Выход");
        }
        public static void MenuItem(int id)
        {
            Console.Clear();
            ThisMenu(id);
            for (int i = 0; i < _menuItem.Length; i++)
                Console.WriteLine(" {0, " + SpaceCellId + "} {1, " + _spaceCell + "}", "[" + (i + 1) + "]", _menuItem[i]);
            if (id == 3)
                Console.WriteLine(" {0, " + SpaceCellId + "} {1, " + _spaceCell + "}", "[" + _menuItem.Length + "]", "Поиск");
            Console.WriteLine(" {0, " + SpaceCellId + "} {1, " + _spaceCell + "}", "[" + 0 + "]", "Назад");
        }
        public static void ThisMenu(int id)
        {
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("Таблица: " + _menuUP[id - 1]);
            Console.ResetColor();
        }
        /// <summary>
        /// Возвращает увеличенный в mult раз токен
        /// </summary>
        /// <param name="token">символ</param>
        /// <param name="mult">token * mult</param>
        /// <returns>строка</returns>
        public static string GetMultipliedToken(char token, int mult)
        {
            string str = "";
            for (int i = 0; i < mult; i++)
                str += token;
            return str;
        }
    }
}
