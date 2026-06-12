<%@ Page Title="" Language="C#" MasterPageFile="~/Vista/Site1.Master" AutoEventWireup="true" CodeBehind="EvaluarPlan.aspx.cs" Inherits="PlanMejoramientoWeb.Vista.Instructor.EvaluarPlan" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style>
        .eval-card {
            background: #fff;
            border-radius: 12px;
            border: 1px solid #e5e5e0;
            padding: 1.5rem;
            margin-bottom: 1rem;
        }

        .criterio-row {
            display: flex;
            align-items: center;
            gap: 1rem;
            padding: 0.75rem 0;
            border-bottom: 1px solid #f0f0ec;
        }

            .criterio-row:last-child {
                border-bottom: none;
            }

        .criterio-label {
            width: 180px;
            font-weight: 600;
            color: #1a1a1a;
            font-size: 0.95rem;
        }

        .criterio-desc {
            flex: 1;
            color: #666;
            font-size: 0.85rem;
        }

        .criterio-radio {
            display: flex;
            gap: 1rem;
        }

        .radio-opt {
            display: flex;
            align-items: center;
            gap: 6px;
            cursor: pointer;
            font-size: 0.9rem;
        }

            .radio-opt input {
                accent-color: #0F6E56;
                width: 16px;
                height: 16px;
            }

        .aprueba-label {
            color: #0F6E56;
            font-weight: 600;
        }

        .noaprueba-label {
            color: #dc3545;
            font-weight: 600;
        }

        .info-plan {
            background: #f8fdf8;
            border-left: 4px solid #0F6E56;
            border-radius: 0 8px 8px 0;
            padding: 1rem 1.25rem;
            margin-bottom: 1.5rem;
        }

            .info-plan strong {
                color: #0F6E56;
            }

        .badge-interno {
            background: #d1fae5;
            color: #065f46;
            padding: 3px 10px;
            border-radius: 20px;
            font-size: 0.8rem;
            font-weight: 600;
        }

        .badge-comite {
            background: #fef3c7;
            color: #92400e;
            padding: 3px 10px;
            border-radius: 20px;
            font-size: 0.8rem;
            font-weight: 600;
        }

        .result-box {
            padding: 1rem 1.25rem;
            border-radius: 8px;
            font-weight: 600;
            margin-top: 1rem;
            display: none;
        }

        .result-ok {
            background: #d1fae5;
            color: #065f46;
            border: 1px solid #6ee7b7;
        }

        .result-err {
            background: #fee2e2;
            color: #991b1b;
            border: 1px solid #fca5a5;
        }

        .result-alerta {
            background: #fef3c7;
            color: #92400e;
            border: 1px solid #fcd34d;
        }
    </style>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

    <div style="padding: 1.5rem;">

        <h4 style="font-family: 'DM Serif Display',serif; color: #0F6E56; margin-bottom: 1.5rem;">
            <i class="ti ti-clipboard-check" style="font-size: 1.4rem; vertical-align: middle; margin-right: 6px;"></i>
            Evaluar Plan de Mejoramiento
        </h4>

        <!-- Panel: seleccionar plan -->
        <asp:Panel ID="pnlSeleccionar" runat="server">
            <div class="eval-card">
                <label style="font-weight: 600; color: #1a1a1a; margin-bottom: 6px; display: block;">
                    Seleccione el plan a evaluar
                </label>
                <div style="display: flex; gap: 1rem; align-items: flex-end;">
                    <asp:DropDownList
                        ID="ddlPlanes"
                        runat="server"
                        CssClass="form-control"
                        Style="max-width: 450px;"
                        AutoPostBack="true"
                        OnSelectedIndexChanged="ddlPlanes_SelectedIndexChanged">
                    </asp:DropDownList>
                </div>
            </div>
        </asp:Panel>

        <!-- Panel: formulario de evaluación (visible al elegir plan) -->
        <asp:Panel ID="pnlEvaluar" runat="server" Visible="false">

            <div class="info-plan">
                <div style="display: flex; align-items: center; gap: 10px; flex-wrap: wrap;">
                    <strong>
                        <asp:Label ID="lblNombrePlan" runat="server"></asp:Label>
                    </strong>
                    <asp:Label ID="lblBadgeTipo" runat="server"></asp:Label>
                </div>
                <div style="margin-top: 6px; color: #555; font-size: 0.88rem;">
                    Aprendiz: <strong>
                        <asp:Label ID="lblAprendiz" runat="server"></asp:Label></strong>
                    &nbsp;|&nbsp;
                    Fecha límite:
                    <asp:Label ID="lblFechaLimite" runat="server"></asp:Label>
                </div>
            </div>

            <div class="eval-card">
                <h6 style="color: #0F6E56; font-weight: 700; margin-bottom: 1rem;">Criterios de Evaluación</h6>

                <!-- Producto -->
                <div class="criterio-row">
                    <div class="criterio-label">
                        <i class="ti ti-file-check" style="margin-right: 4px;"></i>Producto
                    </div>
                    <div class="criterio-desc">Evaluación de la evidencia entregada</div>
                    <div class="criterio-radio">
                        <label class="radio-opt">
                            <asp:RadioButton ID="rbProductoAprueba" runat="server" GroupName="Producto" Text="" />
                            <span class="aprueba-label">Aprueba</span>
                        </label>
                        <label class="radio-opt">
                            <asp:RadioButton ID="rbProductoNoAprueba" runat="server" GroupName="Producto" Text="" />
                            <span class="noaprueba-label">No Aprueba</span>
                        </label>
                    </div>
                </div>

                <!-- Conocimiento -->
                <div class="criterio-row">
                    <div class="criterio-label">
                        <i class="ti ti-brain" style="margin-right: 4px;"></i>Conocimiento
                    </div>
                    <div class="criterio-desc">Sustentación o explicación del trabajo realizado</div>
                    <div class="criterio-radio">
                        <label class="radio-opt">
                            <asp:RadioButton ID="rbConocimientoAprueba" runat="server" GroupName="Conocimiento" Text="" />
                            <span class="aprueba-label">Aprueba</span>
                        </label>
                        <label class="radio-opt">
                            <asp:RadioButton ID="rbConocimientoNoAprueba" runat="server" GroupName="Conocimiento" Text="" />
                            <span class="noaprueba-label">No Aprueba</span>
                        </label>
                    </div>
                </div>

                <!-- Desempeño -->
                <div class="criterio-row">
                    <div class="criterio-label">
                        <i class="ti ti-chart-line" style="margin-right: 4px;"></i>Desempeño
                    </div>
                    <div class="criterio-desc">Capacidad para realizar mejoras o nuevas funcionalidades</div>
                    <div class="criterio-radio">
                        <label class="radio-opt">
                            <asp:RadioButton ID="rbDesempenoAprueba" runat="server" GroupName="Desempeno" Text="" />
                            <span class="aprueba-label">Aprueba</span>
                        </label>
                        <label class="radio-opt">
                            <asp:RadioButton ID="rbDesempenoNoAprueba" runat="server" GroupName="Desempeno" Text="" />
                            <span class="noaprueba-label">No Aprueba</span>
                        </label>
                    </div>
                </div>

                <div style="margin-top: 1.25rem;">
                    <asp:Button
                        ID="btnEvaluar"
                        runat="server"
                        Text="Registrar Evaluación"
                        CssClass="btn btn-success"
                        OnClick="btnEvaluar_Click"
                        Style="padding: 0.5rem 1.5rem;" />
                </div>

                <!-- Resultado -->
                <asp:Panel ID="pnlResultado" runat="server" Visible="false">
                    <div id="divResultado" runat="server" class="result-box" style="display: block;">
                        <asp:Label ID="lblResultado" runat="server"></asp:Label>
                    </div>
                </asp:Panel>

            </div>
        </asp:Panel>

    </div>

</asp:Content>
