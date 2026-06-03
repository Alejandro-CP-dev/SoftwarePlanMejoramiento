<%@ Page Title="" Language="C#" MasterPageFile="~/Vista/Site1.Master" AutoEventWireup="true" CodeBehind="Ficha.aspx.cs" Inherits="PlanMejoramientoWeb.Vista.Administrador.Ficha" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style>
        .pg-title {
            font-size: 20px;
            font-weight: 600;
            color: #1a1a1a;
        }

        .pg-subtitle {
            font-size: 13px;
            color: #888;
            margin-top: 2px;
            margin-bottom: 1.5rem;
        }

        .card {
            background: #fff;
            border: 1px solid #e5e5e0;
            border-radius: 12px;
            padding: 1.25rem 1.5rem;
            margin-bottom: 1.25rem;
            width: 100%;
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

        .form-grid-full {
            grid-column: span 3;
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
            .field input[type=date],
            .field select,
            .field textarea {
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

            .field input[type=text],
            .field input[type=date],
            .field select {
                height: 38px;
            }

            .field textarea {
                padding: 8px 10px;
                resize: vertical;
                min-height: 80px;
                line-height: 1.5;
            }

                .field input:focus,
                .field select:focus,
                .field textarea:focus {
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

        .gv-wrapper {
            overflow-x: auto;
        }

        .gv-fichas {
            width: 100%;
            border-collapse: collapse;
            font-size: 13px;
            font-family: 'DM Sans', sans-serif;
        }

            .gv-fichas th {
                text-align: left;
                padding: 8px 12px;
                font-size: 11px;
                font-weight: 500;
                color: #888;
                text-transform: uppercase;
                letter-spacing: 0.05em;
                border-bottom: 1px solid #e5e5e0;
                white-space: nowrap;
            }

            .gv-fichas td {
                padding: 10px 12px;
                color: #1a1a1a;
                border-bottom: 1px solid #f0f0ea;
                white-space: nowrap;
            }

            .gv-fichas tbody tr:hover td {
                background: #fafaf8;
            }

            .gv-fichas tbody tr:last-child td {
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

        .badge-Activa {
            background: #E1F5EE;
            color: #085041;
        }

        .badge-Terminada {
            background: #E6F1FB;
            color: #0C447C;
        }

        .badge-Cancelada {
            background: #fcebeb;
            color: #791F1F;
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

    <asp:HiddenField ID="hfIdFicha" runat="server" />

    <div class="pg-title">Gestión de Fichas</div>
    <div class="pg-subtitle">Registra, edita y elimina fichas de formación</div>

    <%-- Formulario --%>
    <div class="card">
        <div class="card-title">Datos de la ficha</div>
        <div class="form-grid">

            <div class="field">
                <label for="<%= txtCodigoFicha.ClientID %>">Código Ficha</label>
                <asp:TextBox ID="txtCodigoFicha" runat="server" placeholder="Ej: 2954786" />
            </div>

            <div class="field">
                <label for="<%= txtFechaInicio.ClientID %>">Fecha Inicio</label>
                <asp:TextBox ID="txtFechaInicio" runat="server" TextMode="Date" />
            </div>

            <div class="field">
                <label for="<%= txtFechaFinalizacion.ClientID %>">Fecha Finalización</label>
                <asp:TextBox ID="txtFechaFinalizacion" runat="server" TextMode="Date" />
            </div>

            <div class="field">
                <label for="<%= ddlPrograma.ClientID %>">Programa</label>
                <asp:DropDownList ID="ddlPrograma" runat="server" />
            </div>

            <div class="field">
                <label for="<%= ddlJornada.ClientID %>">Jornada</label>
                <asp:DropDownList ID="ddlJornada" runat="server" />
            </div>

            <div class="field">
                <label for="<%= ddlEstado.ClientID %>">Estado</label>
                <asp:DropDownList ID="ddlEstado" runat="server">
                    <asp:ListItem Text="Activa" Value="Activa" />
                    <asp:ListItem Text="Terminada" Value="Terminada" />
                    <asp:ListItem Text="Cancelada" Value="Cancelada" />
                </asp:DropDownList>
            </div>

            <div class="field form-grid-full">
                <label for="<%= txtDescripcion.ClientID %>">Descripción</label>
                <asp:TextBox ID="txtDescripcion" runat="server" TextMode="MultiLine" Rows="3" placeholder="Descripción opcional de la ficha..." />
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
        <div class="card-title">Fichas registradas</div>
        <div class="gv-wrapper">
            <asp:GridView
                ID="gvFichas"
                runat="server"
                AutoGenerateColumns="false"
                CssClass="gv-fichas"
                GridLines="None">

                <Columns>
                    <asp:BoundField DataField="CodigoFicha" HeaderText="Código" />
                    <asp:BoundField DataField="FechaInicio" HeaderText="Fecha Inicio" DataFormatString="{0:yyyy-MM-dd}" />
                    <asp:BoundField DataField="FechaFinalizacion" HeaderText="Fecha Fin" DataFormatString="{0:yyyy-MM-dd}" />

                    <asp:TemplateField HeaderText="Programa">
                        <ItemTemplate><%# Eval("Programa.Nombre") %></ItemTemplate>
                    </asp:TemplateField>

                    <asp:TemplateField HeaderText="Jornada">
                        <ItemTemplate><%# Eval("Jornada.Nombre") %></ItemTemplate>
                    </asp:TemplateField>

                    <asp:TemplateField HeaderText="Estado">
                        <ItemTemplate>
                            <span class="badge badge-<%# Eval("Estado") %>">
                                <%# Eval("Estado") %>
                            </span>
                        </ItemTemplate>
                    </asp:TemplateField>

                    <asp:BoundField DataField="Descripcion" HeaderText="Descripción" />

                    <asp:TemplateField HeaderText="Acciones">
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
                                OnClientClick="return confirm('¿Eliminar esta ficha?');" />
                        </ItemTemplate>
                    </asp:TemplateField>
                </Columns>

            </asp:GridView>
        </div>
    </div>

</asp:Content>
