<%@ Page Title="" Language="C#" MasterPageFile="~/Vista/Site1.Master" AutoEventWireup="true" CodeBehind="Programas.aspx.cs" Inherits="PlanMejoramientoWeb.Vista.Administrador.Programas" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style>
        .pg-header {
            display: flex;
            align-items: center;
            justify-content: space-between;
            margin-bottom: 1.5rem;
        }

        .pg-title {
            font-size: 20px;
            font-weight: 600;
            color: #1a1a1a;
        }

        .pg-subtitle {
            font-size: 13px;
            color: #888;
            margin-top: 2px;
        }

        .card {
            background: #fff;
            border: 1px solid #e5e5e0;
            border-radius: 12px;
            padding: 1.25rem 1.5rem;
            margin-bottom: 1.25rem;
        }

        .card-title {
            font-size: 11px;
            font-weight: 500;
            color: #888;
            margin-bottom: 1rem;
            text-transform: uppercase;
            letter-spacing: 0.05em;
        }

        .form-grid {
            display: grid;
            grid-template-columns: 1fr 1fr 1fr;
            gap: 12px;
        }

        .form-grid-wide {
            grid-column: span 2;
        }

        .field {
            display: flex;
            flex-direction: column;
            gap: 5px;
        }

            .field label {
                font-size: 12px;
                font-weight: 500;
                color: #666;
            }

            .field input[type=text],
            .field select {
                height: 38px;
                padding: 0 10px;
                font-size: 13px;
                font-family: 'DM Sans', sans-serif;
                background: #f8f8f6;
                border: 1px solid #e0e0da;
                border-radius: 8px;
                color: #1a1a1a;
                outline: none;
                width: 100%;
            }

                .field input[type=text]:focus,
                .field select:focus {
                    border-color: #0F6E56;
                    box-shadow: 0 0 0 3px rgba(15,110,86,0.1);
                    background: #fff;
                }

        .btn-row {
            display: flex;
            gap: 8px;
            margin-top: 1rem;
        }

        .btn-guardar, .btn-actualizar, .btn-limpiar {
            height: 36px;
            padding: 0 16px;
            border-radius: 8px;
            font-size: 13px;
            font-family: 'DM Sans', sans-serif;
            font-weight: 500;
            cursor: pointer;
            border: none;
        }

        .btn-guardar {
            background: #0F6E56;
            color: #fff;
        }

            .btn-guardar:hover {
                background: #085041;
            }

        .btn-actualizar {
            background: #fff;
            color: #444;
            border: 1px solid #e0e0da;
        }

            .btn-actualizar:hover {
                background: #f4f4f0;
            }

        .btn-limpiar {
            background: #f4f4f0;
            color: #666;
        }

            .btn-limpiar:hover {
                background: #e8e8e4;
            }

        /* GridView */
        .gv-wrapper {
            overflow-x: auto;
        }

        .gv-programas {
            width: 100%;
            border-collapse: collapse;
            font-size: 13px;
            font-family: 'DM Sans', sans-serif;
        }

            .gv-programas th {
                text-align: left;
                padding: 8px 12px;
                font-size: 11px;
                font-weight: 500;
                color: #888;
                text-transform: uppercase;
                letter-spacing: 0.05em;
                border-bottom: 1px solid #e5e5e0;
            }

            .gv-programas td {
                padding: 10px 12px;
                color: #1a1a1a;
                border-bottom: 1px solid #f0f0ea;
            }

            .gv-programas tbody tr:hover td {
                background: #fafaf8;
            }

            .gv-programas tbody tr:last-child td {
                border-bottom: none;
            }

        .badge {
            display: inline-flex;
            align-items: center;
            font-size: 11px;
            font-weight: 500;
            padding: 3px 9px;
            border-radius: 20px;
        }

        .badge-Activo {
            background: #E1F5EE;
            color: #085041;
        }

        .badge-Inactivo {
            background: #f1efe8;
            color: #5f5e5a;
        }

        .btn-editar, .btn-eliminar {
            font-size: 12px;
            background: none;
            border: none;
            cursor: pointer;
            padding: 4px 8px;
            border-radius: 6px;
            font-family: 'DM Sans', sans-serif;
            font-weight: 500;
        }

        .btn-editar {
            color: #0F6E56;
        }

            .btn-editar:hover {
                background: #E1F5EE;
            }

        .btn-eliminar {
            color: #a32d2d;
        }

            .btn-eliminar:hover {
                background: #fcebeb;
            }

        .tbl-sep {
            color: #e0e0da;
            margin: 0 2px;
        }
    </style>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

    <asp:HiddenField ID="hfIdPrograma" runat="server" />

    <div class="pg-header">
        <div>
            <div class="pg-title">Gestión de Programas</div>
            <div class="pg-subtitle">Registra, edita y elimina programas de formación</div>
        </div>
    </div>

    <%-- Formulario --%>
    <div class="card">
        <div class="card-title">Datos del programa</div>
        <div class="form-grid">

            <div class="field">
                <label for="<%= txtCodigo.ClientID %>">Código</label>
                <asp:TextBox ID="txtCodigo" runat="server" placeholder="Ej: 228106" />
            </div>

            <div class="field form-grid-wide">
                <label for="<%= txtNombre.ClientID %>">Nombre</label>
                <asp:TextBox ID="txtNombre" runat="server" placeholder="Nombre del programa" />
            </div>

            <div class="field">
                <label for="<%= txtVersion.ClientID %>">Versión</label>
                <asp:TextBox ID="txtVersion" runat="server" placeholder="Ej: 1" />
            </div>

            <div class="field">
                <label for="<%= txtDuracion.ClientID %>">Duración (horas)</label>
                <asp:TextBox ID="txtDuracion" runat="server" placeholder="Ej: 2200" />
            </div>

            <div class="field">
                <label for="<%= ddlNivelFormacion.ClientID %>">Nivel de formación</label>
                <asp:DropDownList ID="ddlNivelFormacion" runat="server" />
            </div>

            <div class="field">
                <label for="<%= ddlEstado.ClientID %>">Estado</label>
                <asp:DropDownList ID="ddlEstado" runat="server">
                    <asp:ListItem Text="Activo" />
                    <asp:ListItem Text="Inactivo" />
                </asp:DropDownList>
            </div>

        </div>

        <div class="btn-row">
            <asp:Button ID="btnGuardar" runat="server" Text="💾 Guardar" CssClass="btn-guardar" OnClick="btnGuardar_Click" />
            <asp:Button ID="btnActualizar" runat="server" Text="↻ Actualizar" CssClass="btn-actualizar" OnClick="btnActualizar_Click" />
            <asp:Button ID="btnLimpiar" runat="server" Text="✕ Limpiar" CssClass="btn-limpiar" OnClick="btnLimpiar_Click" />
        </div>
    </div>

    <%-- Tabla --%>
    <div class="card">
        <div class="card-title">Programas registrados</div>
        <div class="gv-wrapper">
            <asp:GridView
                ID="gvProgramas"
                runat="server"
                AutoGenerateColumns="false"
                DataKeyNames="Id"
                CssClass="gv-programas"
                GridLines="None">

                <Columns>
                    <asp:BoundField DataField="Codigo" HeaderText="Código" />
                    <asp:BoundField DataField="Nombre" HeaderText="Nombre" />
                    <asp:BoundField DataField="Version" HeaderText="Versión" />
                    <asp:BoundField DataField="Duracion" HeaderText="Duración" />

                    <asp:TemplateField HeaderText="Nivel">
                        <ItemTemplate><%# Eval("NivelFormacion.Nombre") %></ItemTemplate>
                    </asp:TemplateField>

                    <asp:TemplateField HeaderText="Estado">
                        <ItemTemplate>
                            <span class="badge badge-<%# Eval("Estado") %>">
                                <%# Eval("Estado") %>
                            </span>
                        </ItemTemplate>
                    </asp:TemplateField>

                    <asp:TemplateField>
                        <ItemTemplate>
                            <asp:LinkButton ID="btnEditar" runat="server"
                                Text="✎ Editar"
                                CssClass="btn-editar"
                                CommandArgument='<%# Eval("Id") %>'
                                OnClick="btnEditar_Click" />
                            <span class="tbl-sep">|</span>
                            <asp:LinkButton ID="btnEliminar" runat="server"
                                Text="✕ Eliminar"
                                CssClass="btn-eliminar"
                                CommandArgument='<%# Eval("Id") %>'
                                OnClick="btnEliminar_Click"
                                OnClientClick="return confirm('¿Eliminar este programa?');" />
                        </ItemTemplate>
                    </asp:TemplateField>
                </Columns>

            </asp:GridView>
        </div>
    </div>

</asp:Content>
