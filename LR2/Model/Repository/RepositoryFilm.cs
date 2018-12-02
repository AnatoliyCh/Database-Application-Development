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
        public enum Subtable
        {
            Ganres = 0,
            Actors = 1
 
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
            CreateFilms_Actors(obj);
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
            {
                item.GenresList = ReadFilms_Genres(item);
                item.ActorsList = ReadFilms_Actors(item);
            }                
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
            UpdateFilms_Actors(obj);
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
                        DeleteFilms_Actors(type, int.Parse(param));//удаляем всё из Films_Actors
                        query.SetParameter("param", int.Parse(param)).ExecuteUpdate();
                    }
                    else
                    {
                        DeleteFilms_Genres(type, 0,param);//удаляем всё из Films_Genres
                        DeleteFilms_Actors(type, 0, param);//удаляем всё из Films_Actors
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
                    DeleteFilms_Actors("Id", obj.Id);//удаляем всё из Films_Actors
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

        //жанры
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
            tmpList = SortListGenre(tmpList, query.List<Films_Genres>());//сортировка по Index_List
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
                IQuery query = Singleton.Instance.OpenSession().CreateQuery("DELETE FROM Films_Genres fg WHERE fg.Id_Film IN (:param)");
                query.SetParameterList("param", tmpList).ExecuteUpdate();
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
                    if (item.Index_List == minId)
                        tmpList.Add((listGenre as List<Genre>)[(listGenre as List<Genre>).FindIndex(obj => Equals(obj.Id, item.Id_Genres))]);
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

        //актёры
        //заполняем промежуточную таблицу актёров фильмов
        private void CreateFilms_Actors(Film obj)
        {
            using (ISession session = Singleton.Instance.OpenSession())
            {
                using (ITransaction transaction = session.BeginTransaction())
                {
                    List<Films_Actors> tmpList = GetFilmActors(obj);
                    foreach (var item in tmpList)
                        session.Save(item);
                    transaction.Commit();
                }
            }
        }
        //возвращаем в фильм актёров
        private ICollection<Actor> ReadFilms_Actors(Film obj)
        {
            ICollection<Actor> tmpList = new List<Actor>();//список жанров
            IQuery query = Singleton.Instance.OpenSession().CreateQuery("SELECT fa FROM Films_Actors fa WHERE fa.Id_Film = :param");
            query.SetParameter("param", obj.Id);
            foreach (var item in query.List<Films_Actors>())
                tmpList.Add(Singleton.Instance.Actor.Read("Id", item.Id_Actors.ToString())[0]);
            tmpList = SortListActors(tmpList, query.List<Films_Actors>());//сортировка по Index_List
            return tmpList;
        }
        //обновление связанных строчек с фильмом в таблице Films_Actors
        private void UpdateFilms_Actors(Film obj)
        {
            DeleteFilms_Actors("Id", obj.Id);
            CreateFilms_Actors(obj);//опирации равные
        }
        //удаление связанных строчек с фильмом в таблице Films_Actors
        private void DeleteFilms_Actors(string type, int id_Film, string param = "")
        {
            if (type == "Id")
            {
                IQuery query = Singleton.Instance.OpenSession().CreateQuery("DELETE FROM Films_Actors fa WHERE fa.Id_Film = :param");
                query.SetParameter("param", id_Film).ExecuteUpdate();
            }
            else
            {
                //достаем Id всех фильмов с этим параметром
                IList<int> tmpList = new List<int>();
                foreach (var item in Singleton.Instance.Film.Read(type, param))
                    tmpList.Add(item.Id);
                //удаляем
                IQuery query = Singleton.Instance.OpenSession().CreateQuery("DELETE FROM Films_Actors fa WHERE fa.Id_Film IN (:param)");
                query.SetParameterList("param", tmpList).ExecuteUpdate();
            }
        }
        //сортировка листа актёров по Index_List
        private ICollection<Actor> SortListActors(ICollection<Actor> listActors, ICollection<Films_Actors> films_Actors)
        {
            ICollection<Actor> tmpList = new List<Actor>();//список актёров отсортированный по Index_List
            int minId = 0;//минимальный id в Index_List
            for (int i = 0; i < films_Actors.Count; i++)
            {
                foreach (var item in films_Actors)
                    if (item.Index_List == minId)
                        tmpList.Add((listActors as List<Actor>)[(listActors as List<Actor>).FindIndex(obj => Equals(obj.Id, item.Id_Actors))]);
                minId++;
            }
            return tmpList;
        }
        //возвращаем лист id фильмов с id актёров
        private List<Films_Actors> GetFilmActors(Film obj)
        {
            List<Films_Actors> tmpList = new List<Films_Actors>();
            int lastId = GetLastId();
            foreach (var item in obj.ActorsList)
            {
                Films_Actors tmpFA = new Films_Actors();
                tmpFA.Id_Film = lastId;
                tmpFA.Id_Actors = item.Id;
                tmpFA.Index_List = (obj.ActorsList as List<Actor>).FindIndex(s => string.Equals(s.Name, item.Name, StringComparison.CurrentCultureIgnoreCase));
                tmpList.Add(tmpFA);
            }
            return tmpList;
        }

        /// <summary>
        /// Поиск в таблице фильмов
        /// </summary>
        /// <param name="type">столбец</param>
        /// <param name="param">значение</param>
        /// <param name="viewedType">просмотрено ли</param>
        /// <returns></returns>
        public IList<Film> SearchTableFilms(string type, string param, ViewedType viewedType)
        {
            IQuery query;
            switch (viewedType)
            {
                case ViewedType.Full:
                    query = Singleton.Instance.OpenSession().CreateQuery("SELECT f FROM Film f WHERE f." + type + " :param");
                    if (type.Contains("Rating"))
                        query.SetParameter("param", float.Parse(param));
                    else query.SetParameter("param", param);
                    return query.List<Film>();
                case ViewedType.TRUE:
                    query = Singleton.Instance.OpenSession().CreateQuery("SELECT f FROM Film f WHERE f." + type + " :param AND f.Viewed = true");
                    if (type.Contains("Rating")) query.SetParameter("param", float.Parse(param));
                    else query.SetParameter("param", param);
                    return query.List<Film>();
                case ViewedType.FALSE:
                    query = Singleton.Instance.OpenSession().CreateQuery("SELECT f FROM Film f WHERE f." + type + " :param AND f.Viewed = false");
                    if (type.Contains("Rating")) query.SetParameter("param", float.Parse(param));
                    else query.SetParameter("param", param);
                    return query.List<Film>();
            }
            return null;
        }
        public IList<Film> SearchFilmsCascade(int Id_param, Subtable subtable, ViewedType viewedType)
        {
            IQuery query;
            switch (subtable)
            {   
                case Subtable.Ganres:
                    switch (viewedType)
                    {
                        case ViewedType.Full:
                            query = Singleton.Instance.OpenSession().CreateQuery(
                                "SELECT f FROM Film f " +
                                "WHERE f.Id IN (SELECT fg.Id_Film FROM Films_Genres fg WHERE fg.Id_Genres = :param)");
                            query.SetParameter("param", Id_param);
                            return query.List<Film>();
                        case ViewedType.TRUE:
                            query = Singleton.Instance.OpenSession().CreateQuery(
                                "SELECT f FROM Film f " +
                                "WHERE f.Id IN (SELECT fg.Id_Film FROM Films_Genres fg WHERE fg.Id_Genres = :param) " +
                                "AND f.Viewed = true");
                            query.SetParameter("param", Id_param);
                            return query.List<Film>();
                        case ViewedType.FALSE:
                            query = Singleton.Instance.OpenSession().CreateQuery(
                                "SELECT f FROM Film f " +
                                "WHERE f.Id IN (SELECT fg.Id_Film FROM Films_Genres fg WHERE fg.Id_Genres = :param) " +
                                "AND f.Viewed = false");
                            query.SetParameter("param", Id_param);
                            return query.List<Film>();
                    }
                    break;
                case Subtable.Actors:
                    switch (viewedType)
                    {
                        case ViewedType.Full:
                            query = Singleton.Instance.OpenSession().CreateQuery(
                                "SELECT f FROM Film f " +
                                "WHERE f.Id IN (SELECT fa.Id_Film FROM Films_Actors fa WHERE fa.Id_Actors = :param)");
                            query.SetParameter("param", Id_param);
                            return query.List<Film>();
                        case ViewedType.TRUE:
                            query = Singleton.Instance.OpenSession().CreateQuery(
                                "SELECT f FROM Film f " +
                                "WHERE f.Id IN (SELECT fa.Id_Film FROM Films_Actors fa WHERE fa.Id_Actors = :param) " +
                                "AND f.Viewed = true");
                            query.SetParameter("param", Id_param);
                            return query.List<Film>();
                        case ViewedType.FALSE:
                            query = Singleton.Instance.OpenSession().CreateQuery(
                                "SELECT f FROM Film f " +
                                "WHERE f.Id IN (SELECT fa.Id_Film FROM Films_Actors fa WHERE fa.Id_Actors = :param) " +
                                "AND f.Viewed = false");
                            query.SetParameter("param", Id_param);
                            return query.List<Film>();
                    }
                    break;
                default:
                    break;
            }
            
            /*
             * SELECT* from Films WHERE Films.Id IN(SELECT fg.Фильм FROM films_genres fg WHERE fg.Жанр = '2')
             * 
             * */
            return null;
        }
    }
}
