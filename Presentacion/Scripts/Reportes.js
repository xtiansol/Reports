function mpeSeleccionOnOk() {
    var lista1 = document.getElementById("CamposSeleccionados");
    var listaF = document.getElementById("CamposSeleccionadosFin");
    var contF = 0;
    for (cont = 0; cont < lista1.options.length; cont++) {
        var campoHD = lista1.options[cont].text;
        if (document.getElementById("hdf" + campoHD) != null) {
            var campoFin = "";
            var combo = document.getElementById("cmb" + campoHD);
            var x = combo.selectedIndex;
            var y = combo.options;
            var comboText = x > 0 ? y[x].text : "";
            var text = document.getElementById("txt" + campoHD).value;
            campoFin = campoHD + " " + comboText + " " + text;
            var no = new Option();
            no.value = campoFin;
            no.text = campoFin;
            listaF[contF] = no;
            contF++;
        }


    }

}
function mpeSeleccionOnCancel() {
    //var txtSituacion = document.getElementById("txtSituacion");
    //txtSituacion.value = "";
    //txtSituacion.style.backgroundColor = "#FFFF99";
}

//Consecutivo de campos a agregar
campoNuevo = 0;
//Funcion de agregar campos
function AgregarCampos() {
    campoNuevo = campoNuevo + 1;
    campo = '<li><label>Campo ' + campoNuevo + ':</label><input type="text" size="20" name="campo' + campoNuevo + '"  /></li>';
    $("#campos").append(campo);
}
//Pasar los valores txt por QueryString
function EnviarDatos() {
    var query = "";
    $('input[type=text][name!=""]').each(function (index, domEle) {
        //alert(index + ': ' + $(domEle).val());
        query = query + "&campo" + index + "=" + $(domEle).val();
    });
    window.location.href = "/CamposAdicionales.aspx?" + query;
}