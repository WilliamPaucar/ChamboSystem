using Sistema_Puntuacion_Chambo.Controllers;
using Sistema_Puntuacion_Chambo.Models.DatosBD;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Sistema_Puntuacion_Chambo.Filtros
{
    public class ControlSesion: ActionFilterAttribute
    {
        private tbl_users_jueces usuario;
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            try
            {
                base.OnActionExecuting(filterContext);
                usuario = (tbl_users_jueces)HttpContext.Current.Session["username"];
                if (filterContext.Controller is UnoController== false)
                {
                    filterContext.HttpContext.Response.Redirect("/Uno/Login");

                }
                    
            }
            catch(Exception)
            {
                filterContext.Result = new RedirectResult("~/Uno/Login");

            }
            
        }
    }
}