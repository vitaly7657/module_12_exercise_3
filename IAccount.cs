using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace module_12_exercise_3
{
    internal interface IAccount<in T> //инерфейс    
    {
        T SetValue { set; }
        void SetValueMethod(T args);
    }
}
