<%@ Page Title="" Language="C#" MasterPageFile="~/Vista/Site1.Master" AutoEventWireup="true" CodeBehind="CrearPlan.aspx.cs" Inherits="PlanMejoramientoWeb.Vista.Instructor.CrearPlan" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style>
        .tipo-selector {
            display: flex;
            gap: 1rem;
            margin-top: 6px;
        }

        .tipo-opt {
            flex: 1;
            border: 2px solid #e5e5e0;
            border-radius: 10px;
            padding: 0.9rem 1rem;
            cursor: pointer;
            display: flex;
            align-items: center;
            gap: 10px;
            transition: border-color .15s, background .15s;
        }

            .tipo-opt:hover {
                border-color: #0F6E56;
                background: #f0faf6;
            }

            .tipo-opt input[type=radio] {
                accent-color: #0F6E56;
                width: 17px;
                height: 17px;
            }

        .tipo-opt-interno .tipo-icon {
            color: #0F6E56;
            font-size: 1.3rem;
        }

        .tipo-opt-comite .tipo-icon {
            color: #b45309;
            font-size: 1.3rem;
        }

        .tipo-opt-label {
            font-weight: 600;
            font-size: .9rem;
            color: #1a1a1a;
        }

        .tipo-opt-desc {
            font-size: .8rem;
            color: #777;
        }
    </style>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

    <div style="padding: 1.5rem;">

        <h4 style="font-family: 'DM Serif Display',serif; color: #0F6E56; margin-bottom: 1.5rem;">
            <i class="ti ti-clipboard-plus" style="font-size: 1.4rem; vertical-align: middle; margin-right: 6px;"></i>
            Crear Plan de Mejoramiento
        </h4>

        <div class="card p-4" style="border-radius: 12px; border: 1px solid #e5e5e0;">

            <!-- Tipo de plan -->
            <div class="mb-3">
                <label style="font-weight: 600; color: #1a1a1a; display: block; margin-bottom: 6px;">Tipo de Plan</label>
                <div class="tipo-selector">
                    <label class="tipo-opt tipo-opt-interno">
                        <asp:RadioButton ID="rbInterno" runat="server" GroupName="TipoPlan" Checked="true" />
                        <i class="ti ti-clipboard-list tipo-icon"></i>
                        <div>
                            <div class="tipo-opt-label">Interno</div>
                            <div class="tipo-opt-desc">Primera oportunidad de recuperación</div>
                        </div>
                    </label>
                    <label class="tipo-opt tipo-opt-comite">
                        <asp:RadioButton ID="rbComite" runat="server" GroupName="TipoPlan" />
                        <i class="ti ti-gavel tipo-icon"></i>
                        <div>
                            <div class="tipo-opt-label">Por Comité</div>
                            <div class="tipo-opt-desc">Asignar tras reprobar el plan interno</div>
                        </div>
                    </label>
                </div>
            </div>

            <!-- Aprendiz -->
            <div class="mb-3">
                <label style="font-weight: 600; color: #1a1a1a;">Aprendiz</label>
                <asp:DropDownList ID="ddlAprendiz" runat="server" CssClass="form-control mt-1"></asp:DropDownList>
            </div>

            <!-- Nombre del plan -->
            <div class="mb-3">
                <label style="font-weight: 600; color: #1a1a1a;">Nombre del Plan</label>
                <asp:TextBox ID="txtNombrePlan" runat="server" CssClass="form-control mt-1" placeholder="Ej: Plan recuperación Lógica de Programación"></asp:TextBox>
            </div>

            <!-- Fecha límite -->
            <div class="mb-3">
                <label style="font-weight: 600; color: #1a1a1a;">Fecha Límite</label>
                <asp:TextBox ID="txtFechaLimite" runat="server" TextMode="Date" CssClass="form-control mt-1"></asp:TextBox>
            </div>

            <!-- Observación -->
            <div class="mb-3">
                <label style="font-weight: 600; color: #1a1a1a;">Observación Inicial</label>
                <asp:TextBox ID="txtObservacion" runat="server" TextMode="MultiLine" Rows="3" CssClass="form-control mt-1" placeholder="Descripción o motivo del plan..."></asp:TextBox>
            </div>

            <asp:Button
                ID="btnGuardar"
                runat="server"
                Text="Crear Plan"
                CssClass="btn btn-success"
                OnClick="btnGuardar_Click"
                Style="padding: .5rem 1.5rem;" />

            <asp:Panel ID="pnlMensaje" runat="server" Visible="false" Style="margin-top: 1rem;">
                <asp:Label ID="lblMensaje" runat="server"></asp:Label>
            </asp:Panel>

        </div>
    </div>

</asp:Content>
