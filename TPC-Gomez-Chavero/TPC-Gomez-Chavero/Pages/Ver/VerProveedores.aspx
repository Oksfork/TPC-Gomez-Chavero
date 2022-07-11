﻿<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="VerProveedores.aspx.cs" Inherits="TPC_Gomez_Chavero.Pages.Ver.VerProveedores" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">


    <link href="../../css/Vistas.css" rel="stylesheet" type="text/css" />
        <div class="container-xxl mt-4 viewStyle" data-aos="flip-up">
        <div class="row justify-content-center">
            <div class="col-md-12 text-center mt-4">
                <h2>Proveedores</h2>
            </div>
        </div>
        <div class="row justify-content-center mt-3">
            <div class="col-md-6">
                <asp:CheckBox id="chkNombre" runat="server" AutoPostBack="true" Text="Ordenar por Nombre" OnCheckedChanged="chkNombre_CheckedChanged" />
            </div>
        </div>
        <div class="row mt-3 mb-5">
            <div class="col-md-12 mb-5">
               <asp:GridView ID="dgvProveedores" runat="server" AllowPaging="true" OnPageIndexChanging="dgvProveedores_PageIndexChanging" OnSelectedIndexChanged="dgvProveedores_SelectedIndexChanged" CssClass="table border-0" AutoGenerateColumns="false">
                   <Columns>
                       <asp:BoundField HeaderText="ID" DataField="ID" />
                       <asp:BoundField HeaderText="Nombre" DataField="Nombre" />
                       <asp:CheckBoxField HeaderText="Activo" DataField="Estado"/>
                       <asp:CommandField ShowSelectButton="true" SelectText="📝" HeaderText="Acciones" />
                   </Columns>
                   <PagerStyle CssClass="pagination" />
               </asp:GridView>
                            </div>
            </div>
    </div>

</asp:Content>
