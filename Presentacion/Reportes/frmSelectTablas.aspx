<%@ Page Title="" Language="C#" MasterPageFile="~/PaginaMaestra/PageMaster.Master" AutoEventWireup="true" CodeBehind="frmSelectTablas.aspx.cs" Inherits="Presentacion.Reportes.frmSelectTablas"  validateRequest="false" enableEventValidation="false"%>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
           <div>
            <asp:ScriptManager ID="ScriptManager1" runat="server" />
                    <asp:UpdatePanel ID="EmployeeInfoUpdatePanel" runat="server">
                      <ContentTemplate>
                          <div class="modal"></div>
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
                                            <asp:ListBox ID="TablasBD" runat="server" Height="283px" Width="167px"></asp:ListBox>
                                        </td>
                                        <td>
                                            <br />
                                            <input id="idAgregaTablasBase" value=">>" class="btn btn-default" type="button" onclick="agregaTablaBase()"/>
                                            <br />
                                            <input id="idQuitaTablasBase" value="<<" class="btn btn-default" type="button" onclick="quitaTablasSel()"/>
                                        </td>
                                        <td>
                                            <asp:ListBox ID="TablaBaseSel" runat="server" Height="277px" Width="174px"></asp:ListBox>
                                        </td>
                                        <td>
                                            <br />
                                            <input id="idAgregaCamposTablasSel" value=">>" class="btn btn-default" type="button" onclick="agregaCamposTablaBase()"/>
                                            <br />
                                        </td>
                                        <td class="auto-style1">
                                            <asp:ListBox ID="CamposTalbaBaseSel" runat="server" Height="277px" Width="174px"></asp:ListBox>
                                        </td>
                                        <td>
                                            <br />
                                            <input id="idAgregaCamposSelTablasSel" value=">>" class="btn btn-default" type="button" onclick="agregaCamposSelTablaBase()"/>
                                            <br />
                                            <input id="idQuitaCamposSelTablasSel" value="<<" class="btn btn-default" type="button" onclick="quitaCamposSelTablaSel()"/>
                                        </td>
                                        <td>
                                            <asp:ListBox ID="CamposSeleccionados" runat="server" class="btn btn-default" Height="277px" Width="174px"></asp:ListBox>
                                        </td>
                                        <td></td>
                                        <td>
                                            <asp:ListBox ID="CamposSeleccionadosFin" runat="server" Height="277px" Width="174px" ViewStateMode="Enabled"></asp:ListBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <button type="button" name="ResetAll" id="ResetAll" class="btn btn-default" onClick="resetAll()" alt="Restarurar todo"> <img src="/Reportes/img/reset-icon2.png"></button>
                                        </td>
                                        <td></td>
                                        <td></td>
                                        <td></td>
                                        <td></td>
                                        <td></td>
                                        <td class="auto-style1">
                                            <button type="button" name="ImageButton3" id="ImageButton3" class="btn btn-default" onClick="ShowFiltrosModal()" alt="Agregar Filtros"> <img src="/Reportes/img/filter_data2.png"></button>
                                        </td>
                                        <td></td>
                                        <td></td>
                                    </tr>

                                </table>
                            </div>

                            <br />

                          <input id="btnGenerarReporte2" value="Genera Reporte" class="btn btn-default" type="button" onclick="ShowReportePrevModal()"/>
                          <div  style='display:none;'>
                          <asp:Button ID="btnGenerarReporte" class="btn btn-default" runat="server" Text="GenerarReporteHid" />
                          <asp:Button ID="btnAsistente" class="btn btn-default" runat="server" Text="GenerarReporteHid" />
                          </div>
                          

                          <cc1:ModalPopupExtender ID="ModalPopupExtenderReportePrev" runat="server" TargetControlID="btnGenerarReporte"
                                PopupControlID="pnlReporteGenerado" OkControlID="btnAceptarReporte" CancelControlID="btnCancelarReporte"
                                OnOkScript="aceptarReporte()" OnCancelScript="cancelarReporte()" DropShadow="True"
                                BackgroundCssClass="FondoAplicacion"  PopupDragHandleControlID="PopupHeader" BehaviorID="ReportePrevModalPopup" />
                          

                            <br />
                            <br />

                            <br />

                            <asp:Panel ID="pnlSeleccionarDatos" runat="server" CssClass="CajaDialogo" Style="display: none;">
                                <div style="padding: 10px; background-color: #0033CC; color: #FFFFFF;">
                                    <asp:Label ID="Label4" runat="server" Text="Filtrar datos por:" />
                                </div>
                                <div>
                                    <asp:Panel runat="server" ID="Panel1" >
                                    </asp:Panel>
                                     <div id="contenedorFiltro" height="200" width="480" >
                                     </div>
                                    <asp:Button ID="btnAceptar" CssClass="btn btn-default" runat="server" Text="Aceptar"  />
                                    &nbsp;&nbsp;
                                    <asp:Button ID="btnCancelar" CssClass="btn btn-default" runat="server" Text="Cancelar" />

                                </div>

                            </asp:Panel>

                            <asp:Panel ID="pnlReporteGenerado" runat="server" CssClass="CajaDialogo" Width="800" Style="display: none;">
                                <div class="FooterDialogo">
                                    <asp:Label ID="LabelNombreReporte" runat="server" Text="Reporte generado:" />
                                </div>
                                <div>
                                    <asp:Label ID="idLabelNombreReporte" runat="server" Text="Nombre de Reporte:">
                                        <asp:TextBox ID="idNombreReporte" runat="server" ></asp:TextBox>
                                    </asp:Label>
                                </div>
                                <div >
                                    <asp:ImageButton ID="ImageButton1" runat="server" ImageUrl="/Reportes/img/PDF-icon2.png" OnClick="Button2_Click" AlternateText="Exportar a PDF" />&nbsp;
                                    <asp:ImageButton ID="ImageButton2" runat="server" ImageUrl="/Reportes/img/Excel-icon2.png" OnClick="Button3_Click" AlternateText="Exportar a XLS"/>
                                </div>
                                <div>
                                    <asp:Panel runat="server" ID="Panel3" HorizontalAlign="Center">
                                         <iframe id="IframeEdit" frameborder="0" height="203" width="600" scrolling="yes" >
                                        </iframe>

                                    </asp:Panel>


                                    <asp:Button ID="btnAceptarReporte" CssClass="btn btn-default" runat="server" Text="Guardar Consulta."  />
                                    &nbsp;&nbsp;
                                    <asp:Button ID="btnCancelarReporte" CssClass="btn btn-default" runat="server" Text="Cerrar" />
                                                                <br />
                            <br />
                            <br />
                                </div>

                            </asp:Panel>
                       <cc1:ModalPopupExtender ID="mpeSeleccion" runat="server" TargetControlID="btnAsistente"
                                PopupControlID="pnlSeleccionarDatos" OkControlID="btnAceptar" CancelControlID="btnCancelar"
                                OnOkScript="mpeSeleccionOnOk()" OnCancelScript="mpeSeleccionOnCancel()" DropShadow="True"
                                BackgroundCssClass="FondoAplicacion" BehaviorID="mpeSeleccion"/>

                 </ContentTemplate>

            </asp:UpdatePanel>
         </div>

    <script src="../Scripts/Reportes.js"></script>
</asp:Content>
