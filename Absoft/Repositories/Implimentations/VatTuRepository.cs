﻿using Absoft.Data;
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
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
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
            var vt = await db.VatTus.FindAsync(id);
            var mvt = mp.Map<VatTuViewModel>(vt);
            return mvt;
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
                            if ((workSheet.Cells[i, 1].Value) != null && (workSheet.Cells[i, 2].Value) != null && (workSheet.Cells[i, 4].Value) != null)
                            {
                                if ((workSheet.Cells[i, 3].Value) == null) workSheet.Cells[i, 3].Value = "";
                                if ((workSheet.Cells[i, 5].Value) == null) workSheet.Cells[i, 5].Value = "";
                                List.Add(new VatTuViewModel
                                {
                                    TenVT = workSheet.Cells[i, 1].Value.ToString(),
                                    TenLoaiVatTu = workSheet.Cells[i, 2].Value.ToString(),
                                    TenDVT = workSheet.Cells[i, 3].Value.ToString(),
                                    TenHM = workSheet.Cells[i, 4].Value.ToString(),
                                    GhiChu = workSheet.Cells[i, 5].Value.ToString(),
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
                                    var MaVT = await CheckTonTai(item.TenVT);
                                    if (MaVT == -1 && item.TenLoaiVatTu != null && !string.IsNullOrEmpty(item.TenLoaiVatTu) && item.TenHM != null && !string.IsNullOrEmpty(item.TenHM))
                                    {
                                        var MaHM = await _hangMucVatTuRepository.CheckTonTai(item.TenHM);
                                        if (MaHM == -1)
                                        {
                                            // them moi hang muc
                                            HangMucVatTuViewModel model = new HangMucVatTuViewModel()
                                            {
                                                TenHM = item.TenHM
                                            };
                                            var rsHM = await _hangMucVatTuRepository.InsertAsync(model);
                                            // lay MaHM moi
                                            if (rsHM == true)
                                            {
                                                MaHM = (await db.HangMucVatTus.FirstOrDefaultAsync(x => x.TenHM == item.TenHM)).MaHM;
                                            }
                                        }
                                        var MaLoaiVatTu = await _loaiVatTuRepository.CheckTonTai(item.TenLoaiVatTu);
                                        if (MaLoaiVatTu == -1)
                                        {
                                            // them moi loai vat tu
                                            LoaiVatTuViewModel model = new LoaiVatTuViewModel()
                                            {
                                                TenLoai = item.TenLoaiVatTu,
                                                MaHM = MaHM
                                            };
                                            var rsLVT = await _loaiVatTuRepository.InsertAsync(model);
                                            if (rsLVT == true)
                                            {
                                                // lay MaLoaiVatTu moi
                                                MaLoaiVatTu = (await db.LoaiVatTus.FirstOrDefaultAsync(x => x.TenLoai == item.TenLoaiVatTu)).MaLoaiVatTu;
                                            }
                                        }
                                        int? MaDVT = null;
                                        if (!string.IsNullOrEmpty(item.TenDVT) && item.TenDVT != null)
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
                                            GhiChu = item.GhiChu
                                        };
                                        var rsVT = await this.InsertAsync(vt);
                                    }
                                    transaction.Commit();
                                    await db.SaveChangesAsync();
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
                    throw ex;
                }
            }
            else
            {
                return false;
            }
        }
        public object ExportVT()
        {
            try
            {
                string rootFolder = _hostingEnvironment.WebRootPath;
                var gui = Guid.NewGuid();
                string fileName = @"ExportVatTu" + gui + ".xls";

                FileInfo file = new FileInfo(Path.Combine(rootFolder, fileName));

                using (ExcelPackage package = new ExcelPackage(file))
                {

                    // IList<VatTu> List = db.VatTus.ToList();
                    var listvt = from vt in db.VatTus
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

                    worksheet.Cells[1, 1].Value = "TenVT";
                    worksheet.Cells[1, 2].Value = "TenLoaiVatTu";
                    worksheet.Cells[1, 3].Value = "TenDVT";
                    worksheet.Cells[1, 4].Value = "TenHM";
                    worksheet.Cells[1, 5].Value = "GhiChu";
                    int i = 0;
                    for (int row = 2; row <= totalRows + 1; row++)
                    {
                        worksheet.Cells[row, 1].Value = List[i].TenVT;
                        worksheet.Cells[row, 2].Value = List[i].TenLoaiVatTu;
                        worksheet.Cells[row, 3].Value = List[i].TenDVT;
                        worksheet.Cells[row, 4].Value = List[i].TenHM;
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
                   url =  url + "/" + fileName,
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
    }
}
