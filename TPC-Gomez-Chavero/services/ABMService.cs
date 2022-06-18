﻿using domain;
using System;
using System.Collections.Generic;
using System.Data;


namespace services
{
    public class ABMService
    {
        public int createTypes(string descripcion, string tabla)
        {
            DataAccess da = new DataAccess();
            try
            {
                da.setConsulta("insert into "+ tabla +" (descripcion) values(@descripcion)");
                da.setConsultaWhitParameters("@descripcion", descripcion);

                da.executeAction();
                return da.getLineCantAfected();

            }
            catch (Exception ex)
            {
                Console.WriteLine("Error al conectar con la base de datos." + ex.Message);
                throw;
            }
            finally
            {
                da.closeConnection();
            }
        }

        public int addClient(string nombre, string cod, string date, string telefono, string email)
        {
            DataAccess da = new DataAccess();

            try
            {
                da.setConsulta("insert into Clientes(Nombre, CuitOrDni, FechaNac, Telefono, Email) values(@nombre, @cod, @date, @telefono, @email)");
                da.setConsultaWhitParameters("@nombre", nombre);
                da.setConsultaWhitParameters("@cod", cod);
                da.setConsultaWhitParameters("@date", date);
                da.setConsultaWhitParameters("@telefono", telefono);
                da.setConsultaWhitParameters("@email", email);

                da.executeAction();
                return da.getLineCantAfected();

            }
            catch (Exception ex)
            {

                throw ex;
            }
            finally
            {
                da.closeConnection();
            }
        }

        public int addProduct(string nombre, string descripcion,long idcategoria, long idmarca, long idtipoproducto, int stock, int stockminimo, int porcentaje )
        {
            DataAccess da = new DataAccess();

            try
            {
                da.setConsulta("insert into Productos(Nombre, Descripcion, IDCATEGORIA, IDMARCA, IDTIPOPRODUCTO, STOCK, STOCKMINIMO, PORCENTAJEVENTA) values(@nombre, @descripcion, @idcategoria, @idmarca, @idtipoproducto, @stock, @stockminimo, @porcentaje)");
                da.setConsultaWhitParameters("@nombre", nombre);
                da.setConsultaWhitParameters("@descripcion", descripcion);
                da.setConsultaWhitParameters("@idcategoria", idcategoria);
                da.setConsultaWhitParameters("@idmarca", idmarca);
                da.setConsultaWhitParameters("@idtipoproducto", idtipoproducto);
                da.setConsultaWhitParameters("@stock", stock);
                da.setConsultaWhitParameters("@stockminimo", stockminimo);
                da.setConsultaWhitParameters("@porcentaje", porcentaje);

                da.executeAction();
                return da.getLineCantAfected();
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error al conectar con la base de datos." + ex.Message);
                throw;
            }
            finally
            {
                da.closeConnection();
            }
        }

        public List<ProductBranch> getBranch()
        {
            List<ProductBranch> branchList = new List<ProductBranch>();
            DataAccess da = new DataAccess();

            try
            {
                da.setConsulta("select Idmarca, Descripcion from Marcas");
                da.execute();

                while (da.dataReader.Read())
                {
                    ProductBranch branch = new ProductBranch();
                    branch.Id = (long)da.dataReader["Idmarca"];
                    branch.Descripcion = (string)da.dataReader["Descripcion"];


                    branchList.Add(branch);
                }

                return branchList;
            }
            catch (Exception ex)
            {

                throw ex;
            }
            finally
            {
                da.closeConnection();
            }
            
        }


        public List<ProductCategory> getCategory()
        {
            List<ProductCategory> categoryList = new List<ProductCategory>();
            DataAccess da = new DataAccess();

            try
            {
                da.setConsulta("select Idcategoria, Descripcion from Categorias");
                da.execute();

                while (da.dataReader.Read())
                {
                    ProductCategory category = new ProductCategory();
                    category.Id = (long)da.dataReader["Idcategoria"];
                    category.Descripcion = (string)da.dataReader["Descripcion"];


                    categoryList.Add(category);
                }

                return categoryList;
            }
            catch (Exception ex)
            {

                throw ex;
            }
            finally
            {
                da.closeConnection();
            }

        }

        public List<ProductType> getProductType()
        {
            List<ProductType> typeList = new List<ProductType>();
            DataAccess da = new DataAccess();

            try
            {
                da.setConsulta("select IdTipoProducto, Descripcion from TipoProducto");
                da.execute();

                while (da.dataReader.Read())
                {
                    ProductType type = new ProductType();
                    type.Id = (long)da.dataReader["Idtipoproducto"];
                    type.Descripcion = (string)da.dataReader["Descripcion"];


                    typeList.Add(type);
                }

                return typeList;
            }
            catch (Exception ex)
            {

                throw ex;
            }
            finally
            {
                da.closeConnection();
            }

        }
    }
}
