using Absoft.Data.Entities;
using Absoft.ViewModels;
using AutoMapper;

namespace Absoft.AutoMapper
{
    public class ModelToViewModelMappingProfile : Profile
    {
        public ModelToViewModelMappingProfile()
        {
            CreateMap<NguoiDung, UserCreationViewModel>();
            CreateMap<NguoiDung, UserDetailViewModel>();
            CreateMap<NguoiDung, UserSessionViewModel>();
            CreateMap<DonViTinh, DonViTinhViewModel>();
        }
    }
}
