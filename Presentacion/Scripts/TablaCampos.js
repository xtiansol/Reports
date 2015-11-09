$(document).ready(function () {
    bindData();
    EnableTrue();
    //bindTable();
});
//==== VARIABLE GLOBAL PARA EL PAGINADO
var pageSize = 10;
var Evento = null;
var cont = 0;
var list = [];

//==== Inicio Get data from database, created HTML table and place inside #divData
function bindTable() {
    $.ajax({
        type: 'POST',
        url: 'frmRelacionCampos.aspx/selectTables',
        data: "{}",
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (response) {
            var datos = (typeof response.d) == 'string' ? eval('(' + response.d + ')') : response.d;
            $("#Tablas").get(0).options.length = 0;
            $("#Tablas").get(0).options[0] = new Option("--Seleccionar--", "-1");
            for (var i = 0; i < datos.length; i++) {
                var val = datos[i].Item1;
                var text = datos[i].Item2;
                $('#Tablas').get(0).options[$("#Tablas").get(0).options.length] = new Option(text, val);
            }
        },
        error: function (result) {
            alert("ERROR " + result.status + " " + result.statusText);
        }
    });
}
function bindDetail(id) {
    $.ajax({
        type: 'POST',
        url: 'frmRelacionCampos.aspx/selectDetail',
        data: "{id:" + id + "}",
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (response) {
            var datos = (typeof response.d) == 'string' ? eval('(' + response.d + ')') : response.d;
            $("#relacion").get(0).options.length = 0;
            $("#relacion").get(0).options[0] = new Option("--Seleccionar--", "-1");
            for (var i = 0; i < datos.length; i++) {
                var val = datos[i].Item1;
                var text = datos[i].Item2;
                $('#relacion').get(0).options[$("#relacion").get(0).options.length] = new Option(text, val);
            }
        },
        error: function (result) {
            alert("ERROR " + result.status + " " + result.statusText);
        }
    });
}
function bindColumns(tableName) {
    $.ajax({
        type: 'POST',
        url: 'frmRelacionCampos.aspx/selectColumns',
        data: "{nombre:'" + tableName + "'}",
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (response) {
            var datos = (typeof response.d) == 'string' ? eval('(' + response.d + ')') : response.d;
            $("#CamposP").get(0).options.length = 0;
            $("#CamposP").get(0).options[0] = new Option("--Seleccionar--", "-1");
            for (var i = 0; i < datos.length; i++) {
                //var val = datos[i].Item1;
                var text = datos[i];
                $('#CamposP').get(0).options[$("#CamposP").get(0).options.length] = new Option(text);
            }
        },
        error: function (result) {
            alert("ERROR " + result.status + " " + result.statusText);
        }
    });
}
function bindColumnsF(tableName) {
    $.ajax({
        type: 'POST',
        url: 'frmRelacionCampos.aspx/selectColumns',
        data: "{nombre:'" + tableName + "'}",
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (response) {
            var datos = (typeof response.d) == 'string' ? eval('(' + response.d + ')') : response.d;
            $("#CamposF").get(0).options.length = 0;
            $("#CamposF").get(0).options[0] = new Option("--Seleccionar--", "-1");
            for (var i = 0; i < datos.length; i++) {
                //var val = datos[i].Item1;
                var text = datos[i];
                $('#CamposF').get(0).options[$("#CamposF").get(0).options.length] = new Option(text);
            }
        },
        error: function (result) {
            alert("ERROR " + result.status + " " + result.statusText);
        }
    });
}
function bindData() {
    $.ajax({
        type: "POST",
        url: "frmRelacionCampos.aspx/selectCampos",
        data: "",
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        async: true,
        cache: false,
        success: function (msg) {
            //var total = msg.d.TotalRecords;
            printCustomer(msg);
            //if (total > 10) {
            //    var pageTotal = Math.ceil(total / pageSize);
            //    if ($('#ul').length != 0) // remove table if it exists
            //    { $("#ul").remove(); }
            //    var paginado = "<ul id='paging' class='pagination'>";
            //    paginado += '<li class="disabled"><a href="#" id="totalRecords"></a></li>';
            //    for (var i = 0; i < pageTotal; i++) {
            //        var row = "<li>";
            //        row += '<a href=\'#\' onClick=\'pageData(' + (i + 1) + ')\'>' + (i + 1) + '</a>';
            //        row += '</li>';
            //        paginado += row;
            //    }
            //    paginado += '</ul>';
            //    $('#paging2').html(paginado);
            //    $("#paging2").slideDown("slow");
            //}
            //else {
            //    $("#paging").text("Registros Insuficientes Para el Paginado.");
            //}
            //$("#totalRecords").text("Total Registros: " + total);
        },
        error: function (response) {
            alert(response.status + ' ' + response.statusText);
        }
    });
}
function pageData(e) {
    var skip = e == 1 ? 0 : (e * pageSize) - pageSize;
    $.ajax({
        type: "POST",
        url: "frmRelacionesTablas.aspx/selectData",
        data: "{skip:" + skip + ",take:" + pageSize + "}",
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        async: true,
        cache: false,
        success: function (msg) {
            printCustomer(msg);
        }
    });
    return false;
}
function printCustomer(customers) {
    var msg = $.parseJSON(customers.d);
    if ($('#table').length != 0) // remove table if it exists
    { $("#table").remove(); }
    var table = "<table class='table table-bordered' id='tblResult' ><thead class='theadTabla'><tr><th>Campo PK</th><th>Campos FK</th><th>Eliminar</th></thead><tbody>";
    for (var i = 0; i < msg.length; i++) {
        var row = "<tr>";
        row += '<td>' + msg[i].Item1 + '</td>';
        row += '<td>' + msg[i].Item2 + '</td>';
        row += '<td><span value="' + msg[i].TablaID + '" class="Eliminar glyphicon glyphicon-trash"></span></td>';
        row += '</tr>';
        table += row;
    }
    table += '</tbody></table>';
    $('#divData').html(table);
    $("#divData").slideDown("slow");
}
//====Fin Get data from database, created HTML table and place inside #divData



