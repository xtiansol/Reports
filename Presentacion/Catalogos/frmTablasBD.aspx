<%@ Page Title="" Language="C#" MasterPageFile="~/PaginaMaestra/PageMaster.Master" AutoEventWireup="true" CodeBehind="frmTablasBD.aspx.cs" Inherits="Presentacion.Catalogos.frmTablasBD" %>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
        <p class="text-danger">
        <asp:Literal runat="server" ID="ErrorMessage" />
    </p>

    <div class="form-horizontal">
        <h4>Crear una nueva Tabla</h4>
        <hr />
        <asp:ValidationSummary runat="server" CssClass="text-danger" />
        <div class="form-group">
            <asp:Label runat="server" AssociatedControlID="txtNombre" CssClass="col-md-2 control-label">Nombre</asp:Label>
            <div class="col-md-10">
                <asp:TextBox runat="server" ID="txtNombre" CssClass="form-control" />
                <asp:RequiredFieldValidator runat="server" ControlToValidate="txtNombre"
                    CssClass="text-danger" ErrorMessage="El campo Nombre es obligatorio." />
            </div>
        </div>
        <div class="form-group">
            <asp:Label runat="server" AssociatedControlID="txtDescricpion" CssClass="col-md-2 control-label">Descripción</asp:Label>
            <div class="col-md-10">
                <asp:TextBox runat="server" ID="txtDescricpion"  CssClass="form-control" />
            </div>
        </div>
        <div class="form-group">
            <asp:Label runat="server" AssociatedControlID="txtTipoTabla" CssClass="col-md-2 control-label">Tipo Tabla</asp:Label>
            <div class="col-md-10">
                <asp:TextBox runat="server" ID="txtTipoTabla" CssClass="form-control" />
                <asp:RequiredFieldValidator runat="server" ControlToValidate="txtTipoTabla"
                    CssClass="text-danger" Display="Dynamic" ErrorMessage="El campo de Tipo Tabla es obligatorio." />
            </div>
        </div>
        <div class="form-group">
            <div class="col-md-offset-2 col-md-10">
                <asp:Button runat="server" Text="Registrarse" CssClass="btn btn-default" OnCommand="Unnamed7_Command" />
            </div>
        </div>
    </div>
    <div class="row">
	<div class="col-md-12 ">
	<div class="green-panel">
	<div class="green-header">
	   <h5><strong>CATALOGO TIPO MOVIMIENTO (SUA)</strong></h5>
	</div>
	</div>
	</div>
</div>
<div class="row">
    <div class="col-md-12">
        <button type="button" id="btnNuevo" class="btn btn-theme03"><i class="fa fa-1x fa-plus-circle"></i>  Nuevo</button>
        <button type="button" id="btnModificar" class="btn btn-theme03"><i class="fa fa-1x fa-edit"></i> Modificar</button>
        <button type="button" id="btnConsultar" class="btn btn-theme03"><i class="fa fa-1x fa-search"></i> Consultar</button>
    </div>
</div>
<div class="row">
    <div class="col-lg-12">
       <div id="divData" class="table-responsive">
       </div>
       <div id="paging" class="pager">
           <span id="totalRecords"></span>
       </div>
    </div>
</div>
    <script src="../Scripts/Tablas.js"></script>
</asp:Content>
