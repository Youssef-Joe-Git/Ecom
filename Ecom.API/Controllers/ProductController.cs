using AutoMapper;
using Ecom.API.Helper;
using Ecom.Core.Dtos;
using Ecom.Core.Entities.Product;
using Ecom.Core.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Ecom.API.Controllers
{
    public class ProductController : BaseController
    {
        public ProductController(IUnitOfWork work, IMapper mapper) : base(work, mapper)
        {
        }
        [HttpGet("getall")]
        public async Task<IActionResult> GetAll()
        {
            try
            {
                var products = await work.Products
                    .GetAllAsync(x => x.Category, x => x.Images);
                if (products == null)
                {
                    return BadRequest(new ResponseAPI(404));
                }
                var result = mapper.Map<List<ProductDto>>(products);
                return Ok(new ResponseAPI(200, result));
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
                var product = await work.Products.GetByIdAsync(id, x => x.Category, x => x.Images);
                if (product == null)
                {
                    return BadRequest(new ResponseAPI(404));
                }
                var result = mapper.Map<ProductDto>(product);

                return Ok(new ResponseAPI(200, result));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpPost("add")]
        public async Task<IActionResult> Add(AddProductDto productDto)
        {
            try
            {
                await work.Products.AddAsync(productDto);
                return Ok(new ResponseAPI(201, productDto, "Product created successfully."));
            }
            catch (Exception ex)
            {
                return BadRequest(new ResponseAPI(400, null, ex.Message));
            }
        }
        [HttpPut("update")]
        public async Task<IActionResult> Update(UpdateProductDto productDto)
        {
            try
            {
                await work.Products.UpdateAsync(productDto);
                return Ok(new ResponseAPI(200, productDto, "Product updated successfully."));
            }
            catch (Exception ex)
            {
                return BadRequest(new ResponseAPI(400, null, ex.Message));
            }


        }
        [HttpDelete("delete/{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                await work.Products.DeleteAsync(id);
                return Ok(new ResponseAPI(200, id, "Product Deleted successfully."));
            }
            catch (Exception ex)
            {
                return BadRequest(new ResponseAPI(400, null, ex.Message));
            }



        }
    }
}
