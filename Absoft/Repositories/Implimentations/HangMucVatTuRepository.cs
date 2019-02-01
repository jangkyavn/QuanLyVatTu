using Absoft.Data;
using Absoft.Data.Entities;
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
    public class HangMucVatTuRepository : IHangMucVatTuRepository
    {
        DataContext db;
        IMapper mp;
        public HangMucVatTuRepository(DataContext data, IMapper mapper)
        {
            db = data;
            mp = mapper;
        }       
        public async Task<List<HangMucVatTuViewModel>> GetAllAsync()
        {
            return await db.HangMucVatTus.Where(x=>x.Status==true)
                .ProjectTo<HangMucVatTuViewModel>(mp.ConfigurationProvider)
                .ToListAsync();
        }

        public async Task<HangMucVatTuViewModel> GetByIdAsync(int id)
        {
            return ( mp.Map<HangMucVatTuViewModel>(await db.HangMucVatTus.FindAsync(id)));
        }

        public async Task<bool> InsertAsync(HangMucVatTuViewModel mhangmucvattu)
        {
            var hangmucvattu = mp.Map<HangMucVatTu>(mhangmucvattu);
            await db.HangMucVatTus.AddAsync(hangmucvattu);
            return await db.SaveChangesAsync() > 0;
        }

        public async Task<bool> UpdateAsync(HangMucVatTuViewModel mhangmucvattu)
        {
            var hangmuc = mp.Map<HangMucVatTu>(mhangmucvattu);
            db.HangMucVatTus.Update(hangmuc);                        
            return await db.SaveChangesAsync() > 0;
        }
        // khong len su dung vi anh huong den ban loai vat tu
        public async Task<bool> DeleteAsync(int id)
        {
          
                var hangmucvt = await db.HangMucVatTus.FindAsync(id);
                db.HangMucVatTus.Remove(hangmucvt);
                return await db.SaveChangesAsync() > 0;
           
        }
        public async Task<bool> IsDelete(int id)
        {
            var entity = await db.HangMucVatTus.FindAsync(id);
            entity.Status = false;
            return await db.SaveChangesAsync() > 0;
        }
    }
}
