$(document).ready(function () {
    carregarGraficoAlertas();
});

function carregarGraficoAlertas() {
    $.ajax({
        url: '/Dashboard/Alerta',
        method: 'GET',
        dataType: 'json',
        success: function (dados) {
            console.log("Dados recebidos:", dados); // para debug

            // Inicializa séries vazias para cada tipo de alerta
            const series = {
                Baixo: [],
                Medio: [],
                Alto: [],
                Critico: []
            };

            dados.forEach(item => {
                const dataCadastro = new Date(item.dataCadastro).getTime();
                const valor = item.nivelRio ?? 1; // usa 1 se não tiver valor

                // Preenche a série correta com base no StatusAlerta
                switch (item.statusAlerta) {
                    case "Baixo":
                        series.Baixo.push([dataCadastro, valor]);
                        break;
                    case "Medio":
                        series.Medio.push([dataCadastro, valor]);
                        break;
                    case "Alto":
                        series.Alto.push([dataCadastro, valor]);
                        break;
                    case "Critico":
                        series.Critico.push([dataCadastro, valor]);
                        break;
                }
            });

            // Cria o gráfico Highcharts
            Highcharts.chart('container', {
                chart: { type: 'area' },
                title: { text: 'Evolução do nível do rio por alerta' },
                xAxis: {
                    type: 'datetime',
                    title: { text: 'Data/Hora' }
                },
                yAxis: {
                    title: { text: 'Nível do rio' },
                    allowDecimals: false
                },
                tooltip: {
                    xDateFormat: '%d/%m/%Y %H:%M',
                    shared: true
                },
                plotOptions: {
                    area: {
                        marker: {
                            enabled: false,
                            symbol: 'circle',
                            radius: 2
                        }
                    }
                },
                series: [
                    { name: 'Baixo', data: series.Baixo, color: '#28a745' },
                    { name: 'Médio', data: series.Medio, color: '#ffc107' },
                    { name: 'Alto', data: series.Alto, color: '#dc3545' },
                    { name: 'Crítico', data: series.Critico, color: '#343a40' }
                ]
            });
        },
        error: function (xhr, status, error) {
            console.error("Erro ao buscar dados:", error);
            Swal.fire("Erro ao carregar os dados do gráfico!");
        }
    });
}

function detalhesAlertas() {
    Swal.fire("Card de visualização em andamento");
}
