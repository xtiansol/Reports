<%@ Page Title="" Language="C#" MasterPageFile="~/PaginaMaestra/PageMaster.Master" AutoEventWireup="true" CodeBehind="frmRelacionCampos.aspx.cs" Inherits="Presentacion.Catalogos.frmRelacionCampos" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
        <div class="row">
        <div class="col-md-12">
            <div class="panel panel-default">
                <div class="panel-heading">FORMULARIO DE REGISTRO DE DATOS</div>
                <div class="panel-body">
                <form id="mySuperCoolForm">
                 <div class="col-md-6">
                 <div class="col-md-6">
                     <button class="btn btn-default" id="btnNuevo" type="button"><i class="glyphicon glyphicon-plus"></i>  Nuevo</button>
                 </div>
                 <div class="col-md-12">
                 <div class="col-md-6">
                     <label>Tablas Principal</label>
                    <select class="form-control" id="Tablas">
                     </select>
                 </div>
                 <div class="col-md-6">
                    <label>Tablas Foraneas</label>
                    <select class="form-control" id="relacion">
                    </select>
                 </div>
                 </div>
                 <div class="col-md-12">
                   <div class="col-md-6">
                     <label>PK</label>
                    <select class="form-control" id="CamposP">
                    </select>
                   </div>
                   <div class="col-md-6">
                     <label>FK</label>
                    <select class="form-control" id="CamposF">
                    </select>
                   </div>
                 </div>
                 <div class="col-md-12">
                     <div class="col-xs-6 col-md-offset-5 btnAceptar">
                         <button class="btn btn-default" id="btnAceptar" type="button"><i class="glyphicon glyphicon-ok"></i> Aceptar</button>
                     </div>
                 </div>
                 </div>
                 <div class="col-md-6">
                    <div class="col-md-6">
                     <button type="button" id="btnGuardar" class="btn btn-default"><i class="glyphicon glyphicon-floppy-saved"></i> Guardar</button>
                     <button type="button" id="btnCancelar" class="btn btn-default"><i class="glyphicon glyphicon-floppy-remove"></i> Cancelar</button>
                    </div>
                    <div class="col-md-8">
                    <label>Relaciones De Campos</label>
                    <select id="CamposR" multiple="multiple"  class="form-control" >
                    </select>
                    </div>
                 </div>
                 </form>
                </div>
            </div>

        </div>

    </div>
    <div class="row">
       <div class="form-group">
          <div class="col-sm-6">
           <div id="divData" class="table-responsive">                
           </div>
            <ul id="paging2" >
            </ul>
          </div>
       </div>
    </div>

<div class="row">
   <div id="dialogEliminar" class="modal fade">
    <div class="modal-dialog modal-sm">
    <div class="modal-content">
      <div class="modal-header">
        <%--<button type="button" class="close" data-dismiss="modal" aria-label="Close"><span aria-hidden="true">&times;</span></button>--%>
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
    <script src="../Scripts/TablaCampos.js"></script>
</asp:Content>
