document.addEventListener("DOMContentLoaded", function () {
    const tipoSelect = document.getElementById("TipoRelatorio");
    const filtros = document.getElementById("filtrosDatas");
    const divTipoAlerta = document.getElementById("divTipoAlerta");
    const tabelaContainer = document.getElementById("tabelaAlertasContainer");
    const graficoAlertasContainer = document.getElementById("graficoAlertasContainer");
    const graficoNivelContainer = document.getElementById("graficoNivelRio");

    tipoSelect.addEventListener("change", function () {
        filtros.style.display = tipoSelect.value ? "block" : "none";
        divTipoAlerta.style.display = tipoSelect.value === "alerta" ? "block" : "none";
        tabelaContainer.style.display = tipoSelect.value === "alerta" ? "block" : "none";
        graficoAlertasContainer.style.display = tipoSelect.value === "alerta" ? "block" : "none";
        graficoNivelContainer.style.display = tipoSelect.value === "nivel" ? "block" : "none";
    });

    document.getElementById("formFiltro").addEventListener("submit", function (e) {
        e.preventDefault();
        carregarRelatorio();
    });
});

function carregarRelatorio() {
    const inicio = document.getElementById("DataInicio").value;
    const fim = document.getElementById("DataFim").value;
    const tipo = document.getElementById("TipoRelatorio").value;
    const tipoAlerta = document.getElementById("TipoAlerta")?.value || 0;

    let url = "";
    if (tipo === "alerta") {
        url = `/Relatorios/BuscarRelatorioAlertas?dataInicio=${inicio}&dataFim=${fim}&tipoAlerta=${tipoAlerta}`;
    } else if (tipo === "nivel") {
        url = `/Relatorios/BuscarNivelRio?dataInicio=${inicio}&dataFim=${fim}`;
    } else {
        console.error("Tipo de relatório inválido");
        return;
    }

    fetch(url)
        .then(resp => resp.json())
        .then(res => {
            if (!res.success || !Array.isArray(res.data)) {
                console.error("Retorno inválido:", res);
                return;
            }

            if (tipo === "alerta") {
                preencherTabela(res.data);
                renderizarGraficoAlertas(res.data);
            } else if (tipo === "nivel") {
                renderizarGraficoNivelRio(res.data);
            }
        })
        .catch(err => console.error("Erro ao buscar dados:", err));
}

function preencherTabela(data) {
    const tbody = document.getElementById("tabelaAlertas");
    tbody.innerHTML = "";
    data.forEach(item => {
        const tr = document.createElement("tr");
        tr.innerHTML = `
            <td>${item.descricao ?? ""}</td>
            <td>${item.nomeAlerta}</td>
            <td>${item.statusAlerta}</td>
            <td>${new Date(item.data).toLocaleString()}</td>
        `;
        tbody.appendChild(tr);
    });
}

function renderizarGraficoAlertas(data) {
    const contagem = { Baixo: 0, Médio: 0, Alto: 0, Crítico: 0 };
    data.forEach(a => {
        switch (a.statusAlerta) {
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
        series: [{ name: 'Alertas', data: Object.values(contagem) }]
    });
}

function renderizarGraficoNivelRio(data) {
    const container = document.getElementById("waveChart");
    container.style.display = "block";

    Highcharts.chart('waveChart', {
        chart: { type: 'areaspline' },
        title: { text: 'Nível do Rio' },
        xAxis: {
            type: 'datetime',
            labels: { format: '{value:%d/%m/%Y %H:%M}' }
        },
        yAxis: { title: { text: 'Nível (m)' } },
        series: [{
            name: 'Nível do Rio',
            data: data.map(d => [new Date(d.dataCadastroAlerta).getTime(), d.nivelRio ?? 0])
        }]
    });
}
