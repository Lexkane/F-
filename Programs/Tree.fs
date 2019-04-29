type TreeNode=
    |Empty
    |TreeNode of
    (int * TreeNode*TreeNode)
let rec reverse=function
    | Empty ->Empty
    | TreeNode(value,left,right) ->TreeNode(value,reverse right,reverse left)


type Tree<'a>=
    | Empty
    | Node of 'a*
    Tree <'a>* Tree <'a>
let rec reverse=
function
    | Empty->Empty
    | Node(value,left,right)->Node(value,reverse right,reverse left)
                    