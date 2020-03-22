using System;
using System.Collections.Generic;
using System.IO;
using Keneyz_03.Model;
using Keneyz_03.Tools.Managers;

namespace Keneyz_03.Tools.DataStorage
{
    internal class SerializedDataStorage : IDataStorage
    {
        private readonly List<Person> _persons;

        internal SerializedDataStorage()
        {
            try
            {
                _persons = SerializationManager.Deserialize<List<Person>>(FileFolderHelper.StorageFilePath);
            }
            catch (FileNotFoundException)
            {
                _persons = new List<Person>();

                string[] names =
                {
                    "Julia",
                    "Simon",
                    "Audrey",
                    "Sonia",
                    "Alan",
                    "Austin",
                    "Angela",
                    "Paul",
                    "Andrew",
                    "Felicity",
                    "Eric",
                    "Joanne",
                    "Sam",
                    "Dominic",
                    "Neil",
                    "Brian",
                    "Olivia",
                    "Kevin",
                    "Katherine",
                    "John",
                    "Sasha",
                    "Lucas",
                    "Adam",
                    "Matt",
                    "Justin",
                    "Joshua",
                    "Edward",
                    "Joe",
                    "Evan",
                    "Amelia",
                    "Brandon",
                    "Luke",
                    "Woody",
                    "Ben",
                    "Carol",
                    "Bella",
                    "Jan",
                    "Irene",
                    "Rose",
                    "Carl",
                    "Brian",
                    "Harry",
                    "Sue",
                    "Isaac",
                    "Zohan",
                    "Frank",
                    "Theresa",
                    "Wanda",
                    "Tony",
                    "Zoe"
                };

                string[] surnames =
                {
                    "Clark",
                    "Graham",
                    "Johnston",
                    "Edmunds",
                    "Fraser",
                    "Bond",
                    "Alsop",
                    "Dickens",
                    "North",
                    "Martin",
                    "Coleman",
                    "Gates",
                    "Wallace",
                    "Springer",
                    "Quinn",
                    "Kerr",
                    "Jones",
                    "MvGrath",
                    "Pullman",
                    "Ince",
                    "Roberts",
                    "Parson",
                    "Campbell",
                    "Slater",
                    "Carr",
                    "Price",
                    "Dowd",
                    "Dyer",
                    "Black",
                    "Gibson",
                    "Turner",
                    "Hoogarden",
                    "Gray",
                    "Glover",
                    "Haris",
                    "Langdon",
                    "Lee",
                    "Hardacre",
                    "Lewis",
                    "Mackay",
                    "Paige",
                    "Morgan",
                    "Miller",
                    "May",
                    "Poole",
                    "Smith",
                    "Skinner",
                    "Walker",
                    "Stark",
                    "Sharp"
                };

                Random random = new Random();

                for (var i = 0; i < 50; i++)
                {
                    DateTime birthday = new DateTime(random.Next(1919, 2014), random.Next(1, 12), random.Next(1, 28));

                    string email = names[i].ToLower() + "." + surnames[i][0] + "@test.com";

                    Person temp = new Person(names[i], surnames[i], email, birthday);

                    _persons.Add(temp);
                }

                SaveChanges();
            }
        }

        public bool PersonExists(Person person)
        {
            return _persons.Contains(person);
        }

        public void AddPerson(Person person)
        {
            _persons.Add(person);
            SaveChanges();
        }

        public void RemovePerson(Person person)
        {
            _persons.Remove(person);
            SaveChanges();
        }

        public void ApplyChanges()
        {
            SaveChanges();
        }

        public List<Person> PersonsList => _persons;

        private void SaveChanges()
        {
            SerializationManager.Serialize(_persons, FileFolderHelper.StorageFilePath);
        }
    }
}