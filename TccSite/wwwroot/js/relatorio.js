document.addEventListener("DOMContentLoaded", function () {
    var tipoRelatorio = document.getElementById("TipoRelatorio");
    var filtrosDatas = document.getElementById("filtrosDatas");

    if (!tipoRelatorio || !filtrosDatas) return;

    // Esconde inicialmente
    filtrosDatas.style.display = "none";

    // Ao selecionar tipo de relatório
    tipoRelatorio.addEventListener("change", function () {
        if (tipoRelatorio.value) {
            filtrosDatas.style.display = "block";
        } else {
            filtrosDatas.style.display = "none";
        }
    });
});
