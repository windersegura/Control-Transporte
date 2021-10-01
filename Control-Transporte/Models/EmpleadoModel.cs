using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Control_Transporte.Models
{
    public class EmpleadoModel
    {
        [Display(Name = "ID")]
        public int iidPiloto { get; set; }
        [Display(Name = "Nombres")]
        public string nombre_apellido { get; set; }
        [Display(Name = "No. Empleado")]
        public int numero { get; set; }
        [Display(Name = "Viaticos Asignados")]
        public double viaticos_asignados { get; set; }
        [Display(Name = "Costo Extra Asignado")]
        public double costos_adicionales_asignados { get; set; }
        [Display(Name = "Tipo Empleado")]
        public int id_tipo { get; set; }
        [Display(Name = "Tipo de Empleado")]
        public string nombre_tipo { get; set; }
    }
}