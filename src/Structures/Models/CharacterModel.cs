using SampSharp.GameMode;
using System;
using System.Collections.Generic;
using System.Text;
using WashingtonRP.Structures;

namespace WashingtonRP.Structures.Models
{
    public class CharacterModel
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public int Account { get; set; }
        public float PositionX { get; set; }
        public float PositionY { get; set; }
        public float PositionZ { get; set; }
        public float Rotation { get; set; }
        public int Interior { get; set; }
        public int VirtualWorld { get; set; }
        public float Health { get; set; }
        public float Chaleco { get; set; }
        public int Skin { get; set; }
        public int RightHand { get; set; }
        public int RightHandAmount { get; set; }
        public int LeftHand { get; set; }
        public int LeftHandAmount { get; set; }
        public int RightWrist { get; set; }
        public int RightWristAmount { get; set; }
        public int LeftWrist { get; set; }
        public int LeftWristAmount { get; set; }
        public int Crack { get; set; }
        public int Admin { get; set; }
    }
}
