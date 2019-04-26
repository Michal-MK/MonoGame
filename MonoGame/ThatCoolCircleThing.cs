using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace MonoGame {
	public class ThatCoolCircleThing : Component {

		const double TAU = 2 * Math.PI;

		List<Circle> Circles { get; set; }

		public ThatCoolCircleThing(int width, int height, Random rng) {
			Circles = new List<Circle>();

			//for (int i = 0; i < 200; i++) {
			//	Circles.Add(new Circle() { Color = Color.White, Radius = 0, Position = MoreRandom.RandomVec2(width, height, rng) });
			//}
		}

		public override void Draw(GameTime time, SpriteBatch device) {
			foreach (Circle circle in Circles) {
				foreach (Circle other in Circles) {
					if (other == circle) {
						continue;
					}
					//device.DrawLine(circle.Position, other.Position, Color.Orange);

					Vector2 direction = circle.Position - other.Position;
					//device.DrawCircle(other.Position + direction, 5, 3, Color.Red);
					float length = direction.Length();
					direction /= 2;

					Vector2 ortho = new Vector2(direction.Y, -direction.X);


					//device.DrawCircle(other.Position + direction, 5, 3, Color.Green);
					//device.DrawCircle(other.Position + ortho + direction, 5, 3, Color.LightPink);

					if (circle.IsOverlapping(other)) {
						circle.ReceiveUpdates = false;
					}

					Color mod = Color.LightSkyBlue;
					mod *= 0.05f;
					device.DrawLine(other.Position + ortho + direction, circle.Position - ortho - direction, mod);
				}
				circle.Draw(time, device);
				//CustomDraw(circle, device);
			}
		}

		private void CustomDraw(Circle c, SpriteBatch batch) {
			for (double i = 0; i < TAU; i += 1 / c.Radius) {
				float x = c.Radius * (float)Math.Cos(i);
				float y = c.Radius * (float)Math.Sin(i);
				batch.PutPixel(c.Position.X + x, c.Position.Y + y, c.Color);
			}
		}

		bool change = false;

		public override void Update(GameTime time) {
			if (Mouse.GetState().LeftButton == ButtonState.Pressed) {
				if (!change) {
					Circles.Add(new Circle() { Color = Color.White, Position = Mouse.GetState().Position.ToVector2(), Radius = 0, ReceiveUpdates = true });
					change = true;
					Task.Run(async () => { await Task.Delay(100); change = false; });
				}
			}

			foreach (Circle circle in Circles) {
				if (circle.ReceiveUpdates) {
					circle.Radius += 0.1f;
				}
			}
		}
	}
}
