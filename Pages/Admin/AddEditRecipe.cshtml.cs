using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using RazorPage.Models;
using System.Threading.Tasks;

namespace RazorPage.Pages.Admin
{
    public class AddEditRecipeModel : PageModel
    {
        private readonly IRecipesService recipesService;

        [FromRoute]
        public long? Id { get; set; }
        public bool IsNewRecipe { get { return Id == null; } }
        [BindProperty]
        public Microsoft.AspNetCore.Http.IFormFile Image { get; set; }
        [BindProperty]
        public Recipe recipe { get; set; }
        public AddEditRecipeModel(IRecipesService recipesService)
        {
            this.recipesService = recipesService;
        }
        public async Task OnGet()
        {
            var recipesService = new RecipesService();
            recipe = await recipesService.FindAsync(Id.GetValueOrDefault()) ?? new Recipe();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            recipe = await recipesService.FindAsync(Id.GetValueOrDefault()) ?? new Recipe();
            recipe.Id = Id.GetValueOrDefault();
            recipe.Name = recipe.Name;
            
            await recipesService.SaveAsync(recipe);
            return RedirectToPage("/Recipe", new { id = Id });
        }

        public async Task<IActionResult> OnPostDelete()
        {
            await recipesService.DeleteAsync(Id.Value);
            return RedirectToPage("/Index");
        }
    }
}