﻿// ------------------------------------------------------------------------------
// <copyright file="SimpleObjectModification.cs" company="Drake53">
// Licensed under the MIT license.
// See the LICENSE file in the project root for more information.
// </copyright>
// ------------------------------------------------------------------------------

using System.Collections.Generic;
using System.IO;

using War3Net.Build.Extensions;

namespace War3Net.Build.Object
{
    public sealed class SimpleObjectModification
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="SimpleObjectModification"/> class.
        /// </summary>
        public SimpleObjectModification()
        {
        }

        internal SimpleObjectModification(BinaryReader reader, ObjectDataFormatVersion formatVersion)
        {
            ReadFrom(reader, formatVersion);
        }

        public int OldId { get; set; }

        public int NewId { get; set; }

        public List<SimpleObjectDataModification> Modifications { get; init; } = new();

        internal void ReadFrom(BinaryReader reader, ObjectDataFormatVersion formatVersion)
        {
            OldId = reader.ReadInt32();
            NewId = reader.ReadInt32();

            nint modificationCount = reader.ReadInt32();
            for (nint i = 0; i < modificationCount; i++)
            {
                Modifications.Add(reader.ReadSimpleObjectDataModification(formatVersion));
            }
        }

        internal void WriteTo(BinaryWriter writer, ObjectDataFormatVersion formatVersion)
        {
            writer.Write(OldId);
            writer.Write(NewId);

            writer.Write(Modifications.Count);
            foreach (var modification in Modifications)
            {
                writer.Write(modification, formatVersion);
            }
        }
    }
}