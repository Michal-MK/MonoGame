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

			for (int i = 0; i < 20; i++) {
				Circles.Add(new Circle() { Color = Color.White, Radius = 0, Position = MoreRandom.RandomVec2(width, height, rng) });
			}
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

					//if (circle.IsOverlapping(other)) {
					//	circle.ReceiveUpdates = false;
					//}

					Color mod = Color.LightSkyBlue;
					mod *= 0.05f;
					device.DrawLine(other.Position + ortho + direction, circle.Position - ortho - direction, mod);
				}
				//circle.Draw(time, device);
				CustomDraw(circle, device);
			}
		}

		private void CustomDraw(Circle c, SpriteBatch batch) {

			for (double i = 0; i < TAU; i += 1/c.Radius) {
				float x = c.Radius * (float)Math.Cos(i);
				float y = c.Radius * (float)Math.Sin(i);
				Vector2 vec = new Vector2(x, y);

				vec += c.Position;

				bool ignore = false;
				foreach (Circle other in Circles) {
					if (other == c) {
						continue;
					}

					Vector2 diff = other.Position - vec;
					float f = diff.Length();

					if (f < other.Radius) {
						ignore = true;



						float d = (other.Position - c.Position).Length();

						float r0 = other.Radius;
						Vector2 P0 = other.Position;

						float r1 = c.Radius;
						Vector2 P1 = c.Position;

						/*
						d = a + b

						a2 + h2 = other.Radius ^ 2 
						b2 + h2 = c.Radius ^ 2
						*/

						double a = (Math.Pow(r0, 2) - Math.Pow(r1, 2) + Math.Pow(d, 2)) / (2 * d);
						double h = Math.Sqrt(Math.Pow(r0, 2) - Math.Pow(a, 2));

						/*
						P2 = P0 + a ( P1 - P0 ) / d
						*/

						Vector2 P2 = P0 + ((float)a) * (P1 - P0) / d;

						/*
						x3 = x2 +- h ( y1 - y0 ) / d
						y3 = y2 -+ h ( x1 - x0 ) / d
						*/

						float x_31 = (float)(P2.X - (h * (c.Position.Y - other.Position.Y) / d));
						float x_32 = (float)(P2.X + (h * (c.Position.Y - other.Position.Y) / d));

						float y_31 = (float)(P2.Y + (h * (c.Position.X - other.Position.X) / d));
						float y_32 = (float)(P2.Y - (h * (c.Position.X - other.Position.X) / d));

						Vector2 P31 = new Vector2(x_31, y_31);
						Vector2 P32 = new Vector2(x_32, y_32);

						batch.DrawLine(P31, P32, Color.Green);

						batch.DrawCircle(vec, 1, 20, Color.Red);
					}
				}
				if (!ignore)
					batch.PutPixel(c.Position.X + x, c.Position.Y + y, c.Color);


				//bool clamp = false;
				//Circle closest = null;
				//double lowestRad = double.MaxValue;
				//foreach (Circle other in Circles) {
				//	batch.DrawLine(c.Position, vec * c.Radius, Color.Red);

				//	Vector2 v = 

				//	if (other == c) {
				//		continue;
				//	}

				//	double lhs = Math.Pow(vec.X - other.Position.X, 2) + Math.Pow(vec.Y - other.Position.Y, 2);

				//	if (lhs <= Math.Pow(other.Radius,2)) {
				//		//batch.DrawLine(c.Position, vec * c.Radius, Color.Red);

				//		if (lhs < lowestRad) {
				//			lowestRad = lhs;
				//			closest = other;
				//		}
				//		clamp = true;
				//	}
				//	batch.DrawLine(c.Position, vec * c.Radius, Color.Red);
				//}

				//if (!clamp) {
				//	batch.PutPixel(c.Position.X + x, c.Position.Y + y, c.Color);
				//}
				//else {
				//	batch.PutPixel(c.Position.X + (closest.Position.X - c.Position.X), c.Position.Y + (closest.Position.Y - c.Position.Y), c.Color);
				//}
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
					circle.Radius += 0.2f;
				}
			}
		}

		internal void Clear() {
			Circles.Clear();
		}
	}
}
