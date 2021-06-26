using System;
using System.Collections.Generic;

namespace Algorithm
{
    /// <summary>
    /// KMP模式匹配算法1
    /// </summary>
    public class KMPStringMatch1 : ITest
    {
        /// <summary>
        /// 获取next数组
        /// 记录回溯的index
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        private int[] GetNext(string str)
        {
            if (str.Length == 0)
            {
                return new int[0];
            }

            // 前缀尾指针
            int left = -1;
            // 后缀尾指针
            int right = 0;
            // 记录每个元素不匹配时应当回溯的index，-1代表没有
            int[] next = new int[str.Length];
            next[0] = -1;

            while (right < str.Length - 1)
            {
                if (left == -1 || str[left] == str[right])
                {
                    ++left;
                    ++right;
                    /*
                     * 此时，left与right的左边一位必定相等，判断当前位是否相等。
                     * 如果left与right不相等，那么当right与父串不匹配时，就将跳到left位进行比较
                     * 如果left与right相等，那么当right与父串不匹配时，left自然也与父串不匹配，所以left不用再判断，再次往前跳转
                     */
                    if (str[left] == str[right])
                    {
                        next[right] = next[left];
                    }
                    else
                    {
                        next[right] = left;
                    }
                }
                else
                {
                    // left回溯
                    left = next[left];
                }
            }

            return next;
        }

        /// <summary>
        /// KMP模式匹配
        /// </summary>
        /// <param name="haystack"></param>
        /// <param name="needle"></param>
        /// <returns></returns>
        public int StrStr(string haystack, string needle)
        {
            if (String.IsNullOrEmpty(needle))
            {
                return 0;
            }
            if (String.IsNullOrEmpty(haystack))
            {
                return -1;
            }

            var nextMatchArr = GetNext(needle);
            int fatherIndex = 0;
            int childIndex = 0;
            while (fatherIndex < haystack.Length)
            {
                // 父串和字串匹配，如果相等，继续匹配后一位，如果不相等，字串匹配index回溯
                if (childIndex == -1 || haystack[fatherIndex] == needle[childIndex])
                {
                    ++fatherIndex;
                    ++childIndex;
                }
                else
                {
                    childIndex = nextMatchArr[childIndex];
                }

                if (childIndex == needle.Length)
                {
                    return fatherIndex - childIndex;
                }
            }

            return -1;
        }

        public void Test()
        {
            Console.WriteLine(StrStr("aaaaaaab", "aaaab"));
        }
    }

    /// <summary>
    /// KMP模式匹配算法0
    /// </summary>
    public class KMPStringMatch : ITest
    {
        /// <summary>
        /// 获取next数组
        /// 记录当前位的最大相等前缀的长度
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public int[] GetNext(string str)
        {
            if (str.Length == 0)
            {
                return new int[0];
            }

            // i的定义在for语句中
            int j = 0;
            int[] next = new int[str.Length];
            next[0] = j;

            for (int i = 1; i < str.Length; i++)
            {
                while (j > 0 && str[i] != str[j])
                {
                    // 回溯
                    j = next[j - 1];
                }
                if (str[i] == str[j])
                {
                    j++;
                }
                next[i] = j;
            }
            return next;
        }

        /// <summary>
        /// KMP模式匹配
        /// </summary>
        /// <param name="haystack"></param>
        /// <param name="needle"></param>
        /// <returns></returns>
        public int StrStr(string haystack, string needle)
        {
            if (String.IsNullOrEmpty(needle))
            {
                return 0;
            }

            var next = GetNext(needle);
            int j = 0;
            for (int i = 0; i < haystack.Length; i++)
            {
                while (j > 0 && haystack[i] != needle[j])
                {
                    // 发生了不匹配，j回溯
                    j = next[j - 1];
                }
                if (haystack[i] == needle[j])
                {
                    // 前后缀匹配，j往后移
                    j++;
                }
                if (j == needle.Length)
                {
                    // 模式串全部匹配上了
                    return i - j + 1;
                }
            }

            return -1;
        }

        public void Test()
        {
            Console.WriteLine(StrStr("aaaaaaab", "aaaab"));
        }
    }
}
