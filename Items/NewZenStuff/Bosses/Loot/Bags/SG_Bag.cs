using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ModLoader;
using Terraria.ID;
using ZensTweakstest.Items.NewZenStuff.Bosses.Loot.BagLoot;
using ZensTweakstest.Items.NewZenStuff.Bosses.Loot.BagLoot.SummonStaff;
using ZensTweakstest.Items.NewZenStuff.Items;
using ZensTweakstest.Items.JupiterStuff;
using ZensTweakstest.Items.JupiterStuff.Pet;
using ZensTweakstest.Config;

namespace ZensTweakstest.Items.NewZenStuff.Bosses.Loot.Bags
{
    public class SG_Bag : ModItem
    {
        public override int BossBagNPC => mod.NPCType("SparkGaurdian");
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Treasure Bag");
            Tooltip.SetDefault("{$CommonItemTooltip.RightClickToOpen}");
        }

        public override string Texture => ModContent.GetInstance<SpriteSettings>().MostClassicSprites ? base.Texture + "OLD" : base.Texture;

        public override void SetDefaults()
        {
            item.maxStack = 999;
            item.consumable = true;
            item.width = 32;
            item.height = 32;
            item.rare = ItemRarityID.Pink;
            item.expert = true;
        }

        public override bool CanRightClick()
        {
            return true;
        }

        public override void OpenBossBag(Player player)
        {
            if (Main.rand.Next(0, 15) == 4)
            {
                player.QuickSpawnItem(ModContent.ItemType<JupiMask>());
                player.QuickSpawnItem(ModContent.ItemType<JupiBody>());
                player.QuickSpawnItem(ModContent.ItemType<JupiLegs>());
                player.QuickSpawnItem(ModContent.ItemType<TheAbyss>());
                player.QuickSpawnItem(ModContent.ItemType<BunnyPetItem>());
            }
            player.TryGettingDevArmor();
            player.QuickSpawnItem(ModContent.ItemType<HangingVoid>());
            if (Main.rand.Next(0, 100) >= 75)
            {
                player.QuickSpawnItem(ModContent.ItemType<ZSS_Vanity>());
            }
            if (Main.rand.Next(0, 100) >= 90)
            {
                player.QuickSpawnItem(ModContent.ItemType<Cursed_Zen_Peeve_Essence>());
            }
            if (ZenWorld.DownedZenGaurd)
            {
                player.QuickSpawnItem(ModContent.ItemType<SG_Box>());
            }
            if (Main.rand.NextBool(7))
            {
                player.QuickSpawnItem(ModContent.ItemType<SparkBow>());
                player.QuickSpawnItem(ModContent.ItemType<SparkMask>());
            }
            player.QuickSpawnItem(ModContent.ItemType<ZenSwordBook>());
            if (Main.rand.Next(0, 100) <= 27)
            {
                player.QuickSpawnItem(ModContent.ItemType<Zen_Stone_Trident>());
            }
            player.QuickSpawnItem(ModContent.ItemType<Zen_Peeve_Essence>(), 35);
            if (Main.rand.Next(0, 100) >= 63)
            {
                player.QuickSpawnItem(ModContent.ItemType<GlobeStaff>());
            }
        }

        public override bool PreDrawInWorld(SpriteBatch spriteBatch, Color lightColor, Color alphaColor, ref float rotation, ref float scale, int whoAmI)
        {
            Texture2D texture = ModContent.GetTexture("ZensTweakstest/Items/NewZenStuff/Bosses/Loot/Bags/SG_OutlineBag");
            Vector2 position = item.position - Main.screenPosition + new Vector2(item.width / 2, item.height - texture.Height * 0.5f + 2f);
            // We redraw the item's sprite 4 times, each time shifted 2 pixels on each direction, using Main.DiscoColor to give it the color changing effect
            for (int i = 0; i < 4; i++)
            {
                Vector2 offsetPositon = Vector2.UnitY.RotatedBy(MathHelper.PiOver2 * i) * 4;
                spriteBatch.Draw(texture, position + offsetPositon, null, Main.DiscoColor, rotation, texture.Size() * 0.5f, scale, SpriteEffects.None, 0f);
            }
            // Return true so the original sprite is drawn right after
            return true;
        }
    }
}
