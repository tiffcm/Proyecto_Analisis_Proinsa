document.addEventListener('DOMContentLoaded', (event) => {
    var submitButton = document.getElementById('submitButton');
    var form = document.querySelector('form');

    if (submitButton && form) {
        submitButton.addEventListener('click', function (event) {
            var toastId = 'btnEliminar';
            localStorage.setItem('showToastId', toastId);
        });
    }

    // Manejar enlaces con clase toastBtnEliminar
    var toastLinks = document.querySelectorAll('.toastBtnE');

    toastLinks.forEach(function (link) {
        link.addEventListener('click', function (event) {
            var toastId = 'btnEliminar';
            localStorage.setItem('showToastId', toastId);
        });
    });
});