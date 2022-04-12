using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.ObjectData;
using static Terraria.ModLoader.ModContent;

namespace ZensTweakstest.Items.CryoDepths
{
    public class EndothermicAlter : ModTile
    {
        public override void SetDefaults()
        {
            Main.tileFrameImportant[Type] = true;
            Main.tileNoAttach[Type] = true;
            minPick = 90;
            Main.tileLavaDeath[Type] = false;
            disableSmartCursor = true;
            TileObjectData.newTile.CopyFrom(TileObjectData.Style3x2);
            TileObjectData.addTile(Type);
            ModTranslation name = CreateMapEntryName();
            name.SetDefault("Alter");
            AddMapEntry(new Color(0, 174, 255), name);
        }
        public override bool CanKillTile(int i, int j, ref bool blockDamaged)
        {
            return true;
        }
        public override void KillMultiTile(int i, int j, int frameX, int frameY)
        {
            Item.NewItem(i * 16, j * 16, 32, 48, ItemType<EndothermicAlterItem>());
        }
    }
    public class EndothermicAlterItem : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Endothermic Alter");
        }

        public override void SetDefaults()
        {
            item.width = 46;
            item.height = 32;
            item.maxStack = 99;
            item.consumable = true;
            item.useStyle = 1;
            item.value = Item.buyPrice(silver: 30);
            item.value = Item.sellPrice(silver: 20);
            item.rare = ItemRarityID.Orange;
            item.useTime = 6;
            item.useAnimation = 10;
            item.autoReuse = true;
            item.createTile = ModContent.TileType<EndothermicAlter>();
        }
    }
}
