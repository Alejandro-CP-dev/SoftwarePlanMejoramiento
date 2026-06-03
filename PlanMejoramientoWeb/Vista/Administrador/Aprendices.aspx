<%@ Page Title="" Language="C#" MasterPageFile="~/Vista/Site1.Master" AutoEventWireup="true" CodeBehind="Aprendices.aspx.cs" Inherits="PlanMejoramientoWeb.Vista.Administrador.Aprendices" %>

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
            .field input[type=email],
            .field input[type=password],
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

                .field input:focus, .field select:focus {
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

        .gv-aprendices {
            width: 100%;
            border-collapse: collapse;
            font-size: 13px;
            font-family: 'DM Sans', sans-serif;
        }

            .gv-aprendices th {
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

            .gv-aprendices td {
                padding: 10px 12px;
                color: #1a1a1a;
                border-bottom: 1px solid #f0f0ea;
                white-space: nowrap;
            }

            .gv-aprendices tbody tr:hover td {
                background: #fafaf8;
            }

            .gv-aprendices tbody tr:last-child td {
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

        .badge-En-Formacion {
            background: #E6F1FB;
            color: #0C447C;
        }

        .badge-Egresado {
            background: #EAF3DE;
            color: #27500A;
        }

        .badge-Cancelado {
            background: #fcebeb;
            color: #791F1F;
        }

        .btn-editar, .btn-eliminar, .btn-asignar {
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

        .btn-asignar {
            color: #185FA5;
        }

            .btn-asignar:hover {
                background: #E6F1FB;
            }

        .tbl-sep {
            color: #e0e0da;
            margin: 0 2px;
        }

        /* Modal */
        .modal-content {
            border-radius: 12px;
            border: 1px solid #e5e5e0;
        }

        .modal-header {
            border-bottom: 1px solid #f0f0ea;
            padding: 1rem 1.25rem;
        }

            .modal-header h5 {
                font-size: 15px;
                font-weight: 600;
                color: #1a1a1a;
                margin: 0;
            }

        .modal-body {
            padding: 1.25rem;
        }

        .modal-footer {
            border-top: 1px solid #f0f0ea;
            padding: 1rem 1.25rem;
        }

        .modal-label {
            font-size: 12px;
            font-weight: 500;
            color: #666;
            display: block;
            margin-bottom: 5px;
        }

        .modal-name {
            font-size: 14px;
            font-weight: 500;
            color: #1a1a1a;
            margin-bottom: 1rem;
            display: block;
        }

        .modal-select {
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

            .modal-select:focus {
                border-color: #0F6E56;
                box-shadow: 0 0 0 3px rgba(15,110,86,0.1);
                background: #fff;
            }

        .btn-asignar-modal {
            height: 36px;
            padding: 0 16px;
            border-radius: 8px;
            font-size: 13px;
            font-family: 'DM Sans', sans-serif;
            font-weight: 500;
            cursor: pointer;
            border: none;
            background: #0F6E56;
            color: #fff;
        }

            .btn-asignar-modal:hover {
                background: #085041;
            }
    </style>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

    <asp:HiddenField ID="hfIdAprendiz" runat="server" />

    <%-- Modal Asignar Ficha --%>
    <div class="modal fade" id="modalAsignarFicha" tabindex="-1">
        <div class="modal-dialog">
            <div class="modal-content">
                <div class="modal-header">
                    <h5>Asignar Ficha a Aprendiz</h5>
                </div>
                <div class="modal-body">
                    <span class="modal-label">Aprendiz</span>
                    <asp:Label ID="lblAprendiz" runat="server" CssClass="modal-name" />
                    <span class="modal-label">Ficha</span>
                    <asp:DropDownList ID="ddlFichaAsignar" runat="server" CssClass="modal-select" />
                </div>
                <div class="modal-footer">
                    <asp:Button ID="btnGuardarAsignacion" runat="server"
                        Text="✔ Asignar"
                        CssClass="btn-asignar-modal"
                        OnClick="btnGuardarAsignacion_Click" />
                </div>
            </div>
        </div>
    </div>

    <div class="pg-title">Gestión de Aprendices</div>
    <div class="pg-subtitle">Registra, edita y asigna fichas a aprendices</div>

    <%-- Formulario --%>
    <div class="card">
        <div class="card-title">Datos del aprendiz</div>
        <div class="form-grid">

            <div class="field">
                <label>Tipo de documento</label>
                <asp:DropDownList ID="ddlTipoDocumento" runat="server">
                    <asp:ListItem Value="">-- Seleccione --</asp:ListItem>
                    <asp:ListItem Value="CC">CC — Cédula de Ciudadanía</asp:ListItem>
                    <asp:ListItem Value="TI">TI — Tarjeta de Identidad</asp:ListItem>
                    <asp:ListItem Value="CE">CE — Cédula Extranjería</asp:ListItem>
                </asp:DropDownList>
            </div>

            <div class="field">
                <label for="<%= txtNumeroDocumento.ClientID %>">Número de documento</label>
                <asp:TextBox ID="txtNumeroDocumento" runat="server" placeholder="Ej: 1234567890" />
            </div>

            <div class="field">
                <label for="<%= txtNombre.ClientID %>">Nombre</label>
                <asp:TextBox ID="txtNombre" runat="server" placeholder="Nombres" />
            </div>

            <div class="field">
                <label for="<%= txtApellido.ClientID %>">Apellido</label>
                <asp:TextBox ID="txtApellido" runat="server" placeholder="Apellidos" />
            </div>

            <div class="field">
                <label for="<%= txtCorreo.ClientID %>">Correo electrónico</label>
                <asp:TextBox ID="txtCorreo" runat="server" placeholder="correo@sena.edu.co" />
            </div>

            <div class="field">
                <label for="<%= txtTelefono.ClientID %>">Teléfono</label>
                <asp:TextBox ID="txtTelefono" runat="server" placeholder="Ej: 3001234567" />
            </div>

            <div class="field">
                <label for="<%= txtContrasena.ClientID %>">Contraseña</label>
                <asp:TextBox ID="txtContrasena" runat="server" TextMode="Password" placeholder="••••••••" />
            </div>

            <div class="field">
                <label>Estado académico</label>
                <asp:DropDownList ID="ddlEstadoAcademico" runat="server" />
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
        <div class="card-title">Aprendices registrados</div>
        <div class="gv-wrapper">
            <asp:GridView ID="gvAprendices" runat="server"
                AutoGenerateColumns="false"
                CssClass="gv-aprendices"
                GridLines="None">
                <Columns>
                    <asp:BoundField DataField="TipoDocumento" HeaderText="Tipo Doc." />
                    <asp:BoundField DataField="NumeroDocumento" HeaderText="Documento" />
                    <asp:BoundField DataField="Nombre" HeaderText="Nombre" />
                    <asp:BoundField DataField="Apellido" HeaderText="Apellido" />
                    <asp:BoundField DataField="Correo" HeaderText="Correo" />
                    <asp:BoundField DataField="Telefono" HeaderText="Teléfono" />

                    <asp:TemplateField HeaderText="Estado Académico">
                        <ItemTemplate>
                            <span class="badge badge-<%# Eval("EstadoAcademico.Nombre")?.ToString().Replace(" ","-") %>">
                                <%# Eval("EstadoAcademico.Nombre") %>
                            </span>
                        </ItemTemplate>
                    </asp:TemplateField>

                    <asp:TemplateField HeaderText="Acciones">
                        <ItemTemplate>
                            <asp:LinkButton ID="btnEditar" runat="server"
                                Text="✎ Editar" CssClass="btn-editar"
                                CommandArgument='<%# Eval("Id") %>'
                                OnClick="btnEditar_Click" />
                            <span class="tbl-sep">|</span>
                            <asp:LinkButton ID="btnEliminar" runat="server"
                                Text="✕ Eliminar" CssClass="btn-eliminar"
                                CommandArgument='<%# Eval("Id") %>'
                                OnClick="btnEliminar_Click"
                                OnClientClick="return confirm('¿Eliminar este aprendiz?');" />
                            <span class="tbl-sep">|</span>
                            <asp:LinkButton ID="btnAsignarFicha" runat="server"
                                Text="＋ Asignar Ficha" CssClass="btn-asignar"
                                CommandArgument='<%# Eval("Id") %>'
                                OnClick="btnAsignarFicha_Click" />
                        </ItemTemplate>
                    </asp:TemplateField>
                </Columns>
            </asp:GridView>
        </div>
    </div>

</asp:Content>
