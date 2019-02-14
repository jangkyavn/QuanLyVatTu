using Absoft.Data;
using Absoft.Data.Entities;
using Absoft.Helpers;
using Absoft.Repositories.Interfaces;
using Absoft.ViewModels;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Absoft.Repositories.Implimentations
{
    public class VatTuRepository : IVatTuRepository
    {
        DataContext db;
        IMapper mp;
        public VatTuRepository(DataContext data, IMapper mapper)
        {
            db = data;
            mp = mapper;
        }
        public async Task<bool> DeleteAsync(int id)
        {
           
                var vt = await db.VatTus.FindAsync(id);
                db.VatTus.Remove(vt);
                return await db.SaveChangesAsync() > 0;
          
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
                        where vt.Status==true
                        select new VatTuViewModel
                        {
                            MaVatTu= vt.MaVatTu,
                            MaLoaiVatTu = vt.MaLoaiVatTu,
                            MaDVT =  vt.MaDVT,
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
            var listVT = await db.VatTus.Where(o => listMaLoaiVT.Contains(o.MaLoaiVatTu)).ToListAsync();
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
            var vt = mp.Map<VatTu>(mvattu);
            db.VatTus.Update(vt);
            return await db.SaveChangesAsync() > 0;
        }
        public async Task<bool> IsDelete(int id)
        {
            var entity = await db.VatTus.FindAsync(id);
            entity.Status = false;
            return await db.SaveChangesAsync() > 0;
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
        public async Task<PagedList<VatTuViewModel>> GetAllPagingAsync(PagingParams pagingParams)
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
    }
}
