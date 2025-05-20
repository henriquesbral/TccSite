$(document).ready(function () {
    $("#Login").on("click", function (event) {
        event.preventDefault();

        const usuario = $("#usuario").val().trim();
        const senha = $("#senha").val().trim();

        if (usuario === "") {
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
            usuario: usuario,
            senha: senha
        };

        $.ajax({
            url: '/Login/Logar',
            type: 'POST',
            contentType: 'application/json', // Envia como JSON
            data: JSON.stringify(data),      // Converte para JSON string
            dataType: 'json',
            beforeSend: function () {
                $("#Login").hide();
                $("#loader").show(); // Se tiver um loader, senão remova esta linha
            },
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
