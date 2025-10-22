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
});

function Cadastrar() {
    window.location.href = "/Cadastrar";
}
