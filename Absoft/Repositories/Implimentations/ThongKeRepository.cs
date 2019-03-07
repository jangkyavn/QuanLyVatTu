using Absoft.Repositories.Interfaces;
using Absoft.ViewModels;
using Dapper;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace Absoft.Repositories.Implimentations
{
    public class ThongKeRepository : IThongKeRepository
    {
        public IConfiguration _configuration;
        public ThongKeRepository(IConfiguration IConfiguration)
        {
            _configuration = IConfiguration;
        }

        public async Task<IEnumerable<ThongKeNhapViewModel>> ThongKeNhap(string fromDate, string toDate, int? maLoaiVT, int? maHM, int? maKho)
        {
            using (var sqlConnection = new SqlConnection(_configuration.GetConnectionString("DefaultConnection")))
            {
                await sqlConnection.OpenAsync();
                var parameters = new DynamicParameters();
                parameters.Add("@fromDate",fromDate);
                parameters.Add("@toDate",toDate);
                parameters.Add("@maLoaiVT",maLoaiVT);
                parameters.Add("@MaHM", maHM);
                parameters.Add("@maKho",maKho);

                try
                {
                    var rs = await sqlConnection.QueryAsync<ThongKeNhapViewModel>(
                        "TKVatTuByNgayNhap", parameters, commandType: CommandType.StoredProcedure);
                    return rs;
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
        }

        public async Task<IEnumerable<ThongKeNhapXuatTonViewModel>> ThongKeNhapXuatTon(string fromDate, string toDate, int? maLoaiVT, int? maHM, int? maKho, int? maVatTu)
        {
            using (var sqlConnection = new SqlConnection(_configuration.GetConnectionString("DefaultConnection")))
            {
                await sqlConnection.OpenAsync();
                var parameters = new DynamicParameters();
                parameters.Add("@fromDate", fromDate);
                parameters.Add("@toDate", toDate);
                parameters.Add("@maLoaiVT", maLoaiVT);
                parameters.Add("@MaHM", maHM);
                parameters.Add("@maKho", maKho);
                parameters.Add("@maVatTu", maVatTu);

                try
                {
                    var rs = await sqlConnection.QueryAsync<ThongKeNhapXuatTonViewModel>(
                        "TKNhapXuatTon", parameters, commandType: CommandType.StoredProcedure);
                    return rs;
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
        }

        public async Task<IEnumerable<ThongKeThanhLyViewModel>> ThongKeThanhLy(string fromDate, string toDate, int? maLoaiVT, int? maHM, int? maKho)
        {
            using (var sqlConnection = new SqlConnection(_configuration.GetConnectionString("DefaultConnection")))
            {
                await sqlConnection.OpenAsync();
                var parameters = new DynamicParameters();
                parameters.Add("@fromDate", fromDate);
                parameters.Add("@toDate", toDate);
                parameters.Add("@maLoaiVT", maLoaiVT);
                parameters.Add("@MaHM", maHM);
                parameters.Add("@maKho", maKho);

                try
                {
                    var rs = await sqlConnection.QueryAsync<ThongKeThanhLyViewModel>(
                        "TKVatTuByNgayThanhLy", parameters, commandType: CommandType.StoredProcedure);
                    return rs;
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
        }

        public async Task<IEnumerable<ThongKeXuatViewModel>> ThongKeXuat(string fromDate, string toDate, int? maLoaiVT, int? maHM, int? maKho)
        {
            using (var sqlConnection = new SqlConnection(_configuration.GetConnectionString("DefaultConnection")))
            {
                await sqlConnection.OpenAsync();
                var parameters = new DynamicParameters();
                parameters.Add("@fromDate", fromDate);
                parameters.Add("@toDate", toDate);
                parameters.Add("@maLoaiVT", maLoaiVT);
                parameters.Add("@MaHM", maHM);
                parameters.Add("@maKho", maKho);

                try
                {
                    var rs = await sqlConnection.QueryAsync<ThongKeXuatViewModel>(
                        "TKVatTuByNgayXuat", parameters, commandType: CommandType.StoredProcedure);
                    return rs;
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
        }
    }
}
