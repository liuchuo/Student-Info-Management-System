using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudentInfoManagmentSystem.Model {
    public static class DBHelper {

        public static StudentInfoEntities Context { get; set; } = new StudentInfoEntities();

        public static List<College> GetAllColleges() {
            var list = new List<College>();
            foreach (var item in Context.Colleges) {
                list.Add(item);
            }
            return list;
        }

        public static int GetCollegeIdByName(string name) {
            if (string.IsNullOrEmpty(name)) return -1;
            foreach (var item in Context.Colleges) {
                if (item.Name.Equals(name)) return item.Id;
            }
            return -1;
        }

        public static bool ExistCollege(int collegeId) {
            foreach (var item in Context.Colleges) {
                if (item.Id == collegeId) return true;
            }
            return false;
        }

        public static List<Major> GetAllMajorsInCollege(int collegeId) {
            var list = new List<Major>();
            foreach (var item in Context.Majors) {
                if (item.CollegeId == collegeId) list.Add(item);
            }
            return list;
        }

        public static void AddMajor(Major major) {
            if (!ExistCollege(major.CollegeId)) return;
            Context.Majors.Add(major);
            Update();
        }

        public static void DeleteMajor(int id) {
            foreach (var item in Context.Majors) {
                if (item.Id == id) {
                    Context.Majors.Remove(item);
                    break;
                }
            }
            Update();
        }

        public static List<MajorClass> GetAllClassesInMajor(int id) {
            var list = new List<MajorClass>();
            foreach (var item in Context.MajorClasses) {
                if (item.MajorId == id) list.Add(item);
            }
            return list;
        }

        public static bool ExistMajor(int id) {
            foreach (var item in Context.Majors) {
                if (item.Id == id) return true;
            }
            return false;
        }

        public static void AddMajorClass(MajorClass mClass) {
            if (!ExistCollege(mClass.MajorId)) return;
            Context.MajorClasses.Add(mClass);
            Update();
        }

        public static void DeleteMajorClass(int id) {
            foreach (var item in Context.MajorClasses) {
                if (item.Id == id) {
                    Context.MajorClasses.Remove(item);
                    break;
                }
            }
            Update();
        }

        public static bool ExistMajorClass(int id) {
            foreach (var item in Context.MajorClasses) {
                if (item.Id == id) return true;
            }
            return false;
        }

        public static List<Student> GetAllStudentsInMajorClass(int id) {
            var list = new List<Student>();
            foreach (var item in Context.Students) {
                if (item.MajorClassId == id) list.Add(item);
            }
            return list;
        }

        public static void AddStudent(Student s) {
            if (!ExistMajorClass(s.MajorClassId)) return;
            Context.Students.Add(s);
            Update();
        }

        public static void DeleteStudent(string id) {
            foreach (var item in Context.Students) {
                if (item.Id == id) {
                    Context.Students.Remove(item);
                    break;
                }
            }
            Update();
        }

        public static void UpdateStudent(string id, Student s) {
            if (!ExistMajorClass(s.MajorClassId)) return;
            foreach (var item in Context.Students) {
                if (item.Id == id) {
                    item.Id = id;
                    item.Name = s.Name;
                    item.Sex = s.Sex;
                    item.Age = s.Age;
                    break;
                }
            }
            Update();
        }

        public static bool ExistStudent(string id) {
            foreach (var item in Context.Students) {
                if (item.Id == id) return true;
            }
            return false;
        }

        public static bool CheckUser(string name, string pwd, int collegeId) {
            if (collegeId < 0) return false;
            foreach (var item in Context.SUsers) {
                if (item.Name.Equals(name) && item.Pwd.Equals(pwd) && item.CollegeId == collegeId) {
                    return true;
                }
            }
            return false;
        }

        public static bool ExistUser(string name, int collegeId) {
            foreach (var item in Context.SUsers) {
                if (item.Name == name && item.CollegeId == collegeId) return true;
            }
            return false;
        }

        public static void AddUser(SUser user) {
            if (!ExistCollege(user.CollegeId)) return;
            Context.SUsers.Add(user);
            Update();
        }

        public static void Update() {
            Context.SaveChanges();
            Context?.Dispose();
            Context = new StudentInfoEntities();
        }

    }
}
