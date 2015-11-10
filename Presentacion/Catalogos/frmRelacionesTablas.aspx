<%@ Page Title="" Language="C#" MasterPageFile="~/PaginaMaestra/PageMaster.Master" AutoEventWireup="true" CodeBehind="frmRelacionesTablas.aspx.cs" Inherits="Presentacion.Catalogos.frmRelacionesTablas" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <div class="row">
        <div class="col-md-12">
            <div class="panel panel-default">
                <div class="panel-heading">Bootstrap: Horizontal Form</div>
                <div class="panel-body">
                <form id="mySuperCoolForm">
                 <div class="col-md-12">
                     <button type="button" id="btnNuevo" class="btn btn-default"><i class="glyphicon glyphicon-plus"></i>  Nuevo</button>
                 </div>
                 <div class="col-md-3">
                     <label>Tablas</label>
                    <select id="Tablas" multiple="multiple" class="form-control altoSelect" >
                    </select>
                 </div>
                 <div class="col-md-1">
                     <br />
                     <br />
                     <button type="button" id="btnPasart" class="btn btn-default"><i class="glyphicon glyphicon-forward"></i></button>
                 </div>
                 <div class="col-md-3">
                    <label>Relación</label>
                    <select multiple="multiple" id="relacion" class="form-control altoSelect" >
                    </select>
                 </div>
                 <div class="col-md-1">
                     <br />
                     <br />
                     <button type="button" id="btnPasarR" class="btn btn-default"><i class="glyphicon glyphicon-forward"></i></button>
                     <br />
                     <br />
                     <button type="button" id="btnRegresaR" class="btn btn-default"><i class="glyphicon glyphicon-backward"></i></button>
                </div>
                 <div class="col-md-3">
                    <label>Relación</label>
                    <select id="relacionadas" multiple="multiple" name="prueba" class="form-control altoSelect" >
                    </select>
                </div>
                 <div class="col-md-6">
                    <br />
                    <label>Descripcion</label>
                    <input id="txtDescricpion" type="text"  class="form-control" />
                </div>
                 <div class="col-md-6">
                     <br />
                    <br />
                    <br />
                     <button type="button" id="btnGuardar" class="btn btn-default"><i class="glyphicon glyphicon-floppy-saved"></i> Guardar</button>
                     <button type="button" id="btnCancelar" class="btn btn-default"><i class="glyphicon glyphicon-floppy-remove"></i> Cancelar</button>
                </div>
                 </form>
                </div>
            </div>

        </div>
    </div>
    <div class="row">
    <div class="col-md-10 col-md-offset-3">
       <div class="form-group">
          <div class="col-sm-6">
              <input id="hdID" type="hidden" />
           <div id="divData" class="table-responsive">                
           </div>
            <ul id="paging2" >
            </ul>
          </div>
       </div>
    </div>
    </div>
<div class="row">
   <div id="dialogEliminar" class="modal fade"><!-- /.modal -->
    <div class="modal-dialog modal-sm"><!-- /.modal-dialog -->
    <div class="modal-content"><!-- /.modal-content -->
      <div class="modal-header">
       <h4 class="modal-title">Eliminar !!</h4>
      </div>
      <div class="modal-body">
        <p>¿Esta seguro de eliminar este registro?&hellip;</p>
      </div>
      <div class="modal-footer">
        <button type="button" class="btn btn-default" data-dismiss="modal">No</button>
        <button  id="Delete" type="button" class="btn btn-primary">Si</button>
      </div>
    </div><!-- /.modal-content -->
    </div><!-- /.modal-dialog -->
   </div><!-- /.modal -->
</div>
    <script src='<%=ResolveUrl("~/Scripts/TablasRelaciones.js")%>'></script>
</asp:Content>
