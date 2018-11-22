using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LR2.Model;
using LR2.Model.Table;

namespace LR2.View
{
    class PrintGenre
    {
        private const int _spaceCell = 5;
        private const int _spaceCellTable = 10;
        public void PrintTable()
        {
            Console.Clear();
            PrintCMD.ThisMenu(1);
            Console.WriteLine("|{0, " + _spaceCellTable + "} |{1, " + _spaceCellTable + "}|", "Id", "Название");
            Console.WriteLine("|{0, " + _spaceCellTable + "} |{1, " + _spaceCellTable + "}|", "----------", "----------");
            foreach (var item in Singleton.Instance.Genre.Read().List<Genre>())
                Console.WriteLine("|{0, " + _spaceCellTable + "} |{1, " + _spaceCellTable + "}|", item.Id, item.Title);
            Console.Write("\n");
        }
        public void ReadMenu()
        {
            Console.WriteLine(" {0, " + PrintCMD.SpaceCellId + "} {1, " + _spaceCell + "}", "[?]", "Обновить");
            Console.WriteLine(" {0, " + PrintCMD.SpaceCellId + "} {1, " + _spaceCell + "}", "[" + 0 + "]", "Назад");
        }
        public void CreateMenu()
        {
            Console.WriteLine(" {0, " + PrintCMD.SpaceCellId + "} {1, " + _spaceCell + "}", "[1]", "Новый жанр");
            Console.WriteLine(" {0, " + PrintCMD.SpaceCellId + "} {1, " + _spaceCell + "}", "[" + 0 + "]", "Назад");
        }
        public void UpdateMenu()
        {
            Console.WriteLine(" {0, " + PrintCMD.SpaceCellId + "} {1, " + _spaceCell + "}", "[1]", "Изменить по Id");
            Console.WriteLine(" {0, " + PrintCMD.SpaceCellId + "} {1, " + _spaceCell + "}", "[2]", "Изменить по Названию");
            Console.WriteLine(" {0, " + PrintCMD.SpaceCellId + "} {1, " + _spaceCell + "}", "[" + 0 + "]", "Назад");
        }
        public void DeleteMenu()
        {
            Console.WriteLine(" {0, " + PrintCMD.SpaceCellId + "} {1, " + _spaceCell + "}", "[1]", "Удалить по Id");
            Console.WriteLine(" {0, " + PrintCMD.SpaceCellId + "} {1, " + _spaceCell + "}", "[2]", "Удалить по Названию");
            Console.WriteLine(" {0, " + PrintCMD.SpaceCellId + "} {1, " + _spaceCell + "}", "[" + 0 + "]", "Назад");
        }
    }
}
