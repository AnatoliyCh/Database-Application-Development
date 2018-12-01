using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LR2.Model.Table;
using NHibernate;

namespace LR2.Model.Repository
{
    class RepositoryFilm : IRepository<Film>
    {
        public enum ViewedType
        {
            Full = 0,
            TRUE = 1,
            FALSE = 2
        };
        void IRepository<Film>.Create(Film obj)
        {
            using (ISession session = Singleton.Instance.OpenSession())
            {
                using (ITransaction transaction = session.BeginTransaction())
                {
                    session.Save(obj);
                    transaction.Commit();
                }
            }
            CreateFilms_Genres(obj);
        }
        IList<Film> IRepository<Film>.Read()
        {
            IQuery query = Singleton.Instance.OpenSession().CreateQuery("from Film");
            return query.List<Film>();
        }
        IList<Film> IRepository<Film>.Read(string type, string param)
        {
            IQuery query = Singleton.Instance.OpenSession().CreateQuery("SELECT f FROM Film f WHERE f." + type + " = :param");
            if (type == "Id") query.SetParameter("param", int.Parse(param));
            else query.SetParameter("param", param);
            foreach (var item in query.List<Film>())
                item.GenresList = ReadFilms_Genres(item);
            return query.List<Film>();
        }
        void IRepository<Film>.Update(Film obj)
        {
            using (ISession session = Singleton.Instance.OpenSession())
            {
                using (ITransaction transaction = session.BeginTransaction())
                {                    
                    session.Update(obj);
                    transaction.Commit();
                }
            }
            UpdateFilms_Genres(obj);
        }
        void IRepository<Film>.Delete(string type, string param)
        {
            using (ISession session = Singleton.Instance.OpenSession())
            {
                using (ITransaction transaction = session.BeginTransaction())
                {
                    IQuery query = Singleton.Instance.OpenSession().CreateQuery("DELETE FROM Film f WHERE f." + type + " = :param");
                    if (type == "Id")
                    {
                        DeleteFilms_Genres(type, int.Parse(param));//удаляем всё из Films_Genres
                        query.SetParameter("param", int.Parse(param)).ExecuteUpdate();
                    }
                    else
                    {
                        DeleteFilms_Genres(type, 0,param);//удаляем всё из Films_Genres
                        query.SetParameter("param", param).ExecuteUpdate();
                    }
                }
            }
        }
        void IRepository<Film>.Delete(Film obj)
        {
            using (ISession session = Singleton.Instance.OpenSession())
            {
                using (ITransaction transaction = session.BeginTransaction())
                {
                    DeleteFilms_Genres("Id", obj.Id);//удаляем всё из Films_Genres
                    session.Delete(obj);
                    transaction.Commit();
                }
            }
        }        
        long IRepository<Film>.GetAmountElements()
        {
            IQuery query = Singleton.Instance.OpenSession().CreateQuery("SELECT count(*) FROM Film");
            return (long)query.UniqueResult();
        }
        /*
         * RepositoryFilm
         */
        //возвращаем последний Id
        private int GetLastId()
        {
            IQuery query = Singleton.Instance.OpenSession().CreateQuery("SELECT max(Id) FROM Film");            
            return (int)query.UniqueResult();
        }

