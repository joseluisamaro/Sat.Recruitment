
using System;
using System.Net;
using System.Threading.Tasks;
using System.Web.Http.Results;
using System.Xml.Linq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Sat.Recruitment.Api.Data;
using Sat.Recruitment.Api.Models;
using static Sat.Recruitment.Api.Models.UserModelRequest;

namespace Sat.Recruitment.Api.Controllers
{

    /// <summary>
    /// Controlador con operaciónes para gestión de usuarios
    /// </summary>
    [ApiController]    
    [Route("[controller]")]
    public partial class UsersController : ControllerBase
    {
        //private readonly List<UserModelRequest> _users = new List<UserModelRequest>();

        private readonly AppSettings _appSettings;

        public UsersController(IOptions<AppSettings> appSettings)
        {
            _appSettings = appSettings.Value;
        }

        /// <summary>
        /// Crea un nuevo usuario
        /// </summary>
        /// <param name="user">Objeto de tipo <see cref="UserModelRequest"/></param>
        /// <returns></returns>
        [HttpPost]        
        [Route("/create-user")]
        public async Task<ActionResult> CreateUser(UserModelRequest user)
        {
            // aunque se ejecutan las validaciones implícitas en el controlador, es
            // necesaria esta verificación para los tests unitarios
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            UserModelBase newUser = null; 

            switch (user.UserType)
            {
                case UserTypeEnum.Normal:
                    newUser = new NormalUserModel(user);
                    break;
                case UserTypeEnum.SuperUser:
                    newUser = new SuperUserModel(user);
                    break;
                case UserTypeEnum.Premium:
                    newUser = new UserPremiumModel(user);
                    break;
            }

            try
            {
                var dataStream = await DataAccess.ReadUsersFromFile(_appSettings.Filename);
                if (dataStream != null)
                {
                    foreach (var line in dataStream)
                    {
                        if (!string.IsNullOrEmpty(line))
                        {
                            // NOTA: En lugar deutilizar una separación por comas, sería mejor una deserialización
                            // desde un fichero en fomato json
                            var splitUser = line.Split(',');
                            if (splitUser.Length >= 4)
                            {
                                if (newUser.Email.Equals(splitUser[1], StringComparison.InvariantCultureIgnoreCase) ||
                                    newUser.Phone.Equals(splitUser[2], StringComparison.InvariantCultureIgnoreCase) ||
                                    (newUser.Name.Equals(splitUser[0], StringComparison.InvariantCultureIgnoreCase) &&
                                    newUser.Address.Equals(splitUser[3], StringComparison.InvariantCultureIgnoreCase)))
                                {
                                    return BadRequest("User is duplicated");
                                }
                            }
                        }
                    }
                }
            }
            catch(Exception ex)
            {
                return Problem(detail: ex.Message);
            }

            return Ok("User Created");
        }
    }
}
