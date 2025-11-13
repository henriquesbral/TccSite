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
                const utcDate = new Date(item.dataCadastro);
                const localDate = new Date(utcDate.getTime() - utcDate.getTimezoneOffset() * 60000);
                const dataCadastro = localDate.getTime();
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

$(document).ready(function () {
    atualizarHora();
    obterClima();

    setInterval(atualizarHora, 1000);        // Atualiza a hora a cada segundo
    setInterval(obterClima, 15 * 60 * 1000); // Atualiza o clima a cada 15 minutos
});

function atualizarHora() {
    const agora = new Date();
    const horaFormatada = agora.toLocaleString("pt-BR", { hour12: false });
    $("#dataHoraLocal").text(horaFormatada);
}

function obterClima() {
    const cidade = "São Paulo"; // 🌎 Altere para sua cidade
    const url = `https://wttr.in/${cidade}?format=j1`;

    $.getJSON(url, function (data) {
        if (!data || !data.current_condition) {
            $("#climaAtual").text("Clima indisponível");
            return;
        }

        const clima = data.current_condition[0];
        const temp = clima.temp_C;
        const descricao = clima.lang_pt ? clima.lang_pt[0].value : clima.weatherDesc[0].value;
        const weatherCode = clima.weatherCode;

        const horaAtual = new Date().getHours();
        const ehNoite = horaAtual >= 18 || horaAtual < 6;

        // 🌦️ Mapeia ícones Bootstrap baseados no código do clima
        let icone = "bi-cloud";
        if (descricao.toLowerCase().includes("sol")) icone = ehNoite ? "bi-moon-stars" : "bi-brightness-high";
        else if (descricao.toLowerCase().includes("chuva")) icone = "bi-cloud-rain";
        else if (descricao.toLowerCase().includes("nublado")) icone = "bi-clouds";
        else if (descricao.toLowerCase().includes("neblina")) icone = "bi-cloud-fog2";
        else if (descricao.toLowerCase().includes("tempestade")) icone = "bi-cloud-lightning-rain";
        else if (descricao.toLowerCase().includes("neve")) icone = "bi-snow";

        // Atualiza painel
        $("#climaAtual").html(`${temp}°C - ${descricao}`);
        $("#iconeClima").html(`<i class="bi ${icone} fs-3 pulsando"></i>`);
    }).fail(function () {
        $("#climaAtual").text("Clima indisponível");
        $("#iconeClima").html('<i class="bi bi-cloud-slash fs-4"></i>');
    });
}