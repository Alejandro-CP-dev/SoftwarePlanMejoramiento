<%@ Page Title="" Language="C#" MasterPageFile="~/Vista/Site1.Master" AutoEventWireup="true" CodeBehind="Ficha.aspx.cs" Inherits="PlanMejoramientoWeb.Vista.Administrador.Ficha" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <asp:HiddenField ID="hfIdFicha" runat="server" />

    <div class="card">
        <div class="card-header">
            <h4>Gestión de Fichas</h4>
        </div>

        <div class="card-body">

            <div class="row">

                <div class="col-md-4 mb-3">
                    <label>Código Ficha</label>
                    <asp:TextBox ID="txtCodigoFicha"
                        runat="server"
                        CssClass="form-control">
                    </asp:TextBox>
                </div>

                <div class="col-md-4 mb-3">
                    <label>Fecha Inicio</label>
                    <asp:TextBox ID="txtFechaInicio"
                        runat="server"
                        TextMode="Date"
                        CssClass="form-control">
                    </asp:TextBox>
                </div>

                <div class="col-md-4 mb-3">
                    <label>Fecha Finalización</label>
                    <asp:TextBox ID="txtFechaFinalizacion"
                        runat="server"
                        TextMode="Date"
                        CssClass="form-control">
                    </asp:TextBox>
                </div>

            </div>

            <div class="row">

                <div class="col-md-4 mb-3">
                    <label>Programa</label>
                    <asp:DropDownList ID="ddlPrograma"
                        runat="server"
                        CssClass="form-select">
                    </asp:DropDownList>
                </div>

                <div class="col-md-4 mb-3">
                    <label>Jornada</label>
                    <asp:DropDownList ID="ddlJornada"
                        runat="server"
                        CssClass="form-select">
                    </asp:DropDownList>
                </div>

                <div class="col-md-4 mb-3">
                    <label>Estado</label>
                    <asp:DropDownList ID="ddlEstado"
                        runat="server"
                        CssClass="form-select">

                        <asp:ListItem Text="Activa" Value="Activa"></asp:ListItem>
                        <asp:ListItem Text="Terminada" Value="Terminada"></asp:ListItem>
                        <asp:ListItem Text="Cancelada" Value="Cancelada"></asp:ListItem>

                    </asp:DropDownList>
                </div>

            </div>

            <div class="row">

                <div class="col-md-12 mb-3">
                    <label>Descripción</label>
                    <asp:TextBox ID="txtDescripcion"
                        runat="server"
                        TextMode="MultiLine"
                        Rows="3"
                        CssClass="form-control">
                    </asp:TextBox>
                </div>

            </div>

            <div class="mb-3">

                <asp:Button ID="btnGuardar"
                    runat="server"
                    Text="Guardar"
                    CssClass="btn btn-success"
                    OnClick="btnGuardar_Click" />

                <asp:Button ID="btnActualizar"
                    runat="server"
                    Text="Actualizar"
                    CssClass="btn btn-warning"
                    OnClick="btnActualizar_Click" />

                <asp:Button ID="btnLimpiar"
                    runat="server"
                    Text="Limpiar"
                    CssClass="btn btn-secondary"
                    OnClick="btnLimpiar_Click" />

            </div>

        </div>
    </div>

    <br />

    <asp:GridView ID="gvFichas"
        runat="server"
        AutoGenerateColumns="False"
        CssClass="table table-bordered table-hover">

        <Columns>

            <asp:BoundField DataField="Id"
                HeaderText="ID" />

            <asp:BoundField DataField="CodigoFicha"
                HeaderText="Código" />

            <asp:BoundField DataField="FechaInicio"
                HeaderText="Fecha Inicio"
                DataFormatString="{0:yyyy-MM-dd}" />

            <asp:BoundField DataField="FechaFinalizacion"
                HeaderText="Fecha Finalización"
                DataFormatString="{0:yyyy-MM-dd}" />

            <asp:BoundField DataField="Estado"
                HeaderText="Estado" />

            <asp:BoundField DataField="Descripcion"
                HeaderText="Descripción" />

            <asp:TemplateField HeaderText="Programa">
                <ItemTemplate>
                    <%# Eval("Programa.Nombre") %>
                </ItemTemplate>
            </asp:TemplateField>

            <asp:TemplateField HeaderText="Jornada">
                <ItemTemplate>
                    <%# Eval("Jornada.Nombre") %>
                </ItemTemplate>
            </asp:TemplateField>

            <asp:TemplateField HeaderText="Acciones">

                <ItemTemplate>

                    <asp:LinkButton ID="btnEditar"
                        runat="server"
                        Text="Editar"
                        CssClass="btn btn-primary btn-sm"
                        CommandArgument='<%# Eval("Id") %>'
                        OnClick="btnEditar_Click" />

                    <asp:LinkButton ID="btnEliminar"
                        runat="server"
                        Text="Eliminar"
                        CssClass="btn btn-danger btn-sm"
                        CommandArgument='<%# Eval("Id") %>'
                        OnClick="btnEliminar_Click" />

                </ItemTemplate>

            </asp:TemplateField>

        </Columns>

    </asp:GridView>
</asp:Content>
