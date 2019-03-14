using Absoft.Data;
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
    public class ChucNangRepository : IChucNangRepository
    {
        private readonly DataContext _dataContext;
        private readonly IMapper _mapper;

        public ChucNangRepository(DataContext dataContext,
            IMapper mapper)
        {
            _dataContext = dataContext;
            _mapper = mapper;
        }

        public async Task<List<ChucNangViewModel>> GetAllAsync()
        {
            return await _dataContext.ChucNangs
                .OrderBy(x => x.ViTri)
                .ProjectTo<ChucNangViewModel>(_mapper.ConfigurationProvider)
                .ToListAsync();
        }

        public async Task<PagedList<ChucNangViewModel>> GetAllPagingAsync(PagingParams pagingParams)
        {
            var query = from cn in _dataContext.ChucNangs
                        where cn.Status == true
                        orderby cn.ViTri
                        select new ChucNangViewModel
                        {
                            MaChucNang = cn.MaChucNang,
                            TenChucNang = cn.TenChucNang,
                            ViTri = cn.ViTri,
                            BieuTuong = cn.BieuTuong,
                            DuongDan = cn.DuongDan,
                            HasRead = (from cnhd in _dataContext.ChucNangHanhDongs
                                       where cnhd.MaChucNang == cn.MaChucNang && cnhd.MaHanhDong == "READ"
                                       select cnhd).Any(),
                            HasCreate = (from cnhd in _dataContext.ChucNangHanhDongs
                                         where cnhd.MaChucNang == cn.MaChucNang && cnhd.MaHanhDong == "CREATE"
                                         select cnhd).Any(),
                            HasUpdate = (from cnhd in _dataContext.ChucNangHanhDongs
                                         where cnhd.MaChucNang == cn.MaChucNang && cnhd.MaHanhDong == "UPDATE"
                                         select cnhd).Any(),
                            HasDelete = (from cnhd in _dataContext.ChucNangHanhDongs
                                         where cnhd.MaChucNang == cn.MaChucNang && cnhd.MaHanhDong == "DELETE"
                                         select cnhd).Any(),
                            Status = cn.Status
                        };

            if (!string.IsNullOrEmpty(pagingParams.Keyword))
            {
                var keyword = pagingParams.Keyword.ToUpper().ToTrim();

                query = query.Where(x => x.TenChucNang.ToUpper().ToUnSign().Contains(keyword.ToUnSign()) ||
                                        x.TenChucNang.ToUpper().Contains(keyword));
            }

            if (!string.IsNullOrEmpty(pagingParams.SortValue) && !pagingParams.SortValue.Equals("null") && !pagingParams.SortValue.Equals("undefined"))
            {
                switch (pagingParams.SortKey)
                {
                    case "tenChucNang":
                        if (pagingParams.SortValue == "ascend")
                        {
                            query = query.OrderBy(x => x.TenChucNang);
                        }
                        else
                        {
                            query = query.OrderByDescending(x => x.TenChucNang);
                        }
                        break;
                    default:
                        break;
                }
            }

            return await PagedList<ChucNangViewModel>.CreateAsync(query, pagingParams.PageNumber, pagingParams.PageSize);
        }

        private bool CheckHasAction(string functionId, string actionId)
        {
            return _dataContext.ChucNangHanhDongs.Where(x => x.MaChucNang == functionId && x.MaHanhDong == actionId).Any();
        }
    }
}
