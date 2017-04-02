$(document).ready(function () {
    // put response into modal
    $('#import-submit').click(function (e) {
        e.preventDefault();

        // get files form form
        //let data = new FormData();
        //$.each($('#importForm')[0].files, function (i, file) {
        //    data.append('file-' + i, file);
        //});

        let form = $('#import-form');
        let url = form.attr('action');
        let formData = new FormData(form[0]);

        // submit form
        $.ajax({
            url: url,
            data: formData,
            cache: false,
            contentType: false,
            processData: false,
            type: 'POST',
            success: function (data) {
                showModal(data);
            }
        });
    });
});

var showModal = function (data) {
    $('#import-container').html(data);
    $('#import-container').modal('show');
}