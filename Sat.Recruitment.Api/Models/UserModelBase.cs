using System.ComponentModel.DataAnnotations;

namespace Sat.Recruitment.Api.Models
{
    /// <summary>
    /// Clase base de la que heredarán todos los usuarios del sistema
    /// </summary>
    public abstract class UserModelBase
    {
        /// <summary>
        /// Crea una nueva instancia de un tipo <see cref="UserModelBase"/> utilizando parámetros para sus propiedades
        /// </summary>
        public UserModelBase()
        {
        }

        public UserModelBase(UserModelRequest user)
        {
            Name = user.Name;
            Email = user.Email;
            Address = user.Address;
            Phone = user.Phone;
        }

        /// <summary>
        /// Crea una nueva instancia de un tipo <see cref="UserModelBase"/> utilizando una cadena de texto
        /// </summary>
        /// <param name="user">Cadena de texto con las propiedades usuario.
        /// La cadena debe contener las 4 propiedades obligatorias de un usuario</param>
        public UserModelBase(string user)
        {
            if (!string.IsNullOrEmpty(user))
            {
                var splitUser = user.Split(',');
                if (splitUser.Length >= 4)
                {
                    Name = splitUser[0];
                    Email = splitUser[1];
                    Address = splitUser[2];
                    Phone = splitUser[3];                    
                }
            }
        }

        /// <summary>
        /// Cadena con el nombre del usuario
        /// </summary>
        public string Name { get; set; } = null!;

        /// <summary>
        /// Cadena con el email del usuario
        /// </summary>
        public string Email { get; set; }

        /// <summary>
        /// Cadena con la dirección completa
        /// </summary>
        public string Address { get; set; }

        /// <summary>
        /// Cadena con el numero de teléfono del usuario
        /// </summary>
        public string Phone { get; set; }

        /// <summary>
        /// Valor numerico con el valor monetario del usuario
        /// </summary>
        public virtual decimal Money { get; set; }
    }
}
