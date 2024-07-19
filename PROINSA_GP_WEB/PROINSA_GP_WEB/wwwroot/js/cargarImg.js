$(document).ready(function () {
    $("#upload").change(function (event) {
        var file = event.target.files[0];
        if (file && file.size > 1048576) { // 1MB = 1048576 bytes
            toastr.error('El archivo es demasiado grande. El tamaño máximo permitido es 1MB.');
            event.target.value = ''; // Clear the file input
            $("#imgSeleccionada").attr("hidden", true); // Hide the selected image
        } else if (file) {
            // Mostrar la imagen seleccionada
            $("#imgSeleccionada").removeAttr("hidden");
            $("#imgSeleccionada").attr("src", window.URL.createObjectURL(file));

            // Ocultar la imagen de avatar cargada usando CSS
            $("#uploadedAvatar").css("display", "none");
        }
    });
});