using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.ObjectData;
using ZensTweakstest.Helper;
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
        public override bool PreDraw(int i, int j, SpriteBatch spriteBatch)
        {
            Tile tile = Framing.GetTileSafely(i, j);
            if (tile.frameX == 18 && tile.frameY == 0) 
            {
                Main.spriteBatch.End();
                spriteBatch.Begin(SpriteSortMode.Deferred, BlendState.Additive, null, null, null, null, Main.GameViewMatrix.EffectMatrix);
                Color color = Helplul.CycleColor(Color.LightSkyBlue, Color.DarkBlue);
                Vector2 zero = new Vector2(Main.offScreenRange, Main.offScreenRange);
                Vector2 OffsetShrine = new Vector2(-4, -16);
                Main.spriteBatch.Draw(ModContent.GetTexture("ZensTweakstest/Helper/NotMyBalls"), new Vector2(i * 16 - (int)Main.screenPosition.X, j * 16 - (int)Main.screenPosition.Y) + zero + OffsetShrine, color);
                Main.spriteBatch.End();
                spriteBatch.Begin(SpriteSortMode.Deferred, null, null, null, null, null, Main.GameViewMatrix.EffectMatrix);

            }
            return true;
        }
        public override void DrawEffects(int i, int j, SpriteBatch spriteBatch, ref Color drawColor, ref int nextSpecialDrawIndex)
        {
            Tile tile = Framing.GetTileSafely(i, j);
            if (tile.frameX == 18 && tile.frameY == 0)
            {
                if (Main.rand.NextFloat() < 0.244186f)
                {
                    Dust dust;
                    Color color = Helplul.CycleColor(Color.LightSkyBlue, Color.DarkBlue);
                    // You need to set position depending on what you are doing. You may need to subtract width/2 and height/2 as well to center the spawn rectangle.
                    Vector2 OffsetShrine = new Vector2(-4, -16);
                    Vector2 position = new Vector2(i * 16, j * 16) + OffsetShrine;
                    dust = Terraria.Dust.NewDustDirect(position, 24, 24, 91, 0f, -1.627907f, 0, new Color(255, 255, 255), 0.7f);//43
                    dust.noGravity = true;
                }
            }
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
