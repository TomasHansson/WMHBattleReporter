using SQLite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WMHBattleReporter.Model
{
    public class EmailProvider
    {   
        [PrimaryKey]
        public string Address { get; set; }
        public string Password { get; set; }
    }
}
