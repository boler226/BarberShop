using AutoMapper;
using AutoMapper.QueryableExtensions;
using BarberShop.Database.Context;
using BarberShop.Services.ControllerServices.Interfaces;
using BarberShop.ViewModels.Comment;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BarberShop.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class CommentsController(
        DataContext context,
        IMapper mapper,
        ICommentsControllerService service
        ) : ControllerBase
    {
        [HttpGet]
        public async Task<IActionResult> GetAll() {
            var comments = await context.Comments
                    .ProjectTo<CommentVm>(mapper.ConfigurationProvider)
                    .ToArrayAsync();

            return Ok(comments);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(long id) {
            var comment = await context.Comments
                    .ProjectTo<CommentVm>(mapper.ConfigurationProvider)
                    .FirstOrDefaultAsync(c => c.Id == id);

            if (comment is null)
                return NotFound();

            return Ok(comment);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromForm] CreateCommentVm vm) {
            await service.CreateAsync(vm);

            return Ok();
        }

        [HttpPut]
        public async Task<IActionResult> Update([FromForm] UpdateCommentVm vm) { 
            await service.UpdateAsync(vm);

            return Ok();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(long id) {
            await service.DeleteIfExistsAsync(id);

            return Ok();
        }
    }
}
