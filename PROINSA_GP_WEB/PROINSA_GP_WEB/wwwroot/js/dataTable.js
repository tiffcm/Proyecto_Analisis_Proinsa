document.addEventListener('DOMContentLoaded', function () {
    var dataTable = document.getElementById('dataTable');
    if (dataTable) {
        var dataTableInstance = new DataTable(dataTable, {
            language: {
                url: '//cdn.datatables.net/plug-ins/2.0.5/i18n/es-ES.json'
            }
        });
    }
});
