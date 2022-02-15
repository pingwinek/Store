using Microsoft.AspNetCore.Mvc;
using MVC.Models.IRepository;

namespace MVC.Components
{
    public class NavigationMenuViewComponent : ViewComponent
    {
        private IStoreRepository _repository;

        public NavigationMenuViewComponent(IStoreRepository repo) {
            _repository = repo;
        }

        public IViewComponentResult Invoke() {
            return View(_repository.Products.Select(x => x.Category).Distinct().OrderBy(x => x));
        }
    }
}