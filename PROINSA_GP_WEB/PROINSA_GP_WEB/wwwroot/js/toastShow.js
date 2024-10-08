﻿document.addEventListener('DOMContentLoaded', (event) => {
    var submitButton = document.getElementById('submitButton');
    var form = document.querySelector('form');

    if (submitButton && form) {
        submitButton.addEventListener('click', function (event) {
            var toastId = 'btnActualizaMante';
            localStorage.setItem('showToastId', toastId);
        });
    }

    // Manejar enlaces con clase toastBtn
    var toastLinks = document.querySelectorAll('.toastBtn');

    toastLinks.forEach(function (link) {
        link.addEventListener('click', function (event) {
            var toastId = 'btnActualizaMante';
            localStorage.setItem('showToastId', toastId);
        });
    });
});