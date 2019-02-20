using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Absoft.Data;
using Absoft.Data.Entities;
using Absoft.Extentions;
using Absoft.Helpers;
using Absoft.Repositories.Interfaces;
using Absoft.ViewModels;
using AutoMapper;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using OfficeOpenXml;

namespace Absoft.Controllers
{
    public class DonViTinhController : BaseController
    {
        IDonViTinhRepository _donViTinhRepository;
        private readonly IHostingEnvironment _hostingEnvironment;       
        public DonViTinhController(IDonViTinhRepository donViTinhRepository, IHostingEnvironment hostingEnvironment)
        {
            _donViTinhRepository = donViTinhRepository;
            _hostingEnvironment = hostingEnvironment;
           
        }
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var models = await _donViTinhRepository.GetAllAsync();
            return Ok(models);
        }

        [HttpGet("getAllPaging")]
        public async Task<IActionResult> GetAllPaging([FromQuery]PagingParams pagingParams)
        {
            var paged = await _donViTinhRepository.GetAllPagingAsync(pagingParams);
            Response.AddPagination(paged.CurrentPage, paged.PageSize, paged.TotalCount, paged.TotalPages);
            return Ok(paged.Items);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetDetail(int id)
        {
            var models = await _donViTinhRepository.GetById(id);
            return Ok(models);
        }
        [HttpPut]
        public async Task<IActionResult> Update(DonViTinhViewModel donViTinhViewModel)
        {
            var result = await _donViTinhRepository.UpdateAsync(donViTinhViewModel);
            return Ok(result);
        }
        [HttpPost]
        public async Task<IActionResult> Insert(DonViTinhViewModel donViTinhViewModel)
        {
            var id = await _donViTinhRepository.CheckTonTai(donViTinhViewModel.TenDVT);
            if (id == -1)
            {
                var result = await _donViTinhRepository.InsertAsync(donViTinhViewModel);
                return Ok(result);
            }
            else
            {
                if (await _donViTinhRepository.GetStatus(id) == true)
                {
                    return Ok(-1); // đã tồn tại không cho thêm
                }
                else
                {
                    donViTinhViewModel.MaDVT = id;
                    var result = await _donViTinhRepository.UpdateAsync(donViTinhViewModel);
                    var resultChange = await _donViTinhRepository.ChangStatus(id);
                    return Ok(resultChange);
                }
            }
        }
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var result = await _donViTinhRepository.IsDelete(id);
            return Ok(result);
        }
        [HttpGet]
        [Route("ImportDVT")]
        public async Task<IActionResult> ImportDVT(IList<IFormFile> files)
        {
            //string rootFolder = _hostingEnvironment.WebRootPath;
            //string fileName = @"DonViTinh.xls";
            //FileInfo file = new FileInfo(Path.Combine(rootFolder, fileName));
            var upload = new UploadFile(_hostingEnvironment);
            var fileUrl = upload.InsertFile(files);
            if (!String.IsNullOrEmpty(fileUrl))
            {
                try
                {
                    FileInfo file = new FileInfo(fileUrl);
                    using (ExcelPackage package = new ExcelPackage(file))
                    {
                        ExcelWorksheet workSheet = package.Workbook.Worksheets["DonViTinh"];
                        int totalRows = workSheet.Dimension.Rows;
                        List<DonViTinhViewModel> List = new List<DonViTinhViewModel>();
                        for (int i = 2; i <= totalRows; i++)
                        {
                            List.Add(new DonViTinhViewModel
                            {
                                TenDVT = workSheet.Cells[i, 1].Value.ToString(),
                                Status = true,
                            });
                        }
                        // _db.Phones.AddRange(List);
                        // _db.SaveChanges();               
                        var rs = await _donViTinhRepository.InsertListAsync(List);
                        if (rs == true)
                        {
                            file.Delete();
                            return Ok(rs);
                        }
                        else return Ok(false);
                    }
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
            else
            {
                return Ok(false);
            }
        }
        //[HttpGet]
        //[Route("ExportDVT")]
        //public string ExportDVT()
        //{
        //    try
        //    {
        //        string rootFolder = _hostingEnvironment.WebRootPath;
        //        string fileName = @"ExportDonViTinh.xls";

        //        FileInfo file = new FileInfo(Path.Combine(rootFolder, fileName));

        //        using (ExcelPackage package = new ExcelPackage(file))
        //        {

        //            IList<DonViTinh> List = _db.DonViTinhs.ToList();

        //            ExcelWorksheet worksheet = package.Workbook.Worksheets.Add("DonViTinh");
        //            int totalRows = List.Count();

        //            // worksheet.Cells[1, 1].Value = "Id";
        //            worksheet.Cells[1, 1].Value = "TenDVT";
        //            //worksheet.Cells[1, 3].Value = "Status";                
        //            int i = 0;
        //            for (int row = 2; row <= totalRows + 1; row++)
        //            {
        //                // worksheet.Cells[row, 1].Value = List[i].Id;
        //                worksheet.Cells[row, 1].Value = List[i].TenDVT;
        //                // worksheet.Cells[row, 3].Value = List[i].Status;                    
        //                i++;
        //            }
        //            package.Save();
        //        }
        //        return " DonViTinh list has been exported successfully";
        //    }
        //    catch (Exception ex)
        //    {
        //        throw ex;
        //    }
        //}
    }
}
