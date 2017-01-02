using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudentInfoManagmentSystem {
    public static class CurrentUser {

        class CUser {
            public string Name { get; set; }
            public string Pwd { get; set; }
            public int CollegeId { get; set; }
        }

        private static CUser _cuser;

        public static void SignIn(string name, string pwd, int id) {
            _cuser = new CUser { Name = name, Pwd = pwd, CollegeId = id };
        }

        public static void SignOff() { _cuser = null; }

        public static int GetCollegeId() {
            return _cuser == null ? -1 : _cuser.CollegeId;
        }
    }
}
