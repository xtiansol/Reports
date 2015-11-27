var gPrefijo = "MainContent_";
var arrTabSel = [];
var arrAliasTabSel = [];

var arrTabSelCampos = [];
var arrAliasTabSelCampos = [];
var arrCamposTabSelCampos = [];

var tablaSelActual = "";
var aliasTablaSelActual = "";

$body = $("body");

$(document).on({
    ajaxStart: function () { $body.addClass("loading"); },
    ajaxStop: function () { $body.removeClass("loading"); }
});

function ShowFiltrosModal() {
    var cont = $("#" + gPrefijo + "CamposSeleccionados option").length;
    if (cont > 0) {
        $("#contenedorFiltro").empty();
        $("#contenedorFiltro").load("/Reportes/frmFiltros.aspx");
        $find('mpeSeleccion').show();
    } else {
        alert("Seleccione campos al reporte");
    }
}

function ShowReportePrevModal() {
    $("#" + gPrefijo + "idNombreReporte").val("");
    var cont = $("#" + gPrefijo + "CamposSeleccionados option").length;
    if (cont > 0) {
        $("#contenedorFiltro").empty(); //Vacia el contenedor de filtros para que no truene el llamado al generar reporte
        $("#IframeEdit2").empty();
        var frame = $get('IframeEdit');
        frame.src = "frmReportePrev.aspx";
        $find('ReportePrevModalPopup').show();
    } else {
        alert("Seleccione campos al reporte");
    }
}

function ShowHistoricoModal() {
    $("#contenedorFiltro").empty(); //Vacia el contenedor de filtros para que no truene el llamado al generar reporte
    var frame = $get('IframeEdit2');
    frame.src = "frmHistorico.aspx";
    $find('mpeSeleccionHistorial').show();
}

function mpeSeleccionOnOk() {

    var lista1 = $("#" + gPrefijo + "CamposSeleccionados");
    var listaF = document.getElementById(gPrefijo + "CamposSeleccionadosFin");
    var filtroFinVar = "";
    var campoNom = "";
    var contF = 0;

    if ($("#" + gPrefijo + "CamposSeleccionados option").length > 0) {

        $('select#' + gPrefijo + 'CamposSeleccionados').find('option').each(function () {

            var campoHD = $(this).val();

            if ($("#" + "hdf" + campoHD) != null) {

                var campoFin = "";
                var campoAlias = "";
                var combo = $("#" + gPrefijo + "cmb" + campoHD + " option:selected");
                var x = combo.index();
                var valorCampo = $("#" + gPrefijo + "txt" + campoHD).val();
                if (x > 0 && valorCampo != null && valorCampo != "") {
                    var comboText = x > 0 ? combo.val() : "";
                    //var text = $("#" + gPrefijo + "txt" + campoHD).val();
                    //var filtro = document.getElementById("hiddenFiltros");
                    campoFin = campoHD + " " + comboText + " '" + $.trim(valorCampo) + "'";
                    filtroFinVar = filtroFinVar + "|" + campoFin;
                    campoNom = campoNom + "|" + campoHD;
                    var no = new Option();
                    no.value = campoFin;
                    no.text = campoFin;
                    listaF[contF] = no;
                    contF++;
                }
            }

        });
        if (filtroFinVar == "" && campoNom == "") {
            alert("No existen campos para agregar filtros!!");
        } else {
            $.ajax({
                type: "POST",
                url: "../Reportes/SolicitudesGen.asmx/AgregaFiltros",
                data: "{filtroFin:\"" + filtroFinVar + "\",campos:\"" + campoNom + "\"}",
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: function (msg) {
                    if (msg.hasOwnProperty('d')) {
                        msg = msg.d;
                    }
                    var json = JSON.parse(msg);
                    alert(json.mensaje);
                },
                error: function (xhr, status, error) {
                    alert("No se pudo agregar el Filtro...");
                }
            });
        }
    } else {
        alert("Debe agregar campos a la lista de campos seleccionados.");
    }


}

