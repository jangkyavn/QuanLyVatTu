using Absoft.Data;
using Absoft.Data.Entities;
using Absoft.Helpers;
using Absoft.Repositories.Interfaces;
using Absoft.ViewModels;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Absoft.Repositories.Implimentations
{
    public class HangSanXuatRepository: IHangSanXuatRepository
    {
        DataContext db;
        IMapper mp;
        public HangSanXuatRepository(DataContext data, IMapper mapper)
        {
            db = data;
            mp = mapper;

        }

        public async Task<bool> IsDelete(int id)
        {
            var entity = await db.HangSanXuats.FindAsync(id);
            entity.Status = false;
            return await db.SaveChangesAsync() > 0;
        }
        public async Task<bool> DeleteAsync(int id)
        {
            
                var hangsanxuat = await db.HangSanXuats.FindAsync(id);
                db.HangSanXuats.Remove(hangsanxuat);
                return await db.SaveChangesAsync() > 0;
                     
        }
        public async Task<List<HangSanXuatViewModel>> GetAllAsync()
        {
            return await db.HangSanXuats.Where(x => x.Status == true)
                .ProjectTo<HangSanXuatViewModel>(mp.ConfigurationProvider)
                .ToListAsync();
        }

        public async Task<HangSanXuatViewModel> GetByIdAsync(int id)
        {
            return mp.Map<HangSanXuatViewModel>(await db.HangSanXuats.FindAsync(id));
        }

        public async Task<bool> InsertAsync(HangSanXuatViewModel mhangsanxuat)
        {
            var hangsanxuat = mp.Map<HangSanXuat>(mhangsanxuat);
            await db.HangSanXuats.AddAsync(hangsanxuat);
            return await db.SaveChangesAsync() > 0;
        }

        public async Task<bool> UpdateAsync(HangSanXuatViewModel mhangsanxuat)
        {
            var hangsanxuat = mp.Map<HangSanXuat>(mhangsanxuat);
            db.HangSanXuats.Update(hangsanxuat);        
            return (await db.SaveChangesAsync()) > 0;
            
        }
        public async Task<int> CheckTonTai(string name)
        {
            var rs = await db.HangSanXuats.AnyAsync(x => x.TenHang.ToUpper().Equals(name.ToUpper().ToTrim()));
            if (rs == true)
            {
                return (await db.HangSanXuats.FirstOrDefaultAsync(x => x.TenHang.ToUpper().ToTrim() == name.ToUpper().ToTrim())).MaHang;
            }
            else return -1;
        }
        public async Task<bool> ChangStatus(int id)
        {
            var model = await db.HangSanXuats.FindAsync(id);
            model.Status = !model.Status;
            return await db.SaveChangesAsync() > 0;
        }
        public async Task<bool> GetStatus(int id)
        {
            return (await db.HangSanXuats.FindAsync(id)).Status;
        }
        public async Task<PagedList<HangSanXuatViewModel>> GetAllPagingAsync(PagingParams pagingParams)
        {
            var query = from hsx in db.HangSanXuats
                          where hsx.Status == true
                          select new HangSanXuatViewModel
                          {
                              MaHang = hsx.MaHang,
                              TenHang = hsx.TenHang,
                              GhiChu = hsx.GhiChu,
                              Status = hsx.Status
                          };

            if (!string.IsNullOrEmpty(pagingParams.Keyword))
            {
                var keyword = pagingParams.Keyword.ToUpper().ToTrim();

                query = query.Where(x => x.TenHang.ToUpper().ToUnSign().Contains(keyword.ToUnSign()) ||
                                        x.TenHang.ToUpper().Contains(keyword) ||
                                        x.GhiChu.ToUpper().ToUnSign().Contains(keyword.ToUnSign()) ||
                                        x.GhiChu.ToUpper().Contains(keyword));
            }

            if (!string.IsNullOrEmpty(pagingParams.SortValue) && !pagingParams.SortValue.Equals("null") && !pagingParams.SortValue.Equals("undefined"))
            {
                switch (pagingParams.SortKey)
                {
                    case "tenHang":
                        if (pagingParams.SortValue == "ascend")
                        {
                            query = query.OrderBy(x => x.TenHang);
                        }
                        else
                        {
                            query = query.OrderByDescending(x => x.TenHang);
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

            return await PagedList<HangSanXuatViewModel>.CreateAsync(query, pagingParams.PageNumber, pagingParams.PageSize);
        }
    }
}
