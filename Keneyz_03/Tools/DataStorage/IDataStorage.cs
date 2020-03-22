using System.Collections.Generic;
using Keneyz_03.Model;

namespace Keneyz_03.Tools.DataStorage
{
    internal interface IDataStorage
    {
        bool PersonExists(Person person);

        void AddPerson(Person person);

        void RemovePerson(Person person);

        void ApplyChanges();

        List<Person> PersonsList { get; }
    }
}