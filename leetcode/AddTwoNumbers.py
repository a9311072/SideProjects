'''
Hi, here's your problem today. This problem was recently asked by Microsoft:
You are given two linked-lists representing two non-negative integers. The digits are stored in reverse order and each of their nodes contain a single digit. Add the two numbers and return it as a linked list.
Example:
Input: (2 -> 4 -> 3) + (5 -> 6 -> 4)
Output: 7 -> 0 -> 8
Explanation: 342 + 465 = 807.
'''
# Definition for singly-linked list.
class ListNode(object):
    def __init__(self, x):
        self.val = x
        self.next = None

class Solution:
    def addTwoNumbers(self, l1, l2):
        carry = 0
        resultHead = ListNode(0)
        result_tmp = resultHead
        parentNode = None
        while l1 is not None or l2 is not None:
            if parentNode is not None:
                result_tmp = parentNode.next = ListNode(0)
            parentNode = result_tmp
            l1_val = l2_val = 0
            if l1 is not None:
                l1_val = l1.val
                l1 = l1.next
            if l2 is not None:
                l2_val = l2.val
                l2 = l2.next
            result_tmp.val = (l1_val + l2_val + carry) % 10
            carry = (l1_val + l2_val + carry) // 10
        if carry != 0:
            parentNode.next = ListNode(carry)
        return resultHead

import unittest

class TestAddTwoNumbers(unittest.TestCase):
    
    def toNodeHelper(self, x):
        assert x >= 0
        parent = None
        head = tmp = ListNode(0)
        while x!=0:
            if parent is not None:
                tmp = ListNode(0)
                parent.next = tmp
            tmp.val = x % 10
            x //= 10
            parent = tmp
        return head

    def toStringHelper(self, node):
        nodes_val = []
        while node:
            nodes_val.append(str(node.val))
            node = node.next
        return ''.join(nodes_val[::-1])

    def test_example_case(self):
        #243
        l1 = self.toNodeHelper(243)
        #564
        l2 = self.toNodeHelper(564)

        result = Solution().addTwoNumbers(l1, l2)
        self.assertEqual('807', self.toStringHelper(result))

    def test_length_not_eq_case(self):
        #2435
        l1 = self.toNodeHelper(2435)
        #564
        l2 = self.toNodeHelper(564)

        result = Solution().addTwoNumbers(l1, l2)
        self.assertEqual('2999', self.toStringHelper(result))

    def test_carry_case(self):
        #2435
        l1 = self.toNodeHelper(2435)
        #565
        l2 = self.toNodeHelper(565)

        result = Solution().addTwoNumbers(l1, l2)
        self.assertEqual('3000', self.toStringHelper(result))

        #9999
        l1 = self.toNodeHelper(9999)
        #9999
        l2 = self.toNodeHelper(9999)

        result = Solution().addTwoNumbers(l1, l2)
        self.assertEqual('19998', self.toStringHelper(result))

        #9999
        l1 = self.toNodeHelper(9999)
        #1
        l2 = self.toNodeHelper(1)

        result = Solution().addTwoNumbers(l1, l2)
        self.assertEqual('10000', self.toStringHelper(result))

    def test_empty_case(self):
        #0
        l1 = self.toNodeHelper(0)
        #0
        l2 = self.toNodeHelper(0)
        
        result = Solution().addTwoNumbers(None, None)
        self.assertEqual('0', self.toStringHelper(result))
        result = Solution().addTwoNumbers(l1, None)
        self.assertEqual('0', self.toStringHelper(result))
        result = Solution().addTwoNumbers(None, l2)
        self.assertEqual('0', self.toStringHelper(result))
        result = Solution().addTwoNumbers(l1, l2)
        self.assertEqual('0', self.toStringHelper(result))

if __name__ == '__main__':
    # unittest.main()

    l1 = ListNode(1)
    l1.next = ListNode(2)

    l2 = ListNode(8)
    l2.next = ListNode(7)
    l2.next.next = ListNode(8)
    result = Solution().addTwoNumbers(l1, l2)
    tmp=[]
    while result:
        tmp.append(result.val)
        result = result.next
    tmp.reverse()
    print(tmp)