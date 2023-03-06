
using System.Threading.Tasks.Dataflow;

namespace LINQ
{
    internal class Program
    {
        static void Main(string[] args)
        {
            WhereLINQ();

            PeopleByAge();
            
            ThenByLINQ();
            
            ThenByDecending();

            ToLookUPLINQ();

            JoinLINQ();

            GroupJoinLINQ();

            SelectLINQ();

            AllAndAnyLINQ();

            ContainsLINQ();
            
            AggregateLINQ();

            AverageLINQ();

            CountLINQ();

            MaximumLINQ();
            
            SumLINQ();
        }

        public static void WhereLINQ()
        {
            Console.WriteLine("LINQ");
            //ctrl + k +c / u
            //IList<People> peoples = new List<People>()
            //{
            //    new People() { Id = 1, Name = "Joosep", Age = 10 },
            //    new People() { Id = 2, Name = "Kalev", Age = 21 },
            //    new People() { Id = 3, Name = "Roonas", Age = 18},
            //    new People() { Id = 4, Name = "Tom", Age = 12},
            //    new People() { Id = 5, Name = "Bill", Age = 17},
            //    new People() { Id = 6, Name = "Bill", Age = 15},
            //    new People() { Id = 7, Name = "Jaagup", Age = 21},

            //};

            //nätab nimekirjas üle ühe
            Console.WriteLine("------------");
            var filteredResult = PeopleList.people.Where((s, i) =>
            {
                if (i % 2 == 0)
                {
                    return true;
                }
                return false;
            });

            foreach (var People in filteredResult)
            {
                Console.WriteLine(People.Name);
            }

        }

        public static void PeopleByAge()
        {
            Console.WriteLine("Vanuse järgi selekteerimine");

            var peopleByAge = PeopleList.people.Where(s => s.Age >= 14 && s.Age <= 20);
            foreach (var people in peopleByAge)
            {
                Console.WriteLine(people.Age + " " + people.Name);
            }
            
        }

        public static void ThenByLINQ()
        {
            Console.WriteLine("-------------");
            Console.WriteLine("ThenBy ja ThenByDecening järgi reastamine");

            var thenByResult = PeopleList.people
                .OrderBy(x => x.Name)
                .ThenBy(y => y.Age);

            Console.WriteLine("ThenBy järgi: ");
            foreach (var people in thenByResult)
            {
                Console.WriteLine(people.Name + " " + people.Age);
            }
        }

        public static void ThenByDecending()
        {
            Console.WriteLine("-----------------");
            Console.WriteLine("ThenByDecending järgi reastamine");

            var thenByDecending = PeopleList.people
                .OrderBy(x => x.Name)
                .ThenByDescending(y => y.Age);

            foreach (var people in thenByDecending)
            {
                Console.WriteLine(people.Name + " " + people.Age);
            }
        }

        public static void ToLookUPLINQ()
        {
            Console.WriteLine("------------------");
            Console.WriteLine("ToLookup järgi reastamine");

            var toLookUp = PeopleList.people
                .ToLookup(x => x.Age);

            foreach (var people in toLookUp)
            {
                Console.WriteLine("Age Group " + people.Key);

                foreach (People p in people)
                {
                    Console.WriteLine("Person's name: {0} ", p.Name);

                }
            }
        }

        public static void JoinLINQ()
        {
            Console.BackgroundColor = ConsoleColor.DarkRed;
            Console.WriteLine("------------------");
            Console.WriteLine("InnerJoin in LINQ");
            
            var innerJoin = PeopleList.people.Join(
                GenderList.genderList,
                humans => humans.GenderId,
                gender => gender.Id,
                (humans, gender) => new
                {
                    Name = humans.Name,
                    Sex = gender.Sex
                });

            foreach (var obj in innerJoin)
            {
                Console.WriteLine("{0} - {1}", obj.Name, obj.Sex);
            }
        }