function mpeSeleccionOnCancel() {
    //var txtSituacion = document.getElementById("txtSituacion");
    //txtSituacion.value = "";
    //txtSituacion.style.backgroundColor = "#FFFF99";
}


function cancelarReporte() {
    //alert("cancelar");
}

function aceptarReporte() {
    //alert("aceptar");
}


function resetSelectTab() {
    llenaListaTablasBD();
}

function agregaTablaBase() {
    llenaListaTablasBDRel();
}

function agregaCamposTablaBase() {
    llenaListaCamposTablasSel();
}

function agregaCamposSelTablaBase() {
    llenaListaCamposSelTablasSel();
}

function quitaTablasSel() {
    quitaListaTablasBD();
}

function quitaCamposSelTablaSel() {
    quitaListaCamposSelTablasSel();
}

function obtieneTablasBD(func) {
    var listaGen;
    $.ajax({
        type: "POST",
        url: "../Reportes/SolicitudesGen.asmx/ObtieneTablasBD",
        data: "{tablas:''}",
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (resp) {
            if (resp.hasOwnProperty('d')) {
                resp = resp.d;
            }
            var json = JSON.parse(resp);
            //                    alert("Recuperado tamaño obtienTablasBD:" + json.resp.listaGen.length);

            if (func != null) {
                func(json.resp.listaGen);
            }
            listaGen = json.resp.listaGen;

        },
        error: function (xhr, status, error) {
            listaGen = null;
        }
    });
    return listaGen;
}

function obtieneTablasRel(tablas, func) {
    var listaGen;
    $.ajax({
        type: "POST",
        url: "../Reportes/SolicitudesGen.asmx/ObtieneTablasRelacionadas",
        data: "{tablas:'" + tablas + "'}",
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (resp) {
            if (resp.hasOwnProperty('d')) {
                resp = resp.d;
            }
            var json = JSON.parse(resp);
            //                    alert("Recuperado tamaño:" + json.resp.listaGen.length);
            if (func != null) {
                func(json.resp.listaGen);
            }
            listaGen = json.resp.listaGen;

        },
        error: function (xhr, status, error) {
            listaGen = null;
        }
    });
    return listaGen;
}

function obtieneCamposTablas(tablas, func) {
    var listaGen;
    $.ajax({
        type: "POST",
        url: "../Reportes/SolicitudesGen.asmx/ObtieneCamposTablas",
        data: "{tablas:'" + tablas + "'}",
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (resp) {
            if (resp.hasOwnProperty('d')) {
                resp = resp.d;
            }
            var json = JSON.parse(resp);
            //                    alert("Recuperado tamaño:" + json.resp.listaGen.length);
            if (func != null) {
                func(json.resp.listaGen);
            }
            listaGen = json.resp.listaGen;

        },
        error: function (xhr, status, error) {
            alert("No se pudo obtener los campos de las tabla seleccionada...");
        }
    });
    return listaGen;
}

function mantieneTablasSel(tablas, alias, func) {
    var respuesta = false;
    $.ajax({
        type: "POST",
        url: "../Reportes/SolicitudesGen.asmx/MantieneTabSel",
        data: "{tablasSel:'" + tablas + "', " +
              "aliasTablasSel:'" + alias + "' " +
               "}",
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (resp) {
            if (resp.hasOwnProperty('d')) {
                resp = resp.d;
            }
            var json = JSON.parse(resp);
            //                    alert("Recuperado tamaño:" + json.resp.listaGen.length);
            if (func != null) {
                func(json.resp.listaGen);
            }
            //listaGen = json.resp.listaGen;

            //alert(json.mensaje);
            respuesta = true;

        },
        error: function (xhr, status, error) {
            alert("Error al mantener tablas...");
            respuesta = false;
        }
    });
    return respuesta;
}



