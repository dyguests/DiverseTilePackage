using System;
using System.Text.RegularExpressions;
using UnityEngine;
using UnityEngine.Tilemaps;

namespace Koyou
{
    [CreateAssetMenu(fileName = "DiverseTile", menuName = "2D/Tiles/DiverseTile")]
    public class DiverseTile : KoyouRuleTile
    {
        [SerializeField] private MatchType matchType;

        [Tooltip("Binary Digits")] [SerializeField]
        private string matchMaskIn = "00000000000000000000000000000001";

        [Tooltip("Binary Digits")] [SerializeField]
        private string matchMaskOut = "00000000000000000000000000000001";

        private int mMatchMaskIn;
        private int mMatchMaskOut;

        private void OnValidate()
        {
            SetDirtyInternal();
        }

        private void SetDirtyInternal()
        {
            if (!string.IsNullOrEmpty(matchMaskIn))
            {
                matchMaskIn = Regex.Replace(matchMaskIn, @"[^01]", "");
            }

            if (string.IsNullOrEmpty(matchMaskIn))
            {
                matchMaskIn = "00000000000000000000000000000001";
            }

            if (matchType == MatchType.MatchMask)
            {
                matchMaskOut = matchMaskIn;
            }
            else
            {
                if (!string.IsNullOrEmpty(matchMaskOut))
                {
                    matchMaskOut = Regex.Replace(matchMaskOut, @"[^01]", "");
                }

                if (string.IsNullOrEmpty(matchMaskOut))
                {
                    matchMaskOut = "00000000000000000000000000000001";
                }
            }

            mMatchMaskIn = int.Parse(matchMaskIn);
            mMatchMaskOut = int.Parse(matchMaskOut);
        }

        public override bool RuleMatch(int neighbor, TileBase other)
        {
            if (matchType == MatchType.Normal)
            {
                return base.RuleMatch(neighbor, other);
            }

            if (other is RuleOverrideTile) other = (other as RuleOverrideTile).m_InstanceTile;

            int otherMatchMaskOut = 0;
            if (other is DiverseTile diverseTile)
            {
                otherMatchMaskOut = diverseTile.mMatchMaskOut;
            }
            else if (other is RuleTile ruleTile)
            {
                otherMatchMaskOut = 1;
            }

            if (matchType == MatchType.MatchMask || matchType == MatchType.MatchMaskIO)
            {
                switch (neighbor)
                {
                    case TilingRuleOutput.Neighbor.This: return (otherMatchMaskOut & mMatchMaskIn) > 0;
                    case TilingRuleOutput.Neighbor.NotThis: return (otherMatchMaskOut & mMatchMaskIn) == 0;
                }
            }

            return true;
        }

        public override bool RuleMatches(TilingRule rule, Vector3Int position, ITilemap tilemap, int angle)
        {
            var tmpMap = tilemap.GetComponent<Tilemap>();
            if (tmpMap != null)
            {
                var crossTilemap = tmpMap.GetComponent<CrossTilemap>();
                if (crossTilemap != null)
                {
                    return RuleMatches(rule, position, tilemap, crossTilemap, angle);
                }
            }

            return base.RuleMatches(rule, position, tilemap, angle);
        }

        private bool RuleMatches(TilingRule rule, Vector3Int position, ITilemap tilemap, CrossTilemap crossTilemap, in int angle)
        {
            var minCount = Math.Min(rule.m_Neighbors.Count, rule.m_NeighborPositions.Count);
            for (int i = 0; i < minCount; i++)
            {
                int neighbor = rule.m_Neighbors[i];
                Vector3Int positionOffset = GetRotatedPosition(rule.m_NeighborPositions[i], angle);
                TileBase other = tilemap.GetTile(GetOffsetPosition(position, positionOffset));
                if (!RuleMatch(neighbor, other))
                {
                    var anyRelatedMatch = false;
                    foreach (var relatedCrossTilemap in crossTilemap.RelatedTilemaps)
                    {
                        var relatedTilemap = relatedCrossTilemap.Tilemap;
                        var relatedOther = relatedTilemap.GetTile(GetOffsetPosition(position, positionOffset));
                        if (RuleMatch(neighbor, relatedOther))
                        {
                            anyRelatedMatch = true;
                            break;
                        }
                    }

                    if (anyRelatedMatch) return true;


                    return false;
                }
            }

            return true;
        }

        public override bool RuleMatches(TilingRule rule, Vector3Int position, ITilemap tilemap, bool mirrorX, bool mirrorY)
        {
            var tmpMap = tilemap.GetComponent<Tilemap>();
            if (tmpMap != null)
            {
                var crossTilemap = tmpMap.GetComponent<CrossTilemap>();
                if (crossTilemap != null)
                {
                    return RuleMatches(rule, position, tilemap, crossTilemap, mirrorX, mirrorY);
                }
            }

            return base.RuleMatches(rule, position, tilemap, mirrorX, mirrorY);
        }

        private bool RuleMatches(TilingRule rule, Vector3Int position, ITilemap tilemap, CrossTilemap crossTilemap, in bool mirrorX, in bool mirrorY)
        {
            var minCount = Math.Min(rule.m_Neighbors.Count, rule.m_NeighborPositions.Count);
            for (int i = 0; i < minCount; i++)
            {
                int neighbor = rule.m_Neighbors[i];
                Vector3Int positionOffset = GetMirroredPosition(rule.m_NeighborPositions[i], mirrorX, mirrorY);
                TileBase other = tilemap.GetTile(GetOffsetPosition(position, positionOffset));
                if (!RuleMatch(neighbor, other))
                {
                    var anyRelatedMatch = false;
                    foreach (var relatedCrossTilemap in crossTilemap.RelatedTilemaps)
                    {
                        var relatedTilemap = relatedCrossTilemap.Tilemap;
                        var relatedOther = relatedTilemap.GetTile(GetOffsetPosition(position, positionOffset));
                        if (RuleMatch(neighbor, relatedOther))
                        {
                            anyRelatedMatch = true;
                            break;
                        }
                    }

                    if (anyRelatedMatch) return true;


                    return false;
                }
            }

            return true;
        }

        public enum MatchType
        {
            Normal,
            MatchMask,
            MatchMaskIO,
        }
    }
}