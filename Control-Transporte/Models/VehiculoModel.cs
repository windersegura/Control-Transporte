using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Control_Transporte.Models
{
    public class VehiculoModel
    {
        [Display(Name ="ID")]
        public int iidVehiculo { get; set; }
        [Display(Name ="Capacidad")]
        public double capacidad { get; set; }
        [Display(Name ="Consumo")]
        public double consumo_combustible { get; set; }
        [Display(Name ="Costo Depreciación")]
        public double costo_depreciacion { get; set; }
        [Display(Name ="Modelo")]
        public string modelo { get; set; }
        [Display(Name ="Piloto Designado")]
        public int iidPiloto { get; set; }
        [Display(Name ="Tipo de Trasporte")]
        public int iidTipo { get; set; }
        [Display(Name = "Piloto")]
        public string nombre_piloto { get; set; }
        [Display(Name = "Tipo de Vehiculo")]
        public string tipo_vehiculo { get; set; }
    }
}