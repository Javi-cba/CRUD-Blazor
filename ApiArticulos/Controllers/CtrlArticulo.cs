using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using ApiArticulos.Models;
using ApiArticulos.Data;
using System.Runtime.InteropServices;
using Microsoft.AspNetCore.Cors;

namespace ApiArticulos.Controllers
{
   
    public class CtrlArticulo : ControllerBase
    {
     [HttpGet]
        [Route("")]
        [EnableCors("AllowBlazorApp")]
        public IActionResult indexRoute()
        {     
           
           return StatusCode(StatusCodes.Status200OK, new { message = $"TODO OK!" });
           
        }
        
        [HttpGet]
        [Route("art/listado")]
        [EnableCors("AllowBlazorApp")]
        public IActionResult Articulos()
        {     
            try
            {
                List<MArticulo> lista = Data.DA_Articulo.GetArticulos();
                return StatusCode(StatusCodes.Status200OK, lista);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.StackTrace);
                return StatusCode(StatusCodes.Status500InternalServerError, new { message = $"Error al buscar artículos: {ex.Message}" });
            }
        }

        [HttpGet]
        [Route("art/buscarId")]
        [EnableCors("AllowBlazorApp")]
        public IActionResult buscarId([FromQuery]int codnum)
        {           
            try
            {
                MArticulo art = Data.DA_Articulo.GetArticuloPorId(codnum);
                if (art==null)
                {
                    return StatusCode(StatusCodes.Status400BadRequest, new { message = "Error, el artículo no existe o está deshabilitado" });
                }
                else
                {
                    return StatusCode(StatusCodes.Status200OK, art);
                }

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.StackTrace);
                return StatusCode(StatusCodes.Status500InternalServerError, new { message = $"Error al buscar el artículo por ID: {ex.Message}" });
            }
        }

        [HttpPost]
        [Route("art/AggArt")]
        [EnableCors("AllowBlazorApp")]
        public IActionResult AggArt([FromBody] MArticulo obj)
        {
            try
            {
                Data.DA_Articulo.AggArt(obj);
                return StatusCode(StatusCodes.Status201Created, new { message = "Articulo creado!" });
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.StackTrace);
                return StatusCode(StatusCodes.Status500InternalServerError, new { message = $"Error al agregar el artículo: {ex.Message}" });
            }

        }
              
        [HttpPut]
        [Route("art/ModArt")]
        [EnableCors("AllowBlazorApp")]
        public IActionResult ModArt([FromBody] MArticulo obj)
        {
            try
            {
                Data.DA_Articulo.ModArt(obj);
                return StatusCode(StatusCodes.Status200OK, new { message = "Articulo modificado!" });
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.StackTrace);
                return StatusCode(StatusCodes.Status500InternalServerError, new { message = $"Error al modificar el artículo: {ex.Message}" });
            }
        }

        [HttpDelete]
        [Route("art/deshabilitar")]
        [EnableCors("AllowBlazorApp")]
        public IActionResult DeshabilitarArt([FromQuery]int codnum)
        {
            try
            {
                Data.DA_Articulo.DeshabilitarArt(codnum);
                return StatusCode(StatusCodes.Status204NoContent);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.StackTrace);
                return StatusCode(StatusCodes.Status500InternalServerError, new { message = $"Error al eliminar el artículo: {ex.Message}" });
            }

        }



    }
}
