var _emails;
var _break = false;

// Ready ------------------------------------

$(document).ready(function () {

    $('#summernote').summernote({
        minHeight: 220,
        maxHeight: null
    });

    $("#summernote").summernote("code", "<p>Olá @NOME, tudo bem?</p><p><b>Obs: @NOME será substituído pelo nome da planilha.</b></p>");

    $("#btnStop").on('click', function () {
        _break = true;
        $("#btnStop").hide();        
    });

    $("#btnSend").on('click', function () {

        if (!validadeEmails()) {
            alert('Nenhum e-mail foi carregado, favor verificar');
            return;
        }

        if (!validateFields()) {
            alert('Todos os campos são obrigatórios');
            return;
        }

        $("#divForm").hide();
        $("#divResult").show();

        sendMails();        

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

function sendMails() {
    _emails = JSON.parse($("#hiddenEmails").val());
    sendMail(0);
}

function sendMail(index) {
    $.ajax({
        url: "/mail/send",
        type: "post",
        timeout: 0,
        data:
        {
            FromName: $("#name").val(),
            FromEmail: $("#email").val(),
            FromPassword: $("#password").val(),
            EmailSubject: $("#emailSubject").val(),
            EmailText: $('#summernote').summernote('code'),
            ToName: _emails[index].Name,
            ToEmail: _emails[index].Email
        }
    }).done(function (response) {
        addGridResult(response);
                
        index = index + 1;
        if (index < _emails.length) {
            if (_break)
            {
                $("#divResultProcessing").hide();
                $("#divResultStop").show();
                return;
            }
            $("#processingItem").text(index + 1);
            sendMail(index);
        }
        else {
            $("#divResultProcessing").hide();
            $("#divResultSuccess").show();
        }
    });
}

function addGridResult(item) {
    $("#tableItems").append(
        '<tr>' +
            '<td>' + (item.Success ? '<img src="/Images/success.png" />' : '<img src="/Images/error.png" />') + '</td>' +
            '<td scope="row">' + item.Name + '</td>' +
            '<td>' + item.Email + '</td>' +
            '<td>' + item.Message + '</td>' +
        '</tr>');
}

function validateFields() {
    var name = $("#name").val();
    var email = $("#email").val();
    var password = $("#password").val();
    var subject = $("#emailSubject").val();
    var emailtext = $('#summernote').summernote('code');

    return !isEmpty(name) && !isEmpty(email) && !isEmpty(password) && !isEmpty(subject) && !isEmpty(emailtext);
}

function validadeEmails() {
    var obj = JSON.parse($("#hiddenEmails").val());
    return !(obj == undefined || obj == null || obj.length == 0);
}

function isEmpty(val) {
    return val == undefined || val == null || val.trim() == '';
}