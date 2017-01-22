
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
                Parameters: {
                    Name: $("#name").val(),
                    Email: $("#email").val(),
                    Password: $("#password").val(),
                    EmailSubject: $("#emailSubject").val(),
                    EmailText: $('#summernote').summernote('code')
                },
                To: JSON.parse($("#hiddenEmails").val())
            },
            success: function (response) {
                hideLoad();
                bindGridResult(response);
                $("#divForm").hide();
                $("#divResult").show();
            },
            error: function (jqXHR, textStatus, errorThrown) {
                hideLoad();
            }
        });

        event.preventDefault();
    });

    $("#divForm").show();
    $("#divResult").hide();
});

function bindGridResult(response) {
    response.forEach(function (item) {
        $("#divResult > table > tbody").append(
            '<tr>' +
                '<td>' + (item.Success ? '<img src="/Images/success.png" />' : '<img src="/Images/error.png" />') + '</td>' +
                '<td scope="row">' + item.Name + '</td>' +
                '<td>' + item.Email + '</td>' +                
                '<td>' + item.Message + '</td>' +
            '</tr>');
    });    
}

function showLoad() {

}

function hideLoad() {

}