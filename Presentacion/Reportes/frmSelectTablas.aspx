<%@ Page Title="" Language="C#" MasterPageFile="~/PaginaMaestra/PageMaster.Master" AutoEventWireup="true" CodeBehind="frmSelectTablas.aspx.cs" Inherits="Presentacion.Reportes.frmSelectTablas" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
            <div>
            <asp:ScriptManager ID="ScriptManager1" runat="server" />
            <table>
                <tr>
                    <td>Tablas en BD</td>
                    <td></td>
                    <td>Tablas Seleccionadas</td>
                    <td></td>
                    <td class="auto-style1">Campos en Tabla</td>
                    <td></td>
                    <td>Campos Seleccionados</td>
                    <td></td>
                    <td>Filtros</td>
                </tr>
                <tr>
                    <td>
                        <asp:ListBox ID="TablasBD" CssClass="form-control" runat="server" Height="283px" Width="167px"></asp:ListBox>
                    </td>
                    <td>
                        <asp:Button ID="AgregaTablaBase" runat="server" CssClass="btn btn-default" Text=">>" OnClick="AgregaTablaBase_Click" />
                        <br />
                        <asp:Button ID="QuitaTablaBase" runat="server" CssClass="btn btn-default" Text="<<" OnClick="QuitaTablaBase_Click" />
                    </td>
                    <td>
                        <asp:ListBox ID="TablaBaseSel"  CssClass="form-control" runat="server" Height="277px" Width="174px"></asp:ListBox>
                    </td>
                    <td>
                        <asp:Button ID="ObtieneCampos" runat="server" CssClass="btn btn-default" Text=">>" OnClick="ObtieneCampos_Click" />
                        <br />
                    </td>
                    <td class="auto-style1">
                        <asp:ListBox ID="CamposTalbaBaseSel"  CssClass="form-control" runat="server" Height="277px" Width="182px"></asp:ListBox>
                    </td>
                    <td>
                        <asp:Button ID="AgregaCampoTablaSel" runat="server" CssClass="btn btn-default" Text=">>" OnClick="AgregaCampoTablaSel_Click" />
                        <br />
                        <asp:Button ID="QuitaCampoTablaSel" runat="server" CssClass="btn btn-default" Text="<<" OnClick="QuitaCampoTablaSel_Click" />
                    </td>
                    <td>
                        <asp:ListBox ID="CamposSeleccionados"  CssClass="form-control" runat="server" Height="277px" Width="210px"></asp:ListBox>
                    </td>
                    <td>
                        <br />
                        <br />
                        <br />
                    </td>
                    <td>
                        <asp:ListBox ID="CamposSeleccionadosFin"  CssClass="form-control" runat="server" Height="277px" Width="210px"></asp:ListBox>
                    </td>
                </tr>
                <tr>
                    <td> <br /> <asp:Button ID="ResetAll" runat="server" CssClass="btn btn-default" Text="Reset" Width="150" OnClick="ResetAll_Click"  /></td>
                    <td></td>
                    <td></td>
                    <td></td>
                    <td></td>
                    <td></td>
                    <td>
                        <asp:Button ID="btnAsistente" runat="server" CssClass="btn btn-default" Text="Agregar Filtros" Width="150" OnClick="btnAsistente_Click" />
                    </td>
                    <td></td>
                    <td></td>
                </tr>

            </table>
        </div>
<br />

        <br />

        <br />
        <br />

        <asp:Button ID="Button1" runat="server" CssClass="btn btn-default" Text="GenerarReporte" OnClick="Button1_Click" />

        <asp:Button ID="Button2" runat="server" CssClass="btn btn-default" Text="Exportar PDF" OnClick="Button2_Click" />

        <asp:Button ID="Button3" runat="server" CssClass="btn btn-default" Text="Exportar XLS" OnClick="Button3_Click" />

        <asp:Button ID="Button4" runat="server" CssClass="btn btn-default" Text="Exportar CSV" OnClick="Button4_Click" />

        <asp:Button ID="Button5" runat="server" CssClass="btn btn-default" Text="Exportar Word" OnClick="Button5_Click" />

        <br />
        <br />
        <asp:GridView ID="GridView1" runat="server" Height="67px"
            Width="353px"
            CellPadding="4" ForeColor="#333333" GridLines="None">
            <AlternatingRowStyle BackColor="White" ForeColor="#284775" />
            <EditRowStyle BackColor="#999999" />
            <FooterStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
            <HeaderStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
            <PagerStyle BackColor="#284775" ForeColor="White" HorizontalAlign="Center" />
            <RowStyle BackColor="#F7F6F3" ForeColor="#333333" />
            <SelectedRowStyle BackColor="#E2DED6" Font-Bold="True" ForeColor="#333333" />
            <SortedAscendingCellStyle BackColor="#E9E7E2" />
            <SortedAscendingHeaderStyle BackColor="#506C8C" />
            <SortedDescendingCellStyle BackColor="#FFFDF8" />
            <SortedDescendingHeaderStyle BackColor="#6F8DAE" />
        </asp:GridView>
        <br />

        <asp:Panel ID="pnlSeleccionarDatos" runat="server" CssClass="CajaDialogo" Style="display: none;">
            <div style="padding: 10px; background-color: #0033CC; color: #FFFFFF;">
                <asp:Label ID="Label4" runat="server" Text="Filtrar datos por:" />
            </div>

            <div>
                <asp:Panel runat="server" ID="Panel1"></asp:Panel>
                <asp:Button ID="btnAceptar" runat="server" Text="Aceptar" />
                &nbsp;&nbsp;
                <asp:Button ID="btnCancelar" runat="server" Text="Cancelar" />

            </div>
        </asp:Panel>
        <cc1:ModalPopupExtender ID="mpeSeleccion" runat="server" TargetControlID="btnAsistente"
            PopupControlID="pnlSeleccionarDatos" OkControlID="btnAceptar" CancelControlID="btnCancelar"
            OnOkScript="mpeSeleccionOnOk()" OnCancelScript="mpeSeleccionOnCancel()" DropShadow="True"
            BackgroundCssClass="FondoAplicacion" />
    <%--<script src="../Scripts/Reportes.js"></script>--%>

        <script language="javascript" type="text/javascript">
        function mpeSeleccionOnOk() {
            var lista1 = document.getElementById("CamposSeleccionados");
            var listaF = document.getElementById("CamposSeleccionadosFin");
            var contF = 0;
            for (cont = 0; cont < lista1.options.length; cont++) {
                var campoHD = lista1.options[cont].text;
                if (document.getElementById("hdf" + campoHD) != null) {
                    var campoFin = "";
                    var combo =  document.getElementById("cmb"+campoHD);
                    var x = combo.selectedIndex;
                    var y = combo.options;
                    var comboText = x > 0 ? y[x].text:"";
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
    </script>
</asp:Content>
