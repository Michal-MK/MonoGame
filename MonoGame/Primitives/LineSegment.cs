using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace MonoGame {
	public class LineSegment : Component {

		public LineSegment() {

		}

		public Vector2 StartPos { get; set; }

		public Vector2 EndPos { get; set; }

		public Color Color { get; set; }

		public override void Draw(GameTime time, SpriteBatch device) {
			device.DrawLine(StartPos, EndPos, Color);
		}

		public override void Update(GameTime time) {
			/*PASS*/
		}
	}
}
