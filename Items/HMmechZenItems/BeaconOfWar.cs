using Terraria;
using Terraria.ModLoader;
using Terraria.ID;
using Microsoft.Xna.Framework;
using System;
using Microsoft.Xna.Framework.Graphics;
using ZensTweakstest.Helper;
using Terraria.Graphics.Shaders;
using ZensTweakstest.Items.NewZenStuff.Items;
using ZensTweakstest.Helper;
using QwertysRandomContent;

namespace ZensTweakstest.Items.HMmechZenItems
{
    public class BeaconOfWar : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Beacon of War");
            Tooltip.SetDefault("Moment of silence for those lost in heat of war." +
                "\nSummons the gateway between realms. Realms not given a secound chance.");//WHOS WRITTING FANFICTION? - ZEN
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
            recipe.AddTile(TileID.AdamantiteForge);
            recipe.SetResult(this);
            recipe.AddRecipe();
        }
        public override bool PreDrawTooltipLine(DrawableTooltipLine line, ref int yOffset)
        {
            if (line.mod == "Terraria" && line.Name == "ItemName")
            {
                Main.spriteBatch.End(); //end and begin main.spritebatch to apply a shader
                Main.spriteBatch.Begin(SpriteSortMode.Immediate, null, null, null, null, null, Main.UIScaleMatrix);
                GameShaders.Armor.Apply(GameShaders.Armor.GetShaderIdFromItemId(ItemID.RedAcidDye), item, null); //use living rainbow dye shader
                Utils.DrawBorderString(Main.spriteBatch, line.text, new Vector2(line.X, line.Y), Color.White, 1); //draw the tooltip manually
                Main.spriteBatch.End(); //then end and begin again to make remaining tooltip lines draw in the default way
                Main.spriteBatch.Begin(SpriteSortMode.Deferred, null, null, null, null, null, Main.UIScaleMatrix);
                return false;
            }
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
                return true;
            }
            return false;
        }
    }
}
