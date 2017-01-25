
// Ready ------------------------------------
$(document).ready(function () {

    $('#summernote').summernote({
        minHeight: 220,
        maxHeight: null
    });

    $("#summernote").summernote("code", "<p>Olá @NOME, tudo bem?</p><p><b>Obs: @NOME será substituído pelo nome da planilha.</b></p>");

    $("#btnSend").on('click', function () {

        if (!validateFields()) {
            alert('Todos os campos são obrigatórios');
            return;
        }

        showLoad();

        $.ajax({
            url: "/mail/send/execute",
            type: "post",
            timeout: 0,
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

    $("#btnRetry").on('click', function () {
        $("#divForm").show();
        $("#divResult").hide();
    });

    $("#divForm").show();
    $("#divResult").hide();
});

// End Ready ------------------------------------

function bindGridResult(response) {
    $("#divResult > table > tbody > tr").remove();

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

function validateFields() {
    var name = $("#name").val();
    var email = $("#email").val();
    var password = $("#password").val();
    var subject = $("#emailSubject").val();
    var emailtext = $('#summernote').summernote('code');

    return !IsEmpty(name) && !IsEmpty(email) && !IsEmpty(password) && !IsEmpty(subject) && !IsEmpty(emailtext);
}

function IsEmpty(val) {
    return val == undefined || val == null || val.trim() == '';
}

function showLoad() {
    $('#btnSend').text('Enviando...');
    $('#btnSend').prop('disabled', true);    
    $('#loading').show();
}

function hideLoad() {
    $('#btnSend').text('Enviar');
    $('#btnSend').prop('disabled', false);    
    $('#loading').hide();
}