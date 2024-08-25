document.addEventListener('DOMContentLoaded', (event) => {
    var submitButton = document.getElementById('submitButton');
    var form = document.querySelector('form'); 

    if (submitButton && form) {
        submitButton.addEventListener('click', function (event) {

            var toastId = 'btnActualizaMante';
            localStorage.setItem('showToastId', toastId);
        });
    }
});
