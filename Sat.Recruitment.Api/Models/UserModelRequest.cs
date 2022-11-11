using System.ComponentModel.DataAnnotations;

namespace Sat.Recruitment.Api.Models
{
    /// <summary>
    /// Clase usuario con las propiedades necesarias para la identificación de un usuario en el sistema
    /// </summary>
    public class UserModelRequest
    {
        /// <summary>
        /// Tipo enumerodo con los tipos de usuario en el sistema
        /// </summary>
        public enum UserTypeEnum
        {
            /// <summary>
            /// Usuario con menos privilegios
            /// </summary>
            Normal,
            /// <summary>
            /// Usuario com privilegio medios
            /// </summary>
            Premium,
            /// <summary>
            /// Usuario con todos los privilegios posibles
            /// </summary>
            SuperUser
        }

        /// <summary>
        /// Crea una nueva instancia de un tipo <see cref="UserModelRequest"/> utilizando parámetros para sus propiedades
        /// </summary>
        /// <param name="name">Cadena con el nombre del usuario</param>
        /// <param name="email">Cadena con el email del usuario</param>
        /// <param name="address">Cadena con la dirección completa</param>
        /// <param name="phone">Cadena con el numero de teléfono del usuario</param>
        /// <param name="userType">Valor de tipo <see cref="UserType"/> con el provilegio del usuario</param>
        /// <param name="money">Valor numerico con el valor monetario del usuario</param>
        public UserModelRequest(string name, string email, string address, string phone, UserTypeEnum userType, decimal money)
        {
            Name = name;
            Email = email;
            Address = address;
            Phone = phone;
            UserType = userType;
            Money = money;
        }

        /// <summary>
        /// Crea una nueva instancia de un tipo <see cref="UserModelRequest"/>
        /// </summary>
        public UserModelRequest()
        { }

        /// <summary>
        /// Cadena con el nombre del usuario
        /// </summary>
        [Required(AllowEmptyStrings = false, ErrorMessage = "The name is required")]
        [StringLength(100)]
        public string Name { get; set; } = null!;

        /// <summary>
        /// Cadena con el email del usuario
        /// </summary>
        [Required(AllowEmptyStrings = false, ErrorMessage = "The email is required")]
        [RegularExpression("^[a-zA-Z0-9_\\.-]+@([a-zA-Z0-9-]+\\.)+[a-zA-Z]{2,6}$", ErrorMessage = "E-mail is not valid")]
        public string Email { get; set; }

        /// <summary>
        /// Cadena con la dirección completa
        /// </summary>
        [Required(AllowEmptyStrings = false, ErrorMessage = "The adress is required")]
        public string Address { get; set; }

        /// <summary>
        /// Cadena con el numero de teléfono del usuario
        /// </summary>
        [Required(AllowEmptyStrings = false, ErrorMessage = "The phone is required")]
        public string Phone { get; set; }

        /// <summary>
        /// Valor de tipo <see cref="UserType"/> con el provilegio del usuario
        /// </summary>
        [Required]
        public UserTypeEnum UserType { get; set; }

        /// <summary>
        /// Valor numerico con el valor monetario del usuario
        /// </summary>
        public decimal Money { get; set; }
    }
}
