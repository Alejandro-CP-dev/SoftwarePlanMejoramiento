<%@ Page Title="" Language="C#" MasterPageFile="~/Vista/Site1.Master" AutoEventWireup="true" CodeBehind="Inicio.aspx.cs" Inherits="PlanMejoramientoWeb.Vista.Administrador.Inicio" %>

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

        /* Tarjetas métricas */
        .metrics-grid {
            display: grid;
            grid-template-columns: repeat(4, 1fr);
            gap: 12px;
            margin-bottom: 1.25rem;
        }

        .metric-card {
            background: #fff;
            border: 1px solid #e5e5e0;
            border-radius: 12px;
            padding: 1.1rem 1.25rem;
            display: flex;
            flex-direction: column;
            gap: 6px;
        }

        .metric-icon {
            width: 36px;
            height: 36px;
            border-radius: 9px;
            display: flex;
            align-items: center;
            justify-content: center;
            margin-bottom: 4px;
        }

            .metric-icon i {
                font-size: 18px;
            }

        .metric-icon-green {
            background: #E1F5EE;
            color: #0F6E56;
        }

        .metric-icon-blue {
            background: #E6F1FB;
            color: #185FA5;
        }

        .metric-icon-amber {
            background: #FAEEDA;
            color: #854F0B;
        }

        .metric-icon-purple {
            background: #EEEDFE;
            color: #534AB7;
        }

        .metric-label {
            font-size: 12px;
            color: #888;
            font-weight: 500;
        }

        .metric-value {
            font-size: 28px;
            font-weight: 600;
            color: #1a1a1a;
            line-height: 1;
        }

        .metric-sub {
            font-size: 12px;
            color: #aaa;
        }

        /* Tarjetas de acceso rápido */
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

        .quick-grid {
            display: grid;
            grid-template-columns: repeat(4, 1fr);
            gap: 12px;
        }

        .quick-link {
            display: flex;
            flex-direction: column;
            align-items: center;
            gap: 10px;
            padding: 1.25rem 1rem;
            border: 1px solid #e5e5e0;
            border-radius: 10px;
            text-decoration: none;
            color: #1a1a1a;
            transition: border-color 0.15s, background 0.15s;
            text-align: center;
        }

            .quick-link:hover {
                border-color: #0F6E56;
                background: #f7fdf9;
                color: #0F6E56;
            }

            .quick-link i {
                font-size: 24px;
                color: #0F6E56;
            }

            .quick-link span {
                font-size: 13px;
                font-weight: 500;
            }

            .quick-link small {
                font-size: 11px;
                color: #aaa;
            }

        /* Tabla recientes */
        .gv-recientes {
            width: 100%;
            border-collapse: collapse;
            font-size: 13px;
            font-family: 'DM Sans', sans-serif;
        }

            .gv-recientes th {
                text-align: left;
                padding: 8px 12px;
                font-size: 11px;
                font-weight: 500;
                color: #888;
                text-transform: uppercase;
                letter-spacing: 0.05em;
                border-bottom: 1px solid #e5e5e0;
            }

            .gv-recientes td {
                padding: 10px 12px;
                color: #1a1a1a;
                border-bottom: 1px solid #f0f0ea;
            }

            .gv-recientes tbody tr:last-child td {
                border-bottom: none;
            }

            .gv-recientes tbody tr:hover td {
                background: #fafaf8;
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

        .badge-Activo {
            background: #E1F5EE;
            color: #085041;
        }

        .badge-Inactivo {
            background: #f1efe8;
            color: #5f5e5a;
        }

        .two-col {
            display: grid;
            grid-template-columns: 1fr 1fr;
            gap: 1.25rem;
        }

        @media (max-width: 900px) {
            .metrics-grid, .quick-grid {
                grid-template-columns: 1fr 1fr;
            }

            .two-col {
                grid-template-columns: 1fr;
            }
        }
    </style>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

    <div class="pg-title">Panel de administración</div>
    <div class="pg-subtitle">Resumen general del sistema de planes de mejoramiento</div>

    <%-- Métricas --%>
    <div class="metrics-grid">

        <div class="metric-card">
            <div class="metric-icon metric-icon-green">
                <i class="ti ti-books"></i>
            </div>
            <div class="metric-label">Programas</div>
            <div class="metric-value">
                <asp:Label ID="lblTotalProgramas" runat="server" Text="0" />
            </div>
            <div class="metric-sub">registrados en el sistema</div>
        </div>

        <div class="metric-card">
            <div class="metric-icon metric-icon-blue">
                <i class="ti ti-id-badge"></i>
            </div>
            <div class="metric-label">Fichas activas</div>
            <div class="metric-value">
                <asp:Label ID="lblTotalFichas" runat="server" Text="0" />
            </div>
            <div class="metric-sub">en formación actualmente</div>
        </div>

        <div class="metric-card">
            <div class="metric-icon metric-icon-amber">
                <i class="ti ti-users"></i>
            </div>
            <div class="metric-label">Instructores</div>
            <div class="metric-value">
                <asp:Label ID="lblTotalInstructores" runat="server" Text="0" />
            </div>
            <div class="metric-sub">activos en el sistema</div>
        </div>

        <div class="metric-card">
            <div class="metric-icon metric-icon-purple">
                <i class="ti ti-school"></i>
            </div>
            <div class="metric-label">Aprendices</div>
            <div class="metric-value">
                <asp:Label ID="lblTotalAprendices" runat="server" Text="0" />
            </div>
            <div class="metric-sub">matriculados actualmente</div>
        </div>

    </div>

    <%-- Acceso rápido --%>
    <div class="card">
        <div class="card-title">Acceso rápido</div>
        <div class="quick-grid">
            <a href="Programas.aspx" class="quick-link">
                <i class="ti ti-books"></i>
                <span>Programas</span>
                <small>Gestionar programas</small>
            </a>
            <a href="Ficha.aspx" class="quick-link">
                <i class="ti ti-id-badge"></i>
                <span>Fichas</span>
                <small>Gestionar fichas</small>
            </a>
            <a href="Instructores.aspx" class="quick-link">
                <i class="ti ti-users"></i>
                <span>Instructores</span>
                <small>Gestionar instructores</small>
            </a>
            <a href="Aprendices.aspx" class="quick-link">
                <i class="ti ti-school"></i>
                <span>Aprendices</span>
                <small>Gestionar aprendices</small>
            </a>
        </div>
    </div>

    <%-- Tablas recientes --%>
    <div class="two-col">

        <div class="card">
            <div class="card-title">Fichas recientes</div>
            <asp:GridView ID="gvFichasRecientes" runat="server"
                AutoGenerateColumns="false"
                CssClass="gv-recientes"
                GridLines="None">
                <Columns>
                    <asp:BoundField DataField="CodigoFicha" HeaderText="Código" />
                    <asp:TemplateField HeaderText="Programa">
                        <ItemTemplate><%# Eval("Programa.Nombre") %></ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Estado">
                        <ItemTemplate>
                            <span class="badge badge-<%# Eval("Estado") %>"><%# Eval("Estado") %></span>
                        </ItemTemplate>
                    </asp:TemplateField>
                </Columns>
            </asp:GridView>
        </div>

        <div class="card">
            <div class="card-title">Instructores recientes</div>
            <asp:GridView ID="gvInstructoresRecientes" runat="server"
                AutoGenerateColumns="false"
                CssClass="gv-recientes"
                GridLines="None">
                <Columns>
                    <asp:BoundField DataField="Nombre" HeaderText="Nombre" />
                    <asp:BoundField DataField="Apellido" HeaderText="Apellido" />
                    <asp:TemplateField HeaderText="Estado">
                        <ItemTemplate>
                            <span class="badge badge-<%# Eval("Estado") %>"><%# Eval("Estado") %></span>
                        </ItemTemplate>
                    </asp:TemplateField>
                </Columns>
            </asp:GridView>
        </div>

    </div>

</asp:Content>
