using Absoft.Data.Entities;
using Absoft.ViewModels;
using AutoMapper;

namespace Absoft.AutoMapper
{
    public class ViewModelToModelMappingProfile : Profile
    {
        public ViewModelToModelMappingProfile()
        {
            CreateMap<UserCreationViewModel, NguoiDung>();
            CreateMap<UserUpdationViewModel, NguoiDung>();
        }
    }
}
