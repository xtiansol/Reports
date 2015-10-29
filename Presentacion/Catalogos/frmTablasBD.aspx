<%@ Page Title="" Language="C#" MasterPageFile="~/PaginaMaestra/PageMaster.Master" AutoEventWireup="true" CodeBehind="frmTablasBD.aspx.cs" Inherits="Presentacion.Catalogos.frmTablasBD" %>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
        <p class="text-danger">
        <asp:Literal runat="server" ID="ErrorMessage" />
    </p>

    
<div class="row">
<div class="col-md-6">
  <div class="panel panel-info">
    <div class="panel-heading">Bootstrap: Horizontal Form</div>
    <div class="panel-body">
      <div class="form-horizontal" role="form">
  <div class="form-group">
    <div class="col-sm-12">
       <div id="divData" class="table-responsive">
       </div>
    </div>
      <div class="col-sm-6">
       <div id="paging" class="pager">
           <span id="totalRecords"></span>
       </div>
     </div>
  </div>
</div>
    </div>
  </div>
</div>

<div class="col-md-6">
  <div class="panel panel-info">
    <div class="panel-heading">Bootstrap: Horizontal Form</div>
    <div class="panel-body">
      <form class="form-horizontal" role="form">
          <div class="form-group">
        <div class="col-ms-6">
        <button type="button" id="btnNuevo" class="btn btn-theme03"><i class="fa fa-1x fa-plus-circle"></i>  Nuevo</button>
        <button type="button" id="btnModificar" class="btn btn-theme03"><i class="fa fa-1x fa-edit"></i> Modificar</button>
        <button type="button" id="btnConsultar" class="btn btn-theme03"><i class="fa fa-1x fa-search"></i> Consultar</button>
        </div>
  </div>

          <div class="form-group">
            <asp:Label runat="server" AssociatedControlID="txtNombre" CssClass="col-ms-3 control-label">Nombre:</asp:Label>
            <div class="col-ms-9">
                <asp:TextBox runat="server" ID="txtNombre" CssClass="form-control" />
                <asp:RequiredFieldValidator runat="server" ControlToValidate="txtNombre"
                    CssClass="text-danger" ErrorMessage="El campo Nombre es obligatorio." />
            </div>
          </div>

          <div class="form-group">
            <asp:Label runat="server" CssClass="col-ms-3 control-label">Descripción</asp:Label>
            <div class="col-ms-10">
                <asp:TextBox runat="server" ID="txtDescricpion"  CssClass="form-control" />
            </div>
          </div>
          <div class="form-group">
            <asp:Label runat="server" AssociatedControlID="txtTipoTabla" CssClass="col-ms-3 control-label">Tipo Tabla</asp:Label>
            <div class="col-ms-10">
                <asp:TextBox runat="server" ID="txtTipoTabla" CssClass="form-control" />
                <asp:RequiredFieldValidator runat="server" ControlToValidate="txtTipoTabla"
                    CssClass="text-danger" Display="Dynamic" ErrorMessage="El campo de Tipo Tabla es obligatorio." />
            </div>
          </div>
     </form>
    </div>
  </div>
</div>
</div>
    <script src="../Scripts/Tablas.js"></script>
    <%--http://simple-form-bootstrap.plataformatec.com.br/examples?optionsRadios=option1&optionsRadios=option1--%>
</asp:Content>
