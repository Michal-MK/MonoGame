using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace MonoGame {
	public class Circle : Component {

		public Vector2 Position { get; set; }

		public float Radius { get; set; }
		public Color Color { get; internal set; }
		public bool ReceiveUpdates { get; set; } = true;

		public override void Draw(GameTime time, SpriteBatch batch) {
			const double TAU = 2 * Math.PI;
			for (double i = 0; i < TAU; i += 1 / Radius) {
				float x = Radius * (float)Math.Cos(i);
				float y = Radius * (float)Math.Sin(i);
				batch.PutPixel(Position.X + x, Position.Y + y, Color);
			}

			//batch.DrawCircle(Position, Radius, (int)Radius, Color);
		}

		public override void Update(GameTime time) {
			if (!ReceiveUpdates) {
				return;
			}
			Radius -= 0.05f;
			if (Radius < 5) {
				Radius = 20;
			}
		}

		internal bool IsOverlapping(Circle other) {
			return (this.Position - other.Position).Length() <= (other.Radius + this.Radius);
		}
	}
}
