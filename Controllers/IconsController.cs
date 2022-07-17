using Icon.Api.Models;
using Icon.Api.Response;
using Icon.Api.Services;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace Icon.Api.Controllers
{
    [ApiController]
    [Route("api/[Controller]")]
    public class IconsController : ControllerBase
    {
        private readonly IIconService iconService;

        public IconsController(IIconService iconService)
        {
            this.iconService = iconService;
        }

        [HttpGet]
        public async Task<ActionResult<ApiResponse<IconlineAwesome>>> Get()
        {
            var response = new ApiResponse<IconlineAwesome>();
            try
            {
                await this.iconService.LoadHtmlDocument();
                response.Data = this.iconService.GetIcons();
            }
            catch (Exception ex)
            {
                response.Succeeded = false;
                response.Message = ex.Message;
                return BadRequest(response);
            }
            
            return Ok(response);
        }
    }
}
