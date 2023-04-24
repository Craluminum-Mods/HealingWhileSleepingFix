using System;
using Vintagestory.API.Common;
using Vintagestory.API.Common.Entities;
using Vintagestory.API.MathTools;
using Vintagestory.GameContent;

namespace HealingWhileSleepingFix;

public class EntityBehaviorHealingWhileSleepingFix : EntityBehavior
{
    public EntityBehaviorHealingWhileSleepingFix(Entity entity) : base(entity) { }

    public override void OnGameTick(float deltaTime)
    {
        if (!entity.Alive) return;
        if (!IsSleeping(entity as EntityPlayer)) return;

        var behaviorHealth = entity.GetBehavior<EntityBehaviorHealth>();
        if (behaviorHealth.Health >= behaviorHealth.MaxHealth) return;

        var recoverySpeed = 0.01f;

        var behaviorHunger = entity.GetBehavior<EntityBehaviorHunger>();

        if (behaviorHunger != null)
        {
            recoverySpeed = GameMath.Clamp(0.01f * behaviorHunger.Saturation / behaviorHunger.MaxSaturation * 1 / 0.75f, 0, 0.01f);
        }

        behaviorHealth.Health = Math.Min(behaviorHealth.Health + recoverySpeed, behaviorHealth.MaxHealth);
    }

    private bool IsSleeping(EntityPlayer ep) => ep?.MountedOn != null || ep?.GetBehavior<EntityBehaviorTiredness>()?.IsSleeping == true;

    public override string PropertyName() => "healingwhilesleepingfix";
}