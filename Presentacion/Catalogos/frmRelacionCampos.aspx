<%@ Page Title="" Language="C#" MasterPageFile="~/PaginaMaestra/PageMaster.Master" AutoEventWireup="true" CodeBehind="frmRelacionCampos.aspx.cs" Inherits="Presentacion.Catalogos.frmRelacionCampos" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
        <div class="row">
        <div class="col-md-12">
            <div class="panel panel-info">
                <div class="panel-heading">Bootstrap: Horizontal Form</div>
                <div class="panel-body">
                <form id="mySuperCoolForm">
                 <div class="col-md-12">
                     <button type="button" id="btnNuevo" class="btn btn-theme03"><i class="glyphicon glyphicon-plus"></i>  Nuevo</button>
                 </div>
                 <div class="col-md-3">
                     <label>Tablas Principal</label>
                    <select id="Tablas"  class="form-control" >
                    </select>
                 </div>
                 <div class="col-md-3">
                    <label>Tablas Foraneas</label>
                    <select  id="relacion" class="form-control" >
                    </select>
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
