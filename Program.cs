using System;

namespace CSharp9
{

    class Program
    {
        static void Main()
        {
            Program program = new();
            program.Ornek1_1();
        }

        //C# 9.0 İle Gelen Özellikler
        //Yazan: Mert Şen FINDICAK

        //Çıktı Üreten Metot Listesi
        //Ornek1_1(), Ornek1_2()
        //Ornek2_1()
        //Ornek3_1()
        //Ornek5_1()

        //Özellik 1 - Records
        //Özellik 1.1
        //Kayıtlar kimlikleriyle değil içerikleriyle tanımlanır (Value-based equality)
        //class objelerinde == operatörünü kullanırsak referans olarak aynı yere 
        //işaret edip etmediğini kontol eder
        //ama recordlarda içerik kontrolü yapılır.
        //Örnek 1.1 (Çalıştırılabilir)
        public void Ornek1_1()
        {
            Person a = new Person { FirstName = "Mert", LastName = "FINDICAK" };
            Person b = new Person { FirstName = "Mert", LastName = "FINDICAK" };
            Console.WriteLine(a == b); //false
            PersonRecord c = new("Mert", "FINDICAK");
            PersonRecord d = new("Mert", "FINDICAK");
            Console.WriteLine(c == d); //true
        }

        //Özellik 1.2
        //Oluşturulduktan sonra içindeki değerler değişmez (immutable).
        //Yani yalnızca initialize zamanında değer alabilir başka zaman değiştirilemez.
        //Örnek 1.2 (Çalıştırılabilir)
        public record Name(string Name1, string Name2);
        public void Ornek1_2()
        {
            Name yeniName = new("Isim1", "Isim2");
            Console.WriteLine(yeniName.Name1); //Isim1 yazar
            //yeniName.name1 = "Sen"; Compiler ERROR!
        }

        //Özellik 1.3
        //recordlar yalnızca tek satırda oluşturulabilir(positional record).
        //Örnek 1.3
        public record PersonRecord
        {
            public string FirstName { get; init; }
            public string LastName { get; init; }
            public PersonRecord(string firstName, string lastName)
              => (FirstName, LastName) = (firstName, lastName);
            public void Deconstruct(out string firstName, out string lastName)
              => (firstName, lastName) = (FirstName, LastName);
        }
        public record PersonRecord2(string FirstName, string LastName);


        //Özellik 1.4
        //record lar diğer recordlardan kalıtım (Inheritance) olabilir.
        //Örnek 1.4
        public record TestInheritance
        {
            public string Isim { get; init; }
        }
        public record PersonWithId : TestInheritance
        {
            public int ID { get; set; }
        }

        //Özellik 2 - init
        //Özellik 2.1
        //init sadece objeyi initialize ederken propertylere değer atar sonra ise değiştirilemez (immutable).
        //get; set; yerine get; init; yazılır
        //Örnek 2.1 (Çalıştırılabilir)
        public class Person
        {
            public string FirstName { get; init; }
            public string LastName { get; init; }
        }
        public void Ornek2_1()
        {
            var person2_1 = new Person { FirstName = "Mads", LastName = "Torgersen" };
            //person2_1.FirstName = "Degisti"; Compiler ERROR!
            Console.WriteLine($"{person2_1.FirstName} {person2_1.LastName}");
        }

        //Özellik 3 - with İfadesi
        //Özellik 3.1
        //Immutable nesneleri değiştiremiyeceğimiz için with ile var olan nesneden istediğimiz
        //nesneyi kopyalayıp istediğimiz yerlerini değiştirebiliriz.
        //Örnek 3.1 (Çalıştırılabilir)
        public void Ornek3_1()
        {
            var person3_1 = new PersonRecord("Mert", "SEN");
            var person3_1_2 = person3_1 with { LastName = "FINDICAK" };
            Console.WriteLine($"İlk Kayıt = {person3_1.FirstName} {person3_1.LastName}");
            Console.WriteLine($"İkinci Kayıt = {person3_1_2.FirstName} {person3_1_2.LastName}");
        }

        //Özellik 4 - Standart(boilerplate) kodlardan arınma
        //Özellik 4.1/
        //C# 9.0 da boilerplate kodlar kullanmadan 
        //basit kodlarınızı bir main metodu içindeymiş gibi yazabilirsiniz.
        /*
         Örnek:
            -> C# 8.0

            using System;
            class Program
            {
                static void Main()
                {
                    Console.WriteLine("Hello World!");
                }
            }

            -> C# 9.0

            using System;

            Console.WriteLine("Hello World!");
         */


        //Özellik 5 - Hedefi anlayan new ifadesi
        //Özellik 5.1
        //Nesne oluştururken yeni ifadede tipini belirtmemize gerek kalmaz.
        //Örnek 5.1 (Çalıştırılabilir)
        public class Point
        {
            public int FirstPoint { get; set; }
            public int SecondPoint { get; set; }
            public Point(int firstPoint, int secondPoint)
            {
                this.FirstPoint = firstPoint;
                this.SecondPoint = secondPoint;
            }
            public string GetAll()
            {
                return ($"{FirstPoint},{SecondPoint}");
            }
        }
        public void Ornek5_1()
        {
            //Onceden
            Point point1 = new Point(3, 5);
            //Simdi
            Point point2 = new(3, 5);
            //Bu objelerin listesini tutarken işimizi çok kolaylaştırır.
            Point[] pointArray = { new(1, 2), new(5, 2), new(5, -3), new(1, -3) };

            Console.WriteLine($"Point1= {point1.GetAll()}\n" +
                $"Point2= {point2.GetAll()}\n" +
                $"PointArray=");
            Array.ForEach(pointArray, element => Console.WriteLine(element.GetAll()));
        }

        //Özellik 6
        //Override edilen metodun dönüş tipini daha spesifik bir dönüş tipiyle değiştirebiliriz.
        public abstract class Car
        {
            public abstract Car Do();
        }
        public class SportsCar : Car
        {
            public override SportsCar Do()
            {
                return null;
            }
        }

    }
}
