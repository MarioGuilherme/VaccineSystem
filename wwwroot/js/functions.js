// DECLARAÇÃO DAS FUNÇÕES JAVASCRIPTS PARA TODO O ESCOPO DO PROJETO

// Método responsável por fazer requisições assíncronas com ajax
function Ajax(url, data, callback) {
    $.ajax({
        url: url,
        type: "POST",
        dataType: "json",
        data: data,
    }).done(callback);
}

// Método responsável por aparecer o sweetalert na tela
function SweetAlert(icon, msg) {
    Swal.fire({
        icon: icon,
        html: `<h2>${msg}</h2>`,
        confirmButtonText: "OK",
        allowOutsideClick: false
    });
}

// Método responsável por limpar os campos e indexar a primeira option no select
function CleanFields() {
    $("input:not(input[type=button])").val("");
    $("select").prop("selectedIndex", 0);
}

// Método responsável por percorrer um array de campos para verificar campos vazios
function VerifyEmptyFields(fields) {
    var hasEmptyFields = false;
    for (let i = 0; i < fields.length; i++) {
        if (fields[i].val() == "") {
            // Se a variável hasEmptyFields for false, ele deixa o primeiro campo em branco com focus, o que torna o unico com focus
            hasEmptyFields ? "" : $($(fields)[i]).focus();
            // Aparece o aviso de campo obrigatório
            $($(fields)[i]).siblings("span").show();
            hasEmptyFields = true;
        }
    };
    return hasEmptyFields;
}