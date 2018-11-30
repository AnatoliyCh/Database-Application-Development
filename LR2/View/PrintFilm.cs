using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LR2.Model;
using LR2.Model.Table;

namespace LR2.View
{
    class PrintFilm
    {
        private const int _spaceCell = 5;
        private const int _spaceCellTableWide = 20;
        private const int _spaceCellTableNarrow = 12;
        public void PrintTable()
        {
            Console.Clear();
            PrintCMD.ThisMenu(3);
            string lineWide = PrintCMD.GetMultipliedToken('-', _spaceCellTableWide);
            string lineNarrow = PrintCMD.GetMultipliedToken('-', _spaceCellTableNarrow);
            Console.WriteLine("|{0, " + _spaceCellTableNarrow + "}|{1, " + _spaceCellTableWide + "}|{2, " + _spaceCellTableNarrow + "}|{3, " + _spaceCellTableNarrow + "}|", "Id", "Название", "Рейтинг", "Просмотрено");
            Console.WriteLine("|{0, " + _spaceCellTableNarrow + "}|{1, " + _spaceCellTableWide + "}|{2, " + _spaceCellTableNarrow + "}|{3, " + _spaceCellTableNarrow + "}|", lineNarrow, lineWide, lineNarrow, lineNarrow);
            string tmpViewed;//для отображения просмотренного
            foreach (var item in Singleton.Instance.Film.Read())
            {
                if (item.Viewed) tmpViewed = "+";
                else tmpViewed = "-";
                Console.WriteLine("|{0, " + _spaceCellTableNarrow + "}|{1, " + _spaceCellTableWide + "}|{2, " + _spaceCellTableNarrow + "}|{3, " + _spaceCellTableNarrow + "}|", item.Id, item.Title, item.Rating, tmpViewed);             
            }                
            Console.Write("\n");
        }
        public void ReadMenu()
        {
            Console.WriteLine(" {0, " + PrintCMD.SpaceCellId + "} {1, " + _spaceCell + "}", "[?]", "Обновить");
            Console.WriteLine(" {0, " + PrintCMD.SpaceCellId + "} {1, " + _spaceCell + "}", "[" + 0 + "]", "Назад");
        }
        public void CreateMenu()
        {
            Console.WriteLine(" {0, " + PrintCMD.SpaceCellId + "} {1, " + _spaceCell + "}", "[1]", "Новый фильм");
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
        //дополнительное отображение
        public void CreateFilm(Film film)//показываем фильм, который создаем
        {
            Console.Clear();
            Console.Write("\n");
            Console.WriteLine(" {0, " + _spaceCellTableNarrow + "} {1, " + _spaceCellTableWide + "}", "Название:", film.Title);
            Console.WriteLine(" {0, " + _spaceCellTableNarrow + "} {1, " + _spaceCellTableWide + "}", "Рейтинг:", film.Rating);
            Console.WriteLine(" {0, " + _spaceCellTableNarrow + "} {1, " + _spaceCellTableWide + "}", "Просмотрено:", film.Viewed);
            Console.WriteLine(" {0, " + _spaceCellTableNarrow + "}", "Жанры:");
            for (int i = 0; i < film.GenresList.Count; i++)
                Console.WriteLine(" {0, " + _spaceCellTableNarrow + "} {1, " + _spaceCellTableWide + "}", i, (film.GenresList as List<Genre>)[i].Title);
            Console.WriteLine(" {0, " + _spaceCellTableNarrow + "}", "Актеры:");
            for (int i = 0; i < film.GenresList.Count; i++)
                Console.WriteLine(" {0, " + _spaceCellTableNarrow + "} {1, " + _spaceCellTableWide + "}", i, (film.ActorsList as List<Actor>)[i].Name);
            Console.Write("\n");

        }
        public void SubCreateMenu()//меню для создания фильма
        {       
            Console.WriteLine(" {0, " + PrintCMD.SpaceCellId + "} {1, " + _spaceCell + "}", "[1]", "Название");
            Console.WriteLine(" {0, " + PrintCMD.SpaceCellId + "} {1, " + _spaceCell + "}", "[2]", "Новый жанр");
            Console.WriteLine(" {0, " + PrintCMD.SpaceCellId + "} {1, " + _spaceCell + "}", "[3]", "Новый актёр");
            Console.WriteLine(" {0, " + PrintCMD.SpaceCellId + "} {1, " + _spaceCell + "}", "[4]", "Поставить рейтинг");
            Console.WriteLine(" {0, " + PrintCMD.SpaceCellId + "} {1, " + _spaceCell + "}", "[5]", "Отметить просмотренно");
            Console.WriteLine(" {0, " + PrintCMD.SpaceCellId + "} {1, " + _spaceCell + "}", "[6]", "Готово");
            Console.WriteLine(" {0, " + PrintCMD.SpaceCellId + "} {1, " + _spaceCell + "}", "[" + 0 + "]", "Отмена");
        }
    }
}
