<%@ Page Title="" Language="C#" MasterPageFile="~/Vista/Site1.Master" AutoEventWireup="true" CodeBehind="Programas.aspx.cs" Inherits="PlanMejoramientoWeb.Vista.Administrador.Programas" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <h2>Gestión de Programas</h2>

<asp:HiddenField
    ID="hfIdPrograma"
    runat="server" />

<table>

    <tr>
        <td>Código:</td>
        <td>
            <asp:TextBox
                ID="txtCodigo"
                runat="server" />
        </td>
    </tr>

    <tr>
        <td>Nombre:</td>
        <td>
            <asp:TextBox
                ID="txtNombre"
                runat="server"
                Width="300px" />
        </td>
    </tr>

    <tr>
        <td>Versión:</td>
        <td>
            <asp:TextBox
                ID="txtVersion"
                runat="server" />
        </td>
    </tr>

    <tr>
        <td>Duración:</td>
        <td>
            <asp:TextBox
                ID="txtDuracion"
                runat="server" />
        </td>
    </tr>

    <tr>
        <td>Nivel:</td>
        <td>
            <asp:DropDownList
                ID="ddlNivelFormacion"
                runat="server" />
        </td>
    </tr>

    <tr>
        <td>Estado:</td>
        <td>
            <asp:DropDownList
                ID="ddlEstado"
                runat="server">

                <asp:ListItem Text="Activo" />
                <asp:ListItem Text="Inactivo" />

            </asp:DropDownList>
        </td>
    </tr>

    <tr>
        <td colspan="2">

            <asp:Button
                ID="btnGuardar"
                runat="server"
                Text="Guardar"
                OnClick="btnGuardar_Click" />

            <asp:Button
                ID="btnActualizar"
                runat="server"
                Text="Actualizar"
                OnClick="btnActualizar_Click" />

            <asp:Button
                ID="btnLimpiar"
                runat="server"
                Text="Limpiar"
                OnClick="btnLimpiar_Click" />

        </td>
    </tr>

</table>

<br />

<asp:GridView
    ID="gvProgramas"
    runat="server"
    AutoGenerateColumns="false"
    DataKeyNames="Id">

    <Columns>

        <asp:BoundField
            DataField="Codigo"
            HeaderText="Código" />

        <asp:BoundField
            DataField="Nombre"
            HeaderText="Nombre" />

        <asp:BoundField
            DataField="Version"
            HeaderText="Versión" />

        <asp:BoundField
            DataField="Duracion"
            HeaderText="Duración" />

        <asp:TemplateField HeaderText="Nivel">

            <ItemTemplate>
                <%# Eval("NivelFormacion.Nombre") %>
            </ItemTemplate>

        </asp:TemplateField>

        <asp:BoundField
            DataField="Estado"
            HeaderText="Estado" />

        <asp:TemplateField>

            <ItemTemplate>

                <asp:LinkButton
                    ID="btnEditar"
                    runat="server"
                    Text="Editar"
                    CommandArgument='<%# Eval("Id") %>'
                    OnClick="btnEditar_Click" />

                |

               

                <asp:LinkButton
                    ID="btnEliminar"
                    runat="server"
                    Text="Eliminar"
                    CommandArgument='<%# Eval("Id") %>'
                    OnClick="btnEliminar_Click" />

            </ItemTemplate>

        </asp:TemplateField>

    </Columns>

</asp:GridView>
</asp:Content>
