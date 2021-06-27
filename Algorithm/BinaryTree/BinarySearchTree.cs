using System;

namespace Algorithm
{
    /// <summary>
    /// 有序数组转二叉搜索树,LeetCode 108
    /// </summary>
    class BinarySearchTree : ITest
    {
        /// <summary>
        /// 根据数组构造高度平衡的二叉树
        /// </summary>
        /// <param name="nums"></param>
        /// <returns></returns>
        public TreeNode SortedArrayToBST(int[] nums)
        {
            return CreateBST(nums, 0, nums.Length - 1);
        }

        /// <summary>
        /// 从数组给定区间的中点构造二叉树，天然形成高度平衡的二叉树
        /// 当区间长度为奇数，左右子树高度差为0
        /// 当区间长度为偶数，左右子树高度差最多为1
        /// </summary>
        /// <param name="nums"></param>
        /// <param name="leftIndex"></param>
        /// <param name="rightIndex"></param>
        /// <returns></returns>
        public TreeNode CreateBST(int[] nums, int leftIndex, int rightIndex)
        {
            // 超过数组范围，说明给定的区间范围无效，返回null，代表没有节点
            if (rightIndex < 0 || rightIndex > nums.Length - 1 || leftIndex > rightIndex) return null;

            // 递归构造左右子树
            int nodeIndex = (leftIndex + rightIndex) / 2;
            var node = new TreeNode();
            node.val = nums[nodeIndex];
            node.left = CreateBST(nums, leftIndex, nodeIndex - 1);
            node.right = CreateBST(nums, nodeIndex + 1, rightIndex);
            return node;
        }

        public void Test()
        {
            // 可调用二叉树遍历的方法打印，还没写所以先空着
            SortedArrayToBST(new[] { -10, -3, 0, 5, 9 });
        }
    }

    /// <summary>
    /// 二叉树节点
    /// </summary>
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
    }
}
