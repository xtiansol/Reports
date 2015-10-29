$(document).ready(function () {
    bindData();
});
//==== VARIABLE GLOBAL PARA EL PAGINADO
var pageSize = 14;

//==== VENTO DEL BOTON CONSULTAR
$('#btnNuevo').click(function () {
    window.location.href = 'frmTipoMovimientoEdicion.aspx';
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


//==== EVENTO DEL BOTON CONSULTAR
$('#btnConsultar').click(function () {
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
        window.location.href = 'frmTipoMovimientoConsulta.aspx?key=' + valor;
    }
});
//==== Get data from database, created HTML table and place inside #divData
function bindData() {
    $.ajax({
        type: "POST",
        url: "frmTipoMovimiento.aspx/getData",
        data: "{skip:0,take:" + pageSize + "}",
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        async: true,
        cache: false,
        success: function (msg) {
            var total = msg.d.TotalRecords;
            if (total > 0) {
                printCustomer(msg);
                var pageTotal = Math.ceil(total / pageSize);
                if (pageTotal > 14) {
                    for (var i = 0; i < pageTotal; i++) {
                        $("#paging").append("<a class=\"paginado\" onClick=\"pageData(" + (i + 1) + ")\">" + (i + 1) + "</a>");
                    }
                }
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
        url: "frmDelegaciones.aspx/getData",
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
    if ($('#tblResult').length != 0) // remove table if it exists
    { $("#tblResult").remove(); }
    var table = "<table class='tblResult' id='tblResult' ><thead><tr><th>Seleccionar</th><th>Tipo Movimiento</th><th>Descripción</th></thead><tbody>";
    for (var i = 0; i <= (msg.length - 1) ; i++) {
        var row = "<tr>";
        row += '<td><input id="CkbSelect" type="checkbox" value="' + msg[i].Id_TipoMovimiento + '" /> </td>';
        row += '<td>' + msg[i].Tipo_Movimiento + '</td>';
        row += '<td>' + msg[i].Descripcion + '</td>';

        row += '</tr>';
        table += row;
    }
    table += '</tbody></table>';
    $('#divData').html(table);
    $("#divData").slideDown("slow");
}
