$(document).ready(function () {
    console.log("Ruleaza Java Script");


    // Creare element lista selectie afisare date
    let selectieAfisareDateElement = document.getElementById("selectieAfisareDate");
    // Creare element DataTo
    let dataToElement = document.getElementById("to");
    // Element DataFrom
    let dataFromElement = document.getElementById("from");
    // Verificare daca este small screen
    let isSmallScren = (screen.width <= 700) ? "True" : "False";

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
                dataTo: dataToElement.value,
                ecranMic: isSmallScren
            },
            success: function (response) {
                console.log("S-a realizat event selectie date");
                $(".table").html(response);

                // Adaugam eveniment la checkBox 
                // Adaugare Dinamic Event la CheckBox
                // Call functie ajax pentru a seta data in momentul cand se bifeaza checkbox
                const checkboxes = document.querySelectorAll('input[type=checkbox]');
                for (var i = 0; i < checkboxes.length; i++) {
                    checkboxes[i].addEventListener('change', function () {
                        console.log("Id check box: " + this.value);
                        $.ajax({
                            url: " /ProbaModels/CheckBoxDataPreluareProbaEvent",
                            type: 'GET',
                            data: {
                                probaId: this.value
                            },
                            success: function (response) {
                                //console.log(" RAspuns: ");
                                //console.log("Id: " + response.id);
                                //console.log("Data PReluare: " + response.data);
                                //$(".table").html(response);
                                chengeCheckBoxWithData(response.id, response.data);
                            },
                            error: function (response) {
                                console.log("Raspuns din erorr: " + response);
                                console.log("Eroare JS la event chang checkBox");
                            }

                        });
                    });
                }
            },
            error: function (response) {
                console.log("Eroare JS la event selectie date");
            }

        });
    });

    // Adaugare Dinamic Event la CheckBox
    // Call functie ajax pentru a seta data in momentul cand se bifeaza checkbox
    const checkboxes = document.querySelectorAll('input[type=checkbox]'); 
    for (var i = 0; i < checkboxes.length; i++) {
        checkboxes[i].addEventListener('change', function () {
            console.log("Id check box: " + this.value);
            $.ajax({
                url: " /ProbaModels/CheckBoxDataPreluareProbaEvent",
                type: 'GET',
                data: {
                    probaId: this.value
                },
                success: function (response) {
                    //console.log(" RAspuns: ");
                    //console.log("Id: " + response.id);
                    //console.log("Data PReluare: " + response.data);
                    //console.log(checkWithValue(response.id));
                    //$(".table").html(response);
                    chengeCheckBoxWithData(response.id, response.data);
                },
                error: function (response) {
                    console.log("Raspuns din erorr: " + response);
                    console.log("Eroare JS la event chang checkBox");
                }

            });
        });
    }

    // Functie Get Element CheckBox casuta bifata
    function checkWithValue(val) {
        const checkboxes = document.querySelectorAll('input[type=checkbox]');
        for (var i = 0; i < checkboxes.length; i++) {
            if (checkboxes[i].value == val) return checkboxes[i]; 
        }
    }

    // Functie Get Parent Element checkBox pentru a inlocui cu text
    function chengeCheckBoxWithData(id, data) {
        let parinteCheckBox = checkWithValue(id).parentElement;
        parinteCheckBox.innerHTML = data;
    }

    
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