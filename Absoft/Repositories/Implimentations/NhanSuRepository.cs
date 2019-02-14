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
    public class NhanSuRepository : INhanSuRepository
    {
        DataContext db;
        IMapper mp;
        public NhanSuRepository(DataContext data, IMapper mapper)
        {
            db = data;
            mp = mapper;
        }
        public async Task<bool> IsDelete(int id)
        {
            var entity = await db.NhanSus.FindAsync(id);
            entity.Status = false;
            return await db.SaveChangesAsync() > 0;
        }
        public async Task<bool> DeleteAsync(int id)
        {
          
                var ns = await db.NhanSus.FindAsync(id);
                db.NhanSus.Remove(ns);
                return await db.SaveChangesAsync() > 0;
           
        }
        public async Task<List<NhanSuViewModel>> GetAllAsync()
        {
            return await db.NhanSus.Where(x => x.Status == true)
               .ProjectTo<NhanSuViewModel>(mp.ConfigurationProvider)
               .ToListAsync();
        }

        public async Task<NhanSuViewModel> GetByIdAsync(int id)
        {
            var dvt = await db.NhanSus.FindAsync(id);
            var ns = mp.Map<NhanSuViewModel>(dvt);
            return ns;
        }

        public async Task<bool> InsertAsync(NhanSuViewModel mnhansu)
        {
            var ns = mp.Map<NhanSu>(mnhansu);
            await db.NhanSus.AddAsync(ns);
            return await db.SaveChangesAsync() > 0;
        }

        public async Task<bool> UpdateAsync(NhanSuViewModel mnhansu)
        {
            var ns = mp.Map<NhanSu>(mnhansu);
            db.NhanSus.Update(ns);
            return await db.SaveChangesAsync() > 0;
        }
        public async Task<int> CheckTonTai(string name)
        {
            var rs = await db.NhanSus.AnyAsync(x => x.HoTen.ToUpper().Equals(name.ToUpper().ToTrim()));
            if (rs == true)
            {
                return (await db.NhanSus.FirstOrDefaultAsync(x => x.HoTen.ToUpper().ToTrim() == name.ToUpper().ToTrim())).MaNS;
            }
            else return -1;
        }
        public async Task<bool> ChangStatus(int id)
        {
            var model = await db.NhanSus.FindAsync(id);
            model.Status = !model.Status;
            return await db.SaveChangesAsync() > 0;
        }
        public async Task<bool> GetStatus(int id)
        {
            return (await db.NhanSus.FindAsync(id)).Status;
        }
        public async Task<PagedList<NhanSuViewModel>> GetAllPagingAsync(PagingParams pagingParams)
        {
            var query = from ns in db.NhanSus
                          where ns.Status == true
                          select new NhanSuViewModel
                          {
                              MaNS = ns.MaNS,
                              HoTen = ns.HoTen,
                              NgaySinh = ns.NgaySinh,
                              QueQuan = ns.QueQuan,
                              DanToc = ns.DanToc,
                              Status = ns.Status
                          };

            if (!string.IsNullOrEmpty(pagingParams.Keyword))
            {
                var keyword = pagingParams.Keyword.ToUpper().ToTrim();

                query = query.Where(x => x.HoTen.ToUpper().ToUnSign().Contains(keyword.ToUnSign()) ||
                                        x.HoTen.ToUpper().Contains(keyword) ||
                                        x.NgaySinh.Contains(keyword) ||
                                        x.QueQuan.ToUpper().ToUnSign().Contains(keyword.ToUnSign()) ||
                                        x.QueQuan.ToUpper().Contains(keyword) ||
                                        x.DanToc.ToUpper().ToUnSign().Contains(keyword.ToUnSign()) ||
                                        x.DanToc.ToUpper().Contains(keyword));
            }

            if (!string.IsNullOrEmpty(pagingParams.SortValue) && !pagingParams.SortValue.Equals("null") && !pagingParams.SortValue.Equals("undefined"))
            {
                switch (pagingParams.SortKey)
                {
                    case "hoTen":
                        if (pagingParams.SortValue == "ascend")
                        {
                            query = query.OrderBy(x => x.HoTen);
                        }
                        else
                        {
                            query = query.OrderByDescending(x => x.HoTen);
                        }
                        break;
                    case "ngaySinh":
                        if (pagingParams.SortValue == "ascend")
                        {
                            query = query.OrderBy(x => x.NgaySinh);
                        }
                        else
                        {
                            query = query.OrderByDescending(x => x.NgaySinh);
                        }
                        break;
                    case "queQuan":
                        if (pagingParams.SortValue == "ascend")
                        {
                            query = query.OrderBy(x => x.QueQuan);
                        }
                        else
                        {
                            query = query.OrderByDescending(x => x.QueQuan);
                        }
                        break;
                    case "danToc":
                        if (pagingParams.SortValue == "ascend")
                        {
                            query = query.OrderBy(x => x.DanToc);
                        }
                        else
                        {
                            query = query.OrderByDescending(x => x.DanToc);
                        }
                        break;
                    default:
                        break;
                }
            }

            return await PagedList<NhanSuViewModel>.CreateAsync(query, pagingParams.PageNumber, pagingParams.PageSize);
        }
    }
}
