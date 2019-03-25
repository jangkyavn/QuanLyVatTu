using Absoft.Helpers;
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

        public async Task<IEnumerable<ThongKeNhapViewModel>> ThongKeNhap(ThongKeParams thongKeParams)
        {
            using (var sqlConnection = new SqlConnection(_configuration.GetConnectionString("DefaultConnection")))
            {
                await sqlConnection.OpenAsync();
                var parameters = new DynamicParameters();
                parameters.Add("@fromDate", thongKeParams.fromDate);
                parameters.Add("@toDate", thongKeParams.toDate);
                parameters.Add("@maLoaiVT", thongKeParams.maLoaiVT);
                parameters.Add("@MaHM", thongKeParams.maHM);
                parameters.Add("@maKho", thongKeParams.maKho);

                try
                {
                    var rs = await sqlConnection.QueryAsync<ThongKeNhapViewModel>(
                        "TKVatTuByNgayNhap", parameters, commandType: CommandType.StoredProcedure);

                    if (!string.IsNullOrEmpty(thongKeParams.SortValue))
                    {
                        switch (thongKeParams.SortKey)
                        {
                            case "tenVT":
                                if (thongKeParams.SortValue == "ascend")
                                {
                                    rs = rs.OrderBy(x => x.TenVT);
                                }
                                else
                                {
                                    rs = rs.OrderByDescending(x => x.TenVT);
                                }
                                break;
                            case "tongLuongNhap":
                                if (thongKeParams.SortValue == "ascend")
                                {
                                    rs = rs.OrderBy(x => x.TongLuongNhap);
                                }
                                else
                                {
                                    rs = rs.OrderByDescending(x => x.TongLuongNhap);
                                }
                                break;
                            default:
                                break;
                        }
                    }

                    if (!string.IsNullOrEmpty(thongKeParams.SearchValue))
                    {
                        if (thongKeParams.SearchKey == "tenVT")
                            rs = rs.Where(x => x.TenVT == thongKeParams.SearchValue.Trim());
                        if (thongKeParams.SearchKey == "tongLuongNhap")
                            rs = rs.Where(x => x.TongLuongNhap.ToString() == thongKeParams.SearchValue.Trim());
                    }

                    return rs;
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
        }

        public async Task<IEnumerable<ThongKeNhapXuatTonViewModel>> ThongKeNhapXuatTon(ThongKeParams thongKeParams)
        {
            using (var sqlConnection = new SqlConnection(_configuration.GetConnectionString("DefaultConnection")))
            {
                await sqlConnection.OpenAsync();
                var parameters = new DynamicParameters();
                parameters.Add("@fromDate", thongKeParams.fromDate);
                parameters.Add("@toDate", thongKeParams.toDate);
                parameters.Add("@maLoaiVT", thongKeParams.maLoaiVT);
                parameters.Add("@MaHM", thongKeParams.maHM);
                parameters.Add("@maKho", thongKeParams.maKho);
                parameters.Add("@maVatTu", thongKeParams.maVatTu);

                try
                {
                    var rs = await sqlConnection.QueryAsync<ThongKeNhapXuatTonViewModel>(
                        "TKNhapXuatTon", parameters, commandType: CommandType.StoredProcedure);

                    if (!string.IsNullOrEmpty(thongKeParams.SortValue))
                    {
                        switch (thongKeParams.SortKey)
                        {
                            case "tenVT":
                                if (thongKeParams.SortValue == "ascend")
                                {
                                    rs = rs.OrderBy(x => x.TenVT);
                                }
                                else
                                {
                                    rs = rs.OrderByDescending(x => x.TenVT);
                                }
                                break;
                            case "tonDauKy":
                                if (thongKeParams.SortValue == "ascend")
                                {
                                    rs = rs.OrderBy(x => x.TonDauKy);
                                }
                                else
                                {
                                    rs = rs.OrderByDescending(x => x.TonDauKy);
                                }
                                break;
                            case "tongNhap":
                                if (thongKeParams.SortValue == "ascend")
                                {
                                    rs = rs.OrderBy(x => x.TongNhap);
                                }
                                else
                                {
                                    rs = rs.OrderByDescending(x => x.TongNhap);
                                }
                                break;
                            case "tongXuat":
                                if (thongKeParams.SortValue == "ascend")
                                {
                                    rs = rs.OrderBy(x => x.TongXuat);
                                }
                                else
                                {
                                    rs = rs.OrderByDescending(x => x.TongXuat);
                                }
                                break;
                            case "tongThanhLy":
                                if (thongKeParams.SortValue == "ascend")
                                {
                                    rs = rs.OrderBy(x => x.TongThanhLy);
                                }
                                else
                                {
                                    rs = rs.OrderByDescending(x => x.TongThanhLy);
                                }
                                break;
                            case "tongKiemKe":
                                if (thongKeParams.SortValue == "ascend")
                                {
                                    rs = rs.OrderBy(x => x.TongKiemKe);
                                }
                                else
                                {
                                    rs = rs.OrderByDescending(x => x.TongKiemKe);
                                }
                                break;
                            case "tonCuoiKy":
                                if (thongKeParams.SortValue == "ascend")
                                {
                                    rs = rs.OrderBy(x => x.TonCuoiKy);
                                }
                                else
                                {
                                    rs = rs.OrderByDescending(x => x.TonCuoiKy);
                                }
                                break;
                            default:
                                break;
                        }
                    }

                    if (!string.IsNullOrEmpty(thongKeParams.SearchValue))
                    {
                        if (thongKeParams.SearchKey == "tenVT")
                            rs = rs.Where(x => x.TenVT == thongKeParams.SearchValue.Trim());
                        if (thongKeParams.SearchKey == "tonDauKy")
                            rs = rs.Where(x => x.TonDauKy.ToString() == thongKeParams.SearchValue.Trim());
                        if (thongKeParams.SearchKey == "tongNhap")
                            rs = rs.Where(x => x.TongNhap.ToString() == thongKeParams.SearchValue.Trim());
                        if (thongKeParams.SearchKey == "tongXuat")
                            rs = rs.Where(x => x.TongXuat.ToString() == thongKeParams.SearchValue.Trim());
                        if (thongKeParams.SearchKey == "tongThanhLy")
                            rs = rs.Where(x => x.TongThanhLy.ToString() == thongKeParams.SearchValue.Trim());
                        if (thongKeParams.SearchKey == "tongKiemKe")
                            rs = rs.Where(x => x.TongKiemKe.ToString() == thongKeParams.SearchValue.Trim());
                        if (thongKeParams.SearchKey == "tonCuoiKy")
                            rs = rs.Where(x => x.TonCuoiKy.ToString() == thongKeParams.SearchValue.Trim());
                    }

                    return rs;
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
        }

        public async Task<IEnumerable<ThongKeThanhLyViewModel>> ThongKeThanhLy(ThongKeParams thongKeParams)
        {
            using (var sqlConnection = new SqlConnection(_configuration.GetConnectionString("DefaultConnection")))
            {
                await sqlConnection.OpenAsync();
                var parameters = new DynamicParameters();
                parameters.Add("@fromDate", thongKeParams.fromDate);
                parameters.Add("@toDate", thongKeParams.toDate);
                parameters.Add("@maLoaiVT", thongKeParams.maLoaiVT);
                parameters.Add("@MaHM", thongKeParams.maHM);
                parameters.Add("@maKho", thongKeParams.maKho);

                try
                {
                    var rs = await sqlConnection.QueryAsync<ThongKeThanhLyViewModel>(
                        "TKVatTuByNgayThanhLy", parameters, commandType: CommandType.StoredProcedure);

                    if (!string.IsNullOrEmpty(thongKeParams.SortValue))
                    {
                        switch (thongKeParams.SortKey)
                        {
                            case "tenVT":
                                if (thongKeParams.SortValue == "ascend")
                                {
                                    rs = rs.OrderBy(x => x.TenVT);
                                }
                                else
                                {
                                    rs = rs.OrderByDescending(x => x.TenVT);
                                }
                                break;
                            case "tongLuongThanhLy":
                                if (thongKeParams.SortValue == "ascend")
                                {
                                    rs = rs.OrderBy(x => x.TongLuongThanhLy);
                                }
                                else
                                {
                                    rs = rs.OrderByDescending(x => x.TongLuongThanhLy);
                                }
                                break;
                            default:
                                break;
                        }
                    }

                    if (!string.IsNullOrEmpty(thongKeParams.SearchValue))
                    {
                        if (thongKeParams.SearchKey == "tenVT")
                            rs = rs.Where(x => x.TenVT == thongKeParams.SearchValue.Trim());
                        if (thongKeParams.SearchKey == "tongLuongThanhLy")
                            rs = rs.Where(x => x.TongLuongThanhLy.ToString() == thongKeParams.SearchValue.Trim());
                    }

                    return rs;
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
        }

        public async Task<IEnumerable<ThongKeXuatViewModel>> ThongKeXuat(ThongKeParams thongKeParams)
        {
            using (var sqlConnection = new SqlConnection(_configuration.GetConnectionString("DefaultConnection")))
            {
                await sqlConnection.OpenAsync();
                var parameters = new DynamicParameters();
                parameters.Add("@fromDate", thongKeParams.fromDate);
                parameters.Add("@toDate", thongKeParams.toDate);
                parameters.Add("@maLoaiVT", thongKeParams.maLoaiVT);
                parameters.Add("@MaHM", thongKeParams.maHM);
                parameters.Add("@maKho", thongKeParams.maKho);

                try
                {
                    var rs = await sqlConnection.QueryAsync<ThongKeXuatViewModel>(
                        "TKVatTuByNgayXuat", parameters, commandType: CommandType.StoredProcedure);

                    if (!string.IsNullOrEmpty(thongKeParams.SortValue))
                    {
                        switch (thongKeParams.SortKey)
                        {
                            case "tenVT":
                                if (thongKeParams.SortValue == "ascend")
                                {
                                    rs = rs.OrderBy(x => x.TenVT);
                                }
                                else
                                {
                                    rs = rs.OrderByDescending(x => x.TenVT);
                                }
                                break;
                            case "tongLuongXuat":
                                if (thongKeParams.SortValue == "ascend")
                                {
                                    rs = rs.OrderBy(x => x.TongLuongXuat);
                                }
                                else
                                {
                                    rs = rs.OrderByDescending(x => x.TongLuongXuat);
                                }
                                break;
                            default:
                                break;
                        }
                    }

                    if (!string.IsNullOrEmpty(thongKeParams.SearchValue))
                    {
                        if (thongKeParams.SearchKey == "tenVT")
                            rs = rs.Where(x => x.TenVT == thongKeParams.SearchValue.Trim());
                        if (thongKeParams.SearchKey == "tongLuongXuat")
                            rs = rs.Where(x => x.TongLuongXuat.ToString() == thongKeParams.SearchValue.Trim());
                    }

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
