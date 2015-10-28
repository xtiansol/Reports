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
        <h2>Products</h2>
    <table>
    <thead>
        <tr><th>Name</th><th>Price</th></tr>
    </thead>
    <tbody id="products">
    </tbody>
    </table>
    <script src="../Scripts/Product.js"></script>
</asp:Content>
