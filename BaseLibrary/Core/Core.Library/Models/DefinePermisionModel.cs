using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Library.Models
{
    public class DefinePermisionModel
    {
        public string ServiceCode { get; set; }
        public string ServiceName { get; set; }

        public string GroupCode { get; set; }
        public string GroupName { get; set; }

        public string PermisionCode { get; set; }
        public string PermisionName { get; set; }

        public string Description { get; set; }
        public string DependenceCode { get; set; }
        public int Order { get; set; }
        public bool All { get; set; }

        public List<DefinePermisionModel> Actions { get; set; }
    }
}
