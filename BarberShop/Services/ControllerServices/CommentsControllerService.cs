using AutoMapper;
using BarberShop.Database.Context;
using BarberShop.Database.Entities;
using BarberShop.Services.ControllerServices.Interfaces;
using BarberShop.ViewModels.Comment;
using Microsoft.EntityFrameworkCore;

namespace BarberShop.Services.ControllerServices {
    public class CommentsControllerService(
         DataContext context,
        IMapper mapper
        ) : ICommentsControllerService
    {
        public async Task CreateAsync(CreateCommentVm vm) {
            var comment = mapper.Map<Comment>(vm);

            await context.Comments.AddAsync(comment);
            await context.SaveChangesAsync();
        }

        public async Task UpdateAsync(UpdateCommentVm vm) {
            var comment = await context.Comments.FirstAsync(c => c.Id == vm.Id);

            comment.Description = vm.Description;
            comment.Rating = vm.Rating;
            
            await context.SaveChangesAsync();
        }

        public async Task DeleteIfExistsAsync(long id) {
            var comment = await context.Comments.FirstOrDefaultAsync(c => c.Id == id);

            if (comment is null)
                return;

            context.Remove(comment);
            await context.SaveChangesAsync();
        }
    }
}