//==== Inicio Funcione que verifica si una tabla tiene relaciones con otras tablas.
function bindRelation() {
    var id = $('#Tablas option:selected').val();
    var datos = null;
    $.ajax({
        type: "POST",
        url: "frmRelacionesTablas.aspx/tablasRelacionadas",
        data: "{id:" + id + "}",
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        async: true,
        cache: false,
        success: function (msg) {
            datos = msg;

        },
        error: function (response) {
            alert(response.status + ' ' + response.statusText);
        }
    });
    return datos;
}
//==== Method to save record.
function saveData() {
    var errCount = validateData();
    var listFK = [];
    $('#CamposR option:selected').each(function () {
        listFK.push($(this).val());
    });
    //==== If validation pass save the data.
    if (errCount == 0) {
        $.ajax({
            type: "POST",
            url: "frmRelacionCampos.aspx/saveColumns",
            data: "{idRelacion:'" + JSON.stringify(list) + "',campoPK:'" + $("#CamposP option:selected").text() + "', campoFK:'" + JSON.stringify(listFK) + "'}",
            contentType: "application/json; charset=utf-8",
            datatype: "json",
            async: "true",
            success: function (response) {
                if (response) {
                    alert("Los Datos de la tabla " + response.d + " se Guardaron Exitosamente.");
                    //bindData();
                }
                else {
                    alert("Hay un Error, no se pudieron Guardar los Datos.");
                }
            },
            error: function (response) {
                alert(response.status + ' ' + response.statusText);
            }
        });
    }
}
//==== Method to update record.
function updateData(id) {
    var errCount = validateData();
    if (errCount == 0) {
        $.ajax({
            type: "POST",
            url: "frmTablasBD.aspx/updateData",
            data: "{id:'" + id + "',NombreTabla:'" + $("#txtNombre").val() + "', Descripcion:'" + $("#txtDescricpion").val() + "',TipoTabla:'" + $("#txtTipoTabla").val() + "'}",
            contentType: "application/json; charset=utf-8",
            datatype: "json",
            async: "true",
            success: function (response) {
                //var myObject = eval('(' + response.d + ')');
                if (response) {
                    alert("Los Datos de " + response.d + " Actualizaron Exitosamente.");
                    CleanText();
                    bindData();
                }
                else {
                    alert("Hay un Error, no se pudieron Actualizar los Datos.");
                }
            },
            error: function (response) {
                alert(response.status + ' ' + response.statusText);
            }
        });
    }
}
//==== Method to Delete record.
function deleteData(id) {
    $.ajax({
        type: "POST",
        url: "frmRelacionesTablas.aspx/Delete",
        data: "{id:'" + id + "'}",
        contentType: "application/json; charset=utf-8",
        datatype: "json",
        async: "true",
        success: function (response) {
            //var myObject = eval('(' + response.d + ')');
            if (response) {
                alert("Los Datos de " + response.d + " Elimino Exitosamente.");
                bindData();
            }
            else {
                alert("Hay un Error, no se pudieron Actualizar los Datos.");
            }
        },
        error: function (response) {
            alert(response.status + ' ' + response.statusText);
        }
    });
}
//==== FIN Funciones que guardan los datos en la BD


