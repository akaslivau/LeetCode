using System;
using System.Collections;
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
            IsPowerOfTwo(-2147483648);

            int z = 3;
           Console.ReadLine();
        }

        //https://leetcode.com/problems/missing-number/
        //268. Missing Number
        public static int MissingNumber(int[] nums)
        {
            return nums.Length * (nums.Length + 1) / 2 - nums.Sum();
        }

        //258. Add Digits
        //https://leetcode.com/problems/add-digits/
        public int AddDigits(int num)
        {
            if (num < 10) return num;
            return AddDigits(num.ToString().Select(x => int.Parse(x.ToString())).Sum());
        }

        //242. Valid Anagram
        //https://leetcode.com/problems/valid-anagram/
        public static bool IsAnagram(string s, string t)
        {
            return s.Length == t.Length && s.OrderBy(x => x).ToList().SequenceEqual(t.OrderBy(x => x).ToList());
        }

        //231. Power of Two
        //https://leetcode.com/problems/power-of-two/
        public static bool IsPowerOfTwo(int n)
        {
            if (n == int.MinValue) return false;
            var b = (new BitArray(new int[] {n}));
            var cnt = 0;
            for (int i = 0; i < b.Length; i++)
            {
                if (b[i]) cnt++;
            }

            return cnt == 1;
        }


        #region Solved
        public static string ConvertToBase7(int num)
        {
            if (num == 0) return "0";
            bool isNegative = num < 0;
            num = Math.Abs(num);

            var res = "";
            while (num != 0)
            {
                res = (num % 7) + res;
                num /= 7;
            }


            return isNegative ? "-" + res : res;
        }


        //https://leetcode.com/problems/latest-time-by-replacing-hidden-digits/
        //1736. Latest Time by Replacing Hidden Digits
        public static string MaximumTime(string time)
        {
            var res = "";
            if (time[4] == '?') res += "9"; else res += time[4];
            if (time[3] == '?') res = "5" + res; else res = time[3] + res;
            res = ":" + res;

            if (time.Substring(0, 2) == "??")
            {
                res = "23" + res;
                return res;
            }

            if (time[1] == '?')
            {
                if (time[0] == '2') res = "3" + res;
                else
                {
                    res = "9" + res;
                }
            }
            else
            {
                res = time[1] + res;
            }

            if (time[0] == '?')
            {
                var secondDigit = int.Parse(time[1].ToString());
                if (secondDigit <= 3) res = "2" + res;
                else res = "1" + res;
            }
            else
            {
                res = time[0] + res;
            }

            return res;
        }

        //205. Isomorphic Strings
        //https://leetcode.com/problems/isomorphic-strings/
        public static bool IsIsomorphic(string s, string t)
        {
            var map = new Dictionary<char, char>();
            for (int i = 0; i < s.Length; i++)
            {
                if (!map.ContainsKey(s[i]))
                {
                    map.Add(s[i], t[i]);
                    continue;
                }

                if (t[i] != map[s[i]]) return false;
            }

            if (map.Select(x => x.Value).Distinct().Count() != map.Count) return false;

            return true;
        }

        //605. Can Place Flowers
        //https://leetcode.com/problems/can-place-flowers/submissions/
        public static bool CanPlaceFlowers(int[] flowerbed, int n)
        {
            if (n == 0) return true;

            int start = flowerbed.ToList().IndexOf(0);
            if (start == -1) return false;

            var cnt = 0;
            for (int i = start; i < flowerbed.Length; i++)
            {
                if (flowerbed[i] == 1) continue;
                int prevIndex = i - 1;
                int nextIndex = i + 1;

                var leftIsEmpty = prevIndex < 0 || flowerbed[prevIndex] == 0;
                var rightIsEmpty = nextIndex >= flowerbed.Length || flowerbed[nextIndex] == 0;
                if (leftIsEmpty && rightIsEmpty)
                {
                    cnt++;
                    i++;
                }
            }

            return cnt >= n;

            /*var s = string.Join("", flowerbed.Select(x => x.ToString()));
            if (s[0] == '0') s = "0" + s;
            if (s[s.Length - 1] == '0') s = s + "0";

            var items = s.Split(new[] {"1"}, StringSplitOptions.RemoveEmptyEntries);*/
        }


        //414. Third Maximum Number
        //https://leetcode.com/problems/third-maximum-number/
        public static int ThirdMax(int[] nums)
        {
            var unique = nums.Distinct().ToList();
            if (unique.Count < 3) return unique.Max();

            unique = unique.OrderBy(x => x).ToList();
            return unique[unique.Count - 2];
        }

        //859. Buddy Strings
        //https://leetcode.com/problems/buddy-strings/
        public static bool BuddyStrings(string a, string b)
        {
            if (a.Length != b.Length) return false;
            if (a == b)
            {
                return a.Distinct().Count() != a.Length;
            }

            var zipped = a.Zip(b, (x, y) => (x == y)).ToList();
            if (zipped.Count(x => !x) != 2) return false;

            var indexes = zipped.Select((x, i) => new { i, x }).Where(t => !t.x).Select(t => t.i).ToList();
            var chars1 = (new char[] { a[indexes[0]], a[indexes[1]] }).OrderBy(x => x).ToArray();
            var chars2 = (new char[] { b[indexes[0]], b[indexes[1]] }).OrderBy(x => x).ToArray();
            return (new string(chars1)) == (new string(chars2));
        }

        //1185. Day of the Week
        //https://leetcode.com/problems/day-of-the-week/
        public static string DayOfTheWeek(int day, int month, int year)
        {
            return (new DateTime(year, month, day)).ToString("ddddd");
        }

        //https://leetcode.com/problems/running-sum-of-1d-array/
        //1480. Running Sum of 1d Array
        public static int[] RunningSum(int[] nums)
        {
            var res = new int[nums.Length];
            res[0] = nums[0];
            for (int i = 1; i < res.Length; i++)
            {
                res[i] = nums[i] + res[i - 1];
            }

            return res;
        }


        //392. Is Subsequence
        //https://leetcode.com/problems/is-subsequence/
        public static bool IsSubsequence(string s, string t)
        {
            if (s.Length == 0) return true;
            var j = 0;
            var c = s[0];
            for (int i = 0; i < t.Length; i++)
            {
                if (t[i] != c) continue;
                j++;
                if (j == s.Length) return true;
                c = s[j];
            }

            return false;
        }

        //796. Rotate String
        //https://leetcode.com/problems/rotate-string/
        public static bool RotateString(string s, string goal)
        {
            if (s == goal) return true;
            for (int i = 1; i < s.Length; i++)
            {
                var newS = s.Substring(i, s.Length - i) + s.Substring(0, i);
                if (newS == goal) return true;
            }

            return false;
        }

        //844. Backspace String Compare
        //https://leetcode.com/problems/backspace-string-compare/
        public static bool BackspaceCompare(string s, string t)
        {
            var newS = "";
            for (int i = 0; i < s.Length; i++)
            {
                if (s[i] == '#')
                {
                    if (newS.Length < 1) continue;
                    newS = newS.Substring(0, newS.Length - 1);
                }
                else
                {
                    newS += s[i];
                }
            }

            var newT = "";
            for (int i = 0; i < t.Length; i++)
            {
                if (t[i] == '#')
                {
                    if (newT.Length < 1) continue;
                    newT = newT.Substring(0, newT.Length - 1);
                }
                else
                {
                    newT += t[i];
                }
            }

            return newS == newT;
        }

        //1200. Minimum Absolute Difference
        //https://leetcode.com/problems/minimum-absolute-difference/
        public static IList<IList<int>> MinimumAbsDifference(int[] arr)
        {
            Array.Sort(arr);
            int minDif = int.MaxValue;
            for (int i = 0; i < arr.Length - 1; i++)
            {
                var dif = arr[i + 1] - arr[i];
                if (dif < minDif) minDif = dif;
            }

            var res = new List<IList<int>>();
            for (int i = 0; i < arr.Length - 1; i++)
            {
                var dif = arr[i + 1] - arr[i];
                if (dif == minDif) res.Add(new List<int>() { arr[i], arr[i + 1] });
            }

            return res;
        }

        //1154. Day of the Year
        //https://leetcode.com/problems/day-of-the-year/
        public int DayOfYear(string date)
        {
            var d = DateTime.Parse(date);
            return (d - new DateTime(d.Year, 1, 1)).Days + 1;
        }

        //202. Happy Number
        //https://leetcode.com/problems/happy-number/
        public bool IsHappy(int n)
        {
            var intCache = new List<int>();
            while (true)
            {
                if (intCache.Contains(n)) return false;
                intCache.Add(n);
                var digits = n.ToString().Select(x => int.Parse(x.ToString())).ToList();
                var sum = digits.Select(x => x * x).Sum();
                if (sum == 1) return true;
                n = sum;
            }

        }

        //https://leetcode.com/problems/duplicate-zeros/
        //1089. Duplicate Zeros
        public static void DuplicateZeros(int[] arr)
        {
            int n = arr.Length;
            for (var i = 0; i < n - 1; i++)
            {
                if (arr[i] == 0)
                {
                    for (int j = n - 1; j > i + 1; j--)
                    {
                        arr[j] = arr[j - 1];
                    }

                    arr[i + 1] = 0;
                    i++;
                }
            }
        }


        //https://leetcode.com/problems/second-largest-digit-in-a-string/
        //1796. Second Largest Digit in a String
        public int SecondHighest(string s)
        {
            var cnt = s.Count(char.IsDigit);
            if (cnt == 0) return -1;

            var digits = s.Where(char.IsDigit).Select(x => int.Parse(x.ToString())).Distinct().OrderBy(x => x).ToList();
            if (digits.Max() == digits.Min()) return -1;
            return digits[digits.Count - 2];
        }

        //628. Maximum Product of Three Numbers
        //https://leetcode.com/problems/maximum-product-of-three-numbers/
        public int MaximumProduct(int[] nums)
        {
            var sorted = nums.OrderBy(x => x).ToList();
            var max1 = sorted.Skip(sorted.Count - 3).Aggregate((total, next) => total * next);
            var max2 = sorted[0] * sorted[1] * sorted[sorted.Count - 1];
            return Math.Max(max1, max2);
        }


        //645. Set Mismatch
        //https://leetcode.com/problems/set-mismatch/
        public static int[] FindErrorNums(int[] nums)
        {
            var d = new Dictionary<int, int>();
            foreach (var t in nums)
            {
                if (!d.ContainsKey(t)) d[t] = 1;
                else d[t]++;
            }

            var keys = d.Select(x => x.Key).Distinct().OrderBy(x => x).ToList();
            for (int i = 0; i < keys.Count; i++)
            {
                if (keys[i] != i + 1) return new[] { d.Single(x => x.Value == 2).Key, i + 1 };
            }

            return new[] { d.Single(x => x.Value == 2).Key, nums.Length };
        }

        //551. Student Attendance Record I
        //https://leetcode.com/problems/student-attendance-record-i/
        public static bool CheckRecord(string s)
        {
            if (s.Count(x => x == 'A') >= 2) return false;

            int cnt = 0;
            for (int i = 0; i < s.Length; i++)
            {
                if (s[i] == 'L') cnt++; else cnt = 0;
                if (cnt >= 3) return false;
            }
            return true;
        }

        //459. Repeated Substring Pattern
        //https://leetcode.com/problems/repeated-substring-pattern/
        public static bool RepeatedSubstringPattern(string s)
        {
            var n = s.Length;
            for (int i = 1; i < s.Length / 2 + 1; i++)
            {
                if (n % i != 0) continue;
                var pattern = s.Substring(0, i);
                if (!s.Split(new string[] { pattern }, StringSplitOptions.RemoveEmptyEntries).Any()) return true;
            }

            return false;
        }

        //747. Largest Number At Least Twice of Others
        //https://leetcode.com/problems/largest-number-at-least-twice-of-others/
        public static int DominantIndex(int[] nums)
        {
            if (nums.Length == 1) return 0;
            var max = nums.Max();
            var numsToCheck = nums.Where(x => x > 0 && x != max).ToList();
            if (!numsToCheck.Any()) return nums.ToList().IndexOf(max);
            return numsToCheck.All(x => (double)max / x >= 2) ? nums.ToList().IndexOf(max) : -1;

        }


        //1668. https://leetcode.com/problems/maximum-repeating-substring/
        public static int MaxRepeating(string sequence, string word)
        {
            int max = sequence.Length / word.Length;

            for (int i = max; i > 0; i--)
            {
                var concat = string.Concat(Enumerable.Repeat(word, i));
                if (sequence.Contains(concat)) return i;
            }

            return 0;
        }

        //125. https://leetcode.com/problems/valid-palindrome/
        public static bool IsPalindrome(string s)
        {
            var onlyLetters = new string(s.Where(char.IsLetterOrDigit).ToArray());
            onlyLetters = onlyLetters.ToLowerInvariant();
            var n = onlyLetters.Length;

            var s1 = onlyLetters.Substring(0, (n % 2 == 0) ? n / 2 : (n - 1) / 2);
            var s2 = onlyLetters.Substring((n % 2 == 0) ? n / 2 : (n + 1) / 2, (n % 2 == 0) ? n / 2 : (n - 1) / 2);
            s2 = new string(s2.Reverse().ToArray());
            return s1 == s2;
        }

        // 434. https://leetcode.com/problems/number-of-segments-in-a-string/
        public static int CountSegments(string s)
        {
            if (string.IsNullOrEmpty(s)) return 0;
            return s.Split(new[] { " " }, StringSplitOptions.None).Count(x => x.Length > 0);
        }

        // 482. https://leetcode.com/problems/license-key-formatting/
        public static string LicenseKeyFormatting(string s, int k)
        {
            s = s.Replace("-", "");
            s = s.ToUpperInvariant();

            var n = s.Length;
            string res = "";
            while (n > k)
            {
                res = s.Substring(n - k, k) + (res.Length > 0 ? "-" : "") + res;
                n -= k;
            }

            if (n > 0) res = s.Substring(0, n) + (res.Length > 0 ? "-" : "") + res;
            return res;
        }

        //#263
        public static bool IsUgly(int n)
        {
            if (n == 1 || n == 0 || n == -1) return false;

            int[] primes = new[] { 2, 3, 5 };

            for (int i = 0; i < primes.Length; i++)
            {
                int prime = primes[i];
                while (n % prime == 0)
                {
                    n /= prime;
                }
            }

            return n == 1 || n == -1;
        }

        public static int ArrangeCoins(int n)
        {
            if (n == 1) return 1;

            long a = 0;
            long b = n;
            while (a <= b)
            {
                long mid = a + (b - a) / 2;
                long sum = (1 + mid) * mid / 2;

                if (n == sum) return (int)mid;

                if (n > sum) a = mid; else b = mid;
                if ((b - a) == 1) return (int)a;
            }

            return 0;
        }

        public static bool IsPowerOfFour(int n)
        {
            if (n < 1) return false;
            while (n % 4 == 0)
            {
                n /= 4;
            }

            return n == 1;
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

        public static int MajorityElement(int[] nums)
        {
            var n = nums.Length;
            if (n == 1) return nums[0];


            var d = new Dictionary<int, int>();
            for (int i = 0; i < n; i++)
            {
                if (!d.ContainsKey(nums[i])) d.Add(nums[i], 1); else d[nums[i]]++;
            }

            return d.Single(x => x.Value == d.Max(y => y.Value)).Key;
        }

        public static void MoveZeroes(int[] nums)
        {
            for (int i = 0; i < nums.Length; i++)
            {
                var v = nums[i];
                var cnt = 0;
                if (v == 0)
                {
                    for (int j = i; j < nums.Length; j++)
                    {
                        if (nums[j] == 0) cnt++;
                        else break;
                    }

                    for (int j = i; j < nums.Length - cnt; j++)
                    {
                        nums[j] = nums[j + cnt];
                    }

                    for (int j = nums.Length - cnt; j < nums.Length; j++) nums[j] = 0;
                    cnt = 0;
                }
            }
        }

        public static IList<string> CommonChars(string[] A)
        {
            var uniqueChars = new List<char>();
            foreach (var s in A)
            {
                uniqueChars.AddRange(s);
            }
            uniqueChars = uniqueChars.Distinct().ToList();

            var res = new List<string>();
            foreach (var c in uniqueChars)
            {
                if (!A.All(x => x.Contains(c))) continue;

                var min = A.Min(x => x.Count(z => z == c));
                for (var i = 0; i < min; i++) res.Add(c.ToString());

            }

            return res;
        }

        public static int SingleNumber(int[] nums)
        {
            var d = new Dictionary<int, int>();
            for (int i = 0; i < nums.Length; i++)
            {
                var integer = nums[i];
                if (!d.ContainsKey(integer))
                {
                    d.Add(integer, 0);
                }
                else
                {
                    d[integer]++;
                }
            }

            return d.Single(x => x.Value == 0).Key;
        }

        public static int CountLargestGroup(int n)
        {
            var grouped = Enumerable.Range(1, n).Select(x => x.ToString().Sum(c => c - '0')).GroupBy(x => x)
                .Select(x => x.Count()).ToList();

            var max = grouped.Max();

            return grouped.Count(x => x == max);

        }

        public static int TotalMoney(int n)
        {
            var parts = n / 7;
            return Enumerable.Range(1, parts).Select((x, i) => (2 * (i + 1) + 6) * 7 / 2).Sum() + Enumerable.Range(parts + 1, n % 7).Sum();
        }

        public static string[] FindWords(string[] words)
        {
            var rows = new string[] { "qwertyuio", "asdfghjkl", "zxcvbnm" };
            var res = new List<string>();
            for (int i = 0; i < words.Length; i++)
            {
                var word = words[i];
                var test = rows.Select(x => x.ToList().Intersect(word.ToList()).Count()).ToList();
                if (test.Count(x => x > 0) == 1) res.Add(word);
            }

            return res.ToArray();
        }

        public static string ReformatNumber(string number)
        {
            var s = number.Replace(" ", string.Empty).Replace("-", string.Empty);
            if (s.Length > 4)
                return s.Substring(0, 3) + "-" + ReformatNumber(s.Substring(3, s.Length - 3));

            if (s.Length == 4) return s.Substring(0, 2) + "-" + s.Substring(2, 2);
            return s;
        }


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
