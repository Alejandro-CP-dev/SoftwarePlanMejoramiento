<%@ Page Title="" Language="C#" MasterPageFile="~/Vista/Site1.Master" AutoEventWireup="true" CodeBehind="Inicio.aspx.cs" Inherits="PlanMejoramientoWeb.Vista.Gestores.Inicio" %>

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

        .gv-instructores {
            width: 100%;
            border-collapse: collapse;
            font-size: 13px;
            font-family: 'DM Sans', sans-serif;
        }

            .gv-instructores th {
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

            .gv-instructores td {
                padding: 10px 12px;
                color: #1a1a1a;
                border-bottom: 1px solid #f0f0ea;
                white-space: nowrap;
            }

            .gv-instructores tbody tr:hover td {
                background: #fafaf8;
            }

            .gv-instructores tbody tr:last-child td {
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

        .btn-Ver {
            color: black;
            text-decoration: none;
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
    <asp:HiddenField ID="hfIdSupervisor" runat="server" />
    <h2>Planes Del Centro</h2>

    <%-- Modal Asignar Supervisor --%>
    <div class="modal fade" id="modalAsignarSupervisor" tabindex="-1">
        <div class="modal-dialog">
            <div class="modal-content">
                <div class="modal-header">
                    <h5>Asignar Supervisor a Plan De Mejoramiento</h5>
                </div>
                <div class="modal-body">
                    <span class="modal-label">Plan de Mejoramiento</span>
                    <asp:Label ID="lblPlanMejoramiento" runat="server" CssClass="modal-name" />

                    <span class="modal-label">Supervisores</span>
                    <asp:DropDownList ID="ddlSupervisorAsignar" runat="server" CssClass="modal-select" />

                    <span class="modal-label" style="margin-top: 12px;">Indicación / Orientación</span>
                    <asp:TextBox ID="txtIndicacion" runat="server" TextMode="MultiLine" Rows="3" CssClass="modal-select" />
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



    <asp:GridView
        ID="gvPlanes"
        runat="server"
        AutoGenerateColumns="False"
        CssClass="table table-bordered">

        <Columns>

            <asp:TemplateField HeaderText="Documento">
                <ItemTemplate>
                    <%# Eval("Aprendiz.NumeroDocumento") %>
                </ItemTemplate>
            </asp:TemplateField>

            <asp:TemplateField HeaderText="Plan">
                <ItemTemplate>
                    <%# Eval("PlanMejoramiento.Nombre") %>
                </ItemTemplate>
            </asp:TemplateField>

            <asp:TemplateField HeaderText="Fecha Asignación">
                <ItemTemplate>
                    <%# Eval("PlanMejoramiento.FechaAsignacion", "{0:dd/MM/yyyy}") %>
                </ItemTemplate>
            </asp:TemplateField>

            <asp:TemplateField HeaderText="Fecha Límite">
                <ItemTemplate>
                    <%# Eval("PlanMejoramiento.FechaLimite", "{0:dd/MM/yyyy}") %>
                </ItemTemplate>
            </asp:TemplateField>

            <asp:TemplateField HeaderText="Acciones">
                <ItemTemplate>

                    <asp:LinkButton
                        ID="btnAsignarInstructor"
                        runat="server"
                        Text="Asignar Instructor"
                        CssClass="btn-asignar"
                        CommandArgument='<%# Eval("Id") %>'
                        OnClick="btnAsignarInstructor_Click">
                    </asp:LinkButton>

                </ItemTemplate>
            </asp:TemplateField>

        </Columns>

    </asp:GridView>
</asp:Content>
