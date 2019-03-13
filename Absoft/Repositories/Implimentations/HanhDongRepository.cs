using Absoft.Data;
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
    public class HanhDongRepository : IHanhDongRepository
    {
        private readonly DataContext _dataContext;
        private readonly IMapper _mapper;

        public HanhDongRepository(DataContext dataContext,
            IMapper mapper)
        {
            _dataContext = dataContext;
            _mapper = mapper;
        }

        public async Task<List<HanhDongViewModel>> GetAllAsync()
        {
            return await _dataContext.HanhDongs
                .Where(x => x.Status == true)
                .OrderBy(x => x.ViTri)
                .ProjectTo<HanhDongViewModel>(_mapper.ConfigurationProvider)
                .ToListAsync();
        }
    }
}
