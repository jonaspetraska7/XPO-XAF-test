using DevExpress.Xpo;
using System;

namespace WindowsFormsApp1.XPO
{
    public class Student : XPLiteObject
    {
        public Student() : base()
        {
            // This constructor is used when an object is loaded from a persistent storage.
            // Do not place any code here.
        }

        public Student(Session session) : base(session)
        {
            // This constructor is used when an object is loaded from a persistent storage.
            // Do not place any code here.
        }
        public override void AfterConstruction()
        {
            base.AfterConstruction();
            // Place here your initialization code.
        }

        string _code;
        [Key, Persistent("Code")]
        [Size(10)]
        public string Code
        {
            get { return _code; }
            set { SetPropertyValue(nameof(Code), ref _code, value); }
        }

        string _name;
        [Size(50)]
        public string Name
        {
            get { return _name; }
            set { SetPropertyValue(nameof(Name), ref _name, value); }
        }

        string _address;
        [Size(50)]
        public string Address
        {
            get { return _address; }
            set { SetPropertyValue(nameof(Address), ref _address, value); }
        }

        DateTime _dateOfBirth;
        public DateTime DateOfBirth
        {
            get { return _dateOfBirth; }
            set { SetPropertyValue(nameof(DateOfBirth), ref _dateOfBirth, value); }
        }


        string _gender;
        [Size(50)]
        public string Gender
        {
            get { return _gender; }
            set { SetPropertyValue(nameof(Gender), ref _gender, value); }
        }


    }

}