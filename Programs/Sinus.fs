let compute a b=
    let n=100
    let h=(b-a)/(float n)
    {0..n}
    |> Seq.map(fun x->a+h*float(x))
    |> Seq.map(sin>>(*)h)
    |> Seq.sum