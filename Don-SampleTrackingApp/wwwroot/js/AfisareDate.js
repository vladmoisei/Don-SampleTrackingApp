$(document).ready(function () {
    console.log("Ruleaza Java Script");

    // Creare element lista selectie afisare date
    let selectieAfisareDateElement = document.getElementById("selectieAfisareDate");
    // Creare element DataTo
    let dataToElement = document.getElementById("to");
    // Element DataFrom
    let dataFromElement = document.getElementById("from");


    /* Adaugare Event la elelement selectie afisare date in functie de selectie
     * Toate
     * Cele introduse de operator
     * Cele la care operatorul calitate a prelevat proba
     * Cele la care operatorul calitate a dat raspuns
     * Cele cu rezultat negativ (RNC)
     */
    selectieAfisareDateElement.addEventListener('change', function () {
        //alert("S-a selectat alta valoare");
        $.ajax({
            url: " /ProbaModels/_Index",
            type: 'POST',
            data: {
                selectieAfisareDate: this.value,
                dataFrom: dataFromElement.value,
                dataTo: dataToElement.value
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

    //// Adaugare Event la Element DataTo, cand se schimba data se afiseaza doar in data selectata
    //dataToElement.addEventListener('change', function () {
    //    alert("S-a schimbat data To. EventListener");
    //    $.ajax({
    //        url: " /ProbaModels/_Index",
    //        type: 'POST',
    //        data: {
    //            selectieAfisareDate: this.value,
    //            dataFrom: dataFromElement.value,
    //            dataTo: dataToElement.value
    //        },
    //        success: function (response) {
    //            console.log("S-a realizat event selectie date");
    //            $(".table").html(response);
    //        },
    //        error: function (response) {
    //            console.log("Eroare JS la event selectie date");
    //        }

    //    });
    //});
     
});