using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UsersVK.Data.Repositories;
using UsersVK.Models;
using UsersVK.Sevices;
using UsersVK.ViewModels.User;

namespace UsersVK.Controllers
{
    [Route("api/[controller]")]
    [ApiController]

    public class UserController : ControllerBase
    {
        private readonly UserManager<User> _userManager;
        private readonly UserRepository _userRepository;

        public UserController(UserManager<User> userManager, UserRepository userRepository)
        {
            _userManager = userManager;
            _userRepository = userRepository;
        }


        [HttpGet("{login}")]
        public async Task<ActionResult<User>> Get(string login)
        {
            User user = await _userRepository.GetByLogin(login.Substring(1, login.Length - 2));

            if (user != null)
            {
                return new ObjectResult(user);
            }

            return NotFound("Пользователь не найден");

        }

        [HttpGet("users")]
        public async Task<ActionResult<IEnumerable<User>>> GetAllUsers()
        {
            var users = await _userRepository.GetAll();

            if (users != null)
            {
                return new ObjectResult(users);
            }

            return NotFound("Пользователей нет");
        }

        [HttpPost("register")]
        public async Task<ActionResult<User>> Register([FromBody] RegisterViewModel model)
        {
            if (ModelState.IsValid)
            {
                var _user = await _userRepository.GetByLogin(model.Login);

                CheckUserName checkUserName = new CheckUserName();
                bool isCheckUserName = checkUserName.CheckName(model.Login);
                if (isCheckUserName)
                {
                    if (_user == null)
                    {
                        if (model.Password == model.PasswordConfirm)
                        {
                            User user = new User
                            {
                                Login = model.Login,
                                Password = model.Password,
                                CreatedDate = DateTime.Now,
                                UserGroupId = new UserGroup { Description = model.GroupDescription },
                                UserStateId = new UserState { Description = model.StateDescription }
                            };

                            if (model.GroupCode == "User")
                            {
                                user.UserGroupId.Code = GroupEnum.User;
                            }

                            await _userRepository.Add(user);
                            if (model.GroupCode == "Admin")
                            {
                                if (await _userRepository.AddRole(user, model.GroupCode) == null)
                                {
                                    return Content("Админ уже существует");
                                }
                            }

                            await _userRepository.Save();

                            return Ok("Регистрация успешно пройдена");

                        }
                    }
                    else
                    {
                        return Content("Пользователь с таким логином уже есть");
                    }
                }
                else
                {
                    return Content("Логин не должен содержать некорректные символы");
                }
            }
            return ValidationProblem("Ошибка при наборе данных в форме");
        }


        [HttpPost("delete")]
        public async Task<ActionResult<User>> Delete([FromBody] DeleteViewModel model)
        {
            User user = await _userRepository.GetByLogin(model.Login);

            if (user != null)
            {
                await _userRepository.Delete(user);
                await _userRepository.Save();
                return Content("Пользователь заблокирован");
            }

            return NotFound("Пользователь не найден");
        }
    }
}