﻿using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace UserAuth.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class AccessController : ControllerBase
    {
        [HttpGet]
        [Authorize]
        public IActionResult Get()
        {
            return Ok("Acesso Permitido");
        }

    }
}
