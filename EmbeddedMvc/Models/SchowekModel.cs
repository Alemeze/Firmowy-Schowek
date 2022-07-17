using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Threading.Tasks;

namespace EmbeddedMvc.Models
{
    public class SchowekModel
    {
        public int SchwId { get; set; }

        [System.ComponentModel.DisplayName("Nazwa Schowka")]
        public string Name { get; set; }

        [System.ComponentModel.DisplayName("Treść Schowka")]
        public string Description { get; set; }


        [System.ComponentModel.DisplayName("Wykonano")]
        public bool Done { get; set; }

        internal SchowekModel FirstOrDefault(Func<object, bool> p)
        {
            throw new NotImplementedException();
        }
    }
}