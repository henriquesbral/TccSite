document.addEventListener("DOMContentLoaded", function () {
    // Pré-carregar últimos alertas e gráfico ao abrir a página
    carregarUltimosAlertas();

    // Escuta o submit do formulário para filtrar dados
    document.getElementById("formRelatorio").addEventListener("submit", function (e) {
        e.preventDefault();
        carregarDadosFiltrados();
    });
});

// -----------------------
// Pré-carga ao abrir a página
function carregarUltimosAlertas() {
    fetch(`/PainelAlertas/BuscarRelatorioAlerta?tipoAlerta=0&tipoRelatorio=1`)
        .then(resp => resp.json())
        .then(data => {
            if (!data.success || !Array.isArray(data.data)) {
                console.error("Retorno inválido:", data);
                return;
            }
            const alertas = data.data;
            if (alertas.length === 0) return;

            atualizarUltimosAlertas(alertas);
            renderizarGrafico(alertas);
        })
        .catch(err => console.error("Erro ao carregar últimos alertas:", err));
}

// -----------------------
// Carrega dados filtrados ao clicar no botão "Filtrar"
function carregarDadosFiltrados() {
    const inicio = document.getElementById("DataInicio").value;
    const fim = document.getElementById("DataFim").value;
    const tipoAlerta = document.getElementById("TipoAlerta").value;
    const tipoRelatorio = document.getElementById("TipoRelatorio").value;

    fetch(`/PainelAlertas/BuscarRelatorioAlerta?dataInicio=${inicio}&dataFim=${fim}&tipoAlerta=${tipoAlerta}&tipoRelatorio=${tipoRelatorio}`)
        .then(resp => resp.json())
        .then(data => {
            if (!data.success || !Array.isArray(data.data)) {
                console.error("Retorno inválido:", data);
                limparPainel();
                return;
            }

            const alertas = data.data;

            if (alertas.length === 0) {
                limparPainel();
                return;
            }

            atualizarUltimosAlertas(alertas);
            preencherTabela(alertas);
            renderizarGrafico(alertas);
        })
        .catch(err => console.error("Erro ao buscar dados filtrados:", err));
}

// -----------------------
// Atualiza cards com últimos alertas
function atualizarUltimosAlertas(data) {
    const niveis = { 1: 'baixo', 2: 'medio', 3: 'alto', 4: 'critico' };
    const ultimoPorNivel = {};

    data.forEach(a => {
        const nivel = niveis[a.codStatusAlerta];
        if (!ultimoPorNivel[nivel] || new Date(a.dataCadastroAlerta) > new Date(ultimoPorNivel[nivel].dataCadastroAlerta)) {
            ultimoPorNivel[nivel] = a;
        }
    });

    for (const nivel in niveis) {
        const el = document.getElementById(`alerta-${niveis[nivel]}`);
        el.innerText = ultimoPorNivel[niveis[nivel]] ? ultimoPorNivel[niveis[nivel]].nomeAlerta : "--";
    }
}

// -----------------------
// Preenche tabela com os dados filtrados
function preencherTabela(data) {
    const tbody = document.getElementById("tabelaAlertas");
    tbody.innerHTML = "";
    data.forEach(item => {
        const tr = document.createElement("tr");
        tr.innerHTML = `
                <td>${item.descricao ?? ""}</td>
                <td>${item.nomeAlerta}</td>
                <td>${item.nomeStatusAlerta}</td>
                <td>${new Date(item.dataCadastroAlerta).toLocaleString()}</td>
            `;
        tbody.appendChild(tr);
    });
}

// -----------------------
// Renderiza gráfico
function renderizarGrafico(data) {
    const contagem = { Baixo: 0, Médio: 0, Alto: 0, Crítico: 0 };

    data.forEach(a => {
        switch (a.nomeStatusAlerta) {
            case "Baixo": contagem.Baixo++; break;
            case "Médio": contagem.Médio++; break;
            case "Alto": contagem.Alto++; break;
            case "Crítico": contagem.Crítico++; break;
        }
    });

    Highcharts.chart('graficoAlertas', {
        chart: { type: 'column' },
        title: { text: 'Distribuição de Alertas' },
        xAxis: { categories: Object.keys(contagem) },
        yAxis: { title: { text: 'Quantidade' } },
        series: [{
            name: 'Alertas',
            data: Object.values(contagem)
        }]
    });
}

// -----------------------
// Limpa painel (cards, tabela e gráfico)
function limparPainel() {
    ['baixo', 'medio', 'alto', 'critico'].forEach(nivel => {
        document.getElementById(`alerta-${nivel}`).innerText = "--";
    });
    document.getElementById("tabelaAlertas").innerHTML = "";
    Highcharts.chart('graficoAlertas', {
        chart: { type: 'column' },
        title: { text: 'Nenhum dado carregado' },
        xAxis: { categories: [] },
        yAxis: { title: { text: 'Quantidade' } },
        series: []
    });
}
