using System;
using Terraria;
using Terraria.ModLoader;
using Terraria.GameContent.UI;
using Microsoft.Xna.Framework;

namespace terraguardians.Companions.Leopold
{
    public class HeldByBlueBehavior : BehaviorBase
    {
        public TerraGuardian Blue = null;

        public HeldByBlueBehavior(TerraGuardian companion)
        {
            if (!companion.IsSameID(CompanionDB.Blue))
            {
                Deactivate();
                return;
            }
            Blue = companion;
        }

        public override void Update(Companion companion)
        {
            if(Blue == null)
            {
                Deactivate();
                return;
            }
            DrawOrderInfo.AddDrawOrderInfo(companion, Blue, DrawOrderInfo.DrawOrderMoment.InBetweenParent);
            AffectCompanion(Blue);
            Vector2 Position = Vector2.Zero;
            switch(Blue.BodyFrameID)
            {
                default:
                    Position.X = 3 * Blue.direction;
                    Position.Y = -6;
                    break;
                case 32:
                    Position.X = Blue.direction;
                    Position.Y = 6;
                    break;
                case 31:
                    Position.X = Blue.direction;
                    Position.Y = 1;
                    break;
            }
            Position.Y = -64 + (Position.Y);
            if (companion.itemAnimation <= 0)
                companion.direction = Blue.direction;
            companion.position = Blue.Bottom + Position * Blue.Scale;
            companion.velocity.Y = 0;
            companion.velocity.X = 0;
            companion.gfxOffY = 0;
            if (Blue.whoAmI < companion.whoAmI)
            {
                companion.position += Blue.velocity;
            }
            companion.SetFallStart();
            companion.MoveUp = companion.MoveDown = companion.MoveLeft = companion.MoveRight = false;
            companion.ControlJump = false;
        }

        public override void UpdateAnimationFrame(Companion companion)
        {
            short FrameID = 29;
            switch(Blue.BodyFrameID)
            {
                case 26:
                    FrameID = 24;
                    break;
                case 32:
                    FrameID = 15;
                    break;
                case 31:
                    FrameID = 16;
                    break;
            }
            companion.BodyFrameID = FrameID;
            TerraGuardian tg = (TerraGuardian)companion;
            for(int i = 0; i < tg.ArmFramesID.Length; i++)
            {
                if (tg.HeldItems[i].ItemAnimation <= 0)
                {
                    tg.ArmFramesID[i] = FrameID;
                }
            }
        }

        public override void UpdateAffectedCompanionAnimationFrame(Companion companion)
        {
            BlueBase.ApplyHeldBunnyAnimation((TerraGuardian)companion, true);
        }

        public override void OnEnd()
        {
            if (Blue != null && GetOwner != null)
            {
                GetOwner.Teleport(Blue);
            }
        }
    }
}