using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UnityEngine;

namespace Special {
    public static class MyFunctions {
        public static void Split(int value, out int value1, out int value2) {
            value1 = value / 2;
            value2 = value - value1;

        }
    }
}