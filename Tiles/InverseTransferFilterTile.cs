﻿using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria;
using Terraria.DataStructures;
using Terraria.Enums;
using Terraria.ObjectData;

namespace MechTransfer.Tiles
{
    class InverseTransferFilterTile : FilterableTile, ITransferPassthrough
    {
        public override void SetDefaults()
        {
            Main.tileFrameImportant[Type] = true;
            Main.tileNoFail[Type] = true;
            dustType = 1;

            TileObjectData.newTile.CopyFrom(TileObjectData.Style1x1);
            TileObjectData.newTile.HookPostPlaceMyPlayer = new PlacementHook(mod.GetTileEntity<TransferFilterTileEntity>().Hook_AfterPlacement, -1, 0, false);
            TileObjectData.newTile.LavaDeath = false;
            TileObjectData.newTile.AnchorBottom = new AnchorData(AnchorType.None, 0, 0);
            TileObjectData.addTile(Type);

            drop = mod.ItemType("InverseTransferFilterItem");
            AddMapEntry(new Color(200, 200, 200));

            hoverText = "Item excluded:";

            ((MechTransfer)mod).transferAgent.passthroughs.Add(Type, this);
        }

        public override void KillTile(int i, int j, ref bool fail, ref bool effectOnly, ref bool noItem)
        {
            mod.GetTileEntity<TransferFilterTileEntity>().Kill(i, j);
        }

        public bool ShouldPassthrough(TransferUtils agent, Point16 location, Item item)
        {
            int id = mod.GetTileEntity<TransferFilterTileEntity>().Find(location.X, location.Y);
            if (id == -1)
                return false;
            TransferFilterTileEntity entity = (TransferFilterTileEntity)TileEntity.ByID[id];

            return entity.ItemId != item.netID;
        }
    }
}