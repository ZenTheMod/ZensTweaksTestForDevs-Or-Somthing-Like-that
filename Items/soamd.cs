using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace ZensTweakstest.Items
{
    public class soamd : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Soul of a True Demon");
            Tooltip.SetDefault("'The essence of Pure Devistation'");
            Main.RegisterItemAnimation(item.type, new DrawAnimationVertical(5, 4));
            ItemID.Sets.AnimatesAsSoul[item.type] = true;
            ItemID.Sets.ItemIconPulse[item.type] = true;
            ItemID.Sets.ItemNoGravity[item.type] = true;
        }

        public override void SetDefaults()
        {
            Item refItem = new Item();
            refItem.SetDefaults(ItemID.SoulofSight);
            item.width = refItem.width;
            item.height = refItem.height;
            item.maxStack = 1;
            item.value = 1000;
            item.rare = ItemRarityID.Pink;

            item.accessory = true;
        }

        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            player.meleeDamage += 0.01f;
        }

        public override void GrabRange(Player player, ref int grabRange)
        {
            grabRange *= 3;
        }

        public override bool GrabStyle(Player player)
        {
            Vector2 vectorItemToPlayer = player.Center - item.Center;
            Vector2 movement = -vectorItemToPlayer.SafeNormalize(default(Vector2)) * 0.1f;
            item.velocity = item.velocity + movement;
            item.velocity = Collision.TileCollision(item.position, item.velocity, item.width, item.height);
            return true;
        }

        public override void PostUpdate()
        {
            Lighting.AddLight(item.Center, Color.Purple.ToVector3() * 0.75f * Main.essScale);
        }

        public override void AddRecipes()
        {
            ModRecipe DUM = new ModRecipe(mod);

            DUM.AddIngredient(ItemID.Ectoplasm, 50);
            DUM.AddIngredient(ItemID.DemonWings, 1);
            DUM.AddIngredient(ItemID.SoulofFlight, 30);
            DUM.AddIngredient(ItemID.SoulofFright, 15);
            DUM.AddIngredient(ItemID.SoulofLight, 15);
            DUM.AddIngredient(ItemID.SoulofMight, 15);
            DUM.AddIngredient(ItemID.SoulofNight, 15);
            DUM.AddIngredient(ItemID.SoulofSight, 15);

            DUM.AddTile(TileID.AdamantiteForge);
            DUM.SetResult(this);
            DUM.AddRecipe();
        }
    }
}
