class treeNode:
    def __init__(self, val):
        self.val = val
        self.left = None
        self.right = None

    def insertT(self, val):
        if val <= self.val:
            if self.left == None:
                self.left = treeNode(val)
            else:
                self.left.insertT(val)
        else:
            if self.right == None:
                self.right = treeNode(val)
            else:
                self.right.insertT(val)

    def inorderT(self, root):
        res = []
        if root:
            res = self.inorderT(root.left)
            res.append(root.val)
            res = res + self.inorderT(root.right)
        return res

    def PreorderT(self, root):
        res = []
        if root:
            res.append(root.val)
            res = res + self.PreorderT(root.left)
            res = res + self.PreorderT(root.right)
        return res
   
    def PostorderT(self, root):
        res = []
        if root:
            res = self.PostorderT(root.left)
            res = res + self.PostorderT(root.right)
            res.append(root.val)
        return res


root = treeNode(50)
root.insertT(19)
root.insertT(28)
root.insertT(88)
root.insertT(87)
root.insertT(95)
root.insertT(31)
root.insertT(22)

print('中序:' , root.inorderT(root))
print('前序:' , root.PreorderT(root))
print('後序:' , root.PostorderT(root))