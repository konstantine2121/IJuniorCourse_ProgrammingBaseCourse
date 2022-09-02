using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IJuniorCourse_ProgrammingBaseCourse.OOP.DatabaseGenericExample.Interfaces
{
    //Можно вместо него использовать рефлексию и копировать данные из объекта в объект перебирая все поля и свойства объекта.
    public interface IClonable<T>
    {
        T Clone();
    }
}
