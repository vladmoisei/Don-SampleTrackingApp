$(document).ready(function () {
    console.log("Ruleaza Java Script");

    // Creare element lista selectie afisare date
    let selectieAfisareDateElement = document.getElementById("selectieAfisareDate");

    /* Adaugare Event la elelement selectie afisare date in functie de selectie
     * Toate
     * Cele introduse de operator
     * Cele la care operatorul calitate a prelevat proba
     * Cele la care operatorul calitate a dat raspuns
     * Cele cu rezultat negativ (RNC)
     */
    selectieAfisareDateElement.addEventListener('change', function () {
        alert("S-a selectat alta valoare");
        $.ajax({
            url: " /ProbaModels/_Index",
            type: 'POST',
            data: {
                selectieAfisareDate: this.value
            },
            success: function (response) {
                console.log("S-a realizat event selectie date");
                $(".table").html(response);
            },
            error: function (response) {
                console.log("Eroare JS la event selectie date");
            }

        });
    });
     
});