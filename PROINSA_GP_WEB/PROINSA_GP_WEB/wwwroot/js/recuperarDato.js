function asignarJustificacion() {
    var justificacion = document.getElementById('prueba').value; // Obtener el valor del textarea
    document.getElementById('comentarioHidden').value = justificacion; // Actualizar el valor del campo oculto

    // Actualizar atributos asp-route en el botón Aprobar
    var btnAprobar = document.getElementById('submitButton');
    btnAprobar.setAttribute('asp-route-COMENTARIO', justificacion);

    return true; // Permitir que el botón realice su acción (submit)
}