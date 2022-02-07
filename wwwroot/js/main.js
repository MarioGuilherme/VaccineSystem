$(document).ready(() => {
    // MÁSCARA NO CAMPO DE CEP
    $("#cep").mask("00000-000");

    // ANTES DE APLICAR A MÁSCARA, SUBSTITUI AS VÍRGULAS POR PONTOS PARA NÃO CORROMPER O DADO
    $("input[name=vaccine_value]").val($("input[name=vaccine_value]").val().replaceAll(".", ""));
    $("input[name=vaccine_value]").val($("input[name=vaccine_value]").val().replace(",", "."));
    // MÁSCARA PARA O VALOR DA VACINA
    $("input[name=vaccine_value]").inputmask("Regex", {
        regex: "^[0-9]{1,14}(\\.\\d{1,2})?$"
    });
})