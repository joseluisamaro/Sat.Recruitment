using System;
using System.Security.Policy;

namespace Sat.Recruitment.Api.Models
{
    public class UserPremiumModel : UserModelBase
    {
        public UserPremiumModel(UserModelRequest user) : base(user)
        {
            Money = user.Money;
        }

        public override decimal Money
        {
            get => base.Money;

            set
            {
                if (value > 100)
                    base.Money = value * 3;
            }
        }
    }
}