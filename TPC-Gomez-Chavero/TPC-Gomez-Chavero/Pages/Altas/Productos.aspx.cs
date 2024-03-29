﻿using System;
using System.Collections.Generic;
using System.Web.UI;
using services;
using domain;
using Controllers;
using System.Data;
using helpers;
using System.Web.UI.WebControls;

namespace TPC_Gomez_Chavero.Pages.Altas
{
    public partial class Productos : Page
    {
        private ABMService abm;

        private List<ProductBranch> branchList;
        private List<ProductType> typeList;
        private List<ProductCategory> categoryList;
        private List<Product> dadosBaja;
        public User whoIs;

        protected void Page_Load(object sender, EventArgs e)
        {
            abm = new ABMService();

            if (Session["user"] != null)
            {
                whoIs = (User)Session["user"];
                if (whoIs.type.Description != "Administrador")
                {
                    Response.Redirect("~/");
                }
            }
            else
            {
                Response.Redirect("~/");
            }

            if (!IsPostBack)
            {
                dropBranch();
                dropCategory();
                dropProductType();

                if(Session["Comprando"] != null)
                {
                    btnSubmit.Visible = false;
                    btnRetorno.Visible = true;
                }
            }
            checkInputs();
        }

        public void dropBranch()
        {
            branchList = abm.getBranch(1);

            DataTable data = createEmptyDataTable();

            foreach (ProductBranch item in branchList)
            {
                DataRow row = data.NewRow();
                row[0] = item.Id;
                row[1] = StringHelper.upperStartChar(item.Descripcion);
                data.Rows.Add(row);
            }

            populateDropDown(data, dropMarca);
        }

        public void dropProductType()
        {
            typeList = abm.getProductType(1);

            DataTable data = createEmptyDataTable();

            foreach (ProductType item in typeList)
            {
                DataRow row = data.NewRow();
                row[0] = item.Id;
                row[1] = StringHelper.upperStartChar(item.Descripcion);
                data.Rows.Add(row);
            }

            populateDropDown(data, dropProducto);
        }
        
        public void dropCategory()
        {
            categoryList = abm.getCategory(1);

            DataTable data = createEmptyDataTable();

            foreach (ProductCategory item in categoryList)
            {
                DataRow row = data.NewRow();
                row[0] = item.Id;
                row[1] = StringHelper.upperStartChar(item.Descripcion);
                data.Rows.Add(row);
            }

            populateDropDown(data, dropCategoria);
        }

        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            string nombre = txtNombre.Text;
            string des = descripcion.Value;
            long idcategoria = long.Parse(dropCategoria.SelectedItem.Value);
            long idmarca = long.Parse(dropMarca.SelectedItem.Value);
            long idtipo = long.Parse(dropProducto.SelectedItem.Value);
            int stockmin = int.Parse(txtStockMinimo.Text);
            short porc = Int16.Parse(txtPorcentajeVenta.Text);

            Product ex = new Product();
            ProductController pc = new ProductController();
            ex.Nombre = nombre;
            ex.Marca = new ProductBranch();
            ex.Marca.Id = idmarca;

            if (pc.isExists(ex))
            {
                if (abm.addProduct(nombre.ToLower(), des.ToLower(), idcategoria, idmarca, idtipo, stockmin, porc) == 1)
                {
                    lblSuccess.Text = "Producto cargado de forma exitosa!";
                    lblSuccess.Visible = true;
                    btnSubmit.Visible = false;
                    btnContinue.Visible = true;
                }
                else
                {
                    lblSuccess.Text = "Hubo un error al cargar el producto";
                    lblSuccess.Visible = true;
                    btnSubmit.Visible = true;
                    btnContinue.Visible = false;
                }
            }
            else
            {
                lblSuccess.Text = "El producto ya existe o bien podria estar dado de baja, revisar en vista de productos";
                lblSuccess.Visible = true;
                btnSubmit.Visible = true;
                btnContinue.Visible = false;
            }

        }

        private DataTable createEmptyDataTable()
        {
            DataTable data = new DataTable();
            data.Columns.Add("id");
            data.Columns.Add("description");

            DataRow emptyData = data.NewRow();
            emptyData[0] = 0;
            emptyData[1] = "";
            data.Rows.Add(emptyData);

            DataRow newData = data.NewRow();
            newData[0] = -1;
            newData[1] = "Nuevo...";
            data.Rows.Add(newData);

            return data;
        }

        private void populateDropDown(DataTable dataTable, DropDownList dropDown)
        {
            dropDown.DataSource = new DataView(dataTable);
            dropDown.DataTextField = "description";
            dropDown.DataValueField = "id";
            dropDown.DataBind();
        }

        protected void dropCategoriaChanged(object sender, EventArgs e)
        {
            long idSelected = long.Parse(dropCategoria.SelectedValue);

            if (idSelected == -1)
            {
                addCategoryBtn.Visible = true;
                addCategoryTxt.Visible = true;
            }
            else
            {
                addCategoryBtn.Visible = false;
                addCategoryBtn.Enabled = false;
                addCategoryTxt.Visible = false;
            }
        }

