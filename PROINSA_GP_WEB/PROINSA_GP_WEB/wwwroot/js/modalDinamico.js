document.addEventListener('DOMContentLoaded', function () {
    const buttons = document.querySelectorAll('.ver-solicitud');

    buttons.forEach(button => {
        button.addEventListener('click', function () {
            const tipo = this.getAttribute('data-tipo');
            const id = this.getAttribute('data-id');

            switch (tipo) {
                case 'VACACIONES':
                    abrirModal('modalVacaciones', id, tipo);
                    break;
                case 'CAMBIO DE HORARIO':
                    abrirModal('modalHorario', id, tipo);
                    break;
                case 'PERMISO ESPECIAL':
                    abrirModal('modalPermisoEspecial', id, tipo);
                    break;
                case 'INCAPACIDAD':
                    abrirModal('modalIncapacidad', id, tipo);
                    break;
                default:
                    console.error('Tipo de permiso no reconocido:', tipo);
                    break;
            }
        });
    });

    function abrirModal(modalId, solicitudId, tipo) {
        // Abre el modal correspondiente
        const modal = new bootstrap.Modal(document.getElementById(modalId));
        modal.show();

        // Realiza la solicitud AJAX para traer el detalle de las aprobaciones
        var xhr = new XMLHttpRequest();
        xhr.open('GET', '/Aprobaciones/ConsultarAprobDetalle?ID_SOLICITUD=' + solicitudId);
        xhr.setRequestHeader('Content-Type', 'application/json');

        xhr.onload = function () {
            if (xhr.status === 200) {
                var response = JSON.parse(xhr.responseText);

                console.log(response); // Verifica la respuesta recibida en la consola, sí funciona pero no se refleja

                switch (tipo) {
                    case 'VACACIONES':
                        actualizarModalVacaciones(response, modalId);
                        break;
                    case 'CAMBIO DE HORARIO':
                        actualizarModalHorario(response, modalId);
                        break;
                    case 'PERMISO ESPECIAL':
                        actualizarModalPermisoEspecial(response, modalId);
                        break;
                    case 'INCAPACIDAD':
                        actualizarModalIncapacidad(response, modalId);
                        break;
                    default:
                        console.error('Tipo de permiso no reconocido:', tipo);
                        break;
                }
            } else {
                console.error('Error al obtener los detalles de la solicitud. Estado:', xhr.status);
                alert('Error al obtener los detalles de la solicitud.');
            }
        };

        xhr.onerror = function () {
            console.error('Error de red al intentar obtener los detalles de la solicitud.');
            alert('Error de red al intentar obtener los detalles de la solicitud.');
        };

        xhr.send();
    }

    

    function actualizarModalVacaciones(response, modalId) {
        document.getElementById(modalId).querySelector('#IdSolicitud1').value = response.ID_SOLICITUD;
        document.getElementById(modalId).querySelector('#Identificacion1').value = response.ID_EMPLEADO;
        document.getElementById(modalId).querySelector('#Nombre1').value = response.SOLICITANTE;
        document.getElementById(modalId).querySelector('#FechaInicial1').value = response.FECHA_INICIO;
        document.getElementById(modalId).querySelector('#FechaFinal1').value = response.FECHA_FINAL;
        document.getElementById(modalId).querySelector('#TipoPermiso1').value = response.TIPOPERMISO;
        document.getElementById(modalId).querySelector('#Dias1').value = response.DIAS;
    }

    function actualizarModalHorario(response, modalId) {
        document.getElementById(modalId).querySelector('#IdSolicitud2').value = response.ID_SOLICITUD;
        document.getElementById(modalId).querySelector('#Identificacion2').value = response.ID_EMPLEADO;
        document.getElementById(modalId).querySelector('#Nombre2').value = response.SOLICITANTE;
        document.getElementById(modalId).querySelector('#FechaInicial2').value = response.FECHA_INICIO;
        document.getElementById(modalId).querySelector('#FechaFinal2').value = response.FECHA_FINAL;
        document.getElementById(modalId).querySelector('#TipoPermiso2').value = response.TIPOPERMISO;
    }

    function actualizarModalPermisoEspecial(response, modalId) {
        document.getElementById(modalId).querySelector('#IdSolicitud3').value = response.ID_SOLICITUD;
        document.getElementById(modalId).querySelector('#Identificacion3').value = response.ID_EMPLEADO;
        document.getElementById(modalId).querySelector('#Nombre3').value = response.SOLICITANTE;
        document.getElementById(modalId).querySelector('#FechaInicial3').value = response.FECHA_INICIO;
        document.getElementById(modalId).querySelector('#FechaFinal3').value = response.FECHA_FINAL;
        document.getElementById(modalId).querySelector('#TipoPermiso3').value = response.TIPOPERMISO;
    }

    function actualizarModalIncapacidad(response, modalId) {
        document.getElementById(modalId).querySelector('#IdSolicitud4').value = response.ID_SOLICITUD;
        document.getElementById(modalId).querySelector('#Identificacion4').value = response.ID_EMPLEADO;
        document.getElementById(modalId).querySelector('#Nombre4').value = response.SOLICITANTE;
        document.getElementById(modalId).querySelector('#FechaInicial4').value = response.FECHA_INICIO;
        document.getElementById(modalId).querySelector('#FechaFinal4').value = response.FECHA_FINAL;
        document.getElementById(modalId).querySelector('#TipoPermiso4').value = response.TIPOPERMISO;
    }
});