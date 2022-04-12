using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace ZensTweakstest.Items
{
    public class soulofd : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Soul of Daniel's Rage");
            Tooltip.SetDefault(@"'The essence of Daniels Pure Rage...'
Increases melee damage by ALOT.");
            // ticksperframe, frameCount
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
            item.rare = ItemRarityID.Purple;

            item.accessory = true;
        }

        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            player.meleeDamage += 1.5f;
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
            Lighting.AddLight(item.Center, Color.Red.ToVector3() * 0.75f * Main.essScale);
        }

        public override void AddRecipes()
        {
            ModRecipe DUM = new ModRecipe(mod);

            DUM.AddIngredient(ItemID.ShroomiteBar, 30);
            DUM.AddIngredient(ItemID.HallowedBar, 50);
            DUM.AddIngredient(ItemID.Meowmere, 1);
            DUM.AddIngredient(ItemID.StarWrath, 1);
            DUM.AddIngredient(ItemID.LunarBar, 30);
            DUM.AddIngredient(ItemID.TerraBlade, 1);
            DUM.AddIngredient(ItemID.BreakerBlade, 1);
            DUM.AddIngredient(ItemID.SlapHand, 1);
            DUM.AddIngredient(ItemID.DayBreak, 1);
            DUM.AddIngredient(ItemID.SandBlock, 100);
            DUM.AddIngredient(ModContent.ItemType<electrons>(), 50);
            DUM.AddIngredient(ModContent.ItemType<soamd>(), 1);
            DUM.AddIngredient(ItemID.Aglet, 4);
            DUM.AddIngredient(ItemID.FragmentSolar, 25);
            DUM.AddTile(TileID.LunarCraftingStation);
            DUM.SetResult(this);
            DUM.AddRecipe();
        }
    }
}
