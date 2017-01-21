
$(document).ready(function () {
    $('#summernote').summernote({
        minHeight: 220,
        maxHeight: null
    });

    $("#btnEnviar").on('click', function () {
        $.ajax({            
            url: "/Home/SendMail",
            type: "post",
            data:
            {
                Name: $("#name").val(),
                Email: $("#email").val(),
                Password: $("#password").val(),
                EmailSubject: $("#emailSubject").val(),
                EmailText: $('#summernote').summernote('code')
            },
            success: function (response) {
                console.log(response);
            },
            error: function (jqXHR, textStatus, errorThrown) {
                console.log(textStatus, errorThrown);
            }
        });

        event.preventDefault();
    });
});

