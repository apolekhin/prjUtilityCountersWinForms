using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Counter
{
    public class config
    {
        public static string ConnectionString = 
                                      "Trusted_Connection=yes;" +
                                      "Initial catalog=Counters;" +
                                      "connection timeout=30";

        public static string GetConnectionString() {
            return (ConnectionString);
        }
        
    }
}
