$(document).ready(function () {
    var customerTable = $('#customer-table').DataTable({
        "info": false,
        "bLengthChange": false,
        "responsive": true,
        "pagingType": "simple",
        "pageLength": 10,
        "sDom": '<pf<t>i>',
        "order": [[2, "asc"]],
        "columnDefs": [
            {
                "targets": [0],
                "searchable": false,
                "orderable": false,
                "visible": false
            },
            {
                "targets": [1],
                "searchable": true,
                "orderable": true,
                "visible": true
            },
            {
                "targets": [2],
                "searchable": true,
                "orderable": true,
                "visible": true
            }
        ]
    });
});


var MESSAGE_HISTORY_URL = "/Pharmacy/PersonHistory/";

var loadModule = function (url, sendData) {
    $.ajax({
        url: url,
        type: 'POST',
        data: { id: sendData },
        dataType: 'html',
        beforeSend: function () {
            $('#spinner').css('display', 'block');
        },
        success: function (data) {
            showModal(data);
        },
        complete: function () {
            $('#spinner').css('display', 'none');
        }
    });
};


var showModal = function (data) {
    $('#history-container').html(data);
    $('#history-container').modal('show');
}