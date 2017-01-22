
$(document).ready(function () {
    $('#summernote').summernote({
        minHeight: 220,
        maxHeight: null
    });

    $("#btnEnviar").on('click', function () {

        showLoad();

        $.ajax({
            url: "/mail/send/execute",
            type: "post",
            data:
            {
                Name: $("#name").val(),
                Email: $("#email").val(),
                Password: $("#password").val(),
                EmailSubject: $("#emailSubject").val(),
                EmailText: $('#summernote').summernote('code'),
                To: JSON.parse($("#hiddenEmails").val())
            },
            success: function (response) {
                console.log(response);
                hideLoad();
                $("#divForm").hide();
                $("#divResult").show();
            },
            error: function (jqXHR, textStatus, errorThrown) {
                console.log(textStatus, errorThrown);
                hideLoad();
            }
        });

        event.preventDefault();
    });

    $("#divForm").show();
    $("#divResult").hide();
});

function showLoad()
{

}

function hideLoad()
{

}