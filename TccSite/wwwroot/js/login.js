$(document).ready(function () {
    $("#Login").on("click", async function (event) {
        event.preventDefault();

        const email = $("#email").val().trim();
        const senha = $("#senha").val().trim();

        if (!email) {
            return Swal.fire("Erro", "O email não pode ser vazio!", "error");
        }

        if (!senha) {
            return Swal.fire("Erro", "A senha não pode ser vazia!", "error");
        }

        try {
            const response = await $.ajax({
                url: '/Login/Autenticar',
                type: 'POST',
                contentType: 'application/json',
                data: JSON.stringify({ email, senha }),
                dataType: 'json'
            });

            if (response.success) {
                Swal.fire({
                    icon: "success",
                    title: "Bem-vindo!",
                    text: response.msg,
                    timer: 1200,
                    showConfirmButton: false
                }).then(() => {
                    window.location.href = "/Dashboard";
                });
            } else {
                Swal.fire("Erro", response.msg || "Credenciais inválidas", "error");
            }
        } catch (xhr) {
            console.error("Erro na requisição:", xhr.responseText);
            Swal.fire("Erro", "Falha na comunicação com o servidor.", "error");
        }
    });

    // === RESET DE SENHA ===
    $("#btnResetSenha").on("click", async function (event) {
        event.preventDefault();

        const email = $("#resetEmail").val().trim();
        const novaSenha = $("#novaSenha").val().trim();
        const confirmacaoSenha = $("#confirmaSenha").val().trim();

        if (!email) return Swal.fire("Erro", "O email não pode ser vazio!", "error");
        if (!novaSenha) return Swal.fire("Erro", "A nova senha não pode ser vazia!", "error");
        if (!confirmacaoSenha) return Swal.fire("Erro", "A confirmação da nova senha não pode ser vazia!", "error");
        if (novaSenha !== confirmacaoSenha) return Swal.fire("Erro", "As senhas não coincidem!", "error");

        try {
            const response = await $.ajax({
                url: '/ResetSenha/EnviarResetSenha',
                type: 'POST',
                contentType: 'application/json',
                data: JSON.stringify({ email, novaSenha, confirmacaoSenha }),
                dataType: 'json'
            });

            if (response.success) {
                Swal.fire({
                    icon: "success",
                    title: "Senha redefinida!",
                    text: response.msg || "Sua senha foi alterada com sucesso.",
                    timer: 1500,
                    showConfirmButton: false
                }).then(() => {
                    $("#ModalReset").modal('hide');
                    $("#formResetSenha")[0].reset();
                    window.location.href = "/Login";
                });
            } else {
                Swal.fire("Erro", response.msg || "Não foi possível redefinir a senha.", "error");
            }
        } catch (xhr) {
            console.error("Erro na requisição:", xhr.responseText);
            Swal.fire("Erro", "Falha na comunicação com o servidor.", "error");
        }
    });
});

function Cadastrar() {
    window.location.href = "/Cadastrar";
}
