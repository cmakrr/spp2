using DtoGenerator.MainClasses.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tests.TestClasses
{
    internal class DtoContainingItself : IDto
    {
        public double x;
        public DtoContainingItself dto { get; set; }
    }
}
