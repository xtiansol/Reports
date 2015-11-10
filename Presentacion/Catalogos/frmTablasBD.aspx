<%@ Page Title="" Language="C#" MasterPageFile="~/PaginaMaestra/PageMaster.Master" AutoEventWireup="true" CodeBehind="frmTablasBD.aspx.cs" Inherits="Presentacion.Catalogos.frmTablasBD" %>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
<div class="row">
<div class="col-md-5">
  <div class="panel panel-default">
    <div class="panel-heading">FORMULARIO DE REGISTRO DE DATOS</div>
    <div class="panel-body">
      <form class="form-horizontal" role="form">
        <div class="form-group">
          <div class="col-ms-3">
              <button type="button" id="btnNuevo" class="btn btn-default"><i class="glyphicon glyphicon-plus"></i> Nuevo</button>
              <form class="navbar-form" role="search">
                <div class="input-group">
                    <input type="text" class="form-control" placeholder="Search" name="q">
                    <div class="input-group-btn">
                        <button class="btn btn-default" type="submit"><i class="glyphicon glyphicon-search"></i></button>
                    </div>
                </div>
            </form>
          </div>
        </div>
          <div class="form-group">
            <Label class="col-ms-3 control-label">Nombre:</Label>
            <div class="col-ms-9">
                <input type="text" ID="txtNombre" class="form-control" />
            </div>
          </div>
          <div class="form-group">
            <Label  class="col-ms-3 control-label">Descripción</Label>
            <div class="col-ms-10">
                <input type="text" ID="txtDescricpion"  class="form-control" />
            </div>
          </div>
          <div class="form-group">
            <Label runat="server" class="col-ms-3 control-label">Tipo Tabla</Label>
            <div class="col-ms-10">
                <input type="text" ID="txtTipoTabla" class="form-control" />
            </div>
          </div>
          <div class="form-group">
        <button type="button" id="btnGuardar" class="btn btn-default"><i class="glyphicon glyphicon-floppy-saved"></i> Guardar</button>
        <button type="button" id="btnCancelar" class="btn btn-default"><i class="glyphicon glyphicon-floppy-remove"></i> Cancelar</button>
          </div>
     </form>
    </div>
  </div>
</div>

<div class="col-md-7">
  <div class="panel panel-default">
    <div class="panel-heading">DATOS ALMACENADOS</div>
    <div class="panel-body">
      <div class="form-horizontal" role="form">
       <div class="form-group">
          <div class="col-sm-12">
              <input id="hdID" type="hidden" />
           <div id="divData" class="table-responsive">                
           </div>
          </div>
          <div class="col-sm-8">
            <ul id="paging2" >
            </ul>
          </div>
       </div>
      </div>
    </div>
  </div>
</div>
</div>
<div class="row">
   <div id="dialogEliminar" class="modal fade">
    <div class="modal-dialog modal-sm">
    <div class="modal-content">
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
    <script src='<%=ResolveUrl("~/Scripts/Tablas.js")%>'></script>
    
</asp:Content>
