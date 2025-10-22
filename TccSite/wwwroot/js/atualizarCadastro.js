$(document).ready(function () {
    inicializarTela();
});

function inicializarTela() {
    BuscarImagemUsuario();
    ObterEstados();

    $("#Estados").on("change", function () {
        const codEstado = $(this).val();
        ObterCidades(codEstado);
    });

    $("#btnAtualizar").on("click", function () {
        AtualizarCadastro();
    });

    document.getElementById('ImagemPerfil')?.addEventListener('change', function (event) {
        const file = event.target.files[0];
        if (!file) return;
        const reader = new FileReader();
        reader.onload = function (e) {
            document.getElementById('fotoUsuario').src = e.target.result;
        };
        reader.readAsDataURL(file);
    });
}

/* ==============================
   Buscar Imagem Usuário
============================== */
function BuscarImagemUsuario() {
    $.ajax({
        url: '/AtualizarCadastro/BuscarImagemUsuario',
        type: 'GET',
        success: function (data) {
            if (data?.imagemUrl) {
                $("#fotoUsuario").attr("src", data.imagemUrl);
            }
        },
        error: function () {
            console.warn("Falha ao buscar imagem do usuário.");
        }
    });
}

/* ==============================
   Obter Estados
============================== */
function ObterEstados() {
    $.ajax({
        url: '/AtualizarCadastro/ObterEstados',
        type: 'GET',
        success: function (estados) {
            const select = $("#Estados");
            select.empty().append('<option value="">Selecione</option>');
            estados.forEach(e => {
                select.append(`<option value="${e.codEstado}">${e.nomeEstado}</option>`);
            });

            // Seleciona o estado atual do usuário
            const codEstado = select.data("codestado");
            if (codEstado) {
                select.val(codEstado);
                ObterCidades(codEstado);
            }
        },
        error: function () {
            Swal.fire("Erro", "Não foi possível carregar os estados.", "error");
        }
    });
}

/* ==============================
   Obter Cidades
============================== */
function ObterCidades(codEstado) {
    if (!codEstado) return;

    $.ajax({
        url: `/AtualizarCadastro/ObterCidades?codEstado=${codEstado}`,
        type: 'GET',
        success: function (cidades) {
            const select = $("#Cidades");
            select.empty().append('<option value="">Selecione</option>');
            cidades.forEach(c => {
                select.append(`<option value="${c.codCidade}">${c.nomeCidade}</option>`);
            });

            // Seleciona a cidade atual do usuário
            const codCidade = select.data("codcidade");
            if (codCidade) {
                select.val(codCidade);
            }
        },
        error: function () {
            Swal.fire("Erro", "Não foi possível carregar as cidades.", "error");
        }
    });
}

/* ==============================
   Atualizar Cadastro
============================== */
function AtualizarCadastro() {
    const formData = new FormData();
    formData.append("CodUsuario", $("#CodUsuario").val());
    formData.append("CodPessoaCadastro", $("#CodPessoaCadastro").val());
    formData.append("Nome", $("#Nome").val());
    formData.append("Sobrenome", $("#Sobrenome").val());
    formData.append("DataNascimento", $("#DataNascimento").val());
    formData.append("Telefone", $("#Telefone").val());
    formData.append("CPF", $("#CPF").val());
    formData.append("Email", $("#Email").val());
    formData.append("Endereco", $("#Endereco").val());
    formData.append("CEP", $("#CEP").val());
    formData.append("CodCidade", $("#Cidades").val());
    formData.append("CodEstadoSelecionado", $("#Estados").val());

    const imagem = $("#ImagemPerfil")[0].files[0];
    if (imagem) formData.append("ImagemPerfil", imagem);

    $.ajax({
        url: '/AtualizarCadastro/AtualizarUsuario',
        type: 'POST',
        data: formData,
        processData: false,
        contentType: false,
        success: function (response) {
            Swal.fire("Sucesso!", "Cadastro atualizado com sucesso!", "success");
        },
        error: function () {
            Swal.fire("Erro", "Falha ao atualizar o cadastro.", "error");
        }
    });
}