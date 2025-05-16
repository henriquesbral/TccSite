function logar(event) {

    event.preventDefault();
    var login = $("#login").val();
    var senha = $("#senha").val();
    if (login == 'admin@gmail.com' && senha == 'teste') {
        console.log(`Esse é o login:${login} essa é a senha:${senha}`);
        console.log('Usuarios corretos');
        Swal.fire({
            title: "Carregando..",
            html: "Bem-vindo ao site! <br> Redirecionando em <b></b> ms",
            timer: 1000,
            timerProgressBar: true,
            didOpen: () => {
                Swal.showLoading();
                const timer = Swal.getPopup().querySelector("b");
                timerInterval = setInterval(() => {
                    if (timer) timer.textContent = `${Swal.getTimerLeft()}`;
                }, 100);
            },
            willClose: () => {
                clearInterval(timerInterval);
            }
        }).then((result) => {
            /* finalizando o load e abrindo a pagina*/
            if (result.dismiss === Swal.DismissReason.timer) {
                location.href = "/Dashboard";
            }
        });
    } else {
        Swal.fire({
            icon: "error",
            title: "Oops..",
            text: "Usuário ou senha inválidos"
        });
        console.log(`Esse é o login:${login} essa é a senha:${senha}`);
        console.log('Usuário ou senha inválidos');
    }
}

