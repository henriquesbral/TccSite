let monitorando = false;
let intervalo = null;

document.addEventListener("DOMContentLoaded", () => {
    const btn = document.getElementById("btnMonitorar");
    const status = document.getElementById("status");

    btn.addEventListener("click", () => {
        monitorando = !monitorando;

        if (monitorando) {
            btn.textContent = "Parar Monitoramento";
            status.textContent = "Monitoramento em andamento...";
            atualizarImagem(); // executa agora
            const tempo = parseInt(document.getElementById("tempoAtualizacao").value);
            intervalo = setInterval(atualizarImagem, tempo);
        } else {
            btn.textContent = "Iniciar Monitoramento";
            status.textContent = "Monitoramento parado";
            clearInterval(intervalo);
        }
    });
});

function atualizarImagem() {
    $.ajax({
        url: '/PainelControle/UltimaImagem',
        type: 'GET',
        success: function (res) {
            if (res.sucesso) {
                const img = document.getElementById("ultimaImagem");
                img.src = res.imagem.urlImagem + "?t=" + new Date().getTime();

                const label = document.getElementById("ultimaAtualizacao");
                label.textContent = "Última atualização: " + new Date(res.imagem.dataCadastro).toLocaleString("pt-BR");
            } else {
                console.warn(res.mensagem);
            }
        },
        error: function (err) {
            console.error("Erro ao atualizar imagem", err);
        }
    });
}