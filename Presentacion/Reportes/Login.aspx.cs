using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Presentacion.Reportes
{
    public partial class Login : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void Login1_Authenticate(object sender, AuthenticateEventArgs e)
        {
            //Valida el usuario y pwd introducido con el servicio Generico esUsuario
            if (ServiciosGen.esUsuario(Login1.UserName, Login1.Password))
            {
                FormsAuthentication.RedirectFromLoginPage(Login1.UserName, Login1.RememberMeSet);
                //FormsAuthentication.RedirectFromLoginPage(Login1.Username, Login1.RememberMeSet);
            }
            else
            {
                Login1.FailureText = "Username and/or password is incorrect.";
            }
        }
    }
}