function mantieneCamposSelTablasSel(tablas, alias, campos, func) {
    var respuesta;
    $.ajax({
        type: "POST",
        url: "../Reportes/SolicitudesGen.asmx/MantieneCamposSelTabSel",
        data: "{tablasCampoSel:'" + tablas + "', " +
              "aliasTablasCampoSel:'" + alias + "', " +
              "camposSel:'" + campos + "' " +
               "}",
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (resp) {
            if (resp.hasOwnProperty('d')) {
                resp = resp.d;
            }
            var json = JSON.parse(resp);
            //                    alert("Recuperado tamaño:" + json.resp.listaGen.length);
            if (func != null) {
                func(json.resp.listaGen);
            }
            //alert(json.mensaje);

            respuesta = true;

        },
        error: function (xhr, status, error) {
            alert("Error al mantener tablas y campos...");
            respuesta = false;
        }
    });
    return respuesta;
}

function guardaConsultaReporte(nomReporte, func) {
    var respuesta;
    $find('ReportePrevModalPopup').hide();
    $.ajax({
        type: "POST",
        url: "../Reportes/SolicitudesGen.asmx/GuardaReporteHistorial",
        data: "{nombreReporte:'" + nomReporte + "' " +
               "}",
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (resp) {
            $find('ReportePrevModalPopup').show();
            if (resp.hasOwnProperty('d')) {
                resp = resp.d;
            }
            var json = JSON.parse(resp);
            //                    alert("Recuperado tamaño:" + json.resp.listaGen.length);
            if (func != null) {
                func(json.resp.listaGen);
            }
            alert(json.mensaje);

            respuesta = true;

        },
        error: function (xhr, status, error) {
            $find('ReportePrevModalPopup').show();
            alert("Error al guardar el reporte en historial...");
            respuesta = false;
        }
    });
    return respuesta;
}

function guardaReporteHistorial(nombreRep) {
    if (nombreRep != null) {
        guardaConsultaReporte(nombreRep, null);
    }
}


function resetAllReporte(func) {
    var respuesta;
    $.ajax({
        type: "POST",
        url: "../Reportes/SolicitudesGen.asmx/ResetAll",
        data: "{nombreReporte:'' " +
               "}",
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (resp) {
            if (resp.hasOwnProperty('d')) {
                resp = resp.d;
            }
            var json = JSON.parse(resp);
            //                    alert("Recuperado tamaño:" + json.resp.listaGen.length);
            if (func != null) {
                func(json.resp.listaGen);
            }
            //alert(json.mensaje);

            respuesta = true;

        },
        error: function (xhr, status, error) {
            alert("Error al eliminar variables...");
            respuesta = false;
        }
    });
    return respuesta;
}


function obtieneTablasSeleccionadas(separador) {
    var tablasSelec = "";
    var sep = "";
    if (separador == "") {
        separador = "|";
    }
    if ($('select#' + gPrefijo + 'TablaBaseSel') != null) {
        $('select#' + gPrefijo + 'TablaBaseSel').find('option').each(function () {
            var tabla = $(this).html();
            tablasSelec = tablasSelec + sep + tabla;
            sep = separador;
        });
    }
    return tablasSelec;
}

function llenaListaTablasBD() {
    var idComp = gPrefijo + 'TablasBD';
    obtieneTablasBD(function (list) {
        llenaListaGen(idComp, list);
    });

}

function llenaListaTablasBDRel() {
    var idComp = gPrefijo + 'TablasBD';
    var idComp2 = gPrefijo + 'TablaBaseSel';
    //obtieneTablasRel('vehiculo');
    var tabSel = "";
    var sep = "";

    if ($("#" + idComp + " option:selected").length > 0) {

        $("#" + idComp + " option:selected").each(function () {
            arrTabSel.push($(this).html());
            arrAliasTabSel.push("alias" + $(this).html());
            agregaElementoLista(idComp2, $(this).html(), $(this).html());
        });

        var listTablasSelec = obtieneTablasSeleccionadas("|");
        obtieneTablasRel(listTablasSelec, function (list) {
            llenaListaGen(idComp, list);
        });

        mantieneTablasSel(toStringArr(arrTabSel, '|'), toStringArr(arrAliasTabSel, '|'), null);
    } else {
        alert("Debe seleccionar una tabla para agregar a la lista de tablas seleccionadas.");
    }
}

