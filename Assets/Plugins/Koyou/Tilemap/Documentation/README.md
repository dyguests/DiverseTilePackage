# Documentation

中文文档看这里：[README_CN.md](README_CN.md)

## Introduction

This plugin creates a new RuleTile to handle the interaction between different RuleTiles, including the interaction between RuleTiles in different Tilemaps.

## APIs

### DiverseTile

- matchType
    - Normal
        - Normal mode. Equivalent to RuleTile, but also affected by crossTilemap.
        - In this case matchMaskIn=matchMaskOut=1.
        - Normal RuleTile is treated as `matchType == Normal`, matchMaskIn=matchMaskOut=1.
          -Match Mask
        - Matching masks. To determine whether tileB in a certain direction is its own neighbor, use `tileA.matchMaskIn & tileB.matchMaskOut > 0` to determine.
        - In particular, `matchMaskOut = matchMaskIn`
          -Match Mask IO
        - Match mask to distinguish IO. To determine whether tileB in a certain direction is its own neighbor, use `tileA.matchMaskIn & tileB.matchMaskOut > 0` to determine.
- matchMaskIn
    - The value of the match mask, a 32-bit binary value.
    - Here is an example: `0110`
    - `matchMaskOut = matchMaskIn` when `matchType == MatchMask`.
- matchMaskOut
    - The value of the match mask, a 32-bit binary value.
    - Can only be set if `matchType == MatchMaskIO`.
- crossTilemap
    - Whether to match neighbors across Tilemaps.
    - Use the CrossTilemap configuration in the following parameters.

- Common parameters of DiverseTile and RuleTile.

    - Refer to the configuration of RuleTile here.

### CrossTilemap

- RelatedTilemaps

    - Related Tilemaps for matching across Tilemaps.
    - DiverseTIle has crossTilemap checked. You also need to add the CrossTilemap Component in the parent where it is located, and add RelatedTilemaps, and then cross-platform matching can work.

## Example

1. Assets/Plugins/Koyou/Tilemap/Samples/Scenes/1.DiverseTile.unity
2. Assets/Plugins/Koyou/Tilemap/Samples/Scenes/2.Cross-Tilemap.unity