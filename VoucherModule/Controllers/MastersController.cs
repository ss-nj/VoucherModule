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
        private readonly IMasterService _personService;

        public MastersController(IMasterService personService)
        {
            _personService = personService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll([FromQuery] QueryParameters parameters)
        {
            var result = await _personService.GetAllAsync(parameters);
            return Ok(result);
        }

        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetById(int id)
        {
            var result = await _personService.GetByIdAsync(id);
            return Ok(result);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateMasterDto dto)
        {
            var result = await _personService.CreateAsync(dto);
            return CreatedAtAction(nameof(GetById), new { id = result.Id }, result);
        }

        [HttpPut("{id:int}")]
        public async Task<IActionResult> Update(int id, [FromBody] UpdateMasterDto dto)
        {
            await _personService.UpdateAsync(id, dto);
            return NoContent();
        }

        [HttpDelete("{id:int}")]
        public async Task<IActionResult> Delete(int id)
        {
            await _personService.DeleteAsync(id);
            return NoContent();
        }

        [HttpGet("persons/export")]
        public async Task<IActionResult> ExportMastersToExcel([FromQuery] QueryParameters parameters)
        {//by chat gbt 
            // <-- set the license context
            ExcelPackage.LicenseContext = LicenseContext.NonCommercial;

            var persons = await _personService.GetAllAsync(parameters); 

            using var package = new ExcelPackage();
            var worksheet = package.Workbook.Worksheets.Add("Masters");
            //TODO:add debit creadit after addeing subs
            worksheet.Cells[1, 1].Value = "Title";
            worksheet.Cells[1, 2].Value = "Last Name";
            worksheet.Cells[1, 3].Value = "National Id";

            int row = 2;
            foreach (var p in persons.Data)
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

        
    }
}


