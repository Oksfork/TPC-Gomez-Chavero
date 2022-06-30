﻿<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="VerCompras.aspx.cs" Inherits="TPC_Gomez_Chavero.Pages.Ver.VerCompras" %>


<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

        <div class="container-xxl mt-4" data-aos="fade-up">
        <div class="row justify-content-center">
            <div class="col-md-12 text-center">
                <h2>Ventas</h2>
            </div>
        </div>
        <div class="row">
            <div class="col-md-12">
               <asp:GridView ID="dgvCompras" runat="server" CssClass="table border-0" AutoGenerateColumns="false">
                   <Columns>
                       <asp:BoundField HeaderText="ID" DataField="ID" />
                       <asp:BoundField HeaderText="Numero Factura" DataField="NumeroFactura" />
                       <asp:BoundField HeaderText="Tipo Factura" DataField="TiposFactura.Descripcion" />
                       <asp:BoundField HeaderText="Vendedor" DataField="Usuario.Nick" />
                       <asp:BoundField HeaderText="Proveedor" DataField="Proveedor.Nombre" />
                       <asp:BoundField HeaderText="Producto" DataField="Producto.Nombre" />
                       <asp:BoundField HeaderText="Cantidad Comprada" DataField="CantidadComprada" />
                       <asp:BoundField HeaderText="Precio Unitario" DataField="PrecioUnitario" />
                       <asp:BoundField HeaderText="Monto Total" DataField="MontoTotal" />
                       <asp:BoundField HeaderText="Fecha de Venta" DataField="FechaVenta" />
                       <asp:BoundField HeaderText="Detalle de la Venta" DataField="Detalle" />
                       <asp:HyperLinkField text="Eliminar" />
                       <asp:HyperLinkField text="Editar" />
                   </Columns>
               </asp:GridView>
           </div>
        </div>
    </div>

</asp:Content>
