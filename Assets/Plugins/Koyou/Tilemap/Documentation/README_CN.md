# 文档

## 简介

本插件创建了一个新的RuleTile，用来处理不同的RuleTile之间的交互，包括不同Tilemap中的RuleTile之间的交互。

## Api

### DiverseTile

- matchType
    - Normal
        - 普通模式。相当于RuleTile，但是还受crossTilemap影响。
        - 此时 matchMaskIn=matchMaskOut=1。
        - 普通RuleTile被视为`matchType == Normal`,matchMaskIn=matchMaskOut=1。
    - Match Mask
        - 匹配蒙层。要判定tileA是否在某方向上的tileB是否为自己的邻居，通过`tileA.matchMaskIn & tileB.matchMaskOut > 0`来判定。
        - 特别的，`matchMaskOut = matchMaskIn`
    - Match Mask IO
        - 匹配蒙层，区分IO。要判定tileA是否在某方向上的tileB是否为自己的邻居，通过`tileA.matchMaskIn & tileB.matchMaskOut > 0`来判定。
- matchMaskIn
    - 匹配蒙层的值，32位二进制数值。
    - 这是一个例子：`0110`
    - 当`matchType == MatchMask`时，`matchMaskOut = matchMaskIn`。
- matchMaskOut
    - 匹配蒙层的值，32位二进制数值。
    - 仅当`matchType == MatchMaskIO`时，才能设值。
- crossTilemap
    - 是否跨Tilemap匹配邻居。
    - 具体使用参数后文的CrossTilemap配置。

- DiverseTile与RuleTile的共有参数。

    - 此处参考RuleTile的配置。

### CrossTilemap

- RelatedTilemaps

    - 跨Tilemap匹配用的相关的Tilemap。
    - DiverseTIle勾选了crossTilemap。还需要在所在的parent中添加CrossTilemap Component，并添加好RelatedTilemaps后，跨平台匹配才能起作用。

## 用例

1. Assets/Plugins/Koyou/Tilemap/Samples/Scenes/1.DiverseTile.unity
2. Assets/Plugins/Koyou/Tilemap/Samples/Scenes/2.Cross-Tilemap.unity