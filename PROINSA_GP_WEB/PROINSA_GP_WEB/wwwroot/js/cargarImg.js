$(document).ready(function () {
    $("#upload").change(function (event) {
        var files = event.target.files;
        // Mostrar la imagen seleccionada
        $("#imgSeleccionada").removeAttr("hidden");
        $("#imgSeleccionada").attr("src", window.URL.createObjectURL(files[0]));

        // Ocultar la imagen de avatar cargada usando CSS
        $("#uploadedAvatar").css("display", "none");
    });
});
