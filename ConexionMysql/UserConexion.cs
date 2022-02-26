using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace ConexionMysql
{
    public class UserConexion
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string Color { get; set; }
        public string Host { get; set; }
        public string Port { get; set; }
        public string Password { get; set; }
        public string User { get; set; }
        public string DataBase { get; set; }

        [field: NonSerialized()]
        public ICommand Command { get; set; }
        public ICommand open { get; set; }

    }
}
