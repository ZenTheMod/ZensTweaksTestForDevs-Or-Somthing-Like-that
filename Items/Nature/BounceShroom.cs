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
    public class BounceShroom : ModTile
    {
        public override void SetDefaults()
        {
            Main.tileFrameImportant[Type] = true;
            Main.tileSolidTop[Type] = true;
            Main.tileLighted[Type] = true;
            Main.tileLavaDeath[Type] = true;
            TileID.Sets.FramesOnKillWall[Type] = true; // Necessary since Style3x3Wall uses AnchorWall
            TileObjectData.newTile.CopyFrom(TileObjectData.Style3x2);
            TileObjectData.newTile.AnchorValidTiles = new int[]
            {
                TileID.MushroomGrass,
                TileID.Mud
            };
            TileObjectData.addTile(Type);
            dustType = DustID.GlowingMushroom;
            AddMapEntry(new Color(93, 178, 255));
        }
        public override void FloorVisuals(Player player)
        {
            player.velocity.Y -= 14f;
            Main.PlaySound(SoundID.DD2_WitherBeastAuraPulse, player.Center);
            float speed = player.velocity.Length();
            for (int i = 0; i < 12; i++)
            {
                DoDustEffect(player.MountedCenter, 46f - speed * 4.5f, 1.08f - speed * 0.13f, 2.08f - speed * 0.24f, player);
            }
        }
        public override void ModifyLight(int i, int j, ref float r, ref float g, ref float b)
        {
            r = 0f;
            b = 1f;
            g = 0.10f;
        }
        private void DoDustEffect(Vector2 position, float distance, float minSpeed = 2f, float maxSpeed = 3f, object follow = null)
        {
            float angle = Main.rand.NextFloat(-MathHelper.Pi, MathHelper.Pi);
            Vector2 vec = new Vector2((float)Math.Cos(angle), (float)Math.Sin(angle));
            Vector2 vel = vec * Main.rand.NextFloat(minSpeed, maxSpeed);

            int dust = Dust.NewDust(position - vec * distance, 0, 0, DustID.Electric);
            Main.dust[dust].noGravity = true;
            Main.dust[dust].scale *= .6f;
            Main.dust[dust].velocity = vel;
            Main.dust[dust].customData = follow;
        }
    }
    public class BounceShroomItem : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Bouncer Shroom");
            Tooltip.SetDefault(@"Can only be placed on Mushroom Tiles.
Bounces High, Hold Jump sor Higher Bounces and Down to stop Bounces.");
        }
        public override void SetDefaults()
        {
            item.width = 36;//change
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
            item.createTile = ModContent.TileType<BounceShroom>();
        }
        public override void AddRecipes()
        {
            ModRecipe POOP = new ModRecipe(mod);

            POOP.AddIngredient(ItemID.MushroomGrassSeeds, 100);
            POOP.AddIngredient(ItemID.GlowingMushroom, 7);
            POOP.AddTile(TileID.LivingLoom);
            POOP.SetResult(this);
            POOP.AddRecipe();
        }
    }
}
