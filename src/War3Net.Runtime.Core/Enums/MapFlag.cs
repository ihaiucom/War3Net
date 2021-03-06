﻿// ------------------------------------------------------------------------------
// <copyright file="MapFlag.cs" company="Drake53">
// Licensed under the MIT license.
// See the LICENSE file in the project root for more information.
// </copyright>
// ------------------------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.Linq;

using War3Net.Runtime.Core;

namespace War3Net.Runtime.Enums
{
    public sealed class MapFlag : Handle
    {
        private static readonly Dictionary<int, MapFlag> _flags = GetTypes().ToDictionary(t => (int)t, t => new MapFlag(t));

        private readonly Type _type;

        private MapFlag(Type type)
        {
            _type = type;
        }

        [Flags]
        public enum Type
        {
            FogHideTerrain = 1 << 0,
            FogMapExplored = 1 << 1,
            FogAlwaysVisible = 1 << 2,

            UseHandicaps = 1 << 3,
            Observers = 1 << 4,
            ObserversOnDeath = 1 << 5,

            FixedColors = 1 << 7,

            LockResourceTrading = 1 << 8,
            ResourceTradingAlliesOnly = 1 << 9,

            LockAllianceChanges = 1 << 10,
            AllianceChangesHidden = 1 << 11,

            Cheats = 1 << 12,
            CheatsHidden = 1 << 13,

            LockSpeed = 1 << 14,
            LockRandomSeed = 1 << 15,

            SharedAdvancedControl = 1 << 16,

            RandomHero = 1 << 17,
            RandomRaces = 1 << 18,

            Reloaded = 1 << 19,
        }

        public static implicit operator Type(MapFlag mapFlag) => mapFlag._type;

        public static explicit operator int(MapFlag mapFlag) => (int)mapFlag._type;

        public static MapFlag GetMapFlag(int i)
        {
            if (!_flags.TryGetValue(i, out var mapFlag))
            {
                mapFlag = new MapFlag((Type)i);
                _flags.Add(i, mapFlag);
            }

            return mapFlag;
        }

        private static IEnumerable<Type> GetTypes()
        {
            foreach (Type type in Enum.GetValues(typeof(Type)))
            {
                yield return type;
            }
        }
    }
}