function llenaListaCamposTablasSel() {
    var idComp = gPrefijo + 'TablaBaseSel';
    var idComp2 = gPrefijo + 'CamposTalbaBaseSel';
    //obtieneTablasRel('vehiculo');
    var tabsSel = "";
    var sep = "";
    if ($("#" + idComp + " option:selected").length > 0) {
        $("#" + idComp + " option:selected").each(function () {
            tabsSel = tabsSel + sep + $(this).html();
            sep = "|";
            tablaSelActual = $(this).html();
            aliasTablaSelActual = "alias" + $(this).html();
        });


        //var listTablasSelec = obtieneTablasSeleccionadas("|");
        obtieneCamposTablas(tabsSel, function (list) {
            llenaListaGen(idComp2, list);
        });
    } else {
        alert("Debe seleccionar una tabla de la lista de la tablas seleccinadas para mostrar sus campos.")
    }


}

function llenaListaCamposSelTablasSel() {
    var idComp = gPrefijo + 'CamposTalbaBaseSel';
    var idComp2 = gPrefijo + 'CamposSeleccionados';
    //obtieneTablasRel('vehiculo');
    var tabsSel = "";
    var sep = "";
    if ($("#" + idComp + " option:selected").length > 0) {
        $("#" + idComp + " option:selected").each(function () {
            if (!existeElemArre($(this).html(), idComp2)) {
                agregaElementoLista(idComp2, $(this).html(), $(this).html());
                tabsSel = tabsSel + sep + $(this).html();
                sep = "|";

                arrTabSelCampos.push(tablaSelActual);
                arrAliasTabSelCampos.push(aliasTablaSelActual);
                arrCamposTabSelCampos.push($(this).html());
            }
        });
        $("#" + idComp + " option:selected").remove();
        mantieneCamposSelTablasSel(toStringArr(arrTabSelCampos, '|'), toStringArr(arrAliasTabSelCampos, '|'), toStringArr(arrCamposTabSelCampos, '|'), null);
    } else {
        alert("Debe seleccionar un campo de la lista de campos en tabla para agregar.");
    }
}

function eliminarItem(index, idListBox) {
    var indexItem = -1;
    $('#' + idListBox + ' option').each(function () {
        indexItem++;
        if (index == indexItem) {
            $(this).remove();
        }
    });
}

function elimiarItemCamSelTabSel(index) {
    //Eliminar arreglos
    arrTabSelCampos.splice(index, 1);
    arrAliasTabSelCampos.splice(index, 1);
    arrCamposTabSelCampos.splice(index, 1);
    //Elimina el listbox
    eliminarItem(index, gPrefijo + 'CamposSeleccionados');
}

