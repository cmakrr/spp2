using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DtoGenerator.MainClasses.Interfaces
{
    public interface IFaker
    {
        public T Create<T>() where T : IDto;
        public object? Create(Type t);
    }
}
