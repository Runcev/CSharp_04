using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Text.RegularExpressions;
using Keneyz_03.Tools.Exceptions;

namespace Keneyz_03.Model
{
    [Serializable]
    internal class Person
    {
        private string _name;
        private string _surname;
        private string _email;
        private DateTime _birthday;

        private int _age = -1;
        private string _western;
        private string _chinese;
        private string _birthdayString;

        public string Name
        {
            get => _name;
            set
            {
                _name = value;
                OnPropertyChanged();
            }
        }

        public string Surname
        {
            get => _surname;
            set
            {
                _surname = value;
                OnPropertyChanged();
            }
        }

        public string Email
        {
            get => _email;
            set
            {
                _email = value;
                OnPropertyChanged();
            }
        }

        public DateTime Birthday
        {
            get => _birthday;
            set
            {
                _birthday = Convert.ToDateTime(value);
                _birthdayString = value.ToShortDateString();
                _age = AgeCount();
                _western = WesternCount();
                _chinese = ChineseCount();

                OnPropertyChanged();
                OnPropertyChanged("IsAdult");
                OnPropertyChanged("IsBirthday");
                OnPropertyChanged("BirthdayString");
                OnPropertyChanged($"WesternSign");
                OnPropertyChanged("ChineseSign");
            }
        }

        private Person(string name, string surname)
        {
            _name = name;
            _surname = surname;
        }

        public Person(string name, string surname, string email, DateTime birthday)
        {
            _name = name;
            _surname = surname;
            _email = email;
            _birthday = birthday;
        }

        public Person(string name, string surname, string email) : this(name, surname, email, DateTime.Now)
        {

        }

        public Person(string name, string surname, DateTime birthday) : this(name, surname, "", birthday)
        {

        }


        private int Age => (_age == -1) ? (_age = AgeCount()) : _age;

        public bool IsAdult => (_age != -1) ? 18 <= _age : 18 <= (_age = AgeCount());

        public string BirthdayString => _birthdayString ?? (_birthday.ToString("dd MMMM yyyy"));

        public string WesternSign => _western ??= WesternCount();

        public string ChineseSign => _chinese ??= ChineseCount();

        public bool IsBirthday => (_birthday.Day == DateTime.Today.Day) && (_birthday.Month == DateTime.Today.Month);


        public void Validate()
        {
            if (Age < 0)
            {
                throw new NotBornException();
            }

            if (Age > 135)
            {
                throw new TooOldException();
            }

            var emailRegex = new Regex(@"^([\w\.\-]+)@([\w\-]+)((\.(\w){2,3})+)$");
            if (!emailRegex.IsMatch(_email))
            {
                throw new InvalidEmailExceptions(_email);
            }
        }

        private int AgeCount()
        {
            var dateNow = DateTime.Now;
            var year = dateNow.Year - _birthday.Year;

            if (dateNow.Month < _birthday.Month || (dateNow.Month == _birthday.Month && dateNow.Day < _birthday.Day))
            {
                year--;
            }

            return year;
        }

        private string WesternCount()
        {
            switch (_birthday.Month)
            {
                case 1:
                    return (_birthday.Day < 20) ? "Capricorn" : "Aquarius";
                case 2:
                    return (_birthday.Day < 19) ? "Aquarius" : "Pisces";
                case 3:
                    return (_birthday.Day < 21) ? "Pisces" : "Aries";
                case 4:
                    return (_birthday.Day < 20) ? "Aries" : "Taurus";
                case 5:
                    return (_birthday.Day < 21) ? "Taurus" : "Gemini";
                case 6:
                    return (_birthday.Day < 21) ? "Gemini" : "Cancer";
                case 7:
                    return (_birthday.Day < 23) ? "Cancer" : "Leo";
                case 8:
                    return (_birthday.Day < 23) ? "Leo" : "Virgo";
                case 9:
                    return (_birthday.Day < 23) ? "Virgo" : "Libra";
                case 10:
                    return (_birthday.Day < 23) ? "Libra" : "Scorpio";
                case 11:
                    return (_birthday.Day < 22) ? "Scorpio" : "Sagittarius";
                default:
                    return (_birthday.Day < 22) ? "Sagittarius" : "Capricorn";
            }
        }

        private string ChineseCount()
        {
            string[] chineseName =
            {
                "Rat", "Ox", "Tiger", "Rabbit", "Dragon", "Snake",
                "Horse", "Goat", "Monkey", "Rooster", "Dog", "Pig"
            };

            var index = Math.Abs(_birthday.Year - 1900) % 12;

            return chineseName[index];
        }


        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}