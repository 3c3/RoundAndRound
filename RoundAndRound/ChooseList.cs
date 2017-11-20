using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RoundAndRound
{
    public class ChooseList
    {
        private Random random;
        int[] numbers;
        int end;

        public ChooseList(int n, Random rand)
        {
            numbers = new int[n];
            for (int i = 0; i < n; i++) numbers[i] = i;
            end = n;
            random = rand;
        }

        public ChooseList(int n)
        {
            numbers = new int[n];
            for (int i = 0; i < n; i++) numbers[i] = i;
            end = n - 1;
            random = new Random();
        }

        public int Choose()
        {
            int idx = random.Next(end);
            int num = numbers[idx];

            numbers[idx] = numbers[end - 1];
            end--;
            return num;
        }
    }
}
