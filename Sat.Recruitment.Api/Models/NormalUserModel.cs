using System;

namespace Sat.Recruitment.Api.Models
{
    public class NormalUserModel: UserModelBase
    {

        public NormalUserModel(UserModelRequest user) : base(user)
        {
            Money = user.Money;
        }

        public NormalUserModel(string user) : base(user)
        {
            if (!string.IsNullOrEmpty(user))
            {
                var splitUser = user.Split(',');
                if (splitUser.Length >= 4)
                {
                    Money = Convert.ToDecimal(splitUser[5]);
                }
            }
            
        }

        public override decimal Money 
        { 
            get => base.Money;

            set
            {
                if(value > 100)
                    base.Money = value * Convert.ToDecimal(1.12);
                if (value < 100 && value > 10)
                    base.Money = value * Convert.ToDecimal(1.8);
            }
        }
    }
}
