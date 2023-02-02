using System;
using System.Collections.Generic;
using System.Security.Principal;
using System.Windows.Annotations;

namespace module_12_exercise_3
{
    internal class Storage
    {


        public List<Client> ClientsDB { get; set; } //база клиентов
        public List<Account<int, int, object, object, string>> AccountsDB { get; set; } //база счетов с параметризация типов данных

        private Storage()

        {
            ClientsDB = new List<Client>(); //создание новой базы клиентов
            AccountsDB = new List<Account<int, int, object, object, string>>(); //создание новой базы счетов

            IAccount<DepositAcc> concreteStorage = new AccountStorage<Account<int, int, object, object, string>>(); //приведение конкретного типа к общему через КОНТРВАРИАНТНОСТЬ 
            concreteStorage.SetValueMethod(new DepositAcc(1, 1, 500000, 5000, "депозитный"));

            object[] temp1 = (concreteStorage.ToString()).Split(' ');

            var arg0 = Convert.ToInt32(temp1[0]);
            var arg1 = Convert.ToInt32(temp1[1]);
            var arg2 = temp1[2];
            var arg3 = temp1[3];
            var arg4 = Convert.ToString(temp1[4]);

            ClientsDB.Add(new Client(1, "Петров", "Дмитрий", "Иванович"));
            AccountsDB.Add(new Account<int, int, object, object, string>(arg0, arg1, arg2, arg3, arg4));
            AccountsDB.Add(new Account<int, int, object, object, string>(2, 1, "826493", 50, "депозитный"));
            AccountsDB.Add(new Account<int, int, object, object, string>(3, 1, "906562", 9000, "недепозитный"));

            ClientsDB.Add(new Client(2, "Васильев", "Пётр", "Дмитриевич"));
            AccountsDB.Add(new Account<int, int, object, object, string>(4, 2, "165934", 12000, "депозитный"));
            AccountsDB.Add(new Account<int, int, object, object, string>(5, 2, "381950", 185000, "депозитный"));
            AccountsDB.Add(new Account<int, int, object, object, string>(6, 2, "194735", 300, "недепозитный"));
        }

        public static Storage CreateStorage() //создание репозитория с базами
        {
            return new Storage();
        }

        public void AccountAdd(int id, int clientID, object accountNumber, object accountSumm, string isDeposit) //метод добавления нового клиента в базу
        {
            AccountsDB.Add(new Account<int, int, object, object, string>(id, clientID, accountNumber, accountSumm, isDeposit));
        }
    }


}
