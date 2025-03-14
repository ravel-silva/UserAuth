﻿using AutoMapper;
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
        private IMapper _mapper;
        private UserManager<User> _userManager;
        private SignInManager<User> _signInManager;
        private TokenService _tokenService;

        public UserServices(IMapper mapper, UserManager<User> userManager, SignInManager<User> signInManager, TokenService tokenService)
        {
            _mapper = mapper;
            _userManager = userManager;
            _signInManager = signInManager;
            _tokenService = tokenService;
        }
        //criar um novo usuario
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

        //faz o login do usuario
        public async Task<string> Login(LoginUserDto userDto)
        {
            var result = await _signInManager.PasswordSignInAsync(userDto.Username, userDto.Password, false, false);
            
            if (!result.Succeeded)
            {
                throw new ApplicationException("Usuário ou senha inválidos");
            }

            var user = _signInManager
               .UserManager
               .Users
               .FirstOrDefault(user => user.NormalizedUserName == userDto.Username.ToUpper());

            var token = _tokenService.GenerateToken(user);
            return token;
        }
    }
}
