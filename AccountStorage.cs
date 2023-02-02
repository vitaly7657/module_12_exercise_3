using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;

namespace module_12_exercise_3
{
    internal class AccountStorage<T> : IAccount<T> where T : Account<int, int, object, object, string>
    {
        List<Account<int, int, object, object, string>> db;
        public AccountStorage()
        {
            db = new List<Account<int, int, object, object, string>>();
        }
        public T SetValue { set => db.Add(value); }

        public void SetValueMethod(T args)
        {
            db.Add(args);
        }

        public override string ToString() //формат вывода счёта
        {
            return $"{db[0]}";
        }
    }
}
