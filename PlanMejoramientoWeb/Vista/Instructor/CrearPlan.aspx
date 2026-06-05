<%@ Page Title="" Language="C#" MasterPageFile="~/Vista/Site1.Master" AutoEventWireup="true" CodeBehind="CrearPlan.aspx.cs" Inherits="PlanMejoramientoWeb.Vista.Instructor.CrearPlan" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

    <h2>Crear Plan de Mejoramiento</h2>

    <div class="card p-3">

        <div class="row">

            <div class="col-md-6">

                <label>Aprendiz</label>

                <asp:DropDownList
                    ID="ddlAprendiz"
                    runat="server"
                    CssClass="form-control">
                </asp:DropDownList>

            </div>

            <div class="col-md-6">

                <label>Nombre del Plan</label>

                <asp:TextBox
                    ID="txtNombrePlan"
                    runat="server"
                    CssClass="form-control">
                </asp:TextBox>

            </div>

        </div>

        <br />

        <div class="row">

            <div class="col-md-6">

                <label>Fecha Límite</label>

                <asp:TextBox
                    ID="txtFechaLimite"
                    runat="server"
                    TextMode="Date"
                    CssClass="form-control">
                </asp:TextBox>

            </div>

            <div class="col-md-6">

                <label>Observación Inicial</label>

                <asp:TextBox
                    ID="txtObservacion"
                    runat="server"
                    TextMode="MultiLine"
                    Rows="3"
                    CssClass="form-control">
                </asp:TextBox>

            </div>

        </div>

        <br />

        <asp:Button
            ID="btnGuardar"
            runat="server"
            Text="Crear Plan"
            CssClass="btn btn-success"
            OnClick="btnGuardar_Click" />

    </div>
</asp:Content>
