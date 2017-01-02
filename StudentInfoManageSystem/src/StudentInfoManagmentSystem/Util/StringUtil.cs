using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudentInfoManagmentSystem.Util {
    static class StringUtil {

        public static bool IsAnyNullOrEmpty(params string[] strs) {
            foreach (var item in strs) {
                if (string.IsNullOrEmpty(item)) return true;
            }
            return false;
        }

    }
}
