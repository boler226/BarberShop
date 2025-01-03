using BarberShop.ViewModels.Pagination;

namespace BarberShop.Services.Interfaces {
    public interface IPaginationService<EntityVmType, PaginationVmType> where PaginationVmType : PaginationVm {
        Task<PageVm<EntityVmType>> GetPageAsync(PaginationVmType vm);
    }
}
