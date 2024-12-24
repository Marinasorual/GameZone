using GameZone.Settings;

namespace GameZone.Service
{
    public class GamesService : IGamesService
    {
        private readonly ApplicationDbContext _context;
        private readonly IWebHostEnvironment _webHostEnvironment;
        private readonly string _imagesPath;

        public GamesService(ApplicationDbContext context, IWebHostEnvironment webHostEnvironment)
        {
            _context = context;
            _webHostEnvironment = webHostEnvironment;
            _imagesPath = $"{_webHostEnvironment.WebRootPath}{FileSettings.ImagesPath}";
        }

    

    public async Task Create(CreateViewModelFormGame model)
        {
            var coverName = $"{Guid.NewGuid()}{Path.GetExtension(model.Cover.FileName)}";

            var path = Path.Combine(_imagesPath, coverName);
            using (var stream = new FileStream(path, FileMode.Create))
            {
                await model.Cover.CopyToAsync(stream);
            }
            Game game = new()
            {
                Name = model.Name,
                CategoryId = model.CategoryId,
                Cover = coverName,
                Description = model.Description,
                Devices = model.SelectedDevices.Select(D => new GameDevice { DeviceId = D }).ToList()

            }; 
            _context.Add(game);
            await _context.SaveChangesAsync();
        }
    }
}
