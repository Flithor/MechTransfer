using System;
namespace MechTransfer.Tiles.TileInterface
{
    internal interface IProUpgradable
    {
        byte CircuitCount { get; set; }
        byte CircuitMax { get; set; }
        void ProUpgraded();
        void ExtraBehavior();
    }
}
