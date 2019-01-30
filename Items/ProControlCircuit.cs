using System.Linq;
using MechTransfer.Common;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using MechTransfer.Tiles;
using MechTransfer.Tiles.TileInterface;

namespace MechTransfer.Items
{
    public class ProControlCircuit : ModItem
    {
        static ProControlCircuit()
        {
            AllUpgradableTileName = System.Reflection.Assembly.GetExecutingAssembly().GetTypes()
                .Where(t => t.GetInterfaces().Contains(typeof(IProUpgradable)))
                .Select(t => t.Name).ToArray();
        }
        public ProControlCircuit() : base()
        {
            if (AllUpgradableTileTypeID is null)
                AllUpgradableTileTypeID = AllUpgradableTileName.Select(n => mod.TileType(n)).ToArray();
        }
        private static string[] AllUpgradableTileName;
        private static int[] AllUpgradableTileTypeID;
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Pro Control Circuit");
            Tooltip.SetDefault("Used on the transfer device to upgrade it\n" +
                "Each device can insert up to 3 Pro Control Circuits\n" + "" +
                "You need own a \"Grand Design\" in the inventory then can craft it.");
        }
        public override void SetDefaults()
        {
            item.width = 16;
            item.height = 16;
            item.useTime = 20;
            item.useAnimation = 20;
            item.maxStack = 999;
            item.useStyle = 1;
            item.rare = 1;
        }

        public override void AddRecipes()
        {
            CatalystModRecipe rSliver = new CatalystModRecipe(mod);
            rSliver.AddIngredient(ItemID.SilverBar, 10);
            rSliver.AddIngredient(ItemID.Wire, 30);
            rSliver.SetCatalyst(ItemID.WireKite);
            rSliver.SetResult(item.type, 3);
            rSliver.AddTile(TileID.WorkBenches);
            rSliver.AddRecipe();
            CatalystModRecipe rGold = new CatalystModRecipe(mod);
            rGold.AddIngredient(ItemID.GoldBar, 10);
            rGold.AddIngredient(ItemID.Wire, 40);
            rGold.SetCatalyst(ItemID.WireKite);
            rGold.SetResult(item.type, 4);
            rGold.AddTile(TileID.WorkBenches);
            rGold.AddRecipe();
            CatalystModRecipe rCopperd = new CatalystModRecipe(mod);
            rCopperd.AddIngredient(ItemID.CopperBar, 10);
            rCopperd.AddIngredient(ItemID.Wire, 20);
            rCopperd.SetCatalyst(ItemID.WireKite);
            rCopperd.SetResult(item.type, 2);
            rCopperd.AddTile(TileID.WorkBenches);
            rCopperd.AddRecipe();
        }
        public override bool UseItem(Player player)
        {
            item.stack--;
            return true;
        }
        public override bool CanUseItem(Player player)
        {
            var targetTile = Main.tile[Player.tileTargetX, Player.tileTargetY];
            return AllUpgradableTileTypeID.Contains(targetTile.type);
        }
    }
}
