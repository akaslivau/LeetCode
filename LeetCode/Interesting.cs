using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LeetCode
{
    interface IFirst
    {
        void Test();
    }

    interface ISecond
    {
        void Test();
    }

    public class Dog : IFirst, ISecond
    {
        void IFirst.Test()
        {
            Console.WriteLine("I am first dog");
        }

        void ISecond.Test()
        {
            Console.WriteLine("I am second dog");
        }

        public void Test()
        {
            Console.WriteLine("Dog...Just dog");
        }
    }

    public class Interesting
    {
        public Interesting()
        {
            var dog = new Dog();
            dog.Test();
            IFirst iFirst = (IFirst) dog;
            iFirst.Test();
            ISecond iSecond = (ISecond)dog;
            iSecond.Test();
        }
    }
}
