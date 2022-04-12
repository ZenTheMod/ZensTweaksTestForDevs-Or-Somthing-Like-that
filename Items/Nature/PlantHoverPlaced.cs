using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;
using Microsoft.Xna.Framework;
using Terraria.ObjectData;
using Microsoft.Xna.Framework.Graphics;
using ZensTweakstest.Items.NewNonZen.Erichus.Boss;
using Terraria.Graphics.Shaders;
using ZensTweakstest.Items.NewZenStuff.Bosses;
using ZensTweakstest.Items.Dusts;
using System.IO;

namespace ZensTweakstest.Items.Nature
{
    public class PlantHoverPlaced : ModTile
    {
        //private static readonly string NatureItemsDirectory = Path.Combine("Items", "Nature");
        private float Rotation = 0f;
        public override void SetDefaults()
        {
            Main.tileFrameImportant[Type] = true;
            Main.tileLighted[Type] = true;
            Main.tileLavaDeath[Type] = true;
            TileID.Sets.FramesOnKillWall[Type] = true; // Necessary since Style3x3Wall uses AnchorWall
            TileObjectData.newTile.CopyFrom(TileObjectData.Style2x2);
            TileObjectData.newTile.Width = 2;
            TileObjectData.newTile.Height = 4;
            TileObjectData.newTile.CoordinateHeights = new int[] { 16, 16, 16, 18 };
            TileObjectData.newTile.AnchorValidTiles = new int[]
            {
                TileID.Grass,
                TileID.HallowedGrass,
                TileID.JungleGrass
            };
            TileObjectData.addTile(Type);
            dustType = DustID.Grass;
            AddMapEntry(new Color(15, 168, 18));
        }
        public override bool PreDraw(int i, int j, SpriteBatch spriteBatch)
        {
            Tile tile = Framing.GetTileSafely(i, j);
            Vector2 zero = new Vector2(Main.offScreenRange, Main.offScreenRange);
            float HoverSin = (float)Math.Sin(Main.GameUpdateCount / 20f);
            Vector2 offSet = new Vector2(-2, -6);
            if (tile.frameX == 0 && tile.frameY == 0)
            {
                spriteBatch.Draw(mod.GetTexture("Items/Nature/HoverPlantE"), new Vector2(i * 16 - (int)Main.screenPosition.X, j * 16 - (int)Main.screenPosition.Y) + zero + new Vector2(0, HoverSin * 2) + offSet, null, Lighting.GetColor(i, j), 0f, Vector2.Zero, 1f, SpriteEffects.None, 0f);
            }
            return true;
        }
        public override void PostDraw(int i, int j, SpriteBatch spriteBatch)
        {
            // So basically this Rotation Line rotates the draw slowly.
            Rotation += 0.001f;
            // this gets the tile.
            Tile tile = Framing.GetTileSafely(i, j);
            // this one is to set it so it doesnt go weird when off screen.
            Vector2 zero = new Vector2(Main.offScreenRange, Main.offScreenRange);
            // this is a basic sin wave used for a lot of stuff.
            float ScaleSin = (float)Math.Sin(Main.GameUpdateCount / 20f);
            // post draw only renders above the tile it spawns at.
            // so this checks the framing of the tile at the given courds.
            if (tile.frameX == 18 && tile.frameY == 54)
            {// one is before the other for layering perposes.
                // spriteBatch.Draw(mod.GetTexture("Items/Nature/BloomPla"), new Vector2(i * 16 - (int)Main.screenPosition.X, j * 16 - (int)Main.screenPosition.Y) + zero - new Vector2(0, 38), null, Color.White, Rotation, new Vector2(15, 15), 1.3f + ScaleSin / 3f, SpriteEffects.None, 0f);
                Main.spriteBatch.End();
                spriteBatch.Begin(SpriteSortMode.Deferred, BlendState.Additive, null, null, null, null, Main.GameViewMatrix.EffectMatrix);
                spriteBatch.Draw(mod.GetTexture("Items/Nature/BloomPlaGlow"), new Vector2(i * 16 - (int)Main.screenPosition.X, j * 16 - (int)Main.screenPosition.Y) + zero - new Vector2(0, 38), null, Color.White, Rotation, new Vector2(17, 17), 1.3f + ScaleSin / 3f, SpriteEffects.None, 0f);
                Main.spriteBatch.End();
                spriteBatch.Begin(SpriteSortMode.Deferred, null, null, null, null, null, Main.GameViewMatrix.EffectMatrix);
                spriteBatch.Draw(mod.GetTexture("Items/Nature/BloomPla"), new Vector2(i * 16 - (int)Main.screenPosition.X, j * 16 - (int)Main.screenPosition.Y) + zero - new Vector2(0, 38), null, Color.White, Rotation, new Vector2(15, 15), 1.3f + ScaleSin / 3f, SpriteEffects.None, 0f);
            }
        }
    }
    public class PlantHover : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Bloomer");
            Tooltip.SetDefault(@"Can only be placed on pure grass.
Took 28 lines of code for the bloom effect.");
        }
        public override void SetDefaults()
        {
            item.width = 14;//change
            item.height = 32;//change
            item.value = Item.sellPrice(0, 5, 5, 5);
            item.rare = ItemRarityID.Blue;
            item.useStyle = ItemUseStyleID.SwingThrow;
            item.useTurn = true;
            item.useAnimation = 15;
            item.maxStack = 99;
            item.useTime = 10;
            item.autoReuse = true;
            item.consumable = true;
            item.createTile = ModContent.TileType<PlantHoverPlaced>();
        }
        public override void AddRecipes()
        {
            ModRecipe POOP = new ModRecipe(mod);

            POOP.AddIngredient(ItemID.GrassSeeds, 130);
            POOP.AddIngredient(ItemID.Sunflower, 3);
            POOP.AddTile(TileID.LivingLoom);
            POOP.SetResult(this);
            POOP.AddRecipe();
        }
    }
}
