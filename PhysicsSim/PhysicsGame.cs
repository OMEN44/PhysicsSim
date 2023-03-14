using SFML.Graphics;
using SFML.System;
using SFML.Window;

namespace PhysicsSim;

public class PhysicsGame: GameBase
{
    public override void Update(float deltaTime)
    {
        
        if (!Paused)
        {
            foreach (KeyValuePair<string,Entity> e in _entities)
            {
                Entity entity = e.Value;
                if (!entity.IsStatic)
                {
                    entity.ResolveMotion(deltaTime, entity.CheckCollision());
                }
            }
        }
        
    }

    private bool _mouseDown;
    private bool _keyDown;
    public override void HandleInput(float deltaTime)
    {
        GameWindow.MouseButtonReleased += (_, _) => _mouseDown = false;
        GameWindow.MouseButtonPressed += (_, args) =>
        {
            if (!_mouseDown)
            {
                _mouseDown = true;
                if (args.Button == Mouse.Button.Right)
                {
                    Console.WriteLine(Values.PixelToPosition(new(args.X, args.Y)));
                }

                if (args.Button == Mouse.Button.Left)
                {
                    
                }
            }
            
        };

        GameWindow.KeyReleased += (_, _) => _keyDown = false;
        GameWindow.KeyPressed += (_, args) =>
        {
            if (!_keyDown)
            {
                switch (args.Code)
                {
                    case Keyboard.Key.A:
                        
                        break;
                    case Keyboard.Key.S:
                        
                        break;
                    case Keyboard.Key.D:
                        
                        break;
                    case Keyboard.Key.W:
                        
                        break;
                    case Keyboard.Key.Space:
                        ShowGridlines = !ShowGridlines;
                        break;
                    case Keyboard.Key.P:
                        base.Paused = !base.Paused;
                        break;
                }
                _keyDown = true;
            }
        };
    }
}