function quitaListaTablasBD() {
    var idComp = gPrefijo + 'TablasBD';
    var idComp2 = gPrefijo + 'TablaBaseSel';
    var idComp3 = gPrefijo + 'CamposTalbaBaseSel';
    var idComp4 = gPrefijo + 'CamposSeleccionados';
    var idComp5 = gPrefijo + 'CamposSeleccionadosFin';
    var tabsSel = "";
    var sep = "";

    if ($("#" + idComp2 + " option:selected").length > 0) {
        $("#" + idComp2 + " option:selected").each(function () {
            //alert("eliminar:" + $(this).html() + " index:" + $(this).index() + " arreglo.length:" + arrTabSel.length);
            var tablaAEliminar = $(this).html();
            if (tablaSelActual == tablaAEliminar) {
                $("#" + idComp3).empty();
            }

            //Quita Campos seleccionados
            if ($("#" + idComp4 + " option").length > 0) {
                var bandMan = false;
                for (var cont = 0; cont < arrTabSelCampos.length ; cont++) {
                    if (tablaAEliminar == arrTabSelCampos[cont]) {
                        elimiarItemCamSelTabSel(cont);
                        cont = -1;
                        bandMan = true;
                    }
                }
                if (bandMan) {
                    mantieneCamposSelTablasSel(toStringArr(arrTabSelCampos, '|'), toStringArr(arrAliasTabSelCampos, '|'), toStringArr(arrCamposTabSelCampos, '|'), null);
                }
            }

            //Quita Filtros seleccionados
            if ($("#" + idComp5 + " option").length > 0) {
                var bandMan = false;
                //for (var cont = 0; cont < arrTabSelCampos.length ; cont++) {
                //    if (tablaAEliminar == arrTabSelCampos[cont]) {
                //        elimiarItemCamSelTabSel(cont);
                //        cont = -1;
                //        bandMan = true;
                //    }
                //}
                //if (bandMan) {
                //    mantieneCamposSelTablasSel(toStringArr(arrTabSelCampos, '|'), toStringArr(arrAliasTabSelCampos, '|'), toStringArr(arrCamposTabSelCampos, '|'), null);
                //}
            }

            arrTabSel.splice($(this).index(), 1);
            arrAliasTabSel.splice($(this).index(), 1);
            $(this).remove();
            //alert("eliminado:" + " arreglo.length:" + arrTabSel.length);
        });

        if ($("#" + idComp2 + " option").length > 0) {
            llenaListaTablasBDRel();
            mantieneTablasSel(toStringArr(arrTabSel, '|'), toStringArr(arrAliasTabSel, '|'), null);
        } else {
            //llenaListaTablasBD();
            resetAll();
        }
    } else {
        alert("Debe seleccionar una tabla de la lista de tablas seleccionadas para eliminar.");
    }

}

function quitaListaCamposSelTablasSel() {
    var idComp = gPrefijo + 'CamposTalbaBaseSel';
    var idComp2 = gPrefijo + 'CamposSeleccionados';

    //obtieneTablasRel('vehiculo');
    var tabsSel = "";
    var sep = "";
    if ($("#" + idComp + " option:selected").length > 0) {
        $("#" + idComp2 + " option:selected").each(function () {
            if (!existeElemArre($(this).html(), idComp)) {

                var indexItemLis = indexElemArre($(this).html(), idComp);
                if (indexItemLis < 0 && tablaSelActual == arrTabSelCampos[$(this).index()]) {
                    alert("Agrega elementos:" + $(this).html());
                    agregaElementoLista(idComp, $(this).html(), $(this).html());
                    tabsSel = tabsSel + sep + $(this).html();
                    sep = "|";
                }
            }
        });

        $("#" + idComp2 + " option:selected").each(function () {
            //alert("eliminar:" + $(this).html() + " index:" + $(this).index() + " arrTabSelCampos.length:" + arrTabSelCampos.length);
            arrTabSelCampos.splice($(this).index(), 1);
            arrAliasTabSelCampos.splice($(this).index(), 1);
            arrCamposTabSelCampos.splice($(this).index(), 1);
            $(this).remove();
            //alert("eliminado:" + " arrTabSelCampos.length:" + arrTabSelCampos.length);

        });

        mantieneCamposSelTablasSel(toStringArr(arrTabSelCampos, '|'), toStringArr(arrAliasTabSelCampos, '|'), toStringArr(arrCamposTabSelCampos, '|'), null);
    } else {
        alert("Debe seleccionar un campo de la lista de campos seleccionados para eliminar.");
    }
}


function agregaElementoLista(idLista, valor, descripcion) {
    //var repetido = false;
    //$('select#' + idLista).find('option').each(function () {
    //    if ($(this).html() == descripcion) {
    //        repetido = true;
    //    }
    //});
    //if (!repetido) {
    //    $("#" + idLista).append("<option value=\"" + valor + "\">" + descripcion + "</option>");
    //}
    if (!existeElemArre(descripcion, idLista)) {
        $("#" + idLista).append("<option value=\"" + valor + "\">" + descripcion + "</option>");
    }

}

