// painelControle.js
$(document).ready(function () {
    let monitorando = false;
    let intervalo = null;

    const apiUrl = "https://localhost:5001/api/Camera/Capturar"; // URL da sua API
    const ultimaImagemUrl = "/PainelControle/UltimaImagem"; // Rota do seu MVC
    const $status = $("#status");
    const $spinner = $("#loadingSpinner");

    $("#btnMonitorar").on("click", function () {
        if (!monitorando) {
            iniciarMonitoramento();
        } else {
            pararMonitoramento();
        }
    });

    function iniciarMonitoramento() {
        const tempo = parseInt($("#tempoAtualizacao").val());
        $status.text("🟢 Iniciando captura...").addClass("loading");
        $("#btnMonitorar").html('<i class="bi bi-stop-circle"></i> Parar Monitoramento');
        monitorando = true;

        Swal.fire({
            title: "Monitoramento iniciado",
            text: "A captura automática de imagens está em andamento.",
            icon: "success",
            timer: 2500,
            showConfirmButton: false
        });

        capturarFoto();
        atualizarImagem();

        intervalo = setInterval(() => {
            capturarFoto();
            atualizarImagem();
        }, tempo);
    }

    function pararMonitoramento() {
        clearInterval(intervalo);
        monitorando = false;
        $status.removeClass("loading").text("⏸️ Monitoramento parado");
        $("#btnMonitorar").html('<i class="bi bi-play-circle-fill"></i> Iniciar Monitoramento');

        Swal.fire({
            title: "Monitoramento parado",
            text: "O monitoramento foi interrompido.",
            icon: "info",
            timer: 2000,
            showConfirmButton: false
        });
    }

    function capturarFoto() {
        $spinner.show();
        $.ajax({
            url: apiUrl,
            type: "POST",
            success: function (data) {
                console.log("📸 Captura iniciada:", data);
                $status.text("📸 Captura em andamento...").addClass("loading");
            },
            error: function (err) {
                console.error("❌ Erro ao capturar:", err);
                $status.text("❌ Erro ao acionar API de captura.").removeClass("loading");
                pararMonitoramento();

                Swal.fire({
                    title: "Erro na captura",
                    text: "Falha ao comunicar com a API de monitoramento.",
                    icon: "error"
                });
            },
            complete: function () {
                $spinner.hide();
            }
        });
    }

    function atualizarImagem() {
        $.ajax({
            url: ultimaImagemUrl,
            type: "GET",
            success: function (data) {
                if (data.sucesso && data.imagem) {
                    $("#ultimaImagem").attr("src", data.imagem.urlImagem + "?v=" + new Date().getTime());
                    $("#ultimaAtualizacao").text("Última atualização: " + new Date(data.imagem.dataCadastro).toLocaleString());
                    $status.text("🟢 Última captura atualizada").removeClass("loading");
                } else {
                    $status.text("⚠️ Nenhuma imagem disponível ainda.");
                }
            },
            error: function () {
                $status.text("⚠️ Erro ao obter imagem do servidor.");
            }
        });
    }
});
