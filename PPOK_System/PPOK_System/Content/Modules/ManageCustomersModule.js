$(document).ready(function () {
    var comTable = $('#customer-table').DataTable({
        "info": false,
        "pagingType": "simple",
        "pageLength": 10,
        "order": [[1, "asc"]],
        "columnDefs": [
            {
                "targets": [0],
                "searchable": true,
                "orderable": true
            },
            {
                "targets": [1],
                "searchable": true,
                "orderable": true,
                "visible": true
            }
        ]
    });
});