using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace WpfApp1
{
    public class Manager
    {
        private static Manager _INSTANCE;

        public static Manager Instance
        {
            get
            {
                if (_INSTANCE == null)
                {
                    _INSTANCE = new Manager();
                }
                return _INSTANCE;
            }
        }

        public course_work_EFEntities Context { get; set; }

        public Frame MainFrame { get; set; }
        public Frame MenuFrame { get; set; }
        public MainWindow MainWindow { get; set; }
    }
}
