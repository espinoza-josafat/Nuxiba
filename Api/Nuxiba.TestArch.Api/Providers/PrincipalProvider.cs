using System.Security.Principal;

namespace Nuxiba.TestArch.Web.Providers
{
    public class PrincipalProvider
    {
        public IPrincipal CreatePrincipal(string userName)
        {
            if (string.IsNullOrEmpty(userName))
                return null;
            else
            {
                var identity = new GenericIdentity(userName);
                IPrincipal principal = new GenericPrincipal(identity, new string[0] {  });
                return principal;
            }
        }
    }
}