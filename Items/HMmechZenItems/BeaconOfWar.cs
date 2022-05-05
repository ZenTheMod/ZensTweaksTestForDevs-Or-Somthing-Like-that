using Terraria;
using Terraria.ModLoader;
using Terraria.ID;
using Microsoft.Xna.Framework;
using System;
using Microsoft.Xna.Framework.Graphics;
using ZensTweakstest.Helper;
using Terraria.Graphics.Shaders;
using ZensTweakstest.Items.NewZenStuff.Items;
using QwertysRandomContent;
using ZensTweakstest.Items.JupiterStuff;

namespace ZensTweakstest.Items.HMmechZenItems
{
    public class BeaconOfWar : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Beacon of War");
            Tooltip.SetDefault("Moment of silence for those lost in heat of war." +
                "\nIrmeeagine reading thieers lo0l gg." +
                "\nSmusry ancheint texmmtooer. eddeerreeoleemmerr");//WHOS WRITTING FANFICTION? - ZEN
            ItemID.Sets.SortingPriorityBossSpawns[item.type] = 13; // This helps sort inventory know this is a boss summoning item.
        }
        public override void SetDefaults()
        {
            item.width = 22;//change
            item.height = 64;//change
            item.value = Item.buyPrice(0, 25, 5, 5);
            item.rare = 4;
            item.useStyle = ItemUseStyleID.HoldingUp;
            item.useTurn = true;
            item.useAnimation = 15;
            item.maxStack = 30;
            item.useTime = 10;
            item.autoReuse = true;
            item.consumable = true;
            ItemUseGlow.AutoLoadGlow(item);
        }
        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(ModContent.ItemType<ZenStone_I>(), 50);
            recipe.AddIngredient(ItemID.CrystalShard, 15);
            recipe.AddIngredient(ItemID.SoulofMight, 5);
            recipe.AddIngredient(ItemID.SoulofFright, 5);
            recipe.AddIngredient(ItemID.SoulofSight, 5);
            recipe.AddIngredient(ModContent.ItemType<CursedBone>(), 5);
            recipe.AddTile(TileID.AdamantiteForge);
            recipe.SetResult(this);
            recipe.AddRecipe();
        }
        public override bool PreDrawTooltipLine(DrawableTooltipLine line, ref int yOffset)
        {
            if (line.mod == "Terraria" && line.Name == "Tooltip0" || line.Name == "Tooltip1" || line.Name == "Tooltip2")
            { // replace # to the tooltip number in Tooltip.SetDefaults(), 0 is the first tooltip
              // we end and begin a Immediate spriteBatch for shader
                Vector2 messageSize = Helplul.MeasureString(line.text);
                Rectangle rec = new Rectangle(line.X - 40, line.Y - 2, (int)messageSize.X + 88, (int)messageSize.Y);
                Main.spriteBatch.BeginImmediate(true, true);
                GameShaders.Misc["WaveWrapZ"].UseOpacity((float)Main.GameUpdateCount / 500f).Apply();
                Main.spriteBatch.Draw(ModContent.GetTexture("ZensTweakstest/Helper/NotMyBalls"), rec, Color.Black);
                Main.spriteBatch.BeginImmediate(true, true, true);
                GameShaders.Misc["WaveWrapZ"].UseOpacity((float)Main.GameUpdateCount / 500f).Apply();
                Color color = Helplul.CycleColor(Color.Red, Color.DarkRed);
                Main.spriteBatch.Draw(ModContent.GetTexture("ZensTweakstest/Helper/NotMyBalls"), rec, color);
                Main.spriteBatch.BeginImmediate(true, true);
                GameShaders.Misc["WaveWrapZ"].UseOpacity((float)Main.GameUpdateCount / 500f).Apply();
                // redraw the tooltip
                Utils.DrawBorderString(Main.spriteBatch, line.text, new Vector2(line.X, line.Y), Color.White, 1);
                // we end and begin a deffered spriteBatch so it doesnt break everything
                Main.spriteBatch.BeginImmediate(true, true);
                // return false so vanilla doesnt draw it
                return false;
            }
            // this is the item name redrawing
            if (line.mod == "Terraria" && line.Name == "ItemName")
            {
                // we calculate the rectangle 
                Vector2 messageSize = Helplul.MeasureString(line.text);
                Rectangle rec = new Rectangle(line.X - 40, line.Y - 2, (int)messageSize.X + 88, (int)messageSize.Y);
                // we end and begin a Immediate spriteBatch for shader
                Main.spriteBatch.BeginImmediate(true, true);
                GameShaders.Misc["WaveWrapZ"].UseOpacity((float)Main.GameUpdateCount / 500f).Apply();
                Main.spriteBatch.Draw(ModContent.GetTexture("ZensTweakstest/Helper/NotMyBalls"), rec, Color.Black);
                Main.spriteBatch.BeginImmediate(true, true, true);
                GameShaders.Misc["WaveWrapZ"].UseOpacity((float)Main.GameUpdateCount / 500f).Apply();
                Color color = Helplul.CycleColor(Color.Red, Color.DarkRed);
                Main.spriteBatch.Draw(ModContent.GetTexture("ZensTweakstest/Helper/NotMyBalls"), rec, color);
                // we end and begin a deffered spriteBatch so it doesnt break everything
                Main.spriteBatch.BeginImmediate(true, true);
                // return false so vanilla doesnt draw it
                Main.spriteBatch.End(); //end and begin main.spritebatch to apply a shader
                Main.spriteBatch.Begin(SpriteSortMode.Immediate, null, null, null, null, null, Main.UIScaleMatrix);
                GameShaders.Armor.Apply(GameShaders.Armor.GetShaderIdFromItemId(ItemID.RedAcidDye), item, null); //use living rainbow dye shader
                Utils.DrawBorderString(Main.spriteBatch, line.text, new Vector2(line.X, line.Y), Color.White, 1); //draw the tooltip manually
                Main.spriteBatch.End(); //then end and begin again to make remaining tooltip lines draw in the default way
                Main.spriteBatch.Begin(SpriteSortMode.Deferred, null, null, null, null, null, Main.UIScaleMatrix);
                return false;
            }
            // any other line just draw normally
            return true;
        }
        public override bool UseItem(Player player)
        {
            if (NPC.downedMechBoss1 && NPC.downedMechBoss2 && NPC.downedMechBoss3 && !NPC.AnyNPCs(ModContent.NPCType<Items.HMmechZen.ZenMechPortal>()) && !NPC.AnyNPCs(ModContent.NPCType<Items.HMmechZen.MechZen>()) && !Main.dayTime)
            {
                Main.NewText("The sky runs red with terror.", 255, 0, 0, false);
                NPC.SpawnOnPlayer(player.whoAmI, ModContent.NPCType<Items.HMmechZen.ZenMechPortal>());
                Main.PlaySound(mod.GetLegacySoundSlot(SoundType.Custom, "Sounds/Custom/Beam").WithPitchVariance(.5f), player.position);//Main.PlaySound(mod.GetLegacySoundSlot(SoundType.Custom, "Sounds/Custom/Beam").WithPitchVariance(.5f),projectile.position);
                player.CameraShake(23, 50);
                Projectile.NewProjectile(player.position + new Vector2(0, -600), Vector2.Zero, ModContent.ProjectileType<Primordial>(), 0, 0);
                return true;
            }
            return false;
        }
    }
}
