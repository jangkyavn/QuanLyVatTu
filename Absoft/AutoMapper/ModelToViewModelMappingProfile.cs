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
            CreateMap<HangMucVatTu, HangMucVatTuViewModel>();
            CreateMap<HangSanXuat, HangSanXuatViewModel>();
            CreateMap<KhoVatTu, KhoVatTuViewModel>();
            CreateMap<LoaiVatTu, LoaiVatTuViewModel>();
            CreateMap<NguonCungCap, NguonCungCapViewModel>();
            CreateMap<NuocSanXuat, NuocSanXuatViewModel>();
            CreateMap<NhanSu, NhanSuViewModel>();
            CreateMap<VatTu, VatTuViewModel>();
            CreateMap<KhoHang, KhoHangViewModel>();
        }
    }
}
