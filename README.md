Maybe.cs
==

- 返り値のない処理の適用
  - Nothingを返す？
    - `Nothing`と`Maybe ()`は異なる
  - `Select`と`For`にする？
    - `FlatMap`は`SelectMany`？
  - 写像の文脈で命名できそう？
    - 死にそう
- IEnumerableとの適合
  - SelectやWhere
  - AsEnumerable()
    - `IEnumerable<Maybe<T>> -> IEnumerable<T>`
    - これまた名前が分からん
- Errorable型
- 単体テストの用意