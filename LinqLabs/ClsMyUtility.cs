using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LinqLabs
{
    class ClsMyUtility
    {
        //方法交換數字100.200
        internal static void Swap(ref int n1, ref int n2)
        {
            int temp = n2;
            n2 = n1;
            n1 = temp;
        }
        //方法交換英文aaa.bbb
        internal static void Swap(ref string s1, ref string s2)
        {
            string temp = s2;
            s2 = s1;
            s1 = temp;
        }

        //泛用型別方法交換數字100.200
        internal static void SwapAnyType<T> (ref T n1, ref T n2)
        {
           T temp = n2;
            n2 = n1;
            n1 = temp;
        }



    }
}
