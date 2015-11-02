$(document).ready(function () {
    bindData();
    EnableTrue();
});
//==== VARIABLE GLOBAL PARA EL PAGINADO
var pageSize = 10;
var Evento = null;
//==== Inicio Get data from database, created HTML table and place inside #divData
function bindData() {
    $.ajax({
        type: "POST",
        url: "frmTablasBD.aspx/getData",
        data: "{skip:0,take:" + pageSize + "}",
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        async: true,
        cache: false,
        success: function (msg) {
            var total = msg.d.TotalRecords;
            if (total > 10) {
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
        url: "frmTablasBD.aspx/getData",
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
    var table = "<table class='table table-striped' id='tblResult' ><thead><tr><th>Nombre Tabla</th><th>Descripción</th><th>Tipo Tabla</th><th>Editar</th><th>Eliminar</th></thead><tbody>";
    for (var i = 0; i <= (msg.length - 1) ; i++) {
        var row = "<tr>";
        row += '<td>' + msg[i].NombreTabla + '</td>';
        row += '<td>' + msg[i].Descripcion + '</td>';
        row += '<td>' + msg[i].TipoTabla + '</td>';
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
    //==== If validation pass save the data.
    if (errCount == 0) {
        $.ajax({
            type: "POST",
            url: "frmTablasBD.aspx/saveData",
            data: "{NombreTabla:'" + $("#txtNombre").val() + "', Descripcion:'" + $("#txtDescricpion").val() + "',TipoTabla:'" + $("#txtTipoTabla").val() + "'}",
            contentType: "application/json; charset=utf-8",
            datatype: "json",
            async: "true",
            success: function (response) {
                if (response) {
                    alert("Los Datos de la tabla " + response.d + " se Guardaron Exitosamente.");
                    EnableTrue();
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
//==== Inicio Funciones que guardan los datos en la BD

function EnableTrue() {
    $("#txtNombre").prop("disabled", true);
    $("#txtDescricpion").prop("disabled", true);
    $("#txtTipoTabla").prop("disabled", true);
    $("#btnGuardar").prop("disabled", true);
    $("#btnCancelar").prop("disabled", true);
}
function EnableFalse() {
    $("#txtNombre").prop("disabled", false);
    $("#txtDescricpion").prop("disabled", false);
    $("#txtTipoTabla").prop("disabled", false);
    $("#btnCancelar").prop("disabled", false);
}
function CleanText() {
    $("#txtNombre").val("");
    $("#txtDescricpion").val("");
    $("#txtTipoTabla").val("");
}
//==== Method to validate textboxes
function validateData() {

    var txtTipoMovimiento = $("#txtNombre").val();
    var txtDescripcion = $("#txtTipoTabla").val();
    var errCount = 0;
    if (txtTipoMovimiento.length <= 0) {
        errCount++;
        alert("Por Favor Introduce un Nombre de Tabla");
    }
    if (txtDescripcion.length <= 0) {
        errCount++;
        alert("Por Favor Introduce un Tipo de Tabla");
    }
    return errCount;
}

//==== VENTO DEL BOTON Nuevo
$('#btnNuevo').click(function () {
    //window.location.href = 'frmTipoMovimientoEdicion.aspx';
    EnableFalse();
    Evento = "Nuevo";
});
//==== VENTO DEL BOTON Guardar
$('#btnGuardar').click(function () {
    //window.location.href = 'frmTipoMovimientoEdicion.aspx';
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
    //window.location.href = 'frmTipoMovimientoEdicion.aspx';
    CleanText();
    EnableTrue();
});
//==== VENTO DEL BOTON TEXT PARA HABILITAR GUARDAR
$("#txtTipoTabla").focus(function () {
    $("#btnGuardar").prop("disabled", false);
});
//==== VENTO DEL BOTON MODIFICAR
$('#btnModificar').click(function () {
    var cont = 0;
    var valor = null;
    //estado del elemento seleccionado
    if ($('#CkbSelect[type=checkbox]').is(':checked')) {
        //cada elemento seleccionado
        $("#CkbSelect[type=checkbox]:checked").each(function () {
            cont++;
            valor = $(this).val();
        });
    }
    else {
        alert('Seleccione la fila que desea Consultar');
        return false;
    }
    if (cont >= 2) {
        alert("No puede seleccionar 2 filas");
        return false;
    } else {
        window.location.href = 'frmTipoMovimientoEdicion.aspx?key=' + valor;
    }
});
//==== EVENTO DEL BOTON de la tabla para editar
$(document).on("click", ".Seleccionar", function () {
    var tr = $(this).parent().parent();
    var id = $(this).attr("value");
    $("#hdID").val(id);
    $("#txtNombre").val($('td:eq(0)', tr).text()).prop("disabled", false);
    $("#txtDescricpion").val($('td:eq(1)', tr).text()).prop("disabled", false);
    $("#txtTipoTabla").val($('td:eq(2)', tr).text()).prop("disabled", false);
    $("#btnCancelar").prop("disabled", false);
    Evento = "Editar";
});
//==== EVENTO DEL BOTON de la tabla para Eliminar un registro.
$(document).on("click", ".Eliminar", function () {
    var id = $(this).attr("value");
    $('#dialogEliminar').modal({ backdrop: 'static', keyboard: false })
     .one('click', '#Delete', function () {
         deleteData(id);
         $('#dialogEliminar').modal('hide');
     });
});