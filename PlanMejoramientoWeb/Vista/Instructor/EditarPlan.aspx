<%@ Page Title="" Language="C#" MasterPageFile="~/Vista/Site1.Master" AutoEventWireup="true" CodeBehind="EditarPlan.aspx.cs" Inherits="PlanMejoramientoWeb.Vista.Instructor.EditarPlan" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style>
        .info-badge {
            display: inline-block;
            background: #fef3c7;
            color: #92400e;
            border: 1px solid #fcd34d;
            border-radius: 20px;
            padding: 3px 12px;
            font-size: .8rem;
            font-weight: 600;
        }
        .edit-card {
            background: #fff;
            border-radius: 12px;
            border: 1px solid #e5e5e0;
            padding: 1.5rem;
        }
        .aviso-edicion {
            background: #fffbeb;
            border-left: 4px solid #f59e0b;
            border-radius: 0 8px 8px 0;
            padding: .85rem 1rem;
            font-size: .88rem;
            color: #78350f;
            margin-bottom: 1.25rem;
        }

        .badge-interno {
            background: #d1fae5; color: #065f46;
            border: 1px solid #6ee7b7;
            border-radius: 20px; padding: 3px 12px;
            font-size: .8rem; font-weight: 600;
        }
        .badge-comite {
            background: #fef3c7; color: #92400e;
            border: 1px solid #fcd34d;
            border-radius: 20px; padding: 3px 12px;
            font-size: .8rem; font-weight: 600;
        }
    </style>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

    <div style="padding:1.5rem;">

        <h4 style="font-family:'DM Serif Display',serif; color:#0F6E56; margin-bottom:1.5rem;">
            <i class="ti ti-edit" style="font-size:1.4rem; vertical-align:middle; margin-right:6px;"></i>
            Editar Plan de Mejoramiento
        </h4>

        <!-- Selector de plan -->
        <asp:Panel ID="pnlSelector" runat="server">
            <div class="edit-card" style="margin-bottom:1rem;">
                <label style="font-weight:600; color:#1a1a1a; display:block; margin-bottom:6px;">
                    Seleccione el plan a editar
                </label>
                <div style="display:flex; gap:1rem; align-items:center;">
                    <asp:DropDownList
                        ID="ddlPlanes"
                        runat="server"
                        CssClass="form-control"
                        Style="max-width:460px;"
                        AutoPostBack="true"
                        OnSelectedIndexChanged="ddlPlanes_SelectedIndexChanged">
                    </asp:DropDownList>
                </div>
            </div>
        </asp:Panel>

        <!-- Formulario de edición -->
        <asp:Panel ID="pnlEditar" runat="server" Visible="false">

            <div class="aviso-edicion">
                <i class="ti ti-info-circle" style="margin-right:5px;"></i>
                Este plan fue generado automáticamente al reprobar el plan interno. Complete o ajuste la información antes de que el aprendiz lo trabaje.
            </div>

            <div class="edit-card">

                <div class="mb-3">
                    <label style="font-weight:600; color:#1a1a1a;">
                        Tipo &nbsp;
                        <asp:Label ID="lblTipo" runat="server"></asp:Label>
                    </label>
                </div>

                <div class="mb-3">
                    <label style="font-weight:600; color:#1a1a1a;">Nombre del Plan</label>
                    <asp:TextBox
                        ID="txtNombre"
                        runat="server"
                        CssClass="form-control mt-1">
                    </asp:TextBox>
                </div>

                <div class="mb-3">
                    <label style="font-weight:600; color:#1a1a1a;">Fecha Límite</label>
                    <asp:TextBox
                        ID="txtFechaLimite"
                        runat="server"
                        TextMode="Date"
                        CssClass="form-control mt-1">
                    </asp:TextBox>
                </div>

                <div class="mb-3">
                    <label style="font-weight:600; color:#1a1a1a;">Observación / Descripción</label>
                    <asp:TextBox
                        ID="txtObservacion"
                        runat="server"
                        TextMode="MultiLine"
                        Rows="4"
                        CssClass="form-control mt-1"
                        placeholder="Detalle las actividades, compromisos o indicaciones para el aprendiz...">
                    </asp:TextBox>
                </div>

                <asp:HiddenField ID="hfIdPlan" runat="server" />

                <asp:Button
                    ID="btnGuardar"
                    runat="server"
                    Text="Guardar Cambios"
                    CssClass="btn btn-success"
                    OnClick="btnGuardar_Click"
                    Style="padding:.5rem 1.5rem;" />

                <asp:Panel ID="pnlMensaje" runat="server" Visible="false" Style="margin-top:1rem;">
                    <asp:Label ID="lblMensaje" runat="server"></asp:Label>
                </asp:Panel>

            </div>
        </asp:Panel>

    </div>

</asp:Content>
