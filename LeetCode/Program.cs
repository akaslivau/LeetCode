using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.ExceptionServices;
using System.Text;
using System.Threading.Tasks;

namespace LeetCode
{
    class Program
    {
        static void Main(string[] args)
        {
            //var input = Console.ReadLine();
            Console.WriteLine(IsValid("[]{]"));
            Console.ReadLine();
        }


        public static int LengthOfLongestSubstring(string s)
        {
            var n = s.Length;
            if (n == 0) return 0;
            if (n == 1) return 1;

            int max = 1;
            var l = new List<char>();
            l.Add(s[0]);
            for (int i = 1; i < s.Length; i++)
            {
                var op = s[i];
                if (!l.Contains(s[i]))
                {
                    l.Add(s[i]);
                    continue;
                }

                if (l.Count > max) max = l.Count;
                l.Clear();
            }
            if (l.Count > max) max = l.Count;
            return max;
        }



        #region Solved
        public static string LongestCommonPrefix(string[] strs)
        {
            if (strs.Length == 0) return "";
            if (strs.Any(x => x.Length == 0)) return "";
            var minLength = strs.Min(x => x.Length);

            var res = "";
            for (int i = 0; i < minLength; i++)
            {
                var c = strs.Select(x => x[i]).ToArray();
                if (c.All(x => x == c.First())) res += c.First();
                else break;
            }
            return res;
        }

        public static bool IsPalindrome(int x)
        {
            if (x < 0) return false;
            if (x < 10) return true;

            var s = x.ToString();
            var n = s.Length;
            for (int i = 0; i < n; i++)
            {
                if (s[i] != s[n - i - 1]) return false;
            }
            return true;
        }

        public static int Reverse(int x)
        {
            if (x <= int.MinValue) return 0;
            if (x >= int.MaxValue) return 0;

            var s = new string(Math.Abs(x).ToString().Reverse().ToArray());
            if (!int.TryParse(s, out var res)) return 0;
            return x >= 0 ? res : -res;
        }

        public static int[] TwoSum(int[] nums, int target)
        {
            for (int i = 0; i < nums.Length; i++)
            {
                for (int j = i + 1; j < nums.Length; j++)
                {
                    if ((nums[i] + nums[j]) == target)
                    {
                        return new[] { i, j };
                    }
                }
            }

            throw new Exception("Code not works!");
        }

        public static bool IsValid(string s)
        {
            var d = new Dictionary<char, char>()
            {
                {')', '('},
                {']', '['},
                {'}', '{'},
            };
            var stack = new Stack<char>();
            for (int i = 0; i < s.Length; i++)
            {
                var c = s[i];
                // If the current character is a closing bracket.
                if (d.ContainsKey(c))
                {

                    // Get the top element of the stack. If the stack is empty, set a dummy value of '#'
                    char topElement = !stack.Any() ? '#' : stack.Pop();

                    // If the mapping for this bracket doesn't match the stack's top element, return false.
                    if (topElement != d[c])
                    {
                        return false;
                    }
                }
                else
                {
                    // If it was an opening bracket, push to the stack.
                    stack.Push(c);
                }
            }

            return !stack.Any();
        }
        #endregion



    }
}
