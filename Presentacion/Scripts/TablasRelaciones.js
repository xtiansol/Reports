$(document).ready(function () {
    bindData();
});
//==== VARIABLE GLOBAL PARA EL PAGINADO
var pageSize = 10;
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
            if (total > 1) {
                printCustomer(msg);
                var pageTotal = Math.ceil(total / pageSize);
                //if (pageTotal > 14) {
                //if ($('#paging').length != 0) // remove table if it exists
                //{
                //    $("#paging").empty();
                //}
                //else {
                for (var i = 0; i < pageTotal; i++) {
                    $("#paging").append("<li><a href=\"#\" onClick=\"pageData(" + (i + 1) + ")\">" + (i + 1) + "</a></li>");
                }
                //}
            }
            else {
                $("#paging").text("No records were found.");
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
    var table = "<table class='table table-striped' id='tblResult' ><thead><tr><th>Nombre Tabla</th><th>Relaciones</th><th>Editar</th><th>Eliminar</th></thead><tbody>";
    for (var i = 0; i <= (msg.length - 1) ; i++) {
        var row = "<tr>";
        row += '<td>' + msg[i].NombreTabla + '</td>';
        row += '<td>' + msg[i].Count + '</td>';
        row += '<td><a href="#" class="Seleccionar" value="' + msg[i].TablaID + '"><span class="glyphicon glyphicon-arrow-left"></span></a> </td>';
        row += '<td><a href="#" class="Eliminar" value="' + msg[i].TablaID + '"><span class="glyphicon glyphicon-trash"></span></a> </td>';
        row += '</tr>';
        table += row;
    }
    table += '</tbody></table>';
    $('#divData').html(table);
    $("#divData").slideDown("slow");
}
//====Fin Get data from database, created HTML table and place inside #divData



//==== Inicio Funciones que guardan los datos en la BD
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
                    EnableTrue();
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
        url: "frmTablasBD.aspx/deleteData",
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
//==== Method to validate textboxes
function validateData(e) {
    var errCount = 0;
    var selectedOpts = $('#relacionadas option:selected');
    if (selectedOpts.length == 0) {
        alert("Nothing to move.");
        errCount++
        e.preventDefault();
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
}

//==== VENTO DEL BOTON Nuevo
$('#btnNuevo').click(function () {
    Evento = "Nuevo";
    bindTable();
});
//==== VENTO DEL BOTON Guardar
$('#btnGuardar').click(function (e) {
    if (Evento == "Nuevo") {
    saveData();
    }
    else {
        var id = $("#hdID").val();
        updateData(id);
    }
});
//==== VENTO DEL BOTON Cancelar
$('#btnCancelar').click(function () {
    CleanText();
});
$('#btnPasart').click(function (e) {
    var selectedOpts = $('#Tablas option:selected');
    if (selectedOpts.length == 0) {
        alert("Nothing to move.");
        e.preventDefault();
    } else {
        var lstLeftSelectedItems = $('#Tablas option');
        for (var i = 0; i < lstLeftSelectedItems.length; i++) {

            var opt = $(lstLeftSelectedItems[i]).clone();
            $(lstLeftSelectedItems[i]).remove();
            $('#relacion').append(opt);
        }
        var rel = $('#relacion option');
        for (var i = 0; i < rel.length; i++) {

            if (rel[i].text == selectedOpts[0].text) {
                $(rel[i]).remove();
            }
        }
    }

    //$('#relacion').append($(selectedOpts).clone());
    //$(selectedOpts).remove();


    //var rel = $('#relacion option');
    //if (rel.length == 0) {
    //    alert("Nothing to move.");
    //    e.preventDefault();
    //} else {
    //    for (var i = 0; i < rel.length; i++) {

    //        if (rel[i].text == selectedOpts[0].text) {
    //            $(rel[i]).remove();
    //        }
    //    }
    //}
    $('#Tablas').append($(selectedOpts).clone().prop('selected', true));
    e.preventDefault();
});
$('#btnPasarR').click(function (e) {
    var selectedOpts = $('#relacion option:selected');
    if (selectedOpts.length == 0) {
        alert("Nothing to move.");
        e.preventDefault();
    }
    $('#relacionadas').append($(selectedOpts).clone().prop('selected', true));
    $(selectedOpts).remove();
    e.preventDefault();
});
$('#btnRegresaR').click(function (e) {
    var selectedOpts = $('#relacionadas option:selected');
    if (selectedOpts.length == 0) {
        alert("Nothing to move.");
        e.preventDefault();
    }

    $('#relacion').append($(selectedOpts).clone());
    $(selectedOpts).remove();
    e.preventDefault();
});

