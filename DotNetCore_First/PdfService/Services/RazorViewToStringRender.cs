using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Abstractions;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Mvc.Razor;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc.ViewEngines;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.AspNetCore.Routing;
using System;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace WebApplication1.Controllers
{
    public class RazorViewToStringRender
    {
        private readonly IRazorViewEngine _viewEngine;
        private readonly ITempDataProvider _tempDataProvider;
        private readonly HttpContext _context;
        private readonly IHostingEnvironment _env;

        public RazorViewToStringRender(
            IRazorViewEngine viewEngine,
            ITempDataProvider tempDataProvider, IHttpContextAccessor accessor, IHostingEnvironment env)
        {
            _viewEngine = viewEngine;
            _tempDataProvider = tempDataProvider;
            _context = accessor.HttpContext;
            _env = env;
        }

        public async Task<string> RenderViewToStringAsync<TModel>(string viewName, TModel model)
        {
            var actionContext = new ActionContext(_context, new RouteData(), new ActionDescriptor());
            var view = FindView(actionContext, viewName);

            using (var output = new StringWriter())
            {
                var viewDictionary = new ViewDataDictionary<TModel>(new EmptyModelMetadataProvider(), new ModelStateDictionary())
                {
                    Model = model
                };
                var tempDataDictionary = new TempDataDictionary(actionContext.HttpContext, _tempDataProvider);
                var viewContext = new ViewContext(actionContext,view,viewDictionary,tempDataDictionary,output,new HtmlHelperOptions())
                {
                    RouteData = _context.GetRouteData()
                };
                await view.RenderAsync(viewContext);
                return output.GetStringBuilder().ToString();
            }
        }

        private IView FindView(ActionContext actionContext, string viewName)
        {
            var getViewResult = _viewEngine.GetView(_env.ContentRootPath, viewName, false);
            if (getViewResult.Success)
            {
                return getViewResult.View;
            }

            var findViewResult = _viewEngine.FindView(actionContext, viewName, true);
            if (findViewResult.Success)
            {
                return findViewResult.View;
            }

            var searchedLocations = getViewResult.SearchedLocations.Concat(findViewResult.SearchedLocations);
            var errorMessage = string.Join(
                Environment.NewLine,
                new[] { $"Unable to find view '{viewName}'. The following locations were searched:" }.Concat(searchedLocations));
            throw new InvalidOperationException(errorMessage);
        }
    }
}