        //заполняем промежуточную таблицу жанров фильмов
        private void CreateFilms_Genres(Film obj)
        {
            using (ISession session = Singleton.Instance.OpenSession())
            {
                using (ITransaction transaction = session.BeginTransaction())
                {
                    List<Films_Genres> tmpList = GetFilmGenres(obj);
                    foreach (var item in tmpList)
                        session.Save(item);
                    transaction.Commit();
                }
            }
        }
        //возвращаем в фильм жанры
        private ICollection<Genre> ReadFilms_Genres(Film obj)
        {
            ICollection<Genre> tmpList = new List<Genre>();//список жанров
            IQuery query = Singleton.Instance.OpenSession().CreateQuery("SELECT fg FROM Films_Genres fg WHERE fg.Id_Film = :param");
            query.SetParameter("param", obj.Id);

            foreach (var item in query.List<Films_Genres>())
                tmpList.Add(Singleton.Instance.Genre.Read("Id", item.Id_Genres.ToString())[0]);
            tmpList.OrderBy(item => item.Id);//сортировка по id;
            tmpList = SortListGenre(tmpList, query.List<Films_Genres>());//сортировка по оригиналу
            return tmpList;
        }
        //обновление связанных строчек с фильмом в таблице Films_Genres
        private void UpdateFilms_Genres(Film obj)
        {
            DeleteFilms_Genres("Id", obj.Id);
            CreateFilms_Genres(obj);//опирации равные
        }
        //удаление связанных строчек с фильмом в таблице Films_Genres
        private void DeleteFilms_Genres(string type, int id_Film, string param = "")
        {            
            if (type == "Id")
            {
                IQuery query = Singleton.Instance.OpenSession().CreateQuery("DELETE FROM Films_Genres fg WHERE fg.Id_Film = :param");
                query.SetParameter("param", id_Film).ExecuteUpdate();
            }
            else
            {
                //достаем Id всех фильмов с этим параметром
                IList<int> tmpList = new List<int>();
                foreach (var item in Singleton.Instance.Film.Read(type, param))
                    tmpList.Add(item.Id);
                //удаляем
                IQuery query = Singleton.Instance.OpenSession().CreateQuery("DELETE FROM Films_Genres fg WHERE fg.Id_Film = :param");
                foreach (var item in tmpList)
                    query.SetParameter("param", item).ExecuteUpdate();
            }
        }
        //сортировка листа жанров по Index_List
        private ICollection<Genre> SortListGenre(ICollection<Genre> listGenre, ICollection<Films_Genres> films_Genres)
        {
            ICollection<Genre> tmpList = new List<Genre>();//список жанров отсортированный по Index_List
            int minId = 0;//минимальный id в Index_List
            for (int i = 0; i < films_Genres.Count; i++)
            {
                foreach (var item in films_Genres)
                {
                    if (item.Index_List == minId)
                        tmpList.Add((listGenre as List<Genre>)[(listGenre as List<Genre>).FindIndex(obj => Equals(obj.Id, item.Id_Genres))]);
                }
                minId++;
            }                
            return tmpList;
        }
        //возвращаем лист id фильмов с id жанров
        private List<Films_Genres> GetFilmGenres(Film obj)
        {
            List<Films_Genres> tmpList = new List<Films_Genres>();
            int lastId = GetLastId();
            foreach (var item in obj.GenresList)
            {
                Films_Genres tmpFG = new Films_Genres();
                tmpFG.Id_Film = lastId;
                tmpFG.Id_Genres = item.Id;
                tmpFG.Index_List = (obj.GenresList as List<Genre>).FindIndex(s => string.Equals(s.Title, item.Title, StringComparison.CurrentCultureIgnoreCase));
                tmpList.Add(tmpFG);
            }
            return tmpList;
        }


        public IList<Film> Search(string type, string param, ViewedType viewedType)
        {
            IQuery query;
            switch (viewedType)
            {
                case ViewedType.Full:
                    query = Singleton.Instance.OpenSession().CreateQuery("SELECT f FROM Film f WHERE f." + type + " = :param");
                    query.SetParameter("param", param);
                    return query.List<Film>();
                case ViewedType.TRUE:
                    query = Singleton.Instance.OpenSession().CreateQuery("SELECT f FROM Film f WHERE f." + type + " = :param and f.Viewed = true");
                    query.SetParameter("param", param);
                    return query.List<Film>();
                case ViewedType.FALSE:
                    query = Singleton.Instance.OpenSession().CreateQuery("SELECT f FROM Film f WHERE f." + type + " = :param and f.Viewed = false");
                    query.SetParameter("param", param);
                    return query.List<Film>();
            }
            return null;
        }            
        public void JoinSearch(Film obj)
        {
            //using (ISession session = Singleton.Instance.OpenSession())
            //{
            //    using (ITransaction transaction = session.BeginTransaction())
            //    {
            //        List<Films_Genres> tmpList = GetFilmGenres(obj);
            //        foreach (var item in tmpList)
            //            session.Save(item);
            //        transaction.Commit();
            //    }
            //}
            //IQuery query = Singleton.Instance.OpenSession().CreateQuery("SELECT f from Film INNER JOIN SubTuble_Genres_Film s WHERE SubTuble_Genres_Film.Id_Film = 8 AND SubTuble_Genres_Film.Id_Genres = Боевик");
            //IList<Film> list = query.List<Film>();
            //foreach (var arr in list)
            //{
            //    Console.WriteLine(arr.Title);
            //}
        }        
    }
}
