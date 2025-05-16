//function Logar(event) {

//    event.preventDefault();
//    var login = $("#login").val();
//    var senha = $("#senha").val();
//    if (login == 'admin@gmail.com' && senha == 'teste') {
//        Swal.fire({
//            title: "Carregando..",
//            html: "Bem-vindo ao site! <br> Redirecionando em <b></b> ms",
//            timer: 1000,
//            timerProgressBar: true,
//            didOpen: () => {
//                Swal.showLoading();
//                const timer = Swal.getPopup().querySelector("b");
//                timerInterval = setInterval(() => {
//                    if (timer) timer.textContent = `${Swal.getTimerLeft()}`;
//                }, 100);
//            },
//            willClose: () => {
//                clearInterval(timerInterval);
//            }
//        }).then((result) => {
//            /* finalizando o load e abrindo a pagina*/
//            if (result.dismiss === Swal.DismissReason.timer) {
//                window.location.href = "/Dashboard";
//            }
//        });
//    } else {
//        Swal.fire({
//            icon: "error",
//            title: "Oops..",
//            text: "Usuário ou senha inválidos"
//        });
//    }
//}

$("#Login").click(function () {
    $(".alert").remove();

    if ($("#usuario").val() == "") {
        Swal.fire({
            icon: "error",
            title: "Oops...",
            text: "O email não pode ser vazio!"
        });
        return;
    }
    if ($("#senha").val() == "") {
        Swal.fire({
            icon: "error",
            title: "Oops...",
            text: "A senha não pode ser vazia!"
        });
        return;
    }

    var data = {
        "usuario": $("#usuario").val(),
        "senha": $("#senha").val()
    };

    $.ajax({
        url: '/Login/Logar', // ou use '@Url.Action("Logar", "Login")' se for Razor
        type: "POST",
        data: JSON.stringify(data),
        dataType: "json",
        contentType: "application/json",
        beforeSend: function () {
            $("#Login").hide();
            $("#loader").show();
        },
        success: function (data) {
            if (data.success) {
                location.href = "/Dashboard";
            } else {
                Swal.fire({
                    icon: "error",
                    title: "Erro",
                    text: data.msg || "Usuário ou senha inválidos."
                });
                $("#Login").show();
                $("#loader").hide();
            }
        },
        error: function (err) {
            $("#Login").show();
            $("#loader").hide();
            Swal.fire({
                icon: "error",
                title: "Oops...",
                text: "Ocorreu um erro inesperado."
            });
            console.log(err.responseText);
        }
    });
});

function Cadastrar() {
    location.href = "/Cadastrar";
}