        public static void GroupJoinLINQ()
        {
            Console.BackgroundColor = ConsoleColor.Magenta;
            Console.WriteLine("----------------");
            Console.WriteLine("GorupJoinLINQ");
            var groupJoin = GenderList.genderList
                .GroupJoin(PeopleList.people, 
                p => p.Id,
                g => g.GenderId,
                (p, peopleGroup) => new
                {
                    Humans = peopleGroup,
                    GenderFullName = p.Sex
                });

            foreach (var item in groupJoin)
            {
                Console.WriteLine(item.GenderFullName);

                foreach (var name in item.Humans)
                {
                    Console.WriteLine(name.Name);
                }
            }
        }

        public static void SelectLINQ()
        {

            //Võtab objektid ja talletab need ning prindib
            Console.BackgroundColor = ConsoleColor.DarkBlue;
            Console.WriteLine("----------------");
            Console.WriteLine("Select in LINQ");
            var selectResult = PeopleList.people
                .Select(x => new
                {
                    Name = x.Name,
                    Age = x.Age
                });

            foreach (var item in selectResult)
            {
                Console.WriteLine("Human name: {0}, Age: {1}", item.Name, item.Age);
            }
        }

        public static void AllAndAnyLINQ()

        {
            Console.BackgroundColor = ConsoleColor.Black;
            Console.WriteLine("---------------------");
            Console.WriteLine("All LINQ");
            bool areAllPeeopleTeenagers = PeopleList.people
                .All(x => x.Age < 12 && x.Age < 20);
            //vasstus tuleb true
            Console.WriteLine(areAllPeeopleTeenagers);

            Console.WriteLine("----------------");
            Console.WriteLine("Any LINQ");

            bool isAnyPersonTeenager = PeopleList.people
                .Any(x => x.Age > 12);

            //kasvõi üks andmerida vastab tingimusele
            Console.WriteLine(isAnyPersonTeenager);
        }

        public static void ContainsLINQ()
        {
            Console.WriteLine("-------------------");
            Console.WriteLine("Contains LINQ");

            //Pärib kas number 1 on numbrite nimekirjas olemas
            bool result = NumberList.numberList.Contains(1);
            Console.WriteLine(result);
        }

        public static void AggregateLINQ()
        {
            Console.WriteLine("---------------");
            Console.WriteLine("Aggregate LINQ");

            string commaSeperatedPersonNames = PeopleList.people
                .Aggregate<People, string>(
                "People names: ",
                (str, y) => str += y.Name + ", "
                );

            Console.WriteLine(commaSeperatedPersonNames);

        }

        public static void AverageLINQ()
        {
            Console.WriteLine("---------------");
            Console.WriteLine("Average LINQ");
            var averageResult = PeopleList.people
                .Average(x => x.Age);

            Console.WriteLine(averageResult);
        }

        public static void CountLINQ()
        {
            Console.WriteLine("----------------");
            Console.WriteLine("Count LINQ");
            var totalPeople = PeopleList.people.Count();

            Console.WriteLine("Inimesi on kokki: " + totalPeople);

            var adultPeople = PeopleList.people.Count(x => x.Age > 18);

            Console.WriteLine("Täisealisi inimesi on: " + adultPeople);
        }

        public static void MaximumLINQ()
        {
            Console.WriteLine("---------------");
            Console.WriteLine("Maximum LINQ");
            
            var oldest = PeopleList.people
                .Max(x => x.Age);
                

            Console.WriteLine("Oldest person is " + oldest + " years old" );
        }

        public static void SumLINQ()
        {
            Console.WriteLine("-----------------");
            Console.WriteLine("Sum LINQ");
            
            var sumAge = PeopleList.people
                .Sum(x => x.Age);

            Console.WriteLine("Every age summed up: " + sumAge);

            Console.WriteLine("-------------------------------------------------------------");
            Console.WriteLine("Täisealiste isikute koondvanus");
           
            

            //Loeb kokku kõik 18+ inimesed ning liidab nende vanused
            var sumAdults = 0;
            var numAdults = 0;

            for (int i = 0; i < PeopleList.people.Count; i++)
            {
                if (PeopleList.people[i].Age >= 18)
                {
                    numAdults++;
                    sumAdults += PeopleList.people[i].Age;
                }
            }

            Console.WriteLine("Täiskasvanud isikute arv: " + numAdults);
            Console.WriteLine("Täiskavanute vanus kokku: "+ sumAdults);

        }


    }
    
}