using Absoft.Data;
using Absoft.Repositories.Interfaces;
using Absoft.ViewModels;
using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Absoft.Repositories.Implimentations
{
    public class XuatChiTietRepository: IXuatChiTietRepository
    {
        DataContext db;
        IMapper mp;
        public XuatChiTietRepository(DataContext data, IMapper mapper)
        {
            db = data;
            mp = mapper;
        }

        public Task<bool> DeleteXuatChiTietAsync(XuatChiTietViewModel mxuatchitiet, int maphieuxuat, int makho)
        {
            throw new NotImplementedException();
        }

        public Task<bool> InsertAsync(XuatChiTietViewModel mxuatchitiet, int maphieuxuat)
        {
            throw new NotImplementedException();
        }
        public Task<int> UpdateXuatChiTietAsync(XuatChiTietViewModel mxuatchitiet, int maphieuxuat, int makho)
        {
            throw new NotImplementedException();
        }
    }
}
