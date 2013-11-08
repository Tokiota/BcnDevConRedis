using System.Diagnostics;
using System.Web.Mvc;
using System.Web.Routing;

using ServiceStack.Redis;

namespace AutoComplete.Filter
{
    public class LogActionFilter : ActionFilterAttribute
    {
        public override void OnResultExecuted(ResultExecutedContext filterContext)
        {
            Log(filterContext.RouteData);
        }

        private void Log(RouteData routeData)
        {
            var controllerName = routeData.Values ["controller"];
            var actionName = routeData.Values ["action"];
            var clave = string.Format("{0}:{1}", controllerName, actionName);

            using (IRedisClient client = new RedisClient())
            {
                client.IncrementValueInHash("pageCounter", clave, 1);

                var result = client.GetAllEntriesFromHash("pageCounter");
                
                foreach (var item in result)
                {
                    Debug.WriteLine("la pagina {0} se visito {1} vez/veces", item.Key, item.Value);
                }
            }

             
        }
    }
}