function CleanText() {
    var tablas = $('#Tablas option');
    var rel = $('#relacion option');
    var CamposP = $('#CamposP option');
    var CamposF = $('#CamposF option');
    var CamposR = $('#CamposR option');
    $(tablas).remove();
    $(rel).remove();
    $(CamposP).remove();
    (CamposF).remove();
    $(CamposR).remove();
}
function EnableTrue() {
    $("#btnGuardar").prop("disabled", true);
    $("#btnAceptar").prop("disabled", true);
    $("#Tablas").prop("disabled", true);
    $("#relacion").prop("disabled", true);
    $("#CamposP").prop("disabled", true);
    $("#CamposF").prop("disabled", true);
}
//==== Method to validate textboxes
function validateData(e) {
    var errCount = 0;
    var selectedOpts = $('#CamposR option:selected');
    if (selectedOpts.length == 0) {
        alert("Nothing to move.");
        errCount++
    }
    //var txtTipoMovimiento = $("#txtNombre").val();
    //var txtDescripcion = $("#txtTipoTabla").val();
    //var errCount = 0;
    //if (txtTipoMovimiento.length <= 0) {
    //    errCount++;
    //    alert("Por Favor Introduce un Nombre de Tabla");
    //}
    //if (txtDescripcion.length <= 0) {
    //    errCount++;
    //    alert("Por Favor Introduce un Tipo de Tabla");
    //}
    return errCount;
    e.preventDefault();
}

//==== VENTO DEL BOTON Nuevo
$('#btnNuevo').click(function () {
    Evento = "Nuevo";
    bindTable();
    $("#Tablas").prop("disabled", false);
    //$("#btnCancelar").prop("disabled", false);
});
//==== VENTO DEL BOTON Guardar
$('#btnGuardar').click(function (e) {
    if (Evento == "Nuevo") {
        saveData();
        CleanText();
        EnableTrue();
    }
});
//==== VENTO DEL BOTON Eliminar
$(document).on("click", ".Eliminar", function () {
    var id = $(this).attr("value");
    $('#dialogEliminar').modal({ backdrop: 'static', keyboard: false })
     .one('click', '#Delete', function () {
         deleteData(id);
         $('#dialogEliminar').modal('hide');
     });
});
//==== VENTO DEL BOTON Cancelar
$('#btnCancelar').click(function () {
    CleanText();
    EnableTrue();
});
$('#Tablas').change(function () {
    var id = $('#Tablas option:selected').val();
    var tableName = $('#Tablas option:selected').text();
    bindDetail(id);
    bindColumns(tableName);
    $("#CamposP").prop("disabled", false);
    $("#relacion").prop("disabled", false);
});
$('#relacion').change(function () {
    var tableName = $('#relacion option:selected').text();
    bindColumnsF(tableName);
    $("#CamposF").prop("disabled", false);
    $("#btnAceptar").prop("disabled", false);
});

$('#btnAceptar').click(function (e) {
    var selectedP = $('#CamposP option:selected');
    var selectedF = $('#CamposF option:selected');
    
    $('#relacion option:selected').each(function () {
        list.push($(this).val());
    });
    if (selectedP.length == 0 || selectedF.length == 0 || $('#CamposF option:selected').val() == -1) {
        alert("No Hay Datos Seleccionados !!");
        e.preventDefault();
    } else {
        if (cont == 0) {
            $('#CamposR').append($(selectedP).clone());
            $('#CamposR').append($(selectedF).clone().prop('selected', true));
            $('#CamposP').prop('disabled', true);
            cont++;
        } else {
            $('#CamposR').append($(selectedF).clone().prop('selected', true));
        }
        $("#btnGuardar").prop("disabled", false);
        $("#btnCancelar").prop("disabled", false);
    }
    e.preventDefault();
});


