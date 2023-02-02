using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace module_12_exercise_3
{
    internal class DepositAcc : Account<int, int, object, object, string> //депозитный счёт
    {
        public DepositAcc(int ID, int ClientID, object AccountNumber, object AccountSumm, string IsDeposit) : base(ID, ClientID, AccountNumber, AccountSumm, IsDeposit)
        {
        }
        public override string ToString() //формат вывода счёта
        {
            return $"{ID} {ClientID} {AccountNumber} {AccountSumm} {IsDeposit}";
        }
    }
}