        protected void dropMarcaChanged(object sender, EventArgs e)
        {
            long idSelected = long.Parse(dropMarca.SelectedValue);

            if (idSelected == -1)
            {
                addBranchBtn.Visible = true;
                addBranchTxt.Visible = true;
            }
            else
            {
                addBranchBtn.Visible = false;
                addBranchBtn.Enabled = false;
                addBranchTxt.Visible = false;
            }
        }

        protected void dropProductoChanged(object sender, EventArgs e)
        {
            long idSelected = long.Parse(dropProducto.SelectedValue);

            if (idSelected == -1)
            {
                addTypeBtn.Visible = true;
                addTypeTxt.Visible = true;
            }
            else
            {
                addTypeBtn.Visible = false;
                addTypeBtn.Enabled = false;
                addTypeTxt.Visible = false;
            }
        }

        protected void onCreateBranchClicked(object sender, EventArgs e)
        {
            string descripcion = addBranchTxt.Text;
            if (descripcion.Length == 0)
            {
                errorMarca.Text = "Por favor ingrese informacion valida.";
                return;
            }

            if (abm.createTypes(descripcion.ToLower(), "Marcas") == 1)
            {
                dropBranch();
                dropMarca.SelectedIndex = branchList.Count + 1;
                errorMarca.Visible = false;
                addBranchBtn.Visible = false;
                addBranchTxt.Visible = false;
            }
            else 
            {
                dropBranch();
                errorMarca.Text = "Hubo un error al crear la marca";
                errorMarca.Visible = true;
                addBranchBtn.Visible = false;
                addBranchTxt.Visible = false;
            }
        }
    
        protected void onCreateTypeClicked(object sender, EventArgs e)
        {
            string descripcion = addTypeTxt.Text;
            if (descripcion.Length == 0)
            {
                errorTipoProducto.Text = "Por favor ingrese informacion valida.";
                return;
            }

            if (abm.createTypes(descripcion.ToLower(), "TipoProducto") == 1)
            {
                dropProductType();
                dropProducto.SelectedIndex = typeList.Count + 1;
                errorTipoProducto.Visible = false;
                addTypeBtn.Visible = false;
                addTypeTxt.Visible = false;
            }
            else
            {
                dropProductType();
                errorTipoProducto.Text = "Hubo un error al crear el Tipo de Producto";
                errorTipoProducto.Visible = true;
                addTypeBtn.Visible = false;
                addTypeTxt.Visible = false;
            }
        }
    
        protected void onCreateCategoryClicked(object sender, EventArgs e)
        {
            string descripcion = addCategoryTxt.Text;
            if (descripcion.Length == 0)
            {
                errorCategoria.Text = "Por favor ingrese informacion valida.";
                return;
            }

            if (abm.createTypes(descripcion.ToLower(), "Categorias") == 1)
            {
                dropCategory();
                dropCategoria.SelectedIndex = categoryList.Count+1;
                errorCategoria.Visible = false;
                addCategoryBtn.Visible = false;
                addCategoryTxt.Visible = false;
            }
            else
            {
                dropCategory();
                errorCategoria.Text = "Hubo un error al crear la Categoria.";
                errorCategoria.Visible = true;
                addCategoryBtn.Visible = false;
                addCategoryTxt.Visible = false;
            }
        }

        protected void btnRetorno_Click(object sender, EventArgs e)
        {
            string nombre = txtNombre.Text;
            string des = descripcion.Value;
            long idcategoria = long.Parse(dropCategoria.SelectedItem.Value);
            long idmarca = long.Parse(dropMarca.SelectedItem.Value);
            long idtipo = long.Parse(dropProducto.SelectedItem.Value);
            int stockmin = int.Parse(txtStockMinimo.Text);
            short porc = Int16.Parse(txtPorcentajeVenta.Text);

            Product ex = new Product();
            ProductController pc = new ProductController();
            ex.Nombre = nombre;
            ex.Marca = new ProductBranch();
            ex.Marca.Id = idmarca;
            if (pc.isExists(ex))
            {
                if (abm.addProduct(nombre.ToLower(), des.ToLower(), idcategoria, idmarca, idtipo, stockmin, porc) == 1)
                {
                    Response.Redirect("~/Pages/Compras/MisCompras.aspx");
                }
                else
                {
                    lblSuccess.Text = "Hubo un error al cargar el producto";
                    lblSuccess.Visible = true;
                    btnSubmit.Visible = true;
                    btnContinue.Visible = false;
                }
            }
            else
            {
                lblSuccess.Text = "El producto ya existe o bien podria estar dado de baja, revisar en vista de productos";
                lblSuccess.Visible = true;
                btnSubmit.Visible = true;
                btnContinue.Visible = false;
            }
        }

        protected void btnContinue_Click(object sender, EventArgs e)
        {
            txtNombre.Text = "";
            txtStockMinimo.Text = "";
            txtPorcentajeVenta.Text = "";
            descripcion.Value = "";
            btnContinue.Visible = false;
            btnSubmit.Visible = true;
            lblSuccess.Visible = false;
        }

        protected void btnNuevo_Click(object sender, EventArgs e)
        {
            Nuevo.Visible = true;
            menu.Visible = false;
        }

