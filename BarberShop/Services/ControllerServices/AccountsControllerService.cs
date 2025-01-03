using AutoMapper;
using BarberShop.Constants;
using BarberShop.Database.Context;
using BarberShop.Database.Entities.Identity;
using BarberShop.Exceptions;
using BarberShop.Services.ControllerServices.Interfaces;
using BarberShop.Services.Interfaces;
using BarberShop.ViewModels.Account;
using Microsoft.AspNetCore.Identity;

namespace BarberShop.Services.ControllerServices
{
    public class AccountsControllerService(
        DataContext context,
        UserManager<User> userManager,
        IMapper mapper,
        IImageService imageService
        ) : IAccountsControllerService
    {
        public async Task<User> SignUpAsync(RegisterVm vm) {
            User user = mapper.Map<RegisterVm, User>(vm);
            user.Image = await imageService.SaveImageAsync(vm.Image);

            try {
                await CreateUserAsync(user, vm.Password);
            } 
            catch {
                imageService.DeleteImageIfExists(user.Image);
                throw;
            }

            return user;
        }

        private async Task CreateUserAsync(User user, string? password = null) {
            using var transaction = await context.Database.BeginTransactionAsync();

            try {
                IdentityResult identityResult = await CreateUserInDatabaseAsync(user, password);
                if (!identityResult.Succeeded)
                    throw new IdentityException(identityResult, "User creating error");

                identityResult = await userManager.AddToRoleAsync(user, Roles.User);
                if (!identityResult.Succeeded)
                    throw new IdentityException(identityResult, "Role assignment error");

                await transaction.CommitAsync();
            }
            catch {
                await transaction.RollbackAsync();
                throw;
            }
        }

        private async Task<IdentityResult> CreateUserInDatabaseAsync(User user, string? password) {
            if (password is null)
                return await userManager.CreateAsync(user);

            return await userManager.CreateAsync(user, password); 
        }
    }
}
