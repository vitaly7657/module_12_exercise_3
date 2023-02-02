using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;

namespace module_12_exercise_3
{
    internal class Account<T1, T2, T3, T4, T5> //создание параметризованного класса
    {
        public T1 ID { get; set; }
        public T2 ClientID { get; set; }
        public T3 AccountNumber { get; set; }
        public T4 AccountSumm { get; set; }
        public T5 IsDeposit { get; set; }


        public Account(T1 ID, T2 ClientID, T3 AccountNumber, T4 AccountSumm, T5 IsDeposit)
        {
            this.ID = ID;
            this.ClientID = ClientID;
            this.AccountNumber = AccountNumber;
            this.AccountSumm = AccountSumm;
            this.IsDeposit = IsDeposit;

        }

        public override string ToString() //формат вывода счёта
        {
            return $"{ID} {ClientID} {AccountNumber} {AccountSumm} {IsDeposit}";
        }


    }
}
