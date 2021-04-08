using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.ModelBinding;


namespace ModelClassLibrary
{
    public class GameModelBinder : IModelBinder
    {
        public Task BindModelAsync(ModelBindingContext bindingContext)
        {
            var game = new Game();
            game.Player1 = new Player();
            game.Player2 = new Player();
            game.City = bindingContext.HttpContext.Request.Form["gameCity"];
            game.Player1.Name = bindingContext.HttpContext.Request.Form["p1Name"];
            game.Player1.Rank = int.Parse(bindingContext.HttpContext.Request.Form["p1Rank"]);
            game.Player2.Name = bindingContext.HttpContext.Request.Form["p2Name"];
            game.Player2.Rank = int.Parse(bindingContext.HttpContext.Request.Form["p2Rank"]);
            bindingContext.Result = ModelBindingResult.Success(game); // set the model binding result
            return Task.CompletedTask;
        }
    }
}
