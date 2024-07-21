$(document).ready(function () {
    $("#subirDocumento").change(function (event) {
        // Obtiene el archivo que se ha seleccionado
        var file = event.target.files[0];

        // Verifica si el archivo existe y su tamaño es mayor a 5MB (5 * 1024 * 1024 bytes)
        if (file && file.size > 5000000) { // 5MB = 5 * 1024 * 1024 bytes
            // Muestra un mensaje de error usando toastr
            toastr.error('El archivo es demasiado grande. El tamaño máximo permitido es 5MB.');

            // Limpia el campo de entrada de archivo
            event.target.value = '';
        }
    });
});
