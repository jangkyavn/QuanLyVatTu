using Absoft.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Absoft.Repositories.Interfaces
{
    public interface INhapChiTietRepository
    {       
        Task<bool> InsertAsync(NhapChiTietViewModel mnhapchitiet, int maphieunhap);          
    }
}
