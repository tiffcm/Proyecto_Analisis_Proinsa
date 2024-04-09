$(document).ready(function () {
	"use strict";
	$("#basic-datatable").DataTable({
		keys: !0,
		language: {
			url: "//cdn.datatables.net/plug-ins/9dcbecd42ad/i18n/Spanish.json",
			paginate: {
				previous: "<i class='mdi mdi-chevron-left'>",
				next: "<i class='mdi mdi-chevron-right'>"
			}
		},
		drawCallback: function () {
			$(".dataTables_paginate > .pagination").addClass("pagination-rounded")
		}
	});
});