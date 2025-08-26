$(document).ready(function () {
    // Ao enviar o formulário
    $('.filtro_alertas form').on('submit', function (e) {
        e.preventDefault();
        carregarRelatorio();
    });

    // Resetar tabela
    $('.filtro_alertas form').on('reset', function () {
        $('table tbody').empty();
    });
});

function carregarRelatorio() {
    const dataInicio = $('#DataInicio').val();
    const dataFim = $('#DataFim').val();
    const tipoAlerta = $('#TipoAlerta').val();

    $.ajax({
        url: '/PainelAlertas/BuscarRelatorio',
        method: 'GET',
        dataType: 'json',
        data: {
            dataInicio: dataInicio,
            dataFim: dataFim,
            tipoAlerta: tipoAlerta
        },
        success: function (dados) {
            const tbody = $('table tbody');
            tbody.empty();

            if (dados.length === 0) {
                tbody.append('<tr><td colspan="4" class="text-center">Nenhum registro encontrado</td></tr>');
                return;
            }

            dados.forEach(item => {
                const statusTexto = mapStatus(item.codStatusAlerta);
                const dataFormatada = new Date(item.dataCadastro).toLocaleString('pt-BR');
                tbody.append(`
                    <tr>
                        <td><button class="btn btn-sm btn-primary" onclick="detalhesAlerta(${item.codAlerta})">Detalhes</button></td>
                        <td>${statusTexto}</td>
                        <td>${item.nomeAlerta}</td>
                        <td>${dataFormatada}</td>
                    </tr>
                `);
            });
        },
        error: function (xhr, status, error) {
            console.error('Erro ao buscar relatório:', error);
            Swal.fire("Erro ao buscar relatório!");
        }
    });
}

function mapStatus(codStatus) {
    switch (codStatus) {
        case 1: return 'Baixo';
        case 2: return 'Médio';
        case 3: return 'Alto';
        case 4: return 'Crítico';
        default: return 'Desconhecido';
    }
}

function detalhesAlerta(codAlerta) {
    Swal.fire(`Visualizar detalhes do alerta: ${codAlerta}`);
}