using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using UserAuth.Data;
using UserAuth.Data.Dtos;
using UserAuth.Migrations;
using UserAuth.Model;

namespace UserAuth.Services
{
    public class UserServices
    {
        public AppDbContext _context; // Contexto do banco de dados
        private IMapper _mapper;
        private UserManager<User> _userManager;

        public UserServices(AppDbContext context, IMapper mapper, UserManager<User> userManager)
        {
            _context = context;
            _mapper = mapper;
            _userManager = userManager;
        }

        public async Task<IActionResult> CreateUser(CreateUserDto userDto)
        {
            User user = _mapper.Map<User>(userDto);
            IdentityResult result = await 
                _userManager.CreateAsync(user, userDto.Password);

            if (result.Succeeded)
            { 
                return new OkObjectResult("Usuário criado com sucesso!");
            }
            return new BadRequestObjectResult(result.Errors);
            //fazer a criação dos usuarios a cada cadastro ex: USER001, USER002 etc.....
        }
    }
}
