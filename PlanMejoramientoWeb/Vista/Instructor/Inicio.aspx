<%@ Page Title="" Language="C#" MasterPageFile="~/Vista/Site1.Master" AutoEventWireup="true" CodeBehind="Inicio.aspx.cs" Inherits="PlanMejoramientoWeb.Vista.Instructor.Inicio" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <h2>Mis Fichas</h2>

    <asp:GridView
        ID="gvFichas"
        runat="server"
        AutoGenerateColumns="False"
        CssClass="table table-bordered">

        <Columns>

            <asp:BoundField
                DataField="CodigoFicha"
                HeaderText="Código" />

            <asp:BoundField
                DataField="FechaInicio"
                HeaderText="Fecha Inicio" />

            <asp:BoundField
                DataField="FechaFinalizacion"
                HeaderText="Fecha Finalizacion" />

            <asp:TemplateField HeaderText="Jornada">
                <ItemTemplate>
                    <%# Eval("Jornada.Nombre") %>
                </ItemTemplate>
            </asp:TemplateField>

            <asp:BoundField
                DataField="Estado"
                HeaderText="Estado" />
        </Columns>

    </asp:GridView>
</asp:Content>
