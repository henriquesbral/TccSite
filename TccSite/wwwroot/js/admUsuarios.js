// ======================================================
// admUsuarios.js
// ======================================================

$(document).ready(function () {

    // 🔍 Busca em tempo real
    $("#searchUser").on("keyup", function () {
        const value = $(this).val().toLowerCase();
        $("#usuariosTable tbody tr").filter(function () {
            $(this).toggle($(this).text().toLowerCase().indexOf(value) > -1);
        });
    });

    // ✏️ Preenche modal de edição
    $(".edit-btn").on("click", function () {
        const modal = $("#editUsuarioModal");
        modal.find("input[name='CodUsuario']").val($(this).data("id"));
        modal.find("input[name='Nome']").val($(this).data("nome"));
        modal.find("input[name='Email']").val($(this).data("email"));
        modal.find("select[name='PerfilUsuario']").val($(this).data("perfil"));
    });

    // 💾 AJAX — Editar Usuário
    $(".editUsuarioForm").on("submit", function (e) {
        e.preventDefault();
        const formData = $(this).serialize();

        $.ajax({
            url: "/Usuario/EditarUsuario",
            type: "POST",
            data: formData,
            success: function (response) {
                Swal.fire({
                    icon: "success",
                    title: "Usuário atualizado!",
                    text: "As informações foram salvas com sucesso.",
                    timer: 1800,
                    showConfirmButton: false
                }).then(() => location.reload());
            },
            error: function () {
                Swal.fire("Erro", "Falha ao atualizar usuário.", "error");
            }
        });
    });

    // ➕ Modal Novo Usuário — limpa campos
    $("#createUsuarioModal").on("show.bs.modal", function () {
        $(this).find("input, select").val("");
    });

    // ➕ AJAX — Criar Usuário
    $("#createUsuarioForm").on("submit", function (e) {
        e.preventDefault();
        const formData = $(this).serialize();

        $.ajax({
            url: "/Usuario/Criar",
            type: "POST",
            data: formData,
            success: function (response) {
                if (response.success) {
                    Swal.fire({
                        icon: "success",
                        title: "Usuário criado!",
                        text: response.message,
                        timer: 1800,
                        showConfirmButton: false
                    }).then(() => location.reload());
                } else {
                    Swal.fire("Aviso", response.message || "Falha ao criar usuário.", "warning");
                }
            },
            error: function () {
                Swal.fire("Erro", "Erro inesperado ao criar usuário.", "error");
            }
        });
    });

    // 🗑️ Abre modal de exclusão
    $(".btn-delete").on("click", function () {
        const id = $(this).data("id");
        const nome = $(this).data("nome");
        $("#usuarioIdExcluir").val(id);
        $("#usuarioNomeExcluir").text(nome);
    });

    // 🚮 AJAX — Excluir Usuário
    $("#confirmDeleteBtn").on("click", function () {
        const id = $("#usuarioIdExcluir").val();

        $.ajax({
            url: "/Usuario/Delete",
            type: "POST",
            data: { codUsuario: id },
            success: function () {
                Swal.fire({
                    icon: "success",
                    title: "Usuário removido!",
                    text: "O usuário foi excluído com sucesso.",
                    timer: 1800,
                    showConfirmButton: false
                }).then(() => location.reload());
            },
            error: function () {
                Swal.fire("Erro", "Falha ao excluir usuário.", "error");
            }
        });
    });
});
