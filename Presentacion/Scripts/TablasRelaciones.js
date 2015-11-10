$(document).ready(function () {
    bindData();
    EnableTrue();
});
//==== VARIABLE GLOBAL PARA EL PAGINADO
var pageSize = 12;
var Evento = null;

//==== Inicio Get data from database, created HTML table and place inside #divData
function bindTable() {
    $.ajax({
        type: 'POST',
        url: 'frmRelacionesTablas.aspx/selectTables',
        data: "{}",
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (response) {
            var datos = (typeof response.d) == 'string' ? eval('(' + response.d + ')') : response.d;
            $("#Tablas").get(0).options.length = 0;
            //$("#Tablas").get(0).options[0] = new Option("--Seleccionar--", "-1");
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
function bindData() {
    $.ajax({
        type: "POST",
        url: "frmRelacionesTablas.aspx/selectData",
        data: "{skip:0,take:" + pageSize + "}",
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        async: true,
        cache: false,
        success: function (msg) {
            var total = msg.d.TotalRecords;
            printCustomer(msg);
            if (total > 12) {
                var pageTotal = Math.ceil(total / pageSize);
                if ($('#ul').length != 0) // remove table if it exists
                { $("#ul").remove(); }
                var paginado = "<ul id='paging' class='pagination'>";
                paginado += '<li class="disabled"><a href="#" id="totalRecords"></a></li>';
                for (var i = 0; i < pageTotal; i++) {
                    var row = "<li>";
                    row += '<a href=\'#\' onClick=\'pageData(' + (i + 1) + ')\'>' + (i + 1) + '</a>';
                    row += '</li>';
                    paginado += row;
                }
                paginado += '</ul>';
                $('#paging2').html(paginado);
                $("#paging2").slideDown("slow");
            }
            else {
                $("#paging").text("Registros Insuficientes Para el Paginado.");
            }
            $("#totalRecords").text("Total Registros: " + total);
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
    var msg = customers.d.Customers;
    if ($('#table').length != 0) // remove table if it exists
    { $("#table").remove(); }
    var table = "<table class='table table-bordered' id='tblResult' ><thead class='theadTabla'><tr><th>Nombre Tabla</th><th>Relaciones</th><th>Eliminar</th></thead><tbody>";
    for (var i = 0; i <= (msg.length - 1) ; i++) {
        var row = "<tr>";
        row += '<td>' + msg[i].NombreTabla + '</td>';
        row += '<td>' + msg[i].Count + '</td>';
        row += '<td class="text-center"><span value="' + msg[i].TablaID + '" class="Eliminar glyphicon glyphicon-trash"></span></td>';
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
            datos= msg;
         
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
    var list = [];
    $('#relacionadas option:selected').each(function () {
        list.push($(this).val());
    });
    //var jsonText = JSON.stringify({ list: list });
    //==== If validation pass save the data.
    if (errCount == 0) {
        $.ajax({
            type: "POST",
            url: "frmRelacionesTablas.aspx/saveData",
            data: "{idTabla:'" + $("#Tablas option:selected").val() + "',data:'" + JSON.stringify(list) + "', Descripcion:'" + $("#txtDescricpion").val() + "'}",
            contentType: "application/json; charset=utf-8",
            datatype: "json",
            async: "true",
            success: function (response) {
                if (response) {
                    alert("Los Datos de la tabla " + response.d + " se Guardaron Exitosamente.");
                    CleanText();
                    bindData();
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
    var selectedOpts = $('#relacionadas option');
    $(selectedOpts).remove();
    $(rel).remove();
    $(tablas).remove();
    $("#txtDescricpion").val("");
}
function EnableTrue() {
    $("#btnGuardar").prop("disabled", true);
    $("#btnCancelar").prop("disabled", true);
}
//==== Method to validate textboxes
function validateData(e) {
    var errCount = 0;
    var selectedOpts = $('#relacionadas option:selected');
    if (selectedOpts.length == 0) {
        alert("Nothing to move.");
        errCount++
    }
    return errCount;
    e.preventDefault();
}

//==== VENTO DEL BOTON Nuevo
$('#btnNuevo').click(function () {
    Evento = "Nuevo";
    bindTable();
    $("#btnCancelar").prop("disabled", false);
});
//==== VENTO DEL BOTON Guardar
$('#btnGuardar').click(function (e) {
    if (Evento == "Nuevo") {
        saveData();
        $("#btnGuardar").prop("disabled", true);
        $("#btnCancelar").prop("disabled", true);
    }
    else {
        var id = $("#hdID").val();
        updateData(id);
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

$('#btnPasart').click(function (e) {
    var id = $('#Tablas option:selected').val();
    var selectedOpts = $('#Tablas option:selected');
    if (selectedOpts.length == 0) {
        alert("No Hay Datos Seleccionados !!");
        e.preventDefault();
    } else {
    $.ajax({
        type: "POST",
        url: "frmRelacionesTablas.aspx/tablasRelacionadas",
        data: "{id:" + id + "}",
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        async: true,
        cache: false,
        success: function (msg) {
            var datos = msg.d;

            var lstLeftSelectedItems = $('#Tablas option');
            if (datos.length > 0) {
                for (var j = 0; j < datos.length; j++) {
                    for (var i = 0; i < lstLeftSelectedItems.length; i++) {
                        if (datos[j] == lstLeftSelectedItems[i].value) {
                            var opt = $(lstLeftSelectedItems[i]).clone().prop('disabled', true).addClass('item-disabled');
                        } else{
                            var opt = $(lstLeftSelectedItems[i]).clone();
                        }

                        $(lstLeftSelectedItems[i]).remove();
                        $('#relacion').append(opt);
                    }
                }
            } else {
                for (var i = 0; i < lstLeftSelectedItems.length; i++) {

                    var opt = $(lstLeftSelectedItems[i]).clone();
                    $(lstLeftSelectedItems[i]).remove();
                    $('#relacion').append(opt);
                }
            }
            var rel = $('#relacion option');
            for (var i = 0; i < rel.length; i++) {

                if (rel[i].text == selectedOpts[0].text) {
                    $(rel[i]).remove();
                }
            }
            $('#Tablas').append($(selectedOpts).clone().prop('selected', true));
        },
        error: function (response) {
            alert(response.status + ' ' + response.statusText);
        }
    });
    }
    e.preventDefault();
});
$('#btnPasarR').click(function (e) {
    var selectedOpts = $('#relacion option:selected');
    if (selectedOpts.length == 0) {
        alert("No Hay Datos Seleccionados !!");
        $("#btnGuardar").prop("disabled", true);
        e.preventDefault();
    } else{
        $("#btnGuardar").prop("disabled", false);
    }
    $('#relacionadas').append($(selectedOpts).clone().prop('selected', true));
    $(selectedOpts).remove();

    e.preventDefault();
});
$('#btnRegresaR').click(function (e) {
    var selectedOpts = $('#relacionadas option:selected');
    var selectedOpts2 = $('#relacionadas option');
    if (selectedOpts.length == 0) {
        alert("No Hay Datos Seleccionados !!");
        $("#btnGuardar").prop("disabled", true);
        e.preventDefault();
    } else
    {
        $("#btnGuardar").prop("disabled", false);
    }

    $('#relacion').append($(selectedOpts).clone());
    $(selectedOpts).remove();

    e.preventDefault();
});

