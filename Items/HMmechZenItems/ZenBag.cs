using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ModLoader;
using Terraria.ID;

namespace ZensTweakstest.Items.HMmechZenItems
{
    public class ZenBag : ModItem
    {
        public override int BossBagNPC => ModContent.NPCType<Items.HMmechZen.MechZen>();
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Treasure Bag");
            Tooltip.SetDefault("{$CommonItemTooltip.RightClickToOpen}");
        }

        public override void SetDefaults()
        {
            item.maxStack = 999;
            item.consumable = true;
            item.width = 32;
            item.height = 32;
            item.rare = 4;
            item.expert = true;
        }

        public override bool CanRightClick()
        {
            return true;
        }

        public override void OpenBossBag(Player player)
        {
            player.QuickSpawnItem(ModContent.ItemType<VoidSheath>());
            player.QuickSpawnItem(ModContent.ItemType<Clinger_Fruit>(), 10);
            switch (Main.rand.Next(6))
            {
                case 0:
                    player.QuickSpawnItem(ModContent.ItemType<ZenicFlamethrower>());
                    break;
                case 1:
                    player.QuickSpawnItem(ModContent.ItemType<ThePoker>());
                    break;
                case 2:
                    player.QuickSpawnItem(ModContent.ItemType<Items.Weapons.Afterburn>());
                    break;
                case 3:
                    player.QuickSpawnItem(ModContent.ItemType<StellarUmbrella>());
                    break;
                case 4:
                    player.QuickSpawnItem(ModContent.ItemType<VoidSlash>());
                    break;
                default:
                    player.QuickSpawnItem(ModContent.ItemType<ChaosVigil>());//Items.Weapons.Afterburn
                    break;
            }
            if(Main.rand.Next(0,100) < 5)
            {
                player.QuickSpawnItem(ModContent.ItemType<AfflictionDagger>());
            }
            if (Main.rand.Next(0, 100) < 10)
            {
                player.QuickSpawnItem(ModContent.ItemType<HMmechZenItems.BossCosmetics.MechZenMask>());
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
