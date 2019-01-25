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
            CreateMap<DonViTinhViewModel, DonViTinh>();
            CreateMap<HangMucVatTuViewModel, HangMucVatTu>();
            CreateMap<HangSanXuatViewModel,HangSanXuat>();
            CreateMap<KhoVatTuViewModel, KhoVatTu>();
            CreateMap<LoaiVatTuViewModel, LoaiVatTu>();
            CreateMap<NguonCungCapViewModel, NguonCungCap>();
            CreateMap<NuocSanXuatViewModel, NuocSanXuat>();
            CreateMap<NhanSuViewModel, NhanSu>();
            CreateMap<VatTuViewModel, VatTu>();
            CreateMap<KhoHangViewModel, KhoHang>();
            CreateMap<NhapVatTuViewModel, NhapVatTu>();
            CreateMap<NhapChiTietViewModel, NhapChiTiet>();
        }
    }
}
