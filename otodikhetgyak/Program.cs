namespace otodikhetgyak
{
    using System.IO;
    using System.Linq;
    using System.Transactions;

    class Person
    {
        public int id { get; set; }
        public string survey { get; set; }
        public double gender { get; set; }
        public double age { get; set; }
        public double bmi { get; set; }
        public double bloodsugar { get; set; }

        public Person(int id, string survey, double gender, double age, double bmi, double bloodsugar)
        {
            this.id = id;
            this.survey = survey;
            this.gender = gender;
            this.age = age;
            this.bmi = bmi;
            this.bloodsugar = bloodsugar;
        }
    }

    internal class Program
    {
        static void Main(string[] args)
        {
            /* DateTime today = DateTime.Now;
             string userInput = "y";
             List<int> randoms;
             int tries = 0;
             if (userInput == "y")
             {
                 if (tries != 0)
                 {
                     randoms = nums();
                     Console.WriteLine($"On {today.AddDays(tries * 7)} numbers were: ");
                     Console.WriteLine("Another week? y/n");
                     userInput = Console.ReadLine();
                 }
                 else
                 {
                     randoms = nums();
                     Console.WriteLine($"On {today} numbers were: ");
                     Console.WriteLine("Another week? y/n");
                     userInput = Console.ReadLine();
                 }
                 tries++;
             }*/

            Dictionary<int, double> avgBmiResults = avgBmi();
            var lines = avgBmiResults.Select(kvp => kvp.Key + " - " + kvp.Value);
            Console.WriteLine(string.Join(Environment.NewLine, lines));
            //Console.WriteLine($"asd {avgBmiResults.Select(kvp => )} - {avgBmiResults.Values}");
            Console.ReadKey();
        }

        static double obeseAvgAge()
        {
            double obeseAge = 0;
            List<Person> people = personList("NHANES_1999-2018.csv");
            List<Person> obeseList = new List<Person>();

            foreach (Person person in people)
            {
                if(person.bmi > 30.0)
                {
                    obeseList.Add(person);
                }
            }

            for (int i = 0; i < obeseList.Count; i++)
            {
                obeseAge += obeseList[i].age;
            }

            return obeseAge / obeseList.Count;
        }

        static double highestBmiBloodSugar()
        {
            double highestBs = 0;
            int highestBmiIndex = 0;
            double highestBmi = 0;
            List<Person> people = personList("NHANES_1999-2018.csv");

            for (int i = 0; i < people.Count; i++)
            {
                if (people[i].bmi > highestBmi)
                {
                    highestBmiIndex = i;
                    highestBmi = people[i].bmi;
                }
            }

            for (int i = 0; i < people.Count; i++)
            {
                if(i == highestBmiIndex)
                {
                    highestBs = people[i].bloodsugar;
                }
            }

            return highestBs;
        }

        static double highBloodSugar()
        {
            double percentage = 0;

            List<Person> people = personList("NHANES_1999-2018.csv");
            int higher = 0;
            foreach (Person person in people)
            {
                if(person.bloodsugar > 5.6)
                {
                    higher++;
                }
            }
            percentage = higher / people.Count * 100;
            return percentage;
        }

        static Dictionary<int, double> avgBmi()
        {
            List<Person> people = personList("NHANES_1999-2018.csv");
            Dictionary<int, double> avgBmiValues = new Dictionary<int, double>();
            double avgMale = 0;
            double avgFemale = 0;
            double maleNum = 0;
            double femaleNum = 0;
            for (int i = 0; i < people.Count; i++)
            {
                if (people[i].gender == 1.0)
                {
                    avgMale += people[i].bmi;
                    maleNum++;
                }
                else if (people[i].gender == 2.0)
                {
                    avgFemale += people[i].bmi;
                    femaleNum++;
                }
            }

            avgBmiValues.Add(1, avgMale / maleNum);
            avgBmiValues.Add(2, avgFemale / femaleNum);

            return avgBmiValues;
        }

        static List<Person> personList(string path)
        {
            int seqn = 0;
            string survey = "";
            double gender = 0;
            double age = 0;
            double bmi = 0;
            double bloodsugar = 0;
            List<Person> people = new List<Person>();
            StreamReader sr = new StreamReader(path);

            while (!sr.EndOfStream)
            {
                sr.ReadLine();
                string line = sr.ReadLine();
                string[] parts = line.Split(',');
                seqn = Convert.ToInt32(parts[0]);
                survey = parts[1];
                gender = Convert.ToDouble(parts[2]);
                age = Convert.ToDouble(parts[3]);
                bmi = Convert.ToDouble(parts[4]);
                bloodsugar = Convert.ToDouble(parts[5]);
                Person person = new Person(seqn, survey, gender, age, bmi, bloodsugar);
                people.Add(person);
            }
            sr.Close();
            return people;
        }

        static List<int> nums()
        {
            List<int> randomNums = new List<int>(5);

            Random rnd = new Random();
            int random = 0;

            for (int i = 0; i < 5; i++)
            {
                random = rnd.Next(0, 91);
                if (!randomNums.Contains(random))
                {
                    randomNums.Add(rnd.Next(90));
                }
            }
            fileWriter();
            return randomNums;
        }

        static void fileWriter()
        {
            List<int> randomNums = nums();
            StreamWriter sw = new StreamWriter("lottoszamok.txt");
            
            for (int i = 0; i < randomNums.Count; i++)
            {
                sw.WriteLine(randomNums[i]);
            }
            sw.Close();
        }
    }
}