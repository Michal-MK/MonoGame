using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace MonoGame {
	/// <summary>
	/// This is the main type for your game.
	/// </summary>
	public class Game : Microsoft.Xna.Framework.Game {
		private GraphicsDeviceManager GDManager { get; }
		private SpriteBatch Renderer { get; set; }

		private readonly Random RNG = new Random();

		private Texture2D _texture;

		private SpriteFont _arial;

		private ThatCoolCircleThing circlesYay { get; set; }

		const int DIVISOR = 6;

		private OpenSimplex.OpenSimplexNoise Noise { get; set; }

		public Game() {
			GDManager = new GraphicsDeviceManager(this);
			Noise = new OpenSimplex.OpenSimplexNoise();
			Content.RootDirectory = "Content";
		}

		public int ViewPortWidth { get; set; }
		public int ViewPortHeight { get; set; }

		protected override void Initialize() {
			IsMouseVisible = true;
			GDManager.SynchronizeWithVerticalRetrace = true;
			IsFixedTimeStep = true;

			//GDManager.IsFullScreen = false;
			//ViewPortWidth = GDManager.PreferredBackBufferWidth = GDManager.GraphicsDevice.DisplayMode.Width / 2;
			//ViewPortHeight = GDManager.PreferredBackBufferHeight = GDManager.GraphicsDevice.DisplayMode.Height / 2;

			ViewPortWidth = GDManager.PreferredBackBufferWidth = GDManager.GraphicsDevice.DisplayMode.Width;
			ViewPortHeight = GDManager.PreferredBackBufferHeight = GDManager.GraphicsDevice.DisplayMode.Height;
			GDManager.IsFullScreen = true;

			GDManager.ApplyChanges();

			simplexNoiseColors = new Color[ViewPortWidth / DIVISOR * ViewPortHeight / DIVISOR];
			IsMouseVisible = true;
			circlesYay = new ThatCoolCircleThing(ViewPortWidth, ViewPortHeight, RNG);
			base.Initialize();
		}

		protected override void LoadContent() {
			Renderer = new SpriteBatch(GraphicsDevice);
			_texture = Content.Load<Texture2D>("test");
			_arial = Content.Load<SpriteFont>("font");
		}

		protected override void UnloadContent() { }

		private Vector2 _prevMousePos;
		private Vector2 _currMousePos;

		private readonly List<Component> _components = new List<Component>();

		Color[] simplexNoiseColors;

		double zOff = 0;
		double xOff = 0;

		double xIncrement = 0.01;
		double yIncrement = 0.01;
		double zOffsetInc = 0.01;

		protected override void Update(GameTime gameTime) {
			zOff += zOffsetInc;

			_prevMousePos = _currMousePos;
			_currMousePos = Mouse.GetState().Position.ToVector2();
			xOff = 0;

			//for (int i = 0; i < (ViewPortWidth / DIVISOR); i++) {
			//	xOff += xIncrement;
			//	double yOff = 0;
			//	for (int j = 0; j < (ViewPortHeight / DIVISOR); j++) {
			//		yOff += yIncrement;
			//		double value = Noise.Evaluate(xOff, yOff, zOff);

			//		int val = ValueMapper.Map(value, -0.7, 1, 0, 360);
			//		int index = j * (ViewPortWidth / DIVISOR) + i;
			//		simplexNoiseColors[index] = HSV.ColorFromHue(val);
			//	}
			//}

			if (Mouse.GetState().LeftButton == ButtonState.Pressed) {
				if (_prevMousePos == null) {
					return;
				}

				//_components.Add(new LineSegment() {
				//	StartPos = _currMousePos,
				//	EndPos = _prevMousePos,
				//	Color = HSV.ColorFromHue(RNG.Next(0, 360))
				//});

				//_components.Add(new Circle() {
				//	Radius = 20,
				//	Position = _currMousePos,
				//	Color = HSV.ColorFromHue(RNG.Next(0, 360))
				//});
			}

			KeyboardState state = Keyboard.GetState();

			if (state.IsKeyDown(Keys.Up)) {
				yIncrement += 0.001;
			}
			if (state.IsKeyDown(Keys.Down)) {
				yIncrement -= 0.001;
			}
			if (state.IsKeyDown(Keys.Left)) {
				xIncrement -= 0.001;
			}
			if (state.IsKeyDown(Keys.Right)) {
				xIncrement += 0.001;
			}
			if (state.IsKeyDown(Keys.NumPad8)) {
				zOffsetInc += 0.001;
			}
			if (state.IsKeyDown(Keys.NumPad2)) {
				zOffsetInc -= 0.001;
			}


			foreach (Component item in _components) {
				item.Update(gameTime);
			}

			if (Mouse.GetState().RightButton == ButtonState.Pressed) {
				_components.Clear();
				circlesYay.Clear();
			}

			if((state.IsKeyDown(Keys.LeftAlt) || state.IsKeyDown(Keys.RightAlt)) && state.IsKeyDown(Keys.Enter)) {
				if (GDManager.IsFullScreen) {
					GDManager.IsFullScreen = false;
					ViewPortWidth = GDManager.PreferredBackBufferWidth = GDManager.GraphicsDevice.DisplayMode.Width / 2;
					ViewPortHeight = GDManager.PreferredBackBufferHeight = GDManager.GraphicsDevice.DisplayMode.Height / 2;
					GDManager.ApplyChanges();
				}
				else {
					ViewPortWidth = GDManager.PreferredBackBufferWidth = GDManager.GraphicsDevice.DisplayMode.Width;
					ViewPortHeight = GDManager.PreferredBackBufferHeight = GDManager.GraphicsDevice.DisplayMode.Height;
					GDManager.IsFullScreen = true;
					GDManager.ApplyChanges();
				}
			}

			circlesYay.Update(gameTime);

			base.Update(gameTime);
		}

		protected override void Draw(GameTime gameTime) {

			GraphicsDevice.Clear(Color.Black);
			Renderer.Begin(blendState: BlendState.AlphaBlend);

			#region Cool stuff inside
			//Rectangle visible = new Rectangle((int)_currMousePos.X - 25, (int)_currMousePos.Y - 25, 50, 50);

			//int noiseResX = ViewPortWidth / DIVISOR;
			//int noiseResY = ViewPortHeight / DIVISOR;

			//Texture2D texture = new Texture2D(GraphicsDevice, noiseResX, noiseResY);

			//texture.SetData(simplexNoiseColors);

			//Renderer.Draw(texture, new Rectangle(0, 0, ViewPortWidth, ViewPortHeight), new Rectangle(0, 0, noiseResX, noiseResY), Color.White);


			//Renderer.Draw(_texture, visible, visible, Color.White, 0, Vector2.Zero, SpriteEffects.None, 0);


			//Renderer.DrawString(_arial, $"zOff = {zOff.ToString("0:000")}; zInc = {zOffsetInc}", Vector2.Zero, Color.Red);
			//Renderer.DrawString(_arial, $"xInc = {xIncrement}", new Vector2(0, +20), Color.Red);
			//Renderer.DrawString(_arial, $"yInc = {yIncrement}", new Vector2(0, +40), Color.Red);

			//foreach (Component comp in _components) {
			//	comp.Draw(gameTime, Renderer);
			//}

			#endregion

			circlesYay.Draw(gameTime, Renderer);
			Renderer.End();
			base.Draw(gameTime);

			//texture.Dispose();
		}
	}
}
