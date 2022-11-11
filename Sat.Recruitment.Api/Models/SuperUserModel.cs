using System;
using System.Security.Policy;

namespace Sat.Recruitment.Api.Models
{
    public class SuperUserModel : UserModelBase
    {
        public SuperUserModel(UserModelRequest user) : base(user)
        {
            Money = user.Money;
        }

        public override decimal Money
        {
            get => base.Money;

            set
            {
                if (value > 100)
                    base.Money = value * Convert.ToDecimal(1.2);
            }
        }
    }
}