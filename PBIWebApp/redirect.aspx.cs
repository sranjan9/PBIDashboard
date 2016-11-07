using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Microsoft.IdentityModel.Clients.ActiveDirectory;
using System.Threading.Tasks;

namespace PBIWebApp
{
 /* NOTE: This sample is to illustrate how to authenticate a Power BI web app. 
 * In a production application, you should provide appropriate exception handling and refactor authentication settings into 
 * a configuration. Authentication settings are hard-coded in the sample to make it easier to follow the flow of authentication. */

    public partial class Redirect : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            //Redirect uri must match the redirect_uri used when requesting Authorization code.
             string redirectUri = "http://localhost:13526/Redirect";
       //     string redirectUri = "http%3A%2F%2Flocalhost%3A13526%2FRedirect";
            string authorityUri = "https://login.windows.net/common/oauth2/authorize/";
           
            // Get the auth code
            string code = Request.Params.GetValues(0)[0];
            
            // Get auth token from auth code       
            TokenCache TC = new TokenCache();

            AuthenticationContext AC = new AuthenticationContext(authorityUri, TC);
            //ClientCredential cc = new ClientCredential
            //    (Properties.Settings.Default.ClientID,
            //    Properties.Settings.Default.ClientSecret);
            ClientCredential cc = new ClientCredential
    ("32d3e0e1-07d9-41f7-97b3-5dc88caedc5f",
    "fIAB30nK0sRw6jliapR61n1SQLX0BEpmXnwtQlwJFio=");

            AuthenticationResult AR = AC.AcquireTokenByAuthorizationCode(code, new Uri(redirectUri), cc);

            //Set Session "authResult" index string to the AuthenticationResult
            Session["authResult"] = AR;

            //Redirect back to Default.aspx
            Response.Redirect("/Default.aspx");
        }
    }
}