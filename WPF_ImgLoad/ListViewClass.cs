using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WPF_ImgLoad
{
    internal class ListViewClass
    {
        public string name { get; set; }
        public string size { get; set; }
        public string time { get; set; }

        private static List<ListViewClass> instance;

        public static List<ListViewClass> GetInstance()
        {
            if (instance == null)
                instance = new List<ListViewClass>();

            return instance;
        }

    }
}
