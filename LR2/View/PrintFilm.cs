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
        public void PrintTable(IList<Film> films)
        {
            Console.Write("\n");
            string lineWide = PrintCMD.GetMultipliedToken('-', _spaceCellTableWide);
            string lineNarrow = PrintCMD.GetMultipliedToken('-', _spaceCellTableNarrow);
            Console.WriteLine("|{0, " + _spaceCellTableNarrow + "}|{1, " + _spaceCellTableWide + "}|{2, " + _spaceCellTableNarrow + "}|{3, " + _spaceCellTableNarrow + "}|", "Id", "Название", "Рейтинг", "Просмотрено");
            Console.WriteLine("|{0, " + _spaceCellTableNarrow + "}|{1, " + _spaceCellTableWide + "}|{2, " + _spaceCellTableNarrow + "}|{3, " + _spaceCellTableNarrow + "}|", lineNarrow, lineWide, lineNarrow, lineNarrow);
            string tmpViewed;//для отображения просмотренного
            foreach (var item in films)
            {
                if (item.Viewed) tmpViewed = "+";
                else tmpViewed = "-";
                Console.WriteLine("|{0, " + _spaceCellTableNarrow + "}|{1, " + _spaceCellTableWide + "}|{2, " + _spaceCellTableNarrow + "}|{3, " + _spaceCellTableNarrow + "}|", item.Id, item.Title, item.Rating, tmpViewed);
            }
            Console.Write("\n");
        }
        public void ReadMenu()
        {
            if (Singleton.Instance.Film.GetAmountElements() > 0)
                Console.WriteLine(" {0, " + PrintCMD.SpaceCellId + "} {1, " + _spaceCell + "}", "[1]", "Посмотреть конкретный");
            Console.WriteLine(" {0, " + PrintCMD.SpaceCellId + "} {1, " + _spaceCell + "}", "[2]", "Обновить");
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
        //all
        public void SearchMenu()
        {
            Console.WriteLine(" {0, " + PrintCMD.SpaceCellId + "}", "Поиск по:");
            Console.WriteLine(" {0, " + PrintCMD.SpaceCellId + "} {1, " + _spaceCell + "}", "[1]", "Название");
            Console.WriteLine(" {0, " + PrintCMD.SpaceCellId + "} {1, " + _spaceCell + "}", "[2]", "Жанр");
            Console.WriteLine(" {0, " + PrintCMD.SpaceCellId + "} {1, " + _spaceCell + "}", "[3]", "Актёр");
            Console.WriteLine(" {0, " + PrintCMD.SpaceCellId + "} {1, " + _spaceCell + "}", "[4]", "Рейтинг");
            Console.WriteLine(" {0, " + PrintCMD.SpaceCellId + "} {1, " + _spaceCell + "}", "[" + 0 + "]", "Отмена");
        }
        //дополнительное отображение
        public void CurrentFilm(Film film)//показываем фильм, который создаем
        {
            Console.Clear();
            Console.Write("\n");
            Console.WriteLine(" {0, " + _spaceCellTableNarrow + "} {1, " + _spaceCellTableWide + "}", "Название:", film.Title);
            Console.WriteLine(" {0, " + _spaceCellTableNarrow + "} {1, " + _spaceCellTableWide + "}", "Рейтинг:", film.Rating);
            Console.WriteLine(" {0, " + _spaceCellTableNarrow + "} {1, " + _spaceCellTableWide + "}", "Просмотрено:", film.Viewed);            
            Console.WriteLine(" {0, " + _spaceCellTableNarrow + "}", "Жанры:");
            int i = 0;
            foreach (var item in film.GenresList)
            {
                Console.WriteLine(" {0, " + _spaceCellTableNarrow + "} {1, " + _spaceCellTableWide + "}", (i + 1) + ":", item.Title);
                i++;
            }
            i = 0;
            Console.WriteLine(" {0, " + _spaceCellTableNarrow + "}", "Актеры:");
            foreach (var item in film.ActorsList)
            {
                Console.WriteLine(" {0, " + _spaceCellTableNarrow + "} {1, " + _spaceCellTableWide + "}", (i + 1) + ":", item.Name);
                i++;
            }
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
