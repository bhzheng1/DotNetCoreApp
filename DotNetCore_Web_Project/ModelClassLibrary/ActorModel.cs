using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ModelClassLibrary
{
    public partial class ActorModel
    {
        public int ActorID { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime LastUpdate { get; set; }
    }
    //之所以用partial class是为了分离数据和操作

    //操作部分
    public partial class ActorModel
    {
        public IList<FilmModel> GetFilmsInStock()
        {
            IList<FilmModel> FilmsInStock = new List<FilmModel>();
            //call stored procedures
            return FilmsInStock;
        }
        public IList<FilmModel> GetFilmsNotInStock()
        {
            IList<FilmModel> FilmsNotInStock = new List<FilmModel>();
            //call stored procedures
            return FilmsNotInStock;
        }
    }
}
