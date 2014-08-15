using System;

namespace Data.Models
{
    public partial class Admin
    {
        public int AdminId { get; set; }
        public int Typeid { get; set; }

        public string Pwd { get; set; }
        public string Uname { get; set; }



        public string Loginip { get; set; }
        public virtual Admintype Admintype { get; set; }
    }


}
