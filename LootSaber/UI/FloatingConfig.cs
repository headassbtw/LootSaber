using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LootSaber.UI
{
    internal class FloatingConfig
    {
        public virtual bool Enabled { get; set; } = true;
        public virtual UnityEngine.Vector3 Position { get; set; }
        public virtual UnityEngine.Vector3 Rotation { get; set; }
    }
}
