using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;

namespace ListaIgrica
{
    class ConnectionHelp
    {
        public static string connectionString = ConfigurationManager.ConnectionStrings["ListaIgrica.Properties.Settings.DataIgriceConnectionString"].ConnectionString;
    }
}
