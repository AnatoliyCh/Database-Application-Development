using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LR2.Controller
{
    interface IControll
    {
        /// <summary>
        /// Чтение
        /// </summary>
        void Read();
        /// <summary>
        /// Создание
        /// </summary>
        void Create();
        /// <summary>
        /// Обновление
        /// </summary>
        void Update();
        /// <summary>
        /// Удаление
        /// </summary>
        void Delete();
        /// <summary>
        /// Поиск
        /// </summary>
        void Search();
    }
}
