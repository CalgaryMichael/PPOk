$(document).ready(function () {
    // put response into modal
    $('#import-submit').click(function (e) {
        e.preventDefault();

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
    });
});

var showModal = function (data) {
    $('#import-container').html(data);
    $('#import-container').modal('show');
}