using System;
using System.Linq;
namespace D2
{
    class Program
    {
        static void Main(string[] args)
        {
            int[] list = new int[100];
            Random t = new Random();
            for(int i = 0; i < 100; i++)
            {
                list[i] = Convert.ToInt32(t.NextDouble() * 1000);
            }
            int sum = 0;
            int[] listOrdered = list.Select(x => x).OrderByDescending(x => x).ToArray();
            Console.WriteLine("降序排列为如下：");
            foreach(var item in listOrdered)
            {
                Console.WriteLine(item);
                sum = sum + item;
            }
            Console.WriteLine("和为"+sum);
            Console.WriteLine("平均值为"+Convert.ToDouble(sum) / 100);

        }
    }
}
