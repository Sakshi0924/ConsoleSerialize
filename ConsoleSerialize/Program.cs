using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;
using System.Runtime.Serialization;

namespace ConsoleSerialize
{
    [Serializable]
    class Person: IDeserializationCallback
    {
        
        int birthYear;
                   
        public Person(int year)
        {
            birthYear = year;
        }

        [NonSerialized]
        public int age;

        public void OnDeserialization(object sender)
        {
            DateTime date = DateTime.Now;
            age = date.Year - birthYear;
        }
    }
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Enter your Year of Birth");
            int year = int.Parse(Console.ReadLine());
            FileStream fs = new FileStream(@"Age.txt", FileMode.OpenOrCreate, FileAccess.ReadWrite);
            BinaryFormatter bf = new BinaryFormatter();
            Person pobj = new Person(year);
            bf.Serialize(fs, pobj);

            //deSerialize
            fs.Seek(0, SeekOrigin.Begin);
            Person res=(Person)bf.Deserialize(fs);
            Console.WriteLine("Your Age is:"+res.age);

        }
    }
}
