$(document).ready(function () {
    // Chamada AJAX quando a página carregar
    $.ajax({
        url: '/Dashboard/Alerta', // URL da action no controller
        method: 'GET',
        dataType: 'json',
        success: function (data) {
            $('#resultado').html('<pre>' + JSON.stringify(data, null, 2) + '</pre>');
        },
        error: function (xhr, status, error) {
            console.error("Erro ao buscar dados:", error);
        }
    });
});

function detalhesAlertas() {
    Swal.fire("Card de visualização em andamento");
}