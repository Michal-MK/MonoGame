using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace MonoGame {
	public abstract class Component {

		public abstract void Update(GameTime time);
		public abstract void Draw(GameTime time, SpriteBatch device);
	}
}
