using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuadraticFactorization
{
    public class QuadraticFactorizationCore
    {
        public void SplitMethod()
        {

        }
    }
    public class SplitMiddleTermMethod
    {
        public static bool successful = false;
        public static void FindNumber(double addToMake, double multiplyToMake)
        {
            Console.WriteLine("Finding numbers that add to make " + addToMake + " and multiply to make " + multiplyToMake);
            int no = (int)-multiplyToMake;
            double secondno = 0;
            bool found = false;
            while (!found)
            {
                no++;
                secondno = multiplyToMake / no;
                // try
                //{
                Console.WriteLine("Try with " + no + "...");
                if (multiplyToMake % no == 0)
                {
                    found = secondno + no == addToMake;
                    Console.WriteLine(secondno + " and " + no + " multiply to make " + multiplyToMake);
                    Console.WriteLine("But do they add to make " + addToMake + "?");
                    Console.WriteLine(secondno + " " + no + " = " + (secondno + no) + " so " + found);
                }
                else
                {
                    Console.WriteLine(multiplyToMake + " is not divisible by " + no+"!");
                    if (no > 500)
                    {
                        Console.WriteLine("The first number is bigger than 500! I get a feeling that this method isn't the best way to do it :p");
                        successful = false;
                        return;
                    }
                }

            }
            Console.WriteLine("The two numbers are " + no + " and " + secondno);
            successful = true;
            // catch { Console.WriteLine(multiplyToMake" is not divisible by "+no+"!");}
            //  }
        }
    }
}
