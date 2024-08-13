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

document.addEventListener('DOMContentLoaded', function () {
    var dataTable2 = document.getElementById('dataTableDeducciones');
    if (dataTable2) {
        var dataTableInstance = new DataTable(dataTable2, {
            language: {
                url: '//cdn.datatables.net/plug-ins/2.0.5/i18n/es-ES.json'
            },
            "paging": false,
            "bInfo": false
        });
    }
});

document.addEventListener('DOMContentLoaded', function () {
    var dataTable2 = document.getElementById('dataTableIngresos');
    if (dataTable2) {
        var dataTableInstance = new DataTable(dataTable2, {
            language: {
                url: '//cdn.datatables.net/plug-ins/2.0.5/i18n/es-ES.json'
            },
            "paging": false,
            "bInfo": false
        });
    }
});