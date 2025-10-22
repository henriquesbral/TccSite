function CadastrarUsuario() {
    // Validação básica
    const nome = $("#Nome").val().trim();
    const sobrenome = $("#Sobrenome").val().trim();
    const email = $("#Email").val().trim();
    const dataNascimento = $("#DataNascimento").val();
    const senha = $("#Senha").val();
    const repetirSenha = $("#RepetirSenha").val();

    if (!nome || !sobrenome || !email || !dataNascimento || !senha || !repetirSenha) {
        Swal.fire("Erro", "Todos os campos são obrigatórios.", "error");
        return;
    }

    if (senha !== repetirSenha) {
        Swal.fire("Erro", "As senhas não coincidem.", "error");
        return;
    }

    // Monta FormData
    const formData = new FormData();
    formData.append("Nome", nome);
    formData.append("Sobrenome", sobrenome);
    formData.append("Email", email);
    formData.append("DataNascimento", dataNascimento);
    formData.append("Senha", senha);

    // AJAX para backend
    $.ajax({
        url: '/Cadastrar/CadastrarUsuario',
        type: 'POST',
        data: formData,
        processData: false,
        contentType: false,
        success: function (response) {
            Swal.fire("Sucesso", response.mensagem || "Cadastro realizado com sucesso!", "success")
                .then(() => window.location.href = '/Login');
        },
        error: function (xhr) {
            const msg = xhr.responseJSON?.mensagem || "Erro ao cadastrar usuário.";
            Swal.fire("Erro", msg, "error");
        }
    });
}