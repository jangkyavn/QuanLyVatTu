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
    }
}
