﻿namespace PROINSA_GP_API.Entidad
{
    public class Nomina
    {
        public long ID_TIPONOMINA { get; set; }
        public string? DESCRIPCION { get; set; }
        public string? OBSERVACIONES { get; set; }

        public int TipoNomina { get; set; }

        public int CreadorID { get; set; }

    }
}