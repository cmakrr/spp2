using DtoGenerator.MainClasses.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tests.TestClasses
{
    internal class DtoWithList : IDto
    {
        public List<int> list;
        private int x;
    }
}
