using Microsoft.Win32;
using System;

namespace StudentInfoManagmentSystem.Util {
    public static class FileDialogFactory {

        public static OpenFileDialog OFD { get; }
        public static SaveFileDialog SFD { get; }

        static FileDialogFactory() {
            OFD = new OpenFileDialog() { InitialDirectory = AppDomain.CurrentDomain.BaseDirectory };
            SFD = new SaveFileDialog() { InitialDirectory = AppDomain.CurrentDomain.BaseDirectory };
            OFD.Multiselect = false;
        }

        public static OpenFileDialog GetOFD(string filter) {
            OFD.Filter = filter;
            return OFD;
        }

        public static SaveFileDialog GetSFD(string filter) {
            SFD.Filter = filter;
            return SFD;
        }

    }
}
