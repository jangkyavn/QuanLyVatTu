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
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace Absoft.Repositories.Implimentations
{
    public class DonViTinhRepository : IDonViTinhRepository
    {
        DataContext db;
        IMapper mp;
        private readonly IHostingEnvironment _hostingEnvironment;
        public DonViTinhRepository(DataContext data, IMapper mapper, IHostingEnvironment hostingEnvironmen)
        {
            db = data;
            mp = mapper;
            _hostingEnvironment = hostingEnvironmen;
        }
        public async Task<List<DonViTinhViewModel>> GetAllAsync()
        {
            return await db.DonViTinhs.Where(x => x.Status == true)
                .OrderByDescending(x => x.MaDVT)
                .ProjectTo<DonViTinhViewModel>(mp.ConfigurationProvider)
                .ToListAsync();
        }
        public async Task<DonViTinhViewModel> GetById(int id)
        {
            var dvt = await db.DonViTinhs.FindAsync(id);
            return mp.Map<DonViTinhViewModel>(dvt);
        }

        public async Task<bool> InsertAsync(DonViTinhViewModel mdonvitinh)
        {
            var dvt = mp.Map<DonViTinh>(mdonvitinh);
            await db.DonViTinhs.AddAsync(dvt);
            return await db.SaveChangesAsync() > 0;
        }
        public async Task<bool> InsertListAsync(List<DonViTinhViewModel> mdonvitinh)
        {
            using (var transaction = db.Database.BeginTransaction())
            {
                try
                {
                    var dvt = mp.Map<List<DonViTinh>>(mdonvitinh);
                    foreach (var item in dvt)
                    {
                        await db.DonViTinhs.AddAsync(item);
                    }
                    transaction.Commit();
                    return await db.SaveChangesAsync() > 0;
                }
                catch (Exception)
                {
                    return false;
                }
            }
        }
        public async Task<bool> UpdateAsync(DonViTinhViewModel mdonvitinh)
        {
            var entity = mp.Map<DonViTinh>(mdonvitinh);
            var model = await db.DonViTinhs.FindAsync(mdonvitinh.MaDVT);
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
        // ko len su dung vi anh huong den bang vattu
        public async Task<bool> DeleteAsync(int id)
        {
            try
            {
                var dvt = await db.DonViTinhs.FindAsync(id);
                db.DonViTinhs.Remove(dvt);
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
                        var dvt = await db.DonViTinhs.FindAsync(id);
                        if (dvt != null)
                        {
                            db.DonViTinhs.Remove(dvt);
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
        public async Task<bool> IsDelete(int id)
        {
            var dvt = await db.DonViTinhs.FindAsync(id);
            dvt.Status = false;
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
                        var dvt = await db.DonViTinhs.FindAsync(item);
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
        public async Task<int> CheckTonTai(string name)
        {
            var rs = await db.DonViTinhs.AnyAsync(x => x.TenDVT.ToUpper().Equals(name.ToUpper().ToTrim()));
            if (rs == true)
            {
                return (await db.DonViTinhs.FirstOrDefaultAsync(x => x.TenDVT.ToUpper().ToTrim() == name.ToUpper().ToTrim())).MaDVT;
            }
            else return -1;
        }
        public async Task<bool> ChangStatus(int id)
        {
            var model = await db.DonViTinhs.FindAsync(id);
            model.Status = !model.Status;
            return await db.SaveChangesAsync() > 0;
        }
        public async Task<bool> GetStatus(int id)
        {
            return (await db.DonViTinhs.FindAsync(id)).Status;
        }
        public async Task<PagedList<DonViTinhViewModel>> GetAllPagingAsync(PagingParams pagingParams)
        {
            var query = from dvt in db.DonViTinhs
                        where dvt.Status == true
                        orderby dvt.MaDVT descending
                        select new DonViTinhViewModel
                        {
                            MaDVT = dvt.MaDVT,
                            TenDVT = dvt.TenDVT,
                            Status = dvt.Status
                        };
            if (!string.IsNullOrEmpty(pagingParams.KeywordCol))
            {
                if (pagingParams.ColName == "tenDVT")
                    query = query.Where(x => x.TenDVT == pagingParams.KeywordCol.Trim());             
            }
            if (!string.IsNullOrEmpty(pagingParams.Keyword))
            {
                var keyword = pagingParams.Keyword.ToUpper().ToTrim();

                query = query.Where(x => x.TenDVT.ToUpper().ToUnSign().Contains(keyword.ToUnSign()) ||
                                        x.TenDVT.ToUpper().Contains(keyword));
            }
            if (!string.IsNullOrEmpty(pagingParams.SortValue) && !pagingParams.SortValue.Equals("null") && !pagingParams.SortValue.Equals("undefined"))
            {
                switch (pagingParams.SortKey)
                {
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
                    default:
                        break;
                }
            }
            return await PagedList<DonViTinhViewModel>.CreateAsync(query, pagingParams.PageNumber, pagingParams.PageSize);
        }

        public async Task<bool> ImportDVT(IList<IFormFile> files)
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
                        ExcelWorksheet workSheet = package.Workbook.Worksheets["DonViTinh"];
                        int totalRows = workSheet.Dimension.Rows;
                        List<DonViTinhViewModel> List = new List<DonViTinhViewModel>();
                        for (int i = 2; i <= totalRows; i++)
                        {
                            List.Add(new DonViTinhViewModel
                            {
                                TenDVT = workSheet.Cells[i, 1].Value.ToString(),
                                Status = true,
                            });
                        }
                        var rs = await this.InsertListAsync(List);
                        if (rs == true)
                        {
                            file.Delete();
                            return true;
                        }
                        else return false;
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
        public bool ExportDVT()
        {
            try
            {
                string rootFolder = _hostingEnvironment.WebRootPath;
                string fileName = @"ExportDonViTinh.xls";

                FileInfo file = new FileInfo(Path.Combine(rootFolder, fileName));

                using (ExcelPackage package = new ExcelPackage(file))
                {

                    IList<DonViTinh> List = db.DonViTinhs.ToList();

                    ExcelWorksheet worksheet = package.Workbook.Worksheets.Add("DonViTinh");
                    int totalRows = List.Count();

                    // worksheet.Cells[1, 1].Value = "Id";
                    worksheet.Cells[1, 1].Value = "TenDVT";
                    //worksheet.Cells[1, 3].Value = "Status";                
                    int i = 0;
                    for (int row = 2; row <= totalRows + 1; row++)
                    {
                        // worksheet.Cells[row, 1].Value = List[i].Id;
                        worksheet.Cells[row, 1].Value = List[i].TenDVT;
                        // worksheet.Cells[row, 3].Value = List[i].Status;                    
                        i++;
                    }
                    package.Save();
                }
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
    }
}
