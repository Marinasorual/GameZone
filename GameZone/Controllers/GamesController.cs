

namespace GameZone.Controllers
{
    public class GamesController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly ICategoriesService _CategoriesService;
        private readonly IGamesService _gamesService;

        public GamesController(ApplicationDbContext context, ICategoriesService categoriesService, IGamesService gamesService)
        {
            _context = context;
            _CategoriesService = categoriesService;
            _gamesService = gamesService; 
        }


        public IActionResult Index()        {
            return View();
        }
        [HttpGet]
        public IActionResult Create()
        {

            CreateViewModelFormGame ViewModel = new()
            {
                Categories = _CategoriesService.GetSelectList(),
                Devices = _context.Devices.Select(D => new SelectListItem { Value = D.Id.ToString(), Text = D.Name })
                .OrderBy(D => D.Text)
            };

            return View(ViewModel);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(CreateViewModelFormGame model)
        {
            if (!ModelState.IsValid)
            {
                model.Categories = _CategoriesService.GetSelectList();
                model.Devices = _context.Devices.Select(D => new SelectListItem { Value = D.Id.ToString(), Text = D.Name })
                .OrderBy(D => D.Text);
                return View(model);
            }

            await _gamesService.Create(model);
            return RedirectToAction(nameof (Index));
        }
    }
}
