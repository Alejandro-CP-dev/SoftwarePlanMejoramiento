<%@ Page Title="" Language="C#" MasterPageFile="~/Vista/Site1.Master"
    AutoEventWireup="true"
    CodeBehind="GestionarPlan.aspx.cs"
    Inherits="PlanMejoramientoWeb.Vista.Instructor.GestionarPlan" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

    <h2>Gestión del Plan de Mejoramiento</h2>

    <hr />

    <asp:HiddenField ID="hfIdAprendiz" runat="server" />
    <asp:HiddenField ID="hfIdPlan" runat="server" />

    <!-- ================= DATOS APRENDIZ ================= -->

    <div class="card">

        <div class="card-header bg-primary text-white">
            Información del Aprendiz

        </div>

        <div class="card-body">

            <div class="row">

                <div class="col-md-6">

                    <label>Documento</label>

                    <asp:TextBox
                        ID="txtDocumento"
                        runat="server"
                        CssClass="form-control"
                        ReadOnly="true" />

                </div>

                <div class="col-md-6">

                    <label>Nombre Completo</label>

                    <asp:TextBox
                        ID="txtNombre"
                        runat="server"
                        CssClass="form-control"
                        ReadOnly="true" />

                </div>

            </div>

            <br />

            <div class="row">

                <div class="col-md-6">

                    <label>Estado Académico</label>

                    <asp:TextBox
                        ID="txtEstado"
                        runat="server"
                        CssClass="form-control"
                        ReadOnly="true" />

                </div>

                <div class="col-md-6">

                    <label>Ficha</label>

                    <asp:TextBox
                        ID="txtFicha"
                        runat="server"
                        CssClass="form-control"
                        ReadOnly="true" />

                </div>

            </div>

        </div>

    </div>

    <br />

    <!-- ================= CREAR PLAN ================= -->

    <asp:Panel ID="pnlCrearPlan" runat="server">

        <div class="card">

            <div class="card-header bg-success text-white">
                Crear Plan de Mejoramiento

            </div>

            <div class="card-body">

                <div class="row">

                    <div class="col-md-12">

                        <label>Nombre del Plan</label>

                        <asp:TextBox
                            ID="txtNombrePlan"
                            runat="server"
                            CssClass="form-control" />

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
                            CssClass="form-control" />

                    </div>

                </div>

                <br />

                <label>Observación</label>

                <asp:TextBox
                    ID="txtObservacion"
                    runat="server"
                    TextMode="MultiLine"
                    Rows="4"
                    CssClass="form-control" />

                <br />

                <asp:Button
                    ID="btnCrearPlan"
                    runat="server"
                    Text="Crear Plan"
                    CssClass="btn btn-success"
                    OnClick="btnCrearPlan_Click" />

            </div>

        </div>

    </asp:Panel>

    <!-- ================= PLAN EXISTENTE ================= -->

    <asp:Panel
        ID="pnlPlanExistente"
        runat="server"
        Visible="false">

        <div class="card">

            <div class="card-header bg-info text-white">
                Plan Activo

            </div>

            <div class="card-body">

                <div class="row">

                    <div class="col-md-6">

                        <label>Nombre</label>

                        <asp:TextBox
                            ID="txtPlan"
                            runat="server"
                            CssClass="form-control"
                            ReadOnly="true" />

                    </div>

                    <div class="col-md-6">

                        <label>Tipo</label>

                        <asp:TextBox
                            ID="txtTipoPlan"
                            runat="server"
                            CssClass="form-control"
                            ReadOnly="true" />

                    </div>

                </div>

                <br />

                <div class="row">

                    <div class="col-md-6">

                        <label>Fecha Asignación</label>

                        <asp:TextBox
                            ID="txtFechaAsignacion"
                            runat="server"
                            CssClass="form-control"
                            ReadOnly="true" />

                    </div>

                    <div class="col-md-6">

                        <label>Fecha Límite</label>

                        <asp:TextBox
                            ID="txtFechaLimitePlan"
                            runat="server"
                            CssClass="form-control"
                            ReadOnly="true" />

                    </div>

                </div>

                <hr />

                <div class="row text-center">

                    <div class="col-md-4">

                        <div class="card">

                            <div class="card-body">

                                <h5>Resultados</h5>

                                <p>Gestionar resultados incumplidos</p>

                                <asp:Button
                                    ID="btnResultados"
                                    runat="server"
                                    Text="Gestionar"
                                    CssClass="btn btn-primary" />

                            </div>

                        </div>

                    </div>

                    <div class="col-md-4">

                        <div class="card">

                            <div class="card-body">

                                <h5>Actividades</h5>

                                <p>Registrar actividades del plan</p>

                                <asp:Button
                                    ID="btnActividades"
                                    runat="server"
                                    Text="Gestionar"
                                    CssClass="btn btn-warning" />

                            </div>

                        </div>

                    </div>

                    <div class="col-md-4">

                        <div class="card">

                            <div class="card-body">

                                <h5>Evaluación</h5>

                                <p>Evaluar el cumplimiento del plan</p>

                                <asp:Button
                                    ID="btnEvaluar"
                                    runat="server"
                                    Text="Gestionar"
                                    CssClass="btn btn-success" />

                            </div>

                        </div>

                    </div>

                </div>

            </div>

        </div>

    </asp:Panel>

</asp:Content>
