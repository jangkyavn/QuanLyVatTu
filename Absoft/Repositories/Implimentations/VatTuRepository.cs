using Absoft.Data;
using Absoft.Data.Entities;
using Absoft.Helpers;
using Absoft.Repositories.Interfaces;
using Absoft.ViewModels;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using OfficeOpenXml;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace Absoft.Repositories.Implimentations
{
    public class VatTuRepository : IVatTuRepository
    {
        DataContext db;
        IMapper mp;
        private readonly IHostingEnvironment _hostingEnvironment;
        IHangMucVatTuRepository _hangMucVatTuRepository;
        ILoaiVatTuRepository _loaiVatTuRepository;
        IDonViTinhRepository _donViTinhRepository;
        IHttpContextAccessor _IHttpContextAccessor;
        public VatTuRepository(DataContext data, IMapper mapper, IHostingEnvironment hostingEnvironment, IHangMucVatTuRepository hangMucVatTuRepository, ILoaiVatTuRepository loaiVatTuRepository, IDonViTinhRepository donViTinhRepository, IHttpContextAccessor IHttpContextAccessor)
        {
            db = data;
            mp = mapper;
            _hostingEnvironment = hostingEnvironment;
            _hangMucVatTuRepository = hangMucVatTuRepository;
            _loaiVatTuRepository = loaiVatTuRepository;
            _donViTinhRepository = donViTinhRepository;
            _IHttpContextAccessor = IHttpContextAccessor;
        }
        public async Task<bool> DeleteAsync(int id)
        {
            try
            {
                var vt = await db.VatTus.FindAsync(id);
                db.VatTus.Remove(vt);
                return await db.SaveChangesAsync() > 0;
            }
            catch (DbUpdateException)
            {
                return false;
            }
        }
        public async Task<bool> DeleteAllAsync(List<int> listId)
        {
            using (var transaction = db.Database.BeginTransaction())
            {
                try
                {
                    foreach (var id in listId)
                    {
                        var dvt = await db.VatTus.FindAsync(id);
                        if (dvt != null)
                        {
                            db.VatTus.Remove(dvt);
                        }
                    }
                    transaction.Commit();
                    return await db.SaveChangesAsync() > 0;
                }
                catch (DbUpdateException)
                {
                    return false;
                }
            }
        }
        // delete all by maloaivt use transaction 
        public async Task<bool> DeleteByMaLoaiVTAsync(int MaloaiVT)
        {
            using (var transaction = db.Database.BeginTransaction())
                try
                {
                    var mvt = await db.VatTus
                .Where(x => x.MaLoaiVatTu == MaloaiVT)
                .ProjectTo<VatTuViewModel>(mp.ConfigurationProvider)
                .ToListAsync();
                    var vt = mp.Map<List<VatTu>>(mvt);
                    foreach (var item in vt)
                    {
                        db.VatTus.Remove(item);
                    }
                    var result = await db.SaveChangesAsync();

                    // Commit transaction if all commands succeed, transaction will auto-rollback
                    // when disposed if either commands fails
                    transaction.Commit();
                    return result > 0;
                }
                catch (Exception)
                {
                    // TODO: Handle failure
                }
            return false;
        }
        public async Task<List<VatTuViewModel>> GetAllAsync()
        {
            var query = from vt in db.VatTus
                        join dvt in db.DonViTinhs on vt.MaDVT equals dvt.MaDVT
                        join lvt in db.LoaiVatTus on vt.MaLoaiVatTu equals lvt.MaLoaiVatTu
                        where vt.Status == true
                        orderby vt.MaVatTu descending
                        select new VatTuViewModel
                        {
                            MaVatTu = vt.MaVatTu,
                            MaLoaiVatTu = vt.MaLoaiVatTu,
                            MaDVT = vt.MaDVT,
                            TenVT = vt.TenVT,
                            GhiChu = vt.GhiChu,
                            TenDVT = dvt.TenDVT,
                            TenLoaiVatTu = lvt.TenLoai
                        };
            return await query.ToListAsync();
        }
        public async Task<List<VatTuViewModel>> GetByMaLoaiVTAsync(int MaloaiVT)
        {
            return await db.VatTus
               .Where(x => x.MaLoaiVatTu == MaloaiVT)
               .ProjectTo<VatTuViewModel>(mp.ConfigurationProvider)
               .ToListAsync();
        }
        public async Task<VatTuViewModel> GetByIdAsync(int id)
        {
            var query = await (from vt in db.VatTus
                               join dvt in db.DonViTinhs on vt.MaDVT equals dvt.MaDVT into tmpDonViTinhs
                               from dvt in tmpDonViTinhs.DefaultIfEmpty()
                               join lvt in db.LoaiVatTus on vt.MaLoaiVatTu equals lvt.MaLoaiVatTu
                               join hm in db.HangMucVatTus on lvt.MaHM equals hm.MaHM
                               where vt.MaVatTu == id
                               select new VatTuViewModel
                               {
                                   MaVatTu = vt.MaVatTu,
                                   MaDVT = vt.MaDVT,
                                   MaLoaiVatTu = lvt.MaLoaiVatTu,
                                   TenDVT = dvt.TenDVT,
                                   TenLoaiVatTu = lvt.TenLoai,
                                   TenHM = hm.TenHM,
                                   TenVT = vt.TenVT,
                                   GhiChu = vt.GhiChu,
                                   Status = vt.Status
                               }).FirstOrDefaultAsync();
            return query;

        }
        public async Task<List<VatTuViewModel>> GetByMaHM(int maHM)
        {
            var listMaLoaiVT = await db.LoaiVatTus.Where(x => x.MaHM == maHM).Select(x => x.MaLoaiVatTu).ToListAsync();
            var listVT = await db.VatTus.Where(o => listMaLoaiVT.Contains(o.MaLoaiVatTu) && o.Status == true).ToListAsync();
            var listmVT = mp.Map<List<VatTuViewModel>>(listVT);
            return listmVT;
        }
        public async Task<List<VatTuViewModel>> GetByMaDVAsync(int MADV)
        {
            return await db.VatTus
              .Where(x => x.MaDVT == MADV)
              .ProjectTo<VatTuViewModel>(mp.ConfigurationProvider)
              .ToListAsync();
        }
        public async Task<bool> InsertAsync(VatTuViewModel mvattu)
        {
            var vt = mp.Map<VatTu>(mvattu);
            await db.VatTus.AddAsync(vt);
            return await db.SaveChangesAsync() > 0;
        }
        public async Task<bool> UpdateAsync(VatTuViewModel mvattu)
        {
            var entity = mp.Map<VatTu>(mvattu);
            var model = await db.VatTus.FindAsync(mvattu.MaVatTu);
            try
            {
                entity.Status = model.Status;
                db.Entry(model).CurrentValues.SetValues(entity);
                await db.SaveChangesAsync();
                return true;
            }
            catch (Exception)
            {

            }
            return false;
        }
        public async Task<bool> IsDelete(int id)
        {
            var entity = await db.VatTus.FindAsync(id);
            entity.Status = false;
            return await db.SaveChangesAsync() > 0;
        }
        public async Task<bool> IsDeleteMulti(List<int> listid)
        {
            using (var transaction = db.Database.BeginTransaction())
            {
                try
                {
                    foreach (var item in listid)
                    {
                        var dvt = await db.VatTus.FindAsync(item);
                        dvt.Status = false;
                    }
                    transaction.Commit();
                    return await db.SaveChangesAsync() > 0;
                }
                catch (Exception)
                {
                    // TODO: Handle failure                    
                }
                return false;
            }
        }
        public async Task<int> GetTotalCountAsync()
        {
            var count = await db.VatTus.CountAsync();
            return count;
        }
        public async Task<int> CheckTonTai(string name)
        {
            var rs = await db.VatTus.AnyAsync(x => x.TenVT.ToUpper().Equals(name.ToUpper().ToTrim()));
            if (rs == true)
            {
                return (await db.VatTus.FirstOrDefaultAsync(x => x.TenVT.ToUpper().ToTrim() == name.ToUpper().ToTrim())).MaVatTu;
            }
            else return -1;
        }
        public async Task<bool> CheckSuDung(int mavt)
        {
            if (await db.NhapChiTiets.FirstOrDefaultAsync(x => x.MaVatTu == mavt) != null) return true;
            if (await db.XuatChiTiets.FirstOrDefaultAsync(x => x.MaVatTu == mavt) != null) return true;
            if (await db.KhoHangs.FirstOrDefaultAsync(x => x.MaVatTu == mavt) != null) return true;
            return false;

        }
        public async Task<bool> ChangStatus(int id)
        {
            var model = await db.VatTus.FindAsync(id);
            model.Status = !model.Status;
            return await db.SaveChangesAsync() > 0;
        }
        public async Task<bool> GetStatus(int id)
        {
            return (await db.VatTus.FindAsync(id)).Status;
        }
        public async Task<PagedList<VatTuViewModel>> GetAllPagingAsync(PagingParams pagingParams)
        {
            var query = from vt in db.VatTus
                        join lvt in db.LoaiVatTus on vt.MaLoaiVatTu equals lvt.MaLoaiVatTu
                        join dvt in db.DonViTinhs on vt.MaDVT equals dvt.MaDVT into tmpDonViTinhs
                        from dvt in tmpDonViTinhs.DefaultIfEmpty()
                        where vt.Status == true
                        orderby vt.MaVatTu descending
                        select new VatTuViewModel
                        {
                            MaVatTu = vt.MaVatTu,
                            MaLoaiVatTu = vt.MaLoaiVatTu,
                            MaDVT = vt.MaDVT,
                            TenVT = vt.TenVT,
                            GhiChu = vt.GhiChu ?? string.Empty,
                            TenDVT = dvt.TenDVT,
                            TenLoaiVatTu = lvt.TenLoai
                        };

            if (!string.IsNullOrEmpty(pagingParams.Keyword))
            {
                var keyword = pagingParams.Keyword.ToUpper().ToTrim();

                query = query.Where(x => x.TenVT.ToUpper().ToUnSign().Contains(keyword.ToUnSign()) ||
                                        x.TenVT.ToUpper().Contains(keyword) ||
                                        x.TenLoaiVatTu.ToUpper().ToUnSign().Contains(keyword.ToUnSign()) ||
                                        x.TenLoaiVatTu.ToUpper().Contains(keyword) ||
                                        x.TenDVT.ToUpper().ToUnSign().Contains(keyword.ToUnSign()) ||
                                        x.TenDVT.ToUpper().Contains(keyword) ||
                                        x.GhiChu.ToUpper().ToUnSign().Contains(keyword.ToUnSign()) ||
                                        x.GhiChu.ToUpper().Contains(keyword));
            }

            if (!string.IsNullOrEmpty(pagingParams.SortValue) && !pagingParams.SortValue.Equals("null") && !pagingParams.SortValue.Equals("undefined"))
            {
                switch (pagingParams.SortKey)
                {
                    case "tenVT":
                        if (pagingParams.SortValue == "ascend")
                        {
                            query = query.OrderBy(x => x.TenVT);
                        }
                        else
                        {
                            query = query.OrderByDescending(x => x.TenVT);
                        }
                        break;
                    case "tenLoaiVatTu":
                        if (pagingParams.SortValue == "ascend")
                        {
                            query = query.OrderBy(x => x.TenLoaiVatTu);
                        }
                        else
                        {
                            query = query.OrderByDescending(x => x.TenLoaiVatTu);
                        }
                        break;
                    case "tenDVT":
                        if (pagingParams.SortValue == "ascend")
                        {
                            query = query.OrderBy(x => x.TenDVT);
                        }
                        else
                        {
                            query = query.OrderByDescending(x => x.TenDVT);
                        }
                        break;
                    case "ghiChu":
                        if (pagingParams.SortValue == "ascend")
                        {
                            query = query.OrderBy(x => x.GhiChu);
                        }
                        else
                        {
                            query = query.OrderByDescending(x => x.GhiChu);
                        }
                        break;
                    default:
                        break;
                }
            }
            return await PagedList<VatTuViewModel>.CreateAsync(query, pagingParams.PageNumber, pagingParams.PageSize);
        }

        public async Task<bool> ImportVT(IList<IFormFile> files)
        {
            var upload = new UploadFile(_hostingEnvironment);
            var fileUrl = upload.InsertFile(files);
            if (!String.IsNullOrEmpty(fileUrl))
            {
                try
                {
                    FileInfo file = new FileInfo(fileUrl);
                    using (ExcelPackage package = new ExcelPackage(file))
                    {
                        ExcelWorksheet workSheet = package.Workbook.Worksheets["VatTu"];
                        int totalRows = workSheet.Dimension.Rows;
                        List<VatTuViewModel> List = new List<VatTuViewModel>();
                        for (int i = 2; i <= totalRows; i++)
                        {
                            //var test = workSheet.Cells[i, 3].Value.ToString().ToTrim();
                            //var test1 = workSheet.Cells[i, 3].Value.ToString().Trim();
                            //var test2 = workSheet.Cells[i, 3].Value.ToString().Length;                            
                            if ((workSheet.Cells[i, 1].Value) != null && (workSheet.Cells[i, 2].Value) != null && (workSheet.Cells[i, 3].Value) != null && (workSheet.Cells[i, 1].Value.ToString().ToTrim() != "") && (workSheet.Cells[i, 2].Value.ToString().ToTrim() != "") && (workSheet.Cells[i, 3].Value.ToString().ToTrim() != ""))
                            {
                                if ((workSheet.Cells[i, 4].Value) == null) workSheet.Cells[i, 4].Value = "";
                                if ((workSheet.Cells[i, 5].Value) == null) workSheet.Cells[i, 5].Value = "";
                                List.Add(new VatTuViewModel
                                {
                                    TenHM = workSheet.Cells[i, 1].Value.ToString().Trim(),
                                    TenLoaiVatTu = workSheet.Cells[i, 2].Value.ToString().Trim(),
                                    TenVT = workSheet.Cells[i, 3].Value.ToString().Trim(),
                                    TenDVT = workSheet.Cells[i, 4].Value.ToString().Trim(),
                                    GhiChu = workSheet.Cells[i, 5].Value.ToString().Trim(),
                                    Status = true,
                                });
                            }
                        }
                        foreach (var item in List)
                        {
                            // kt vat tu ton tai
                            using (var transaction = db.Database.BeginTransaction())
                            {
                                try
                                {
                                    var MaHM = await _hangMucVatTuRepository.CheckTonTai(item.TenHM);
                                    if (MaHM != -1)
                                    {
                                        var MaVT = await CheckTonTai(item.TenVT);
                                        if (MaVT == -1) // insert vật tư mới
                                        {
                                            var MaLoaiVatTu = await _loaiVatTuRepository.CheckTonTai(item.TenLoaiVatTu);
                                            if (MaLoaiVatTu == -1) // thêm loại vật tư mới
                                            {
                                                LoaiVatTuViewModel model = new LoaiVatTuViewModel()
                                                {
                                                    TenLoai = item.TenLoaiVatTu,
                                                    MaHM = MaHM,
                                                };
                                                var rsLVT = await _loaiVatTuRepository.InsertAsync(model);
                                                if (rsLVT == true)
                                                {
                                                    MaLoaiVatTu = (await db.LoaiVatTus.FirstOrDefaultAsync(x => x.TenLoai == item.TenLoaiVatTu)).MaLoaiVatTu;
                                                }
                                            }
                                            else // cập nhật loại vật tư vào danh mục
                                            {
                                                var entity = await db.LoaiVatTus.FindAsync(MaLoaiVatTu);
                                                entity.MaHM = MaHM;
                                                await db.SaveChangesAsync();
                                            }
                                            int? MaDVT = null;
                                            if ((item.TenDVT.Trim()) != "" && item.TenDVT != null)
                                            {
                                                MaDVT = await _donViTinhRepository.CheckTonTai(item.TenDVT);
                                                if (MaDVT == -1)
                                                {
                                                    // them moi don vi tinh
                                                    DonViTinhViewModel model = new DonViTinhViewModel()
                                                    {
                                                        TenDVT = item.TenDVT
                                                    };
                                                    var rsDVT = await _donViTinhRepository.InsertAsync(model);
                                                    if (rsDVT == true)
                                                    {
                                                        // lay MaDVT moi
                                                        MaDVT = (await db.DonViTinhs.FirstOrDefaultAsync(x => x.TenDVT == item.TenDVT)).MaDVT;
                                                    }
                                                }
                                            }
                                            VatTuViewModel vt = new VatTuViewModel()
                                            {
                                                TenVT = item.TenVT,
                                                MaDVT = MaDVT,
                                                MaLoaiVatTu = MaLoaiVatTu,
                                                GhiChu = item.GhiChu                   // status mặc định = true                                
                                            };
                                            var rsVT = await this.InsertAsync(vt);
                                        }
                                        else // update vật tư
                                        {
                                            // check xem được sử dụng, nếu chưa thì cho update
                                            if (await CheckSuDung(MaVT) == false)
                                            {
                                                var MaLoaiVatTu = await _loaiVatTuRepository.CheckTonTai(item.TenLoaiVatTu);
                                                if (MaLoaiVatTu == -1)
                                                {
                                                    // them moi loai vat tu
                                                    LoaiVatTuViewModel model = new LoaiVatTuViewModel()
                                                    {
                                                        TenLoai = item.TenLoaiVatTu,
                                                        MaHM = MaHM,
                                                    };
                                                    var rsLVT = await _loaiVatTuRepository.InsertAsync(model);
                                                    if (rsLVT == true)
                                                    {
                                                        // lay MaLoaiVatTu moi
                                                        MaLoaiVatTu = (await db.LoaiVatTus.FirstOrDefaultAsync(x => x.TenLoai == item.TenLoaiVatTu)).MaLoaiVatTu;
                                                    }
                                                }
                                                else // update loại vào danh mục 
                                                {
                                                    var entity = await db.LoaiVatTus.FindAsync(MaLoaiVatTu);
                                                    entity.MaHM = MaHM;
                                                    await db.SaveChangesAsync();
                                                }
                                                int? MaDVT = null;
                                                if ((item.TenDVT.Trim()) != "" && item.TenDVT != null)
                                                {
                                                    MaDVT = await _donViTinhRepository.CheckTonTai(item.TenDVT);
                                                    if (MaDVT == -1)
                                                    {
                                                        // them moi don vi tinh
                                                        DonViTinhViewModel model = new DonViTinhViewModel()
                                                        {
                                                            TenDVT = item.TenDVT
                                                        };
                                                        var rsDVT = await _donViTinhRepository.InsertAsync(model);
                                                        if (rsDVT == true)
                                                        {
                                                            // lay MaDVT moi
                                                            MaDVT = (await db.DonViTinhs.FirstOrDefaultAsync(x => x.TenDVT == item.TenDVT)).MaDVT;
                                                        }
                                                    }
                                                }
                                                VatTuViewModel vt = new VatTuViewModel()
                                                {
                                                    MaVatTu = MaVT,
                                                    TenVT = item.TenVT,
                                                    MaDVT = MaDVT,
                                                    MaLoaiVatTu = MaLoaiVatTu,
                                                    GhiChu = item.GhiChu      // status mặc định = true                                             
                                                };
                                                var rsVT = await this.UpdateAsync(vt);
                                            }
                                        }
                                        transaction.Commit();
                                        await db.SaveChangesAsync();
                                    }
                                }
                                catch (Exception)
                                {
                                    return false;
                                }
                            }
                        }
                        file.Delete();
                        return true;
                    }
                }
                catch (Exception ex)
                {
                    FileInfo fileInfo = new FileInfo(fileUrl);
                    fileInfo.Delete();
                    throw ex;
                }
            }
            else
            {
                return false;
            }
        }
        public object ExportVT(List<VatTuViewModel> vatTuViewModels)
        {
            try
            {
                string rootFolder = _hostingEnvironment.WebRootPath;
                var gui = Guid.NewGuid();
                string fileName = @"ExportVatTu" + gui + ".xls";

                FileInfo file = new FileInfo(Path.Combine(rootFolder, fileName));

                using (ExcelPackage package = new ExcelPackage(file))
                {

                    // IList<VatTu> List = db.VatTus.ToList();\
                    var vts = mp.Map<List<VatTu>>(vatTuViewModels);
                    var listvt = from vt in vts
                                 join dvt in db.DonViTinhs on vt.MaDVT equals dvt.MaDVT into tmpdonvitinh
                                 join lvt in db.LoaiVatTus on vt.MaLoaiVatTu equals lvt.MaLoaiVatTu
                                 join hmvt in db.HangMucVatTus on lvt.MaHM equals hmvt.MaHM
                                 from dvt in tmpdonvitinh.DefaultIfEmpty()
                                 select new VatTuViewModel
                                 {
                                     TenVT = vt.TenVT,
                                     TenLoaiVatTu = lvt.TenLoai,
                                     TenDVT = dvt.TenDVT,
                                     TenHM = hmvt.TenHM,
                                     GhiChu = vt.GhiChu
                                 };
                    IList<VatTuViewModel> List = listvt.ToList();

                    ExcelWorksheet worksheet = package.Workbook.Worksheets.Add("VatTu");
                    int totalRows = List.Count();
                    worksheet.Cells[1, 1].Value = "TenHM";
                    worksheet.Cells[1, 2].Value = "TenLoaiVatTu";
                    worksheet.Cells[1, 3].Value = "TenVT";
                    worksheet.Cells[1, 4].Value = "TenDVT";
                    worksheet.Cells[1, 5].Value = "GhiChu";
                    int i = 0;
                    for (int row = 2; row <= totalRows + 1; row++)
                    {
                        worksheet.Cells[row, 1].Value = List[i].TenHM;
                        worksheet.Cells[row, 2].Value = List[i].TenLoaiVatTu;
                        worksheet.Cells[row, 3].Value = List[i].TenVT;
                        worksheet.Cells[row, 4].Value = List[i].TenDVT;
                        worksheet.Cells[row, 5].Value = List[i].GhiChu;
                        i++;
                    }
                    package.Save();


                }
                string url = "";
                if (_IHttpContextAccessor.HttpContext.Request.IsHttps)
                {
                    url = "https://" + _IHttpContextAccessor.HttpContext.Request.Host;
                }
                else
                {
                    url = "http://" + _IHttpContextAccessor.HttpContext.Request.Host;
                }
                return (new
                {
                    url = url + "/" + fileName,
                    fileName
                });
            }
            catch (Exception ex)
            {
                throw ex;
                //                 return false;
            }
        }
        public bool DeleteFileVTAfterExport(string filename)
        {
            if (!String.IsNullOrEmpty(filename))
            {
                string rootFolder = _hostingEnvironment.WebRootPath;

                FileInfo file = new FileInfo(Path.Combine(rootFolder, filename));
                try
                {
                    using (ExcelPackage package = new ExcelPackage(file))
                    {
                        file.Delete();
                        return true;
                    }
                }
                catch (Exception)
                {
                    return false;
                }
            }
            else
            {
                return false;
            }
        }
        public async Task<PagedList<ThongKeVatTuParam>> ThongKeVatTuNhapByMaVT(PagingParams pagingParams, int mavt)
        {
            var query = from vt in db.VatTus
                        join nct in db.NhapChiTiets on vt.MaVatTu equals nct.MaVatTu
                        join nvt in db.NhapVatTus on nct.MaPhieuNhap equals nvt.MaPhieuNhap
                        where vt.MaVatTu == mavt
                        select new ThongKeVatTuParam
                        {
                            MaPN = nct.MaPhieuNhap,
                            NgayNhap = nvt.NgayNhap,
                            SoLuong = nct.SoLuong,
                            DonGia = nct.DonGia,
                            ChietKhau = nvt.ChietKhau,
                            ThanhTien = nct.SoLuong * nct.DonGia * (1 - (nvt.ChietKhau / 100))
                        };

            if (!string.IsNullOrEmpty(pagingParams.Keyword))
            {
                var keyword = pagingParams.Keyword.ToUpper().ToTrim();

                if (DateTime.TryParseExact(keyword, "dd/MM/yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out var date))
                {
                    query = query.Where(x => DateTime.Parse(x.NgayNhap).Day == date.Day && DateTime.Parse(x.NgayNhap).Month == date.Month && DateTime.Parse(x.NgayNhap).Year == date.Year);
                }
                else
                {
                    query = query.Where(x => x.MaPN.ToString().Equals(keyword) ||
                                        x.SoLuong.ToString().Equals(keyword) ||
                                        x.DonGia.ToString().Equals(keyword) ||
                                        x.ChietKhau.ToString().Equals(keyword) ||
                                        x.ThanhTien.ToString().Equals(keyword));
                }
            }

            if (!string.IsNullOrEmpty(pagingParams.SortValue) && !pagingParams.SortValue.Equals("null") && !pagingParams.SortValue.Equals("undefined"))
            {
                switch (pagingParams.SortKey)
                {
                    case "maPN":
                        if (pagingParams.SortValue == "ascend")
                        {
                            query = query.OrderBy(x => x.MaPN);
                        }
                        else
                        {
                            query = query.OrderByDescending(x => x.MaPN);
                        }
                        break;
                    case "ngayNhap":
                        if (pagingParams.SortValue == "ascend")
                        {
                            query = query.OrderBy(x => x.NgayNhap);
                        }
                        else
                        {
                            query = query.OrderByDescending(x => x.NgayNhap);
                        }
                        break;
                    case "soLuong":
                        if (pagingParams.SortValue == "ascend")
                        {
                            query = query.OrderBy(x => x.SoLuong);
                        }
                        else
                        {
                            query = query.OrderByDescending(x => x.SoLuong);
                        }
                        break;
                    case "donGia":
                        if (pagingParams.SortValue == "ascend")
                        {
                            query = query.OrderBy(x => x.DonGia);
                        }
                        else
                        {
                            query = query.OrderByDescending(x => x.DonGia);
                        }
                        break;
                    case "chietKhau":
                        if (pagingParams.SortValue == "ascend")
                        {
                            query = query.OrderBy(x => x.ChietKhau);
                        }
                        else
                        {
                            query = query.OrderByDescending(x => x.ChietKhau);
                        }
                        break;
                    case "thanhTien":
                        if (pagingParams.SortValue == "ascend")
                        {
                            query = query.OrderBy(x => x.ThanhTien);
                        }
                        else
                        {
                            query = query.OrderByDescending(x => x.ThanhTien);
                        }
                        break;
                    default:
                        break;
                }
            }

            return await PagedList<ThongKeVatTuParam>.CreateAsync(query, pagingParams.PageNumber, pagingParams.PageSize);
        }
        public async Task<PagedList<ThongKeVatTuParam>> ThongKeVatTuXuatpByMaVT(PagingParams pagingParams, int mavt)
        {
            var query = from vt in db.VatTus
                        join xct in db.XuatChiTiets on vt.MaVatTu equals xct.MaVatTu
                        join xvt in db.XuatVatTus on xct.MaPhieuXuat equals xvt.MaPhieuXuat
                        where vt.MaVatTu == mavt
                        select new ThongKeVatTuParam
                        {
                            MaPX = xct.MaPhieuXuat,
                            NgayXuat = xvt.NgayNhap,
                            SoLuong = xct.SoLuongXuat,
                            DonGia = xct.DonGia,
                            ChietKhau = xvt.ChietKhau,
                            ThanhTien = xct.SoLuongXuat * xct.DonGia * (1 - (xvt.ChietKhau / 100))
                        };

            if (!string.IsNullOrEmpty(pagingParams.Keyword))
            {
                var keyword = pagingParams.Keyword.ToUpper().ToTrim();

                if (DateTime.TryParseExact(keyword, "dd/MM/yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out var date))
                {
                    query = query.Where(x => DateTime.Parse(x.NgayXuat).Day == date.Day && DateTime.Parse(x.NgayXuat).Month == date.Month && DateTime.Parse(x.NgayXuat).Year == date.Year);
                }
                else
                {
                    query = query.Where(x => x.MaPX.ToString().Equals(keyword) ||
                                        x.SoLuong.ToString().Equals(keyword) ||
                                        x.DonGia.ToString().Equals(keyword) ||
                                        x.ChietKhau.ToString().Equals(keyword) ||
                                        x.ThanhTien.ToString().Equals(keyword));
                }
            }

            if (!string.IsNullOrEmpty(pagingParams.SortValue) && !pagingParams.SortValue.Equals("null") && !pagingParams.SortValue.Equals("undefined"))
            {
                switch (pagingParams.SortKey)
                {
                    case "maPX":
                        if (pagingParams.SortValue == "ascend")
                        {
                            query = query.OrderBy(x => x.MaPX);
                        }
                        else
                        {
                            query = query.OrderByDescending(x => x.MaPX);
                        }
                        break;
                    case "ngayXuat":
                        if (pagingParams.SortValue == "ascend")
                        {
                            query = query.OrderBy(x => x.NgayXuat);
                        }
                        else
                        {
                            query = query.OrderByDescending(x => x.NgayXuat);
                        }
                        break;
                    case "soLuong":
                        if (pagingParams.SortValue == "ascend")
                        {
                            query = query.OrderBy(x => x.SoLuong);
                        }
                        else
                        {
                            query = query.OrderByDescending(x => x.SoLuong);
                        }
                        break;
                    case "donGia":
                        if (pagingParams.SortValue == "ascend")
                        {
                            query = query.OrderBy(x => x.DonGia);
                        }
                        else
                        {
                            query = query.OrderByDescending(x => x.DonGia);
                        }
                        break;
                    case "chietKhau":
                        if (pagingParams.SortValue == "ascend")
                        {
                            query = query.OrderBy(x => x.ChietKhau);
                        }
                        else
                        {
                            query = query.OrderByDescending(x => x.ChietKhau);
                        }
                        break;
                    case "thanhTien":
                        if (pagingParams.SortValue == "ascend")
                        {
                            query = query.OrderBy(x => x.ThanhTien);
                        }
                        else
                        {
                            query = query.OrderByDescending(x => x.ThanhTien);
                        }
                        break;
                    default:
                        break;
                }
            }

            return await PagedList<ThongKeVatTuParam>.CreateAsync(query, pagingParams.PageNumber, pagingParams.PageSize);
        }
        public async Task<PagedList<KhoHangViewModel>> ThongKeVatTuTonKhoByMaVT(PagingParams pagingParams, int mavt)
        {
            var query = from kh in db.KhoHangs
                        join kvt in db.KhoVatTus on kh.MaKho equals kvt.MaKho
                        join vt in db.VatTus on kh.MaVatTu equals vt.MaVatTu
                        where vt.MaVatTu == mavt
                        select new KhoHangViewModel
                        {
                            MaKho = kh.MaKho,
                            MaPhieuNhap = kh.MaPhieuNhap,
                            MaVatTu = kh.MaVatTu,
                            SoLuongTon = kh.SoLuongTon,
                            TenKho = kvt.TenKho,
                            TenVatTu = vt.TenVT
                        };

            if (!string.IsNullOrEmpty(pagingParams.Keyword))
            {
                var keyword = pagingParams.Keyword.ToUpper().ToTrim();

                query = query.Where(x => x.TenKho.ToUpper().ToUnSign().Contains(keyword.ToUnSign()) ||
                                        x.TenKho.ToUpper().Contains(keyword) ||
                                        x.TenVatTu.ToUpper().ToUnSign().Contains(keyword.ToUnSign()) ||
                                        x.TenVatTu.ToUpper().Contains(keyword) ||
                                        x.MaPhieuNhap.Equals(keyword) ||
                                        x.SoLuongTon.Equals(keyword));
            }

            if (!string.IsNullOrEmpty(pagingParams.SortValue) && !pagingParams.SortValue.Equals("null") && !pagingParams.SortValue.Equals("undefined"))
            {
                switch (pagingParams.SortKey)
                {
                    case "tenKho":
                        if (pagingParams.SortValue == "ascend")
                        {
                            query = query.OrderBy(x => x.TenKho);
                        }
                        else
                        {
                            query = query.OrderByDescending(x => x.TenKho);
                        }
                        break;
                    case "tenVatTu":
                        if (pagingParams.SortValue == "ascend")
                        {
                            query = query.OrderBy(x => x.TenVatTu);
                        }
                        else
                        {
                            query = query.OrderByDescending(x => x.TenVatTu);
                        }
                        break;
                    case "maPhieuNhap":
                        if (pagingParams.SortValue == "ascend")
                        {
                            query = query.OrderBy(x => x.MaPhieuNhap);
                        }
                        else
                        {
                            query = query.OrderByDescending(x => x.MaPhieuNhap);
                        }
                        break;
                    case "soLuongTon":
                        if (pagingParams.SortValue == "ascend")
                        {
                            query = query.OrderBy(x => x.SoLuongTon);
                        }
                        else
                        {
                            query = query.OrderByDescending(x => x.SoLuongTon);
                        }
                        break;
                    default:
                        break;
                }
            }
            return await PagedList<KhoHangViewModel>.CreateAsync(query, pagingParams.PageNumber, pagingParams.PageSize);
        }
    }
}
