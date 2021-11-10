using Bar_Simulator1;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Bar_Simulator
{
    class Student
    {
        enum NightlifeActivities { Walk, VisitBar, GoHome };
        enum BarActivities { Drink, Dance, Leave };

        Random random = new Random();

        public string Name { get; set; }
        public int Money { get; set; }
        public int Age { get; set; }
        public Bar Bar { get; set; }
        
        private NightlifeActivities GetRandomNightlifeActivity()
        {
            int n = random.Next(10);
            if (n < 3) return NightlifeActivities.Walk;
            if (n < 8) return NightlifeActivities.VisitBar;
            return NightlifeActivities.GoHome;
        }

        private BarActivities GetRandomBarActivity()
        {
            int n = random.Next(10);
            if (n < 4) return BarActivities.Dance;
            if (n < 7) return BarActivities.Drink;
            return BarActivities.Leave;
        }

        private void WalkOut()
        {
            Console.WriteLine($"{Name} is walking in the streets.");
            Thread.Sleep(200);
        }

        private void VisitBar()
        {
            int n = random.Next(10);
            Age= random.Next(10,60);//babies shouldn't even be in line
            bool staysAtBar;
            Console.WriteLine($"{Name} is getting in the line to enter the bar.");
            if (n>6)
            {
                Console.WriteLine($"{Name} is leaving the queue ");
                    staysAtBar = false;
            }
            if (Age >= 18)
            {
                Bar.Enter(this);
                Console.WriteLine($"{Name} entered the bar!");
                staysAtBar = true;
            }
            else 
            {
                Console.WriteLine($"{Name} is {Age} years old and not old enough to enter!");
                staysAtBar = false;
            }
            while (staysAtBar)
            {
                List<Drink> drinks = new List<Drink>(3);
                Drink drink1 = new Drink();
                {
                    drink1.DName = "Beer";
                    drink1.Count = 50;
                    drink1.Price = 4;
                };
                drinks.Add(drink1);
                Drink drink2 = new Drink();
                {
                    drink2.DName = "Whiskey";
                    drink2.Count = 60;
                    drink2.Price = 3;
                };
                drinks.Add(drink2);
                Drink drink3 = new Drink();
                {
                    drink3.DName = "Wine";
                    drink3.Count = 30;
                    drink3.Price = 6;
                };
                drinks.Add(drink3);
                var nextActivity = GetRandomBarActivity();
                switch (nextActivity)
                {
                    case BarActivities.Dance:
                        Console.WriteLine($"{Name} is dancing.");
                        Thread.Sleep(200);
                        break;
                    case BarActivities.Drink:
                        Money = random.Next(10, 50);
                        int b = random.Next(3);
                        if (b == 1)
                        {
                            if (Money > drink1.Price)
                            {
                                drink1.Count--;
                                Money =(Money - drink1.Price);
                                Console.WriteLine($"{Name} is drinking {drink1.DName} {Money} money left.");
                            }
                            else
                            {
                                Console.WriteLine($"Student {Name} does not have enough for {drink1.DName}");
                            }
                        }
                        else if (b == 2)
                        {
                            if (Money > drink2.Price)
                            {
                                drink2.Count--;
                                Money = (Money - drink2.Price);
                                Console.WriteLine($"{Name} is drinking {drink2.DName} {Money} money left.");
                            }
                            else
                            {
                                Console.WriteLine($"Student {Name} does not have enough for {drink2.DName}");
                            }
                        }
                        else
                        {
                            if (Money > drink3.Price)
                            {
                                drink3.Count--;
                                Money = (Money - drink3.Price);
                                Console.WriteLine($"{Name} is drinking {drink3.DName} {Money} money left.");
                            }
                            else
                            {
                                Console.WriteLine($"Student {Name} does not have enough for {drink3.DName}");
                            }
                        }
                        Thread.Sleep(200);
                        break;
                    case BarActivities.Leave:
                        Console.WriteLine($"{Name} is leaving the bar.");
                        Bar.Leave(this);
                        staysAtBar = false;
                        break;
                    default: throw new NotImplementedException();
                }
            }
        }

        public void PaintTheTownRed()
        {
            WalkOut();
            bool staysOut = true;
            while (staysOut)
            {
                var nextActivity = GetRandomNightlifeActivity();
                switch (nextActivity)
                {
                    case NightlifeActivities.Walk:
                        WalkOut();
                        break;
                    case NightlifeActivities.VisitBar:
                        VisitBar();
                        staysOut = false;
                        break;
                    case NightlifeActivities.GoHome:
                        staysOut = false;
                        break;
                    default: throw new NotImplementedException();
                }
            }
            Console.WriteLine($"{Name} is going back home.");
        }

        public Student(string name, Bar bar)
        {
            Name = name;
            Bar = bar;
        }
    }
}