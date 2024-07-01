document.addEventListener('DOMContentLoaded', function () {
    const buttons = document.querySelectorAll('.ver-solicitud');

    buttons.forEach(button => {
        button.addEventListener('click', function () {
            const tipo = this.getAttribute('data-tipo');
            const id = this.getAttribute('data-id');
            const identificacion = this.getAttribute('data-identificacion');
            const nombre = this.getAttribute('data-nombre');

            if (tipo === 'VACACIONES') {
                // Rellena los datos del modal de Vacaciones
                document.getElementById('IdSolicitud1').value = id;
                document.getElementById('Identificacion1').value = identificacion;
                document.getElementById('Nombre1').value = nombre;
                document.getElementById('TipoPermiso1').value = tipo;

                // Abre el modal de Vacaciones
                new bootstrap.Modal(document.getElementById('modalVacaciones')).show();
            } else if (tipo === 'CAMBIO DE HORARIO') {
                document.getElementById('IdSolicitud2').value = id;
                document.getElementById('Identificacion2').value = identificacion;
                document.getElementById('Nombre2').value = nombre;
                document.getElementById('TipoPermiso2').value = tipo;

                // Abre el modal de cambio de horario
                new bootstrap.Modal(document.getElementById('modalHorario')).show();
            } else if (tipo === 'PERMISO ESPECIAL') {
                document.getElementById('IdSolicitud3').value = id;
                document.getElementById('Identificacion3').value = identificacion;
                document.getElementById('Nombre3').value = nombre;
                document.getElementById('TipoPermiso3').value = tipo;

                // Abre el modal de permiso especial
                new bootstrap.Modal(document.getElementById('modalPermisoEspecial')).show();
            } else if (tipo === 'INCAPACIDAD') {
                document.getElementById('IdSolicitud4').value = id;
                document.getElementById('Identificacion4').value = identificacion;
                document.getElementById('Nombre4').value = nombre;
                document.getElementById('TipoPermiso4').value = tipo;

                // Abre el modal de incapacidad
                new bootstrap.Modal(document.getElementById('modalIncapacidad')).show();
            }
            // Agrega más condiciones para otros tipos de permisos si es necesario
        });
    });
});