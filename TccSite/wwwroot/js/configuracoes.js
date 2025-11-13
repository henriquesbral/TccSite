$(document).ready(function () {
    $("#formConfiguracoes").on("submit", function (e) {
        e.preventDefault();

        const form = $(this);
        const url = form.attr("action") || "/Configuracoes/Salvar";

        const dados = {
            Id: $("#Id").val(),
            LimiteAlertaBaixo: $("#LimiteAlertaBaixo").val(),
            LimiteAlertaMedio: $("#LimiteAlertaMedio").val(),
            LimiteAlertaAlto: $("#LimiteAlertaAlto").val(),
            LimiteAlertaCritico: $("#LimiteAlertaCritico").val(),
            FrequenciaCaptura: $("#FrequenciaCaptura").val(),
            NotificarEmail: $("#notificarEmail").is(":checked"),
            NotificacaoWhatsapp: $("#notificarWhatsapp").is(":checked")
        };

        Swal.fire({
            title: "Salvando...",
            text: "Aplicando as configurações no sistema",
            allowOutsideClick: false,
            didOpen: () => Swal.showLoading()
        });

        $.ajax({
            url: url,
            type: "POST",
            data: dados,
            success: function (response) {
                Swal.close();

                if (response.success === false) {
                    Swal.fire({
                        icon: "error",
                        title: "Erro!",
                        text: response.message
                    });
                } else {
                    Swal.fire({
                        icon: "success",
                        title: "Configurações salvas!",
                        text: response.message || "As alterações foram aplicadas com sucesso."
                    }).then(() => {
                        location.reload();
                    });
                }
            },
            error: function (xhr) {
                Swal.close();
                Swal.fire({
                    icon: "error",
                    title: "Erro no servidor",
                    text: xhr.responseText || "Não foi possível salvar as configurações."
                });
            }
        });
    });
});