function existeElemArre(descripcion, idLista) {
    var repetido = false;
    $('select#' + idLista).find('option').each(function () {
        if ($(this).html() == descripcion) {
            repetido = true;
        }
    });
    return repetido;
}

function indexElemArre(descripcion, idLista) {
    var repetido = -1;
    var cont = -1;
    alert("va en indexElemArre 1");
    $('select#' + idLista).find('option').each(function () {
        alert("va en indexElemArre 2");
        cont++;
        if ($(this).html() == descripcion) {
            alert("va en indexElemArre 3");
            repetido = cont;
        }

    });
    alert("va en indexElemArre 4:" + repetido);
    return repetido;
}

function llenaListaGen(idLista, lista) {
    var tam = lista.length;
    //var listaF = document.getElementById(idLista);
    $("#" + idLista).empty();
    for (var cont = 0; cont < tam ; cont++) {
        agregaElementoLista(idLista, cont, lista[cont]);
    }
}

function toStringArr(arreglo, sep) {
    var sepAux = "";
    var strArr = "";
    if (arreglo != null) {
        for (var cont = 0; cont < arreglo.length; cont++) {
            strArr = strArr + sepAux + arreglo[cont];
            sepAux = sep;
        }
    }
    return strArr;
}

function resetAll() {
    arrTabSel = [];
    arrAliasTabSel = [];

    arrTabSelCampos = [];
    arrAliasTabSelCampos = [];
    arrCamposTabSelCampos = [];

    tablaSelActual = "";
    aliasTablaSelActual = "";

    $("#" + gPrefijo + 'TablasBD').empty();
    $("#" + gPrefijo + 'TablaBaseSel').empty();
    $("#" + gPrefijo + 'CamposTalbaBaseSel').empty();
    $("#" + gPrefijo + 'CamposSeleccionados').empty();
    $("#" + gPrefijo + 'CamposSeleccionadosFin').empty();

    resetAllReporte();
    resetSelectTab();
}

function fDefault() {

}

function AgregaCampoTablaSel_Click(id, obj) {
    var id2 = id;
    var obj2 = obj;
    $("#contenedorRepHist").load("" + obj.href);
    return false;
}

function expPDF_Click(obj) {
    var nombreRep = $("#" + gPrefijo + "idNombreReporte").val();
    if (nombreRep != null && nombreRep != "") {
        //$("#contenedorRepGen").load(encodeURI("/Reportes/frmGenRep.aspx?tipo=PDF&nomRep=" + nombreRep));
        $("#IframeRepGen").empty();
        var frame = $get('IframeRepGen');
        frame.src = "../Reportes/frmGenRep.aspx?" + encodeURI("tipo=PDF&nomRep=" + nombreRep);
    } else {
        alert("Ingrese el nombre del reporte");
    }
    return false;
}

function expXLS_Click(obj) {
    var nombreRep = $("#" + gPrefijo + "idNombreReporte").val();
    if (nombreRep != null && nombreRep != "") {
        //$("#contenedorRepGen").load("/Reportes/frmGenRep.aspx?" + encodeURI("tipo=XLS&nomRep=" + nombreRep));
        $("#IframeRepGen").empty();
        var frame = $get('IframeRepGen');
        frame.src = "../Reportes/frmGenRep.aspx?" + encodeURI("tipo=XLS&nomRep=" + nombreRep);
    } else {
        alert("Ingrese el nombre del reporte");
    }
    return false;
}

function guardar_Click(obj) {
    var nombreRep = $("#" + gPrefijo + "idNombreReporte").val();
    if (nombreRep != null && nombreRep != "") {
        guardaReporteHistorial(nombreRep);
    } else {
        alert("Ingrese el nombre del reporte");
    }
    return false;
}