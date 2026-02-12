using AutoMapper;
using Ecom.API.Helper;
using Ecom.Core.Dto;
using Ecom.Core.Entities.Product;
using Ecom.Core.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace Ecom.API.Controllers
{
    public class CategoryController : BaseController
    {
        public CategoryController(IUnitOfWork work, IMapper mapper) : base(work, mapper)
        {
        }

        [HttpGet("getall")]
        public async Task<IActionResult> GetAll()
        {
            try
            {
                var categories = await work.Categories.GetAllAsync();
                if (categories == null)
                {
                    return BadRequest();
                }
                return Ok(categories);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

        }
        [HttpGet("getbyid/{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            try
            {
                var category = await work.Categories.GetByIdAsync(id);
                if (category == null)
                {
                    return BadRequest(new ResponseAPI(404));
                }
                return Ok(new ResponseAPI(200,category));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpPost("add")]
        public async Task<IActionResult> Add(CategoryDto categoryDto)
        {
            try
            {
                if (categoryDto == null)
                {
                    return BadRequest(new ResponseAPI(400));
                }
                var category = mapper.Map<Category>(categoryDto);

                await work.Categories.AddAsync(category);

                return Ok(new ResponseAPI(201, category));

            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

        }
        [HttpPut("update")]
        public async Task<IActionResult> Update(UpdateCategoryDto categoryDto)
        {
            try
            {
                if (categoryDto == null)
                {
                    return BadRequest(new ResponseAPI(404));
                }
                var category2 = await work.Categories.GetByIdAsync(categoryDto.Id);
                if (category2 == null)
                {
                    return BadRequest(new ResponseAPI(404));
                }
                var category = mapper.Map<Category>(categoryDto);
                await work.Categories.UpdateAsync(category);
                return Ok(new ResponseAPI(200, category));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpDelete("delete/{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                var category = await work.Categories.GetByIdAsync(id);
                if (category == null)
                {
                    return BadRequest(new ResponseAPI(404));
                }
                await work.Categories.DeleteAsync(id);
                return Ok(new ResponseAPI(200));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}