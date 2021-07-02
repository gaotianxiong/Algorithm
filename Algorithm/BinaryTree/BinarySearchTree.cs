using System;

namespace Algorithm
{
    /// <summary>
    /// 二叉搜索树
    /// </summary>
    class BinarySearchTree : ITest
    {
        #region 有序数组转二叉搜索树,LeetCode 108

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

        #endregion

        #region 有序单链表转二叉搜索树,LeetCode 109

        /// <summary>
        /// 有序单链表转二叉搜索树
        /// </summary>
        /// <param name="head"></param>
        /// <returns></returns>
        public TreeNode SortedListToBST(ListNode head)
        {
            m_currNode = head;
            // 这里统计链表的长度
            int length = 0;
            while (m_currNode != null)
            {
                ++length;
                m_currNode = m_currNode.next;
            }
            m_currNode = head;
            return CreateBST(0, length - 1);
        }

        /// <summary>
        /// 有序链表的顺序其实就是二叉搜索树的中序遍历顺序。
        /// 这里使用中序遍历步骤，碰到原步骤中应当打印节点值的地方，替换为赋值即可。
        /// 因为树还未构造，原步骤中判断子节点为null的地方，改为判断左右区间来间接判断是否没有子节点了
        /// </summary>
        /// <param name="left"></param>
        /// <param name="right"></param>
        /// <returns></returns>
        public TreeNode CreateBST(int left, int right)
        {
            if (left > right)
            {
                return null;
            }

            int middle = (left + right) / 2;
            var node = new TreeNode();
            node.left = CreateBST(left, middle - 1);
            node.val = m_currNode.val;
            m_currNode = m_currNode.next;
            node.right = CreateBST(middle + 1, right);
            return node;
        }

        /// <summary>
        /// 当前单链表节点
        /// </summary>
        private ListNode m_currNode;

        #endregion

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

    /// <summary>
    /// 单链表节点
    /// </summary>
    public class ListNode
    {
        public int val;
        public ListNode next;
        public ListNode(int val = 0, ListNode next = null)
        {
            this.val = val;
            this.next = next;
        }
    }
}
