using Vintagestory.API.Common;
using Vintagestory.API.Common.Entities;

[assembly: ModInfo("Healing While Sleeping Fix",
    Authors = new[] { "Craluminum2413" })]

namespace HealingWhileSleepingFix;

public class Core : ModSystem
{
    public override void Start(ICoreAPI api)
    {
        base.Start(api);
        api.RegisterEntityBehaviorClass("healingwhilesleepingfix", typeof(EntityBehaviorHealingWhileSleepingFix));
        api.Event.OnEntitySpawn += AddEntityBehaviors;
        api.World.Logger.Event("started 'Healing While Sleeping Fix' mod");
    }

    private void AddEntityBehaviors(Entity entity)
    {
        if (entity is EntityPlayer) entity.AddBehavior(new EntityBehaviorHealingWhileSleepingFix(entity));
    }
}