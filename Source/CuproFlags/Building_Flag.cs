using System.IO;
using UnityEngine;
using Verse;

namespace CuproFlags
{
    [StaticConstructorOnStartup]
    public class Building_Flag : Building
    {
        public static readonly Graphic Pole = GraphicDatabase.Get<Graphic_Single>("Cupro/Object/Utility/Flags/Pole",
            ShaderDatabase.CutoutComplex, new Vector2(3, 3), Color.white, Color.white);

        private Graphic[] animFrames;
        private Graphic currFrame;
        private bool cutout;
        private int frameLerp;
        private int swayTicks = 10;
        private bool updated;
        private string wd;
        private WindManager windMan;

        public override Graphic Graphic
        {
            get
            {
                if (updated)
                {
                    return Pole;
                }

                return base.Graphic;
            }
        }

        private Color Color
        {
            get
            {
                if (Stuff != null)
                {
                    return Stuff.stuffProps.color;
                }

                return def.graphicData.color;
            }
        }


        public override void SpawnSetup(Map map, bool respawningAfterLoad)
        {
            base.SpawnSetup(map, respawningAfterLoad);
            wd = Path.GetDirectoryName(def.graphicData.texPath)?.Replace("\\", "/");
            windMan = Map.windManager;
            LongEventHandler.ExecuteWhenFinished(GetGraphicArray);
        }


        public void GetGraphicArray()
        {
            cutout = def.graphicData.shaderType.Shader == ShaderDatabase.CutoutComplex;
            animFrames = new Graphic[5];
            if (cutout)
            {
                for (var i = 0; i < 5; i++)
                {
                    animFrames[i] = GraphicDatabase.Get<Graphic_Single>($"{wd}/Anim{i}", ShaderDatabase.CutoutComplex,
                        new Vector2(3, 3), Color.white);
                }
            }
            else
            {
                for (var i = 0; i < 5; i++)
                {
                    animFrames[i] = GraphicDatabase.Get<Graphic_Single>($"{wd}/Anim{i}", ShaderDatabase.DefaultShader,
                        new Vector2(3, 3), Color.white);
                }
            }

            currFrame = animFrames[Rand.Range(0, 5)];
            updated = true;
        }


        public override void Tick()
        {
            base.Tick();

            if (this.IsHashIntervalTick(swayTicks))
            {
                frameLerp++;
                if (frameLerp >= 5)
                {
                    frameLerp = 0;
                }

                currFrame = animFrames[frameLerp];
            }

            if (!this.IsHashIntervalTick(60))
            {
                return;
            }

            if (windMan.WindSpeed >= 1.2f)
            {
                swayTicks = 5;
            }
            else if (windMan.WindSpeed >= 0.8f)
            {
                swayTicks = 10;
            }
            else
            {
                swayTicks = 15;
            }
        }


        public override void Draw()
        {
            base.Draw();
            if (currFrame == null)
            {
                return;
            }

            var matrix = default(Matrix4x4);
            var s = new Vector3(def.graphicData.drawSize.x, 9f, def.graphicData.drawSize.y);
            var x = new Vector3(0f, 0f, 0f);
            matrix.SetTRS(DrawPos + x + Altitudes.AltIncVect, Rotation.AsQuat, s);
            Graphics.DrawMesh(MeshPool.plane10, matrix,
                currFrame.GetColoredVersion(def.graphicData.shaderType.Shader, Color, Color.white).MatSingle, 0);
        }
    }
}