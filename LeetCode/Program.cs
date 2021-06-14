using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.ExceptionServices;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace LeetCode
{

    class Program
    {
        #region Tree methods

        // Definition for a binary tree node.
        public class TreeNode
        {
            public int val;
            public TreeNode left;
            public TreeNode right;

            public TreeNode(int val = 0, TreeNode left = null, TreeNode right = null)
            {
                this.val = val;
                this.left = left;
                this.right = right;
            }

            public TreeNode(int?[] data)
            {
                Load(this, data, 0);
            }
        }

        #endregion

        #region TreeNodeHelper

        public static void Load(TreeNode node, int?[] data, int index)
        {
            if (!data[index].HasValue) return;
            node.val = data[index].Value;
            if (index * 2 + 1 < data.Length)
            {
                if (data[index * 2 + 1].HasValue)
                {
                    node.left = new TreeNode();
                    Load(node.left, data, index * 2 + 1);
                }
            }

            if (index * 2 + 2 < data.Length)
            {
                if (data[index * 2 + 2].HasValue)
                {
                    node.right = new TreeNode();
                    Load(node.right, data, index * 2 + 2);
                }
            }
        }

        public static TreeNode ConvertArrayToTree(int?[] data)
        {
            if (data == null) return null;
            var node = new TreeNode();
            Load(node, data, 0);
            return node;
        }

        public static int?[] ConvertTreeToArray(TreeNode node)
        {
            if (node == null) return null;

            int index = 0;
            var nodesToCheck = new List<TreeNode>() {node};

            var toAddNodes = new List<TreeNode>() {node};

            while (toAddNodes.Any(x => x != null))
            {
                toAddNodes.Clear();
                var skip = index == 0 ? 0 : (int) (Math.Pow(2, index) - 1);
                var take = index == 0 ? 1 : (int) Math.Pow(2, index);

                var iterateNodes = nodesToCheck.Skip(skip).Take(take).ToList();

                foreach (var n in iterateNodes)
                {
                    toAddNodes.Add(n?.left);
                    toAddNodes.Add(n?.right);
                }

                if (toAddNodes.Any(x => x != null)) nodesToCheck.AddRange(toAddNodes);
                index++;
            }

            return nodesToCheck.Select(x => x?.val).ToArray();
        }


        #endregion

        static void Main(string[] args)
        {
            var a1 = new int?[] {1, 3, 2, 5};
            var a2 = new int?[] {2, 1, 3, null, 4, null, 7};

            var r1 = ConvertArrayToTree(a1);
            var r2 = ConvertArrayToTree(a2);
            var op = MergeTrees(r1, r2);

            int z = 3;
            Console.ReadLine();
        }


        #region BinaryTrees
        //https://leetcode.com/problems/diameter-of-binary-tree/
        //543. Diameter of Binary Tree
        public int DiameterOfBinaryTree(TreeNode root)
        {
            Depth(root);
            return d;
        }

        private int d = 0;

        public int Depth(TreeNode root)
        {
            if (root == null) return 0;
            var l = Depth(root.left);
            var r = Depth(root.right);

            if ((l + r) > d) d = l + r;

            return Math.Max(l, r) + 1;
        }



        public static void RecursionHelper(TreeNode node, IList<int> data)
        {
            if (node.left != null) RecursionHelper(node.left, data);
            data.Add(node.val);
            if (node.right != null) RecursionHelper(node.right, data);
        }

        //94. Binary Tree Inorder Traversal
        //https://leetcode.com/problems/binary-tree-inorder-traversal/
        public IList<int> InorderTraversal(TreeNode root)
        {
            var result = new List<int>();
            if (root == null) return result;


            RecursionHelper(root, result);
            return result;
        }

        //https://leetcode.com/problems/merge-two-binary-trees/
        //617. Merge Two Binary Trees
        public static TreeNode MergeTrees(TreeNode root1, TreeNode root2)
        {
            #region Solution 1. Out of memory exception^)))

            /*if (root1 == null) return root2;
            if (root2 == null) return root1;

            var arr1 = ConvertTreeToArray(root1);
            var arr2 = ConvertTreeToArray(root2);

            
            var maxLength = Math.Max(arr1.Length, arr2.Length);
            var res = new List<int?>();
            for (int i = 0; i < maxLength; i++)
            {
                var v1 = i < arr1.Length ? arr1[i] : null;
                var v2 = i < arr2.Length ? arr2[i] : null;
                if (v1 == null && v2 == null)
                { res.Add(null); continue; }

                res.Add((v1 ?? 0) + (v2 ?? 0));
            }

            return ConvertArrayToTree(res.ToArray());*/

            #endregion

            if (root1 == null) return root2;
            if (root2 == null) return root1;

            root1.val = root1.val + root2.val;

            root1.left = MergeTrees(root1.left, root2.left);
            root2.right = MergeTrees(root1.right, root2.right);

            return root1;
        }

        //104. Maximum Depth of Binary Tree
        //https://leetcode.com/problems/maximum-depth-of-binary-tree/
        public static int MaxDepth(TreeNode root)
        {
            if (root == null) return 0;

            if (root.left == null && root.right == null) return 1;

            return Math.Max(MaxDepth(root.left) + 1, MaxDepth(root.right) + 1);
        }

        public static TreeNode InvertTree(TreeNode root)
        {
            if (root == null) return root;

            var left = InvertTree(root.left);
            var right = InvertTree(root.right);

            root.left = right;
            root.right = left;
            return root;
        }

        #endregion


        //101. Symmetric Tree
        //https://leetcode.com/problems/symmetric-tree/
        public bool IsSymmetric(TreeNode root)
        {
            if (root == null) return false;

            return IsSymmetricHelper(root, root);
        }

        public bool IsSymmetricHelper(TreeNode r1, TreeNode r2)
        {
            if (r1 == null && r2 == null) return true;
            if (r1 == null || r2 == null) return false;

            var valEquals = r1.val == r2.val;
            var lr = IsSymmetricHelper(r1.left, r2.right);
            var rl = IsSymmetricHelper(r1.right, r2.left);

            return valEquals && rl && lr;
        }




        public IList<IList<int>> CombinationSum(int[] candidates, int target)
        {
            var res = new List<List<int>>();



            return (IList<IList<int>>) res;
        }

        public static void CombinationSumHelper(int[] candidates, int target)
        {

        }

//448. Find All Numbers Disappeared in an Array
//https://leetcode.com/problems/find-all-numbers-disappeared-in-an-array/
        public static IList<int> FindDisappearedNumbers(int[] nums)
        {
            var res = new int[nums.Length];

            for (int i = 0; i < nums.Length; i++)
            {
                var j = Math.Abs(nums[i]) - 1;
                res[j] -= 1;
            }


            return res.Select((x, i) => new {Val = x, index = i}).Where(x => x.Val == 0).Select(x => x.index + 1)
                .ToList();
        }



//https://leetcode.com/problems/number-of-good-ways-to-split-a-string/
//1525. Number of Good Ways to Split a String
        public static int NumSplits(string s)
        {
            int cnt = 0;

            var d = new Dictionary<char, int>();
            for (int i = 0; i < s.Length; i++)
            {
                if (!d.ContainsKey(s[i])) d.Add(s[i], 1);
                else d[s[i]]++;
            }

            var d2 = new Dictionary<char, int>();
            for (int i = 0; i < s.Length; i++)
            {
                if (!d2.ContainsKey(s[i])) d2.Add(s[i], 1);
                else d2[s[i]]++;
                d[s[i]]--;
                if (d[s[i]] <= 0) d.Remove(s[i]);

                if (d2.Count == d.Count) cnt++;
            }

            return cnt;
        }

//1652. Defuse the Bomb
//https://leetcode.com/problems/defuse-the-bomb/
        public static int[] Decrypt(int[] code, int k)
        {
            if (k == 0) return code.Select(x => 0).ToArray();

            int n = code.Length;
            var res = new List<int>();

            if (k < 0) Array.Reverse(code);

            int i = 0;
            while (i < n)
            {
                var cnt = Math.Abs(k);
                var sum = 0;
                while (cnt > 0)
                {
                    var index = (i + cnt) % n;
                    sum += code[index];
                    cnt--;
                }

                res.Add(sum);
                i++;
            }

            if (k < 0) res.Reverse();

            return res.ToArray();
        }

//https://leetcode.com/problems/sum-of-even-numbers-after-queries/
//985. Sum of Even Numbers After Queries
        public static int[] SumEvenAfterQueries(int[] nums, int[][] queries)
        {
            var res = new int[queries.Length];
            var sum = nums.Where(x => x % 2 == 0).Sum();

            for (int i = 0; i < queries.Length; i++)
            {
                var ind = queries[i][1];
                var val = queries[i][0];
                var oldVal = nums[ind];

                var evenBefore = nums[ind] % 2 == 0;
                nums[ind] += val;
                var evenNow = nums[ind] % 2 == 0;

                if (evenBefore && evenNow)
                {
                    sum += val;
                }

                if (!evenBefore && evenNow)
                {
                    sum += nums[ind];
                }

                if (evenBefore && !evenNow)
                {
                    sum -= oldVal;
                }

                res[i] = sum;

            }

            return res;
        }

//https://leetcode.com/problems/reformat-date/
//1507. Reformat Date
        public static string ReformatDate(string date)
        {
            var splitted = date.Split(new string[] {" "}, StringSplitOptions.None);
            var day = int.Parse(new string(splitted[0].Where(Char.IsDigit).ToArray()));
            var newDate = DateTime.Parse(day + "-" + splitted[1] + "-" + splitted[2]);
            return newDate.ToString("yyyy-MM-dd");
        }

//https://leetcode.com/problems/water-bottles/
//1518. Water Bottles
        public static int NumWaterBottles(int numBottles, int numExchange)
        {
            var res = numBottles;
            while (numBottles >= numExchange)
            {
                res += numBottles / numExchange;
                numBottles = numBottles / numExchange + (numBottles % numExchange);
            }

            return res;
        }

//https://leetcode.com/problems/angle-between-hands-of-a-clock/
//1344. Angle Between Hands of a Clock
        public static double AngleClock(int hour, int minutes)
        {
            if (hour == 12) hour = 0;

            var minuteVector = new[]
            {
                Math.Sin((double) minutes / 60 * 2 * Math.PI),
                Math.Cos((double) minutes / 60 * 2 * Math.PI)
            };

            var hourVector = new double[]
            {
                Math.Sin((hour + (double) minutes / 60) / 12 * 2 * Math.PI),
                Math.Cos((hour + (double) minutes / 60) / 12 * 2 * Math.PI),
            };

            var res = Math.Acos(minuteVector.Zip(hourVector, (x, y) => x * y).Sum()) * 180 / Math.PI;
            return res;
        }



//https://leetcode.com/problems/maximum-subarray/
//53. Maximum Subarray
        public static int MaxSubArray(int[] nums)
        {
            int maxSum = nums[0];
            int curSum = maxSum;
            for (int i = 1; i < nums.Length; i++)
            {
                curSum = Math.Max(nums[i], curSum + nums[i]);
                if (curSum > maxSum) maxSum = curSum;
            }

            return maxSum;
        }

//https://leetcode.com/problems/counting-bits/
//338. Counting Bits
        public static int[] CountBits(int n)
        {
            if (n == 0) return new int[] {0};

            var intRes = new int[n + 1];
            intRes[0] = 0;

            var bools = new bool[17];
            for (int i = 1; i <= n; i++)
            {
                for (int j = 0; j < 17; j++)
                {
                    if (bools[j] == false)
                    {
                        bools[j] = true;
                        intRes[i] = bools.Count(x => x == true);
                        break;
                    }
                    else
                    {
                        bools[j] = false;
                    }
                }
            }

            return intRes;
        }

//https://leetcode.com/problems/climbing-stairs/
//70. Climbing Stairs
        public static int ClimbStairs(int n)
        {
            if (n == 1) return 1;
            var dyn = new int[n + 1];
            dyn[1] = 1;
            dyn[2] = 2;


            for (int i = 3; i <= n; i++)
            {
                dyn[i] = dyn[i - 2] + dyn[i - 1];
            }

            return dyn[n];
        }

//https://leetcode.com/problems/check-if-it-is-a-straight-line/
//1232. Check If It Is a Straight Line
        public static bool CheckStraightLine(int[][] coordinates)
        {
            if (coordinates.Length == 2) return true;
            if (coordinates.Select(x => x[0]).All(x => x == coordinates[0][0])) return true;
            if (coordinates.Select(x => x[1]).All(x => x == coordinates[0][1])) return true;

            var pointList = coordinates.Select(x => new {X = x[0], Y = x[1]}).ToList();
            pointList = pointList.OrderBy(x => x.X).ToList();

            var slope = (decimal) (pointList.Last().Y - pointList.First().Y) /
                        (pointList.Last().X - pointList.First().X);
            var b = pointList.First().Y - slope * pointList.First().X;

            return !(from p in pointList let y = slope * p.X + b where y != p.Y select p).Any();
        }

//https://leetcode.com/problems/search-insert-position/
//35. Search Insert Position
        public static int SearchInsert(int[] nums, int target)
        {
            if (nums.Contains(target)) return nums.ToList().IndexOf(target);
            if (target > nums.Max()) return nums.Length;
            if (target < nums.Min()) return 0;

            return nums.ToList().FindIndex(x => x > target);
        }

//https://leetcode.com/problems/detect-pattern-of-length-m-repeated-k-or-more-times/
//1566. Detect Pattern of Length M Repeated K or More Times
        public static bool ContainsPattern(int[] arr, int m, int k)
        {
            var s = arr.Aggregate("", (current, i) => current + i);

            var cached = new List<string>();
            for (int i = m; i < arr.Length; i++)
            {
                var substring = arr.Skip(i - m).Take(m).Aggregate("", (c, j) => c + j);
                if (cached.Contains(substring)) continue;
                cached.Add(substring);

                if (s.IndexOf(substring) == -1) continue;


                var index = 0;
                int cnt = 0;
                var copy = s;
                while (index != -1 || copy.Length < substring.Length)
                {
                    index = copy.IndexOf(substring);
                    if (index == -1) break;
                    if (index == 0) cnt++;
                    else cnt = 0;
                    copy = index == 0 ? copy.Substring(substring.Length) : copy.Substring(index);
                    if (cnt >= k) return true;
                }
            }

            return false;
        }

//326. Power of Three
//https://leetcode.com/problems/power-of-three/
        public static bool IsPowerOfThree(int n)
        {
            if (n == 1) return true;
            if (n < 3) return false;

            while (n % 3 == 0)
            {
                n /= 3;
            }

            return n == 1;
        }

//367. Valid Perfect Square
//https://leetcode.com/problems/valid-perfect-square/
        public static bool IsPerfectSquare(int num)
        {
            if (num == 1) return true;

            long left = 1;
            long right = num;

            while (left <= right)
            {
                long mid = left + (right - left) / 2;
                var sq = mid * mid;
                if (num == sq) return true;

                if (num < sq) right = mid - 1;
                else left = mid + 1;
            }

            return false;
        }

//https://leetcode.com/problems/plus-one/
//66. Plus One
        public static int[] PlusOne(int[] digits)
        {
            var n = digits.Length;
            if (n == 1 && digits[0] == 9) return new int[] {1, 0};
            if (digits[n - 1] < 9)
            {
                digits[n - 1]++;
                return digits;
            }

            digits[n - 1] = 0;
            return PlusOne(digits.Take(n - 1).ToArray()).Concat(new int[] {0}).ToArray();
        }

//https://leetcode.com/problems/check-if-binary-string-has-at-most-one-segment-of-ones/
//1784. Check if Binary String Has at Most One Segment of Ones
        public static bool CheckOnesSegment(string s)
        {
            return s.Split(new[] {"0"}, StringSplitOptions.RemoveEmptyEntries).Count() == 1;
        }

//https://leetcode.com/problems/word-pattern/
//290. Word Pattern
        public static bool WordPattern(string pattern, string s)
        {
            var d = new Dictionary<char, string>();
            var words = s.Split(new[] {" "}, StringSplitOptions.RemoveEmptyEntries);
            if (words.Length < pattern.Length) return false;

            for (int i = 0; i < pattern.Length; i++)
            {
                if (!d.ContainsKey(pattern[i]))
                {
                    if (d.Any(x => x.Value == words[i])) return false;
                    d.Add(pattern[i], words[i]);
                }

                if (d[pattern[i]] != words[i]) return false;
            }

            return true;
        }


//https://leetcode.com/problems/length-of-last-word/
//58. Length of Last Word
        public static int LengthOfLastWord(string s)
        {
            var words = s.Split(new string[] {" "}, StringSplitOptions.RemoveEmptyEntries).Where(x => x.Length >= 1)
                .ToList();
            if (words.Count == 0)
            {
                s = s.Replace(" ", "");
                return s.Length;
            }

            return words.Last().Length;
        }

//https://leetcode.com/problems/long-pressed-name/
//925. Long Pressed Name
        public static bool IsLongPressedName(string name, string typed)
        {
            if (name == typed) return true;
            if (typed.Length < name.Length) return false;
            if (typed[0] != name[0]) return false;

            name += "~";
            var nameDict = new Dictionary<int, string>();
            int cnt = 1;
            int index = 0;
            for (int i = 1; i < name.Length; i++)
            {
                if (name[i] == name[i - 1])
                {
                    cnt++;
                    continue;
                }

                nameDict.Add(index, name.Substring(i - cnt, cnt));
                cnt = 1;
                index++;
            }

            typed += "~";
            var typedDict = new Dictionary<int, string>();
            cnt = 1;
            index = 0;
            for (int i = 1; i < typed.Length; i++)
            {
                if (typed[i] == typed[i - 1])
                {
                    cnt++;
                    continue;
                }

                typedDict.Add(index, typed.Substring(i - cnt, cnt));
                cnt = 1;
                index++;
            }

            if (nameDict.Count != typedDict.Count) return false;

            for (int i = 0; i < nameDict.Count; i++)
            {
                if (nameDict[i].Length > typedDict[i].Length) return false;
                if (nameDict[i][0] != typedDict[i][0]) return false;
            }

            return true;
        }

//507. Perfect Number
//https://leetcode.com/problems/perfect-number/
        public static bool CheckPerfectNumber(int num)
        {
            if (num == 1) return true;

            var sum = 1;
            int j = 2;
            while (j <= num / 2)
            {
                if (num % j == 0)
                {
                    sum += j;
                }

                j++;
            }

            return num == sum;
        }

//69. Sqrt(x)
//https://leetcode.com/problems/sqrtx/
        public static int MySqrt(int x)
        {
            if (x == 1) return 1;
            long left = 1;
            long right = int.MaxValue;
            long res = -1;
            while (left <= right)
            {
                long c = left + (right - left) / 2;
                if (c * c <= x)
                {
                    res = c;
                    left = c + 1;
                }
                else
                {
                    right = c - 1;
                }
            }

            return (int) res;
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
            if (time[4] == '?') res += "9";
            else res += time[4];
            if (time[3] == '?') res = "5" + res;
            else res = time[3] + res;
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

            var indexes = zipped.Select((x, i) => new {i, x}).Where(t => !t.x).Select(t => t.i).ToList();
            var chars1 = (new char[] {a[indexes[0]], a[indexes[1]]}).OrderBy(x => x).ToArray();
            var chars2 = (new char[] {b[indexes[0]], b[indexes[1]]}).OrderBy(x => x).ToArray();
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
                if (dif == minDif) res.Add(new List<int>() {arr[i], arr[i + 1]});
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
                if (keys[i] != i + 1) return new[] {d.Single(x => x.Value == 2).Key, i + 1};
            }

            return new[] {d.Single(x => x.Value == 2).Key, nums.Length};
        }

        //551. Student Attendance Record I
        //https://leetcode.com/problems/student-attendance-record-i/
        public static bool CheckRecord(string s)
        {
            if (s.Count(x => x == 'A') >= 2) return false;

            int cnt = 0;
            for (int i = 0; i < s.Length; i++)
            {
                if (s[i] == 'L') cnt++;
                else cnt = 0;
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
                if (!s.Split(new string[] {pattern}, StringSplitOptions.RemoveEmptyEntries).Any()) return true;
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
            return numsToCheck.All(x => (double) max / x >= 2) ? nums.ToList().IndexOf(max) : -1;

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
            return s.Split(new[] {" "}, StringSplitOptions.None).Count(x => x.Length > 0);
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

            int[] primes = new[] {2, 3, 5};

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

                if (n == sum) return (int) mid;

                if (n > sum) a = mid;
                else b = mid;
                if ((b - a) == 1) return (int) a;
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
                if (!d.ContainsKey(nums[i])) d.Add(nums[i], 1);
                else d[nums[i]]++;
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
            return Enumerable.Range(1, parts).Select((x, i) => (2 * (i + 1) + 6) * 7 / 2).Sum() +
                   Enumerable.Range(parts + 1, n % 7).Sum();
        }

        public static string[] FindWords(string[] words)
        {
            var rows = new string[] {"qwertyuio", "asdfghjkl", "zxcvbnm"};
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
                        return new[] {i, j};
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
