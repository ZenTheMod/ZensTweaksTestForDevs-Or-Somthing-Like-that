using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ModLoader;
using Terraria.ID;
using ZensTweakstest.Items.NewNonZen.Erichus.Loot;

namespace ZensTweakstest.Items.NewNonZen.Erichus
{
    public class ErichusBag : ModItem
    {
        public override int BossBagNPC => ModContent.NPCType<Boss.ErichusContainment>();
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
            item.rare = ItemRarityID.Pink;
            item.expert = true;
        }
        public override bool CanRightClick()
        {
            return true;
        }
        public override void OpenBossBag(Player player)
        {
            player.TryGettingDevArmor();
            switch (Main.rand.Next(5))
            {
                case 0:
                    player.QuickSpawnItem(ModContent.ItemType<ToxicGrenade>());
                    break;
                case 1:
                    player.QuickSpawnItem(ModContent.ItemType<ToxicBarrel>());
                    break;
                case 2:
                    player.QuickSpawnItem(ModContent.ItemType<ToxicRevolverator>());
                    player.QuickSpawnItem(ModContent.ItemType<ToxicRocket>(), Main.rand.Next(50, 100));
                    break;
                case 3:
                    player.QuickSpawnItem(ModContent.ItemType<NuclearRotation>());
                    break;
                default:
                    player.QuickSpawnItem(ModContent.ItemType<Butcherer>());
                    break;
            }
            if (Main.rand.Next(0, 20) == 14)
            {
                player.QuickSpawnItem(ModContent.ItemType<ErichusContainmentMask>());
                player.QuickSpawnItem(ModContent.ItemType<EricBox>());
            }
            player.QuickSpawnItem(ModContent.ItemType<ToxicRuble>());
        }
        public override bool PreDrawInInventory(SpriteBatch spriteBatch, Vector2 position, Rectangle frame, Color drawColor, Color itemColor, Vector2 origin, float scale)
        {
            Texture2D texture = ModContent.GetTexture("ZensTweakstest/Items/NewNonZen/Erichus/ErichusBagDrawLoad");
            spriteBatch.Draw(texture, position + new Vector2(-11, -8), null, drawColor, 0f, origin, scale, SpriteEffects.None, 0f);
            if (item.color != Color.Transparent)
            {
                spriteBatch.Draw(texture, position + new Vector2(-11, -8), null, itemColor, 0f, origin, scale, SpriteEffects.None, 0f);
            }
            return false;
        }
        public override bool PreDrawInWorld(SpriteBatch spriteBatch, Color lightColor, Color alphaColor, ref float rotation, ref float scale, int whoAmI)
        {
            Texture2D texture = ModContent.GetTexture("ZensTweakstest/Items/NewNonZen/Erichus/ErichusBagGlow");
            Vector2 position = item.position - Main.screenPosition + new Vector2(item.width / 2, item.height - texture.Height * 0.5f + 2f);
            // We redraw the item's sprite 4 times, each time shifted 2 pixels on each direction, using Main.DiscoColor to give it the color changing effect
            for (int i = 0; i < 4; i++)
            {
                Vector2 offsetPositon = Vector2.UnitY.RotatedBy(MathHelper.PiOver2 * i) * 4;
                spriteBatch.Draw(texture, position + offsetPositon, null, Main.DiscoColor, rotation, texture.Size() * 0.5f, scale, SpriteEffects.None, 0f);
            }
            Texture2D texture2 = ModContent.GetTexture("ZensTweakstest/Items/NewNonZen/Erichus/ErichusBagDrawLoad");
            spriteBatch.Draw(texture2, position, null, lightColor, rotation, texture2.Size() * 0.5f, scale, SpriteEffects.None, 0f);
            // Return true so the original sprite is drawn right after
            return false;
        }
        /*public override void PostDrawInInventory(SpriteBatch spriteBatch, Vector2 position, Rectangle frame, Color drawColor, Color itemColor, Vector2 origin, float scale)
        {
            //spriteBatch.Draw(texture, position, item.GetCurrentFrame(ref frame, ref frameCounter, timeFramesPerAnimationFrame, totalAnimationFrames), Color.White, 0f, origin, scale, SpriteEffects.None, 0);
            //spriteBatch.Draw(ModContent.GetTexture("ZensTweakstest/Items/NewNonZen/Erichus/ErichusBagDrawLoad"), position, null, itemColor, 0f, origin, scale, SpriteEffects.None, 0);
        }*/
    }
}
