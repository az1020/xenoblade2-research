﻿using System;

namespace Xb2.Bdat
{
    public class BdatMember
    {
        public string Name { get; }
        public BdatMemberType Type { get; }
        public BdatValueType ValType { get; }
        public int MemberPos { get; }
        public int ArrayCount { get; }
        public int FlagVarOffset { get; }
        public int FlagIndex { get; }
        public uint FlagMask { get; }
        public int FlagVarIndex { get; }

        public BdatMember(byte[] file, int tableOffset, int offset)
        {
            int infoOffset = tableOffset + BitConverter.ToUInt16(file, offset);
            int nameOffset = tableOffset + BitConverter.ToUInt16(file, offset + 4);

            Name = Stuff.GetUTF8Z(file, nameOffset);
            Type = (BdatMemberType)file[infoOffset];

            if (Type == BdatMemberType.Flag)
            {
                int memberTableOffset = BitConverter.ToUInt16(file, tableOffset + 32);
                FlagIndex = file[infoOffset + 1];
                FlagMask = BitConverter.ToUInt32(file, infoOffset + 2);
                FlagVarOffset = BitConverter.ToUInt16(file, infoOffset + 6);
                FlagVarIndex = (FlagVarOffset - memberTableOffset) / 6;
            }
            else
            {
                ValType = (BdatValueType)file[infoOffset + 1];
                MemberPos = BitConverter.ToUInt16(file, infoOffset + 2);
            }
            if (Type == BdatMemberType.Array)
            {
                ArrayCount = BitConverter.ToUInt16(file, infoOffset + 4);
            }
        }
    }
}
