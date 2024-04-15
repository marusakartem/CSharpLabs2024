using System;

using Pr_3.ModuleTask1;
using Pr_3.ModuleTask2;


namespace Pr_3
{
    public class Program
    {
        public static void Main(string[] args)
        {
            Task1();
            //Task2();
        }

        private static void Task1()
        {
            Book book = new Book("1984", "George Orwell", "A dystopian novel set in a totalitarian regime, where individualism and independent thinking are suppressed by a surveillance state.");
            book.Show();
        }

        private static void Task2()
        {
            Point A = new Point(0, 0, "A");
            Point B = new Point(0, 4, "B");
            Point C = new Point(3, 4, "C");
            Point D = new Point(3, 0, "D");

            Figure figure = new Figure(A, B, C, D);

            figure.CalculatePerimeter();
        }
    }
}

