using Absoft.Data;
using Absoft.Data.Entities;
using Absoft.Extentions;
using Absoft.Helpers;
using Absoft.Repositories.Interfaces;
using Absoft.ViewModels;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;

namespace Absoft.Repositories.Implimentations
{
    public class NhanSuRepository : INhanSuRepository
    {
        private readonly DataContext db;
        private readonly IMapper mp;
        private readonly IHostingEnvironment _hostingEnvironment;

        public NhanSuRepository(DataContext data, IMapper mapper, IHostingEnvironment hostingEnvironment)
        {
            db = data;
            mp = mapper;
            _hostingEnvironment = hostingEnvironment;
        }
        public async Task<bool> IsDelete(int id)
        {
            var entity = await db.NhanSus.FindAsync(id);
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
                        var dvt = await db.NhanSus.FindAsync(item);
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
        public async Task<bool> DeleteAsync(int id)
        {
            try
            {
                var ns = await db.NhanSus.FindAsync(id);
                db.NhanSus.Remove(ns);
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
                        var dvt = await db.NhanSus.FindAsync(id);
                        if (dvt != null)
                        {
                            db.NhanSus.Remove(dvt);
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
        public async Task<List<NhanSuViewModel>> GetAllAsync()
        {
            return await db.NhanSus.Where(x => x.Status == true)
                .OrderByDescending(x => x.MaNS)
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
            var entity = mp.Map<NhanSu>(mnhansu);
            var model = await db.NhanSus.FindAsync(mnhansu.MaNS);
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
            var ss = "311,2".Split(',')[1];

            var query = from ns in db.NhanSus
                        where ns.Status == true
                        orderby ns.MaNS descending
                        select new NhanSuViewModel
                        {
                            MaNS = ns.MaNS,
                            HoTen = ns.HoTen ?? string.Empty,
                            NgaySinh = ns.NgaySinh ?? string.Empty,
                            QueQuan = GetAddress(ns.QueQuan) ?? string.Empty,
                            DanToc = GetNation(ns.DanToc) ?? string.Empty,
                            TonGiao = ns.TonGiao,
                            CapBacSHSQ = ns.CapBacSHSQ,
                            ChucVu = ns.ChucVu,
                            NgayNhapNguTuyenDung = ns.NgayNhapNguTuyenDung,
                            Dang = ns.Dang,
                            XuatThan = ns.XuatThan,
                            BanThan = ns.BanThan,
                            QuaTrinhHocTap = ns.QuaTrinhHocTap,
                            ThanNhan = ns.ThanNhan,
                            GhiChu = ns.GhiChu,
                            Status = ns.Status
                        };

            if (!string.IsNullOrEmpty(pagingParams.Keyword))
            {
                var keyword = pagingParams.Keyword.ToUpper().ToTrim();

                if (DateTime.TryParseExact(keyword, "dd/MM/yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out var date))
                {
                    query = query.Where(x => DateTime.Parse(x.NgaySinh).Day == date.Day && DateTime.Parse(x.NgaySinh).Month == date.Month && DateTime.Parse(x.NgaySinh).Year == date.Year);
                }
                else
                {
                    query = query.Where(x => x.HoTen.ToUpper().ToUnSign().Contains(keyword.ToUnSign()) ||
                                        x.HoTen.ToUpper().Contains(keyword) ||
                                        x.NgaySinh.ToConvertFullDateFormat().Contains(keyword) ||
                                        x.QueQuan.ToUpper().ToUnSign().Contains(keyword.ToUnSign()) ||
                                        x.QueQuan.ToUpper().Contains(keyword) ||
                                        x.DanToc.ToUpper().ToUnSign().Contains(keyword.ToUnSign()) ||
                                        x.DanToc.ToUpper().Contains(keyword));
                }
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

        public List<CityParam> LoadCities()
        {
            string path = _hostingEnvironment.WebRootPath + "\\json\\city.json";
            var cities = new List<CityParam>().Deserialize(path).OrderBy(x => x.name).ToList();
            return cities;
        }

        public List<DistrictsParam> LoadDistricts(int? cityId)
        {
            string path = _hostingEnvironment.WebRootPath + "\\json\\district.json";
            var districts = new List<DistrictsParam>().Deserialize(path);

            if (cityId.HasValue)
            {
                districts = districts.Where(x => x.parent_code == cityId.Value).OrderBy(x => x.name).ToList();
            }
            return districts;
        }

        public List<NationsParam> LoadNations()
        {
            string path = _hostingEnvironment.WebRootPath + "\\json\\nation.json";
            var nations = new List<NationsParam>().Deserialize(path);
            return nations;
        }

        private string GetAddress(string address)
        {
            var cityId = Convert.ToInt32(address.Split(',')[1]);
            var districtId = Convert.ToInt32(address.Split(',')[0]);

            var city = LoadCities().FirstOrDefault(x => x.code == cityId).name;
            var district = LoadDistricts(cityId).FirstOrDefault(x => x.code == districtId).name;

            return district + ", " + city;
        }

        private string GetNation(string nation)
        {
            int nationId = Convert.ToInt32(nation);

            return LoadNations().FirstOrDefault(x => x.code == nationId).name;
        }
    }
}