        public void dropDadosBaja()
        {
            dadosBaja = abm.getProducts(0);

            DataTable data = new DataTable();
            data.Columns.Add("id");
            data.Columns.Add("description");

            DataRow emptyData = data.NewRow();
            emptyData[0] = 0;
            emptyData[1] = "";
            data.Rows.Add(emptyData);

            foreach (Product item in dadosBaja)
            {
                DataRow row = data.NewRow();
                row[0] = item.Id;
                row[1] = StringHelper.upperStartChar(item.Nombre);
                data.Rows.Add(row);
            }

            populateDropDown(data, dropElimnacionFisica);
        }

        protected void btnExistente_Click(object sender, EventArgs e)
        {
            dropDadosBaja();

            debaja.Visible = true;
            menu.Visible = false;
        }

        protected void btnOk_Click(object sender, EventArgs e)
        {
            long id = long.Parse(dropElimnacionFisica.SelectedItem.Value);
            if (id == 0)
            {
                lblSucessBaja.Visible = true;
                lblSucessBaja.Text = "Por favor ingrese una opcion valida";
                return;
            }

            if (abm.changeStatus("Productos", "IDProducto", 1, id) == 1)
            {
                lblSucessBaja.Visible = true;
                lblSucessBaja.Text = "El producto Vuelve a estar de alta";
                btnContinuarBaja.Enabled = true;
                btnOk.Enabled = false;
            }
            else
            {
                lblSucessBaja.Visible = true;
                lblSucessBaja.Text = "Hubo un erro al dar de alta el producto";
            }
           
        }

        protected void btnContinuarBaja_Click(object sender, EventArgs e)
        {
            dropDadosBaja();

            btnOk.Visible = true;

            btnContinuarBaja.Enabled = false;
            lblSucessBaja.Visible = false;
            btnOk.Enabled = true;
            btnVolver.Enabled = true;
        }

        protected void btnVolverBaja_Click(object sender, EventArgs e)
        {
            debaja.Visible = false;
            btnOk.Enabled = true;
            btnVolver.Enabled = true;
            btnContinuarBaja.Enabled = false;
            lblSucessBaja.Visible = false;

            menu.Visible = true;

        }
        
        protected void btnVolver_Click(object sender, EventArgs e)
        {
            menu.Visible = true;
            Nuevo.Visible = false;
        }

        private void checkInputs()
        {
            if (txtNombre.Text.Length == 0) return;

            if (long.Parse(dropCategoria.SelectedValue) == 0) return;

            if (long.Parse(dropMarca.SelectedValue) == 0) return;

            if (long.Parse(dropProducto.SelectedValue) == 0) return;

            if (txtStockMinimo.Text.Length < 9 && txtStockMinimo.Text.Length > 0)
            {

                if (txtStockMinimo.Text.Length == 0 ||
                    !FormHelper.validateInputPositiveNumber(txtStockMinimo.Text, errorStock)) return;

            }
            else
            {

                btnSubmit.Enabled = false;
                btnRetorno.Enabled = false;
                return;
            }

            if (txtPorcentajeVenta.Text.Length > 0)
            {
                if (int.Parse(txtPorcentajeVenta.Text) > 100 || !FormHelper.validateInputPositiveNumber(txtPorcentajeVenta.Text, errorPorcentaje)) {
                    btnSubmit.Enabled = false;
                    btnRetorno.Enabled = false;
                    return;
                }

            }
            else
            {
                btnSubmit.Enabled = false;
                btnRetorno.Enabled = false;
                return;
            }

            btnSubmit.Enabled = true;
            btnRetorno.Enabled = true;
        }


        protected void onTextChanged(object sender, EventArgs e)
        {
            TextBox txt = (TextBox)sender;

            if (txtStockMinimo.Text.Length < 9 && txtStockMinimo.Text.Length > 0)
            {
              if (txt.ID == txtStockMinimo.ID) FormHelper.validateInputPositiveNumber(txtStockMinimo.Text, errorStock);
            }
            else
            {
                errorStock.Text = "El numero que ha ingresado es muy grande, por favor, ingrese nuevo dato";
                errorStock.Visible = true;
            }
            if (txtPorcentajeVenta.Text.Length!=0)
            {
                if (int.Parse(txtPorcentajeVenta.Text) < 100)
                {
                     if (txt.ID == txtPorcentajeVenta.ID) FormHelper.validateInputPositiveNumber(txtPorcentajeVenta.Text, errorPorcentaje);
                }
                else
                {
                    errorPorcentaje.Text = "Porcentaje Invalido";
                    errorPorcentaje.Visible = true;
                }
            }


            if (txt.ID == addCategoryTxt.ID)
            {
                if (txt.Text.Length != 0) addCategoryBtn.Enabled = true;
            } 
            if (txt.ID == addBranchTxt.ID)
            {
                if (txt.Text.Length != 0) addBranchBtn.Enabled = true;
            } 
            if (txt.ID == addTypeTxt.ID)
            {
                if (txt.Text.Length != 0) addTypeBtn.Enabled = true;
            }

        }
    }
}