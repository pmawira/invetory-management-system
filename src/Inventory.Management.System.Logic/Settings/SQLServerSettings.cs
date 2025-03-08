using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Inventory.Management.System.Logic.Settings
{
    public class SQLServerSettings
    {
        [Required(AllowEmptyStrings = false)]
        public string ConnectionString { get; set; } = null!;
    }
}
