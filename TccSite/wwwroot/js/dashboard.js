// =====================================
// CARREGAMENTO AUTOMÁTICO
// =====================================
$(document).ready(function () {

    carregarGraficoNivelRio();

    atualizarHora();
    obterClima();

    setInterval(atualizarHora, 1000);
    setInterval(obterClima, 15 * 60 * 1000);
});


// =====================================
// FUNÇÃO: PEGAR DATA/HORA BRASILEIRA
// =====================================
function dataBrasil() {
    return new Date(
        new Date().toLocaleString("en-US", { timeZone: "America/Sao_Paulo" })
    );
}

function toISO(date) {
    return new Date(date.getTime() - date.getTimezoneOffset() * 60000).toISOString();
}


// =====================================
// FUNÇÃO: PARSEAR DATA COMO LOCAL
// Impede que o JS converta para UTC
// =====================================
function parseLocalDate(dateString) {
    const utcDate = new Date(dateString);
    const localDate = new Date(utcDate.getTime() - utcDate.getTimezoneOffset() * 60000);
    const newDataCadastroAlerta = localDate.getTime();

    return newDataCadastroAlerta;
}


// =====================================
// DOWN-SAMPLING PARA MUITOS REGISTROS
// =====================================
function reduzirPontos(lista, max = 500) {

    if (lista.length <= max) return lista;

    const fator = Math.ceil(lista.length / max);
    const reduzido = [];

    for (let i = 0; i < lista.length; i += fator) {
        reduzido.push(lista[i]);
    }

    console.log("🔻 Dados reduzidos de", lista.length, "para", reduzido.length);
    return reduzido;
}


// =====================================
// CARREGAR GRÁFICO DO NÍVEL DO RIO
// =====================================
function carregarGraficoNivelRio() {

    const agoraBR = dataBrasil();

    const dataInicio = new Date(agoraBR.getTime() - 2 * 60 * 60 * 1000);
    const dataFim = new Date(agoraBR.getTime() + 1 * 60 * 60 * 1000);

    $.ajax({
        url: "/Dashboard/Alerta",
        method: "GET",
        data: {
            dataInicio: toISO(dataInicio),
            dataFim: toISO(dataFim)
        },
        dataType: "json",

        success: function (retorno) {

            if (!retorno.success) {
                Swal.fire("Erro", "Falha ao carregar o gráfico!", "error");
                return;
            }

            const dados = retorno.data;

            if (!Array.isArray(dados) || dados.length === 0) {
                Swal.fire("Sem Dados", "Nenhum registro encontrado.", "info");
                return;
            }

            console.log("Dados recebidos:", dados.length);

            // Monta série com PARSE LOCAL para não perder horário!
            let serie = dados.map(item => [
                parseLocalDate(item.dataCadastroAlerta),
                Number(item.nivelRio ?? 0)
            ]);

            // Redução automática
            serie = reduzirPontos(serie);

            // Exibe o card
            document.getElementById("graficoNivelRio").style.display = "block";

            Highcharts.chart("waveChart", {
                chart: { type: "areaspline" },
                title: { text: "Nível do Rio" },
                xAxis: {
                    type: "datetime",
                    title: { text: "Data/Hora" }
                },
                yAxis: {
                    title: { text: "Nível (m)" }
                },
                tooltip: {
                    shared: true,
                    xDateFormat: "%d/%m/%Y %H:%M"
                },
                series: [{
                    name: "Nível do Rio",
                    data: serie,
                    color: "#0077b6"
                }]
            });
        },

        error: function () {
            Swal.fire("Erro", "Erro ao consultar o gráfico!", "error");
        }
    });
}


// =====================================
// CLIMA + DATA/HORA
// =====================================
function atualizarHora() {
    const agora = dataBrasil();
    $("#dataHoraLocal").text(
        agora.toLocaleString("pt-BR", { hour12: false })
    );
}

function obterClima() {
    const cidade = "São Paulo";
    const url = `https://wttr.in/${cidade}?format=j1`;

    $.getJSON(url, function (data) {
        if (!data || !data.current_condition) {
            $("#climaAtual").text("Clima indisponível");
            return;
        }

        const clima = data.current_condition[0];
        const temp = clima.temp_C;
        const descricao = clima.lang_pt ? clima.lang_pt[0].value : clima.weatherDesc[0].value;

        const horaAtual = dataBrasil().getHours();
        const ehNoite = horaAtual >= 18 || horaAtual < 6;

        let icone = "bi-cloud";
        const desc = descricao.toLowerCase();

        if (desc.includes("sol")) icone = ehNoite ? "bi-moon-stars" : "bi-brightness-high";
        else if (desc.includes("chuva")) icone = "bi-cloud-rain";
        else if (desc.includes("nublado")) icone = "bi-clouds";
        else if (desc.includes("neblina")) icone = "bi-cloud-fog2";
        else if (desc.includes("tempestade")) icone = "bi-cloud-lightning-rain";
        else if (desc.includes("neve")) icone = "bi-snow";

        $("#climaAtual").html(`${temp}°C - ${descricao}`);
        $("#iconeClima").html(`<i class="bi ${icone} fs-3 pulsando"></i>`);
    }).fail(function () {
        $("#climaAtual").text("Clima indisponível");
        $("#iconeClima").html('<i class="bi bi-cloud-slash fs-4"></i>');
    });
}
