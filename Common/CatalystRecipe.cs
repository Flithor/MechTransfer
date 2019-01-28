using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Terraria;
using Terraria.ModLoader;

namespace MechTransfer.Common
{
    internal class CatalystModRecipe : ModRecipe
    {
        public CatalystModRecipe(Mod mod) : base(mod) { }
        private int CatalystID { get; set; }
        private int CatalystStack { get; set; }

        public void SetCatalyst(int itemID, int stack = 1)
        {
            CatalystID = itemID;
            CatalystStack = stack;
        }
        public void SetCatalyst(Mod mod, string itemName, int stack = 1)
        {
            SetCatalyst(mod.GetItem(itemName).item.type, stack);
        }
        public void SetCatalyst(ModItem item, int stack = 1)
        {
            SetCatalyst(item.item.type, stack);
        }
        public override bool RecipeAvailable()
        {
            if (CatalystID == 0 || CatalystStack == 0) return true;
            return (Main.LocalPlayer.inventory.FirstOrDefault(i =>
            i.type == CatalystID && i.stack >= CatalystStack) != null);
        }
    }
}
