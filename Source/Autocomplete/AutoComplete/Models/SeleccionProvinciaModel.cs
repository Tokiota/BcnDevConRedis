using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

using AutoComplete.Controllers;

namespace AutoComplete.Models
{
    public class SeleccionProvinciaModel
    {
        public List<SelectListItem> Provincias { get; set; }

        public string ComboProvinciasId { get { return "cmbProvincias"; } }
        public IDictionary<int, string> Poblaciones { get; set; }
        public string Selected { get; set; }

        public void AgregarSeleccioneUnaEnProvincias()
        {
            

        }
    }
}