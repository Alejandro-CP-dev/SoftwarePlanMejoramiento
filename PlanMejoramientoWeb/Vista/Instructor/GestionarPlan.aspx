<%@ Page Title="" Language="C#" MasterPageFile="~/Vista/Site1.Master" AutoEventWireup="true" CodeBehind="GestionarPlan.aspx.cs" Inherits="PlanMejoramientoWeb.Vista.Instructor.GestionarPlan" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style>
        .aprendiz-header {
            background: #fff;
            border-radius: 12px;
            border: 1px solid #e5e5e0;
            padding: 1.25rem 1.5rem;
            margin-bottom: 1.25rem;
            display: flex;
            align-items: center;
            gap: 1.25rem;
        }

        .avatar-circle {
            width: 56px;
            height: 56px;
            border-radius: 50%;
            background: #0F6E56;
            color: #fff;
            display: flex;
            align-items: center;
            justify-content: center;
            font-family: 'DM Serif Display', serif;
            font-size: 1.4rem;
            flex-shrink: 0;
        }

        .aprendiz-nombre {
            font-weight: 700;
            font-size: 1.1rem;
            color: #1a1a1a;
        }

        .aprendiz-meta {
            font-size: .85rem;
            color: #777;
            margin-top: 2px;
        }

        .badge-estado {
            display: inline-block;
            padding: 3px 12px;
            border-radius: 20px;
            font-size: .78rem;
            font-weight: 600;
            margin-top: 6px;
        }

        .badge-activo {
            background: #d1fae5;
            color: #065f46;
        }

        .badge-cancelado {
            background: #fee2e2;
            color: #991b1b;
        }

        .badge-otro {
            background: #e5e7eb;
            color: #374151;
        }

        .plan-card {
            background: #fff;
            border-radius: 12px;
            border: 1px solid #e5e5e0;
            padding: 1.1rem 1.25rem;
            margin-bottom: .9rem;
            display: flex;
            align-items: center;
            justify-content: space-between;
            flex-wrap: wrap;
            gap: .75rem;
        }

        .plan-info {
            flex: 1;
            min-width: 220px;
        }

        .plan-nombre {
            font-weight: 700;
            color: #1a1a1a;
        }

        .plan-meta {
            font-size: .82rem;
            color: #777;
            margin-top: 2px;
        }

        .tag-tipo {
            display: inline-block;
            padding: 2px 10px;
            border-radius: 20px;
            font-size: .75rem;
            font-weight: 600;
            margin-right: 6px;
        }

        .tag-interno {
            background: #d1fae5;
            color: #065f46;
        }

        .tag-comite {
            background: #fef3c7;
            color: #92400e;
        }

        .tag-estado {
            display: inline-block;
            padding: 2px 10px;
            border-radius: 20px;
            font-size: .75rem;
            font-weight: 600;
        }

        .tag-Activo {
            background: #dbeafe;
            color: #1e40af;
        }

        .tag-Aprobado {
            background: #d1fae5;
            color: #065f46;
        }

        .tag-NoAprobado {
            background: #fee2e2;
            color: #991b1b;
        }

        .plan-actions a {
            margin-left: .4rem;
        }

        .empty-msg {
            text-align: center;
            color: #999;
            padding: 2rem;
            font-size: .9rem;
        }
    </style>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

    <div style="padding: 1.5rem;">

        <h4 style="font-family: 'DM Serif Display',serif; color: #0F6E56; margin-bottom: 1.25rem;">
            <i class="ti ti-user-cog" style="font-size: 1.4rem; vertical-align: middle; margin-right: 6px;"></i>
            Gestionar Plan del Aprendiz
        </h4>

        <!-- Cabecera del aprendiz -->
        <div class="aprendiz-header">
            <div class="avatar-circle">
                <asp:Label ID="lblIniciales" runat="server"></asp:Label>
            </div>
            <div>
                <div class="aprendiz-nombre">
                    <asp:Label ID="lblNombreCompleto" runat="server"></asp:Label>
                </div>
                <div class="aprendiz-meta">
                    Documento:
                    <asp:Label ID="lblDocumento" runat="server"></asp:Label>
                    &nbsp;|&nbsp;
                    Correo:
                    <asp:Label ID="lblCorreo" runat="server"></asp:Label>
                </div>
                <asp:Label ID="lblEstadoAcademico" runat="server" CssClass="badge-estado"></asp:Label>
            </div>
        </div>

        <!-- Acciones rápidas -->
        <div style="margin-bottom: 1rem;">
            <a href="CrearPlan.aspx" class="btn btn-success btn-sm">
                <i class="ti ti-plus"></i>Crear nuevo plan para este aprendiz
            </a>
        </div>

        <!-- Lista de planes -->
        <h6 style="color: #0F6E56; font-weight: 700; margin-bottom: .75rem;">Planes de Mejoramiento</h6>

        <asp:Repeater ID="rptPlanes" runat="server" OnItemCommand="rptPlanes_ItemCommand">
            <ItemTemplate>
                <div class="plan-card">
                    <div class="plan-info">
                        <div class="plan-nombre">
                            <span class='<%# Eval("PlanMejoramiento.TipoPlan.CssTag") %>'>
                                <%# Eval("PlanMejoramiento.TipoPlan.Nombre") %>
                            </span>
                            <%# Eval("PlanMejoramiento.Nombre") %>
                        </div>
                        <div class="plan-meta">
                            Fecha límite: <%# Eval("PlanMejoramiento.FechaLimite", "{0:dd/MM/yyyy}") %>
                            &nbsp;|&nbsp;
                            Asignado: <%# Eval("FechaAsignacion", "{0:dd/MM/yyyy}") %>
                        </div>
                        <div style="margin-top: 6px;">
                            <span class='<%# "tag-estado tag-" + Eval("Estado").ToString().Replace(" ", "") %>'>
                                <%# Eval("Estado") %>
                            </span>
                        </div>
                    </div>
                    <div class="plan-actions">
                        <asp:LinkButton
                            runat="server"
                            CssClass="btn btn-outline-success btn-sm"
                            CommandName="Editar"
                            CommandArgument='<%# Eval("PlanMejoramiento.Id") %>'>
                            <i class="ti ti-edit"></i> Editar
                        </asp:LinkButton>
                        <asp:LinkButton
                            runat="server"
                            CssClass="btn btn-success btn-sm"
                            CommandName="Evaluar"
                            CommandArgument='<%# Eval("PlanMejoramiento.Id") %>'
                            Visible='<%# Eval("Estado").ToString() == "Activo" %>'>
                            <i class="ti ti-clipboard-check"></i> Evaluar
                        </asp:LinkButton>
                    </div>
                </div>
            </ItemTemplate>
        </asp:Repeater>

        <asp:Panel ID="pnlSinPlanes" runat="server" Visible="false">
            <div class="empty-msg">
                <i class="ti ti-clipboard-off" style="font-size: 1.5rem;"></i>
                <br />
                Este aprendiz no tiene planes de mejoramiento asignados.
            </div>
        </asp:Panel>

    </div>

</asp:Content>
