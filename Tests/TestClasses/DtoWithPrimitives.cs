using DtoGenerator.MainClasses.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tests.TestClasses
{
    internal class DtoWithPrimitives : IDto
    {
        public int x { get; set; }
        public double y;

        private long z { get; set; }
    }
}
