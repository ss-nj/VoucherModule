using Application.Common.Models;
using Application.DTOs.MasterDtos;
using Application.Interfaces;
using Application.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OfficeOpenXml;
using OfficeOpenXml.Style;

namespace Api.Controllers
{//TODO:add versioning and rate limit
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class MastersController : ControllerBase
    {
        private readonly IMasterService _masterService;

        public MastersController(IMasterService masterService)
        {
            _masterService = masterService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll([FromQuery] QueryParameters parameters)
        {
            var result = await _masterService.GetAllAsync(parameters);
            return Ok(result);
        }

        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetById(int id)
        {
            var result = await _masterService.GetByIdAsync(id);
            return Ok(result);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateMasterDto dto)
        {
            var result = await _masterService.CreateAsync(dto);
            return CreatedAtAction(nameof(GetById), new { id = result.Id }, result);
        }

        [HttpPut("{id:int}")]
        public async Task<IActionResult> Update(int id, [FromBody] UpdateMasterDto dto)
        {
            await _masterService.UpdateAsync(id, dto);
            return NoContent();
        }

        [HttpDelete("{id:int}")]
        public async Task<IActionResult> Delete(int id)
        {
            await _masterService.DeleteAsync(id);
            return NoContent();
        }

        [HttpGet("masters/export")]
        public async Task<IActionResult> ExportMastersToExcel([FromQuery] QueryParameters parameters)
        {//by chat gbt 
            // <-- set the license context
            ExcelPackage.LicenseContext = LicenseContext.NonCommercial;

            var masters = await _masterService.GetAllAsync(parameters); 

            using var package = new ExcelPackage();
            var worksheet = package.Workbook.Worksheets.Add("Masters");
            //TODO:add debit creadit after addeing subs
            worksheet.Cells[1, 1].Value = "Title";
            worksheet.Cells[1, 2].Value = "Code";
            worksheet.Cells[1, 3].Value = "OwnerId";

            int row = 2;
            foreach (var p in masters.Data)
            {
                worksheet.Cells[row, 1].Value = p.Title;
                worksheet.Cells[row, 2].Value = p.Code;
                worksheet.Cells[row, 3].Value = p.OwnerId;
                row++;
            }

            worksheet.Cells.AutoFitColumns();

            var fileBytes = package.GetAsByteArray();
            var fileName = $"Masters_{DateTime.Now:yyyyMMddHHmmss}.xlsx";

            return File(fileBytes,
                        "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet",
                        fileName);
        }
        [HttpGet("hierarchy/{id:int}")]
        public async Task<IActionResult> GetHierarchyReport(int id)
        {
            var data = await _masterService.GetReportAsync( id);
            return Ok(data);
        }

    }
}


