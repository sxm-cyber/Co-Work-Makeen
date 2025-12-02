using Microsoft.AspNetCore.Mvc;
using MakeenCo_Work.Application.Interfaces;
using MakeenCo_Work.Application.Commands.BlogPosts;

[Route("api/[controller]")]
[ApiController]
public class BlogPostController : ControllerBase
{
    private readonly IBlogPostService _service;
    private readonly IWebHostEnvironment _env;

    public BlogPostController(IBlogPostService service, IWebHostEnvironment env)
    {
        _service = service;
        _env = env;
    }

    [HttpGet]
    public async Task<IActionResult> GetAll()
        => Ok(await _service.GetAllAsync());

    [HttpGet("{id}")]
    public async Task<IActionResult> Get(Guid id)
    {
        var post = await _service.GetByIdAsync(id);
        return post == null ? NotFound() : Ok(post);
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CreateBlogPostCommand command)
    {
        var post = await _service.CreateAsync(command);
        return Ok(post);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Update(Guid id, [FromBody] UpdateBlogPostCommand command)
    {
        var post = await _service.UpdateAsync(id, command);
        return post == null ? NotFound() : Ok(post);
    }


    [HttpPost("{id}/image")]
    public async Task<IActionResult> UploadImage(Guid id, IFormFile image)
    {
        var path = $"uploads/blog/images/{Guid.NewGuid()}_{image.FileName}";
        var fullPath = Path.Combine(_env.WebRootPath, path);

        Directory.CreateDirectory(Path.GetDirectoryName(fullPath)!);

        using (var stream = new FileStream(fullPath, FileMode.Create))
            await image.CopyToAsync(stream);

        var result = await _service.UpdateImageAsync(id, path);
        return result ? Ok() : NotFound();
    }


    [HttpPost("{id}/file")]
    public async Task<IActionResult> UploadFile(Guid id, IFormFile file)
    {
        var path = $"uploads/blog/files/{Guid.NewGuid()}_{file.FileName}";
        var fullPath = Path.Combine(_env.WebRootPath, path);

        Directory.CreateDirectory(Path.GetDirectoryName(fullPath)!);

        using (var stream = new FileStream(fullPath, FileMode.Create))
            await file.CopyToAsync(stream);

        var result = await _service.UpdateFileAsync(id, path);
        return result ? Ok() : NotFound();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(Guid id)
    {
        var success = await _service.DeleteAsync(id);
        return success ? Ok() : NotFound();
    }
}
