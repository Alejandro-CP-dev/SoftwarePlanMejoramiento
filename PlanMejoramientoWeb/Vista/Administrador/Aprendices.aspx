<%@ Page Title="" Language="C#" MasterPageFile="~/Vista/Site1.Master" AutoEventWireup="true" CodeBehind="Aprendices.aspx.cs" Inherits="PlanMejoramientoWeb.Vista.Administrador.Aprendices" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <h2>Gestión de Aprendices</h2>

    <asp:HiddenField ID="hfIdAprendiz" runat="server" />

    <div class="row">

        <div class="col-md-4">
            <label>Tipo Documento</label>
            <asp:DropDownList ID="ddlTipoDocumento" runat="server" CssClass="form-control">
                <asp:ListItem Text="Seleccione" Value=""></asp:ListItem>
                <asp:ListItem Text="CC" Value="CC"></asp:ListItem>
                <asp:ListItem Text="TI" Value="TI"></asp:ListItem>
                <asp:ListItem Text="CE" Value="CE"></asp:ListItem>
            </asp:DropDownList>
        </div>

        <div class="col-md-4">
            <label>Número Documento</label>
            <asp:TextBox ID="txtNumeroDocumento" runat="server" CssClass="form-control"></asp:TextBox>
        </div>

        <div class="col-md-4">
            <label>Nombre</label>
            <asp:TextBox ID="txtNombre" runat="server" CssClass="form-control"></asp:TextBox>
        </div>

    </div>

    <br />

    <div class="row">

        <div class="col-md-4">
            <label>Apellido</label>
            <asp:TextBox ID="txtApellido" runat="server" CssClass="form-control"></asp:TextBox>
        </div>

        <div class="col-md-4">
            <label>Correo</label>
            <asp:TextBox ID="txtCorreo" runat="server" CssClass="form-control"></asp:TextBox>
        </div>

        <div class="col-md-4">
            <label>Teléfono</label>
            <asp:TextBox ID="txtTelefono" runat="server" CssClass="form-control"></asp:TextBox>
        </div>

    </div>

    <br />

    <div class="row">

        <div class="col-md-4">
            <label>Contraseña</label>
            <asp:TextBox ID="txtContrasena" runat="server"
                TextMode="Password"
                CssClass="form-control"></asp:TextBox>
        </div>

        <div class="col-md-4">
            <label>Estado Académico</label>
            <asp:DropDownList ID="ddlEstadoAcademico"
                runat="server"
                CssClass="form-control">
            </asp:DropDownList>
        </div>

    </div>

    <br />

    <div class="row">

        <div class="col-md-12">

            <asp:Button ID="btnGuardar"
                runat="server"
                Text="Guardar"
                CssClass="btn btn-success"
                OnClick="btnGuardar_Click" />

            <asp:Button ID="btnActualizar"
                runat="server"
                Text="Actualizar"
                CssClass="btn btn-primary"
                OnClick="btnActualizar_Click" />

            <asp:Button ID="btnLimpiar"
                runat="server"
                Text="Limpiar"
                CssClass="btn btn-secondary"
                OnClick="btnLimpiar_Click" />

        </div>

    </div>

    <hr />

    <asp:GridView ID="gvAprendices"
        runat="server"
        AutoGenerateColumns="False"
        CssClass="table table-bordered table-striped">

        <Columns>

            <asp:BoundField DataField="Id" HeaderText="ID" />

            <asp:BoundField DataField="TipoDocumento" HeaderText="Tipo Doc." />

            <asp:BoundField DataField="NumeroDocumento" HeaderText="Documento" />

            <asp:BoundField DataField="Nombre" HeaderText="Nombre" />

            <asp:BoundField DataField="Apellido" HeaderText="Apellido" />

            <asp:BoundField DataField="Correo" HeaderText="Correo" />

            <asp:BoundField DataField="Telefono" HeaderText="Teléfono" />

            <asp:BoundField DataField="Estado" HeaderText="Estado" />

            <asp:TemplateField HeaderText="Estado Académico">
                <ItemTemplate>
                    <%# Eval("EstadoAcademico.Nombre") %>
                </ItemTemplate>
            </asp:TemplateField>

            <asp:TemplateField HeaderText="Acciones">

                <ItemTemplate>

                    <asp:LinkButton ID="btnEditar"
                        runat="server"
                        Text="Editar"
                        CssClass="btn btn-warning btn-sm"
                        CommandArgument='<%# Eval("Id") %>'
                        OnClick="btnEditar_Click" />

                    <asp:LinkButton ID="btnEliminar"
                        runat="server"
                        Text="Eliminar"
                        CssClass="btn btn-danger btn-sm"
                        CommandArgument='<%# Eval("Id") %>'
                        OnClick="btnEliminar_Click"
                        OnClientClick="return confirm('¿Desea eliminar este aprendiz?');" />

                </ItemTemplate>

            </asp:TemplateField>

        </Columns>

    </asp:GridView>
</asp:Content>
