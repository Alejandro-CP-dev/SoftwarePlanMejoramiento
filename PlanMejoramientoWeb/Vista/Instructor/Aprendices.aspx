<%@ Page Title="" Language="C#" MasterPageFile="~/Vista/Site1.Master" AutoEventWireup="true" CodeBehind="Aprendices.aspx.cs" Inherits="PlanMejoramientoWeb.Vista.Instructor.Aprendices" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <h2>Mis Aprendices</h2>

    <asp:GridView
        ID="gvAprendices"
        runat="server"
        AutoGenerateColumns="False"
        CssClass="table table-bordered">

        <Columns>

            <asp:BoundField
                DataField="NumeroDocumento"
                HeaderText="Documento" />

            <asp:BoundField
                DataField="Nombre"
                HeaderText="Nombre" />

            <asp:BoundField
                DataField="Apellido"
                HeaderText="Apellido" />

            <asp:BoundField
                DataField="Correo"
                HeaderText="Correo" />

            <asp:TemplateField HeaderText="Estado Académico">
                <ItemTemplate>
                    <%# Eval("EstadoAcademico.Nombre") %>
                </ItemTemplate>
            </asp:TemplateField>

            <asp:TemplateField HeaderText="Acciones">
                <ItemTemplate>

                    <asp:LinkButton
                        ID="btnVerPlanes"
                        runat="server"
                        Text="Gestionar Plan"
                        CssClass="btn btn-primary btn-sm"
                        CommandArgument='<%# Eval("Id") %>'
                        OnClick="btnVerPlanes_Click">
                    </asp:LinkButton>

                </ItemTemplate>
            </asp:TemplateField>

        </Columns>

    </asp:GridView>
</asp:Content>
