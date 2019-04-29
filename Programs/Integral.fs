let rec sum ab 
    if a>b then 0
    else a+sum(a+1) b

let rec sum a b  f=
    if a>b then 0.
    else (f a )+ sum(a+1) b f

let integrate f a b =
    let n=100
    let h=(b-a)/float(n)
    sum 0 n (fun i ->
        h*f(a+h*float(i)))

let rec iter f i a b =
    if a>b then if
    else f a (iter f i (a+1))

let sum a b = iter(+) 0 a b 
let fact n =iter(*) 1 1 n
let power x n=
    iter (fun_ acc->acc*x) 1. 1 n  

let integrate f a b =
    let n=100
    let h=(b-a)/float(n)
    iter (fun i acc-> acc+h*f(a+float(i)*h)) 0 . 0 n 


let myexp x = iter
    (fun n acc -> acc+(power x n )/(fact n|> float))

let fact n = iter 1 n (*) 1

let factI n =
    iter 1 n 
        (fun n acc-> (BigInteger n )* acc) 1I

let rec fact' acc n= 
    if n=1 then acc
    else fact' (acc*n) (n-1)
let fact n= fact' 1 n            