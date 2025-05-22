$(document).ready(function () {
    $("#Login").on("click", function (event) {
        event.preventDefault();

        const email = $("#email").val().trim();
        const senha = $("#senha").val().trim();

        if (email === "") {
            Swal.fire({
                icon: "error",
                title: "Oops...",
                text: "O email não pode ser vazio!"
            });
            return;
        }

        if (senha === "") {
            Swal.fire({
                icon: "error",
                title: "Oops...",
                text: "A senha não pode ser vazia!"
            });
            return;
        }

        // Criando o objeto para envio em JSON
        const data = {
            email: email,
            senha: senha
        };

        $.ajax({
            url: '/Login/Autenticar',
            type: 'POST',
            contentType: 'application/json', // Envia como JSON
            data: JSON.stringify(data),      // Converte para JSON string
            dataType: 'json',
            success: function (response) {
                console.log("Resposta do servidor:", response); // Debug

                if (response.success) {
                    window.location.href = "/Dashboard";
                } else {
                    Swal.fire("Erro", response.msg || "Credenciais inválidas", "error");
                }
            },
            error: function (xhr) {
                console.error("Erro na requisição:", xhr.responseText);
                Swal.fire("Erro", "Falha na comunicação com o servidor.", "error");
            },
            complete: function () {
                $("#Login").show();
                $("#loader").hide(); // Se tiver um loader
            }
        });
    });
});

function Cadastrar() {
    location.href = "/Cadastrar";
}
