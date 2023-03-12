using SFML.Graphics;
using SFML.System;
using SFML.Window;

namespace PhysicsSim;

public abstract class GameBase
{
    public bool ShowGridlines = false;
    
    private RenderWindow? _window;
    private Clock? _clock;
    private int _fpsUpdateCount;
    private float _fps;
    private readonly uint _frameRateLimit = 128;
    private readonly Text _fpsDisplay = new
    (
        "", new Font(new Font(Environment.CurrentDirectory+@"\Resources\Ubuntu-Regular.ttf"))
    ) { CharacterSize = 16 };
    
    private readonly Text _pausedText = new (
        "Paused", new Font(new Font(Environment.CurrentDirectory+@"\Resources\Ubuntu-Regular.ttf"))
    ) { CharacterSize = 16 };
    private bool _paused = true;
    public bool Paused
    {
        get => _paused;
        set => _paused = value;
    }
    public readonly Dictionary<string, Entity> _entities = new();

    public Clock GameClock
    {
        get
        {
            if (_clock == null)
            {
                _clock = new();
                return _clock;
            }
            return _clock;
        }
    }
    
    public RenderWindow GameWindow
    {
        get
        {
            if (_window == null)
            {
                throw new NullReferenceException("Window referenced before initialization");
            }
            return _window;
        }
    }

    public void Run()
    {
        // Set up the game window
        _window = new RenderWindow(new VideoMode(Values.WindowWidth, Values.WindowHeight), "Simple Game");
        _window.SetFramerateLimit(_frameRateLimit);
        _window.Closed += (_, _) => _window.Close();
        _window.SetKeyRepeatEnabled(false);
        _window.Resized += (_, args) =>
        {
            FloatRect visibleArea = new FloatRect(0, 0, args.Width, args.Height);
            _window.SetView(new View(visibleArea));
            Values.WindowWidth = args.Width;
            Values.WindowHeight = args.Height;
        };

        // Start the game clock
        _clock = new Clock();
        
        while (_window.IsOpen) 
        {

            
            
            // Handle events
            _window.DispatchEvents();
            //get clock time
            Time elapsed = _clock.Restart();

            HandleInput(elapsed.AsSeconds());
            Update(elapsed.AsSeconds());
            Draw(elapsed.AsSeconds());
            
            _window.Display();
        }
    }

    public abstract void Update(float deltaTime);

    public abstract void HandleInput(float deltaTime);

    private void Draw(float elapsed)
    {
        if (_window == null) return;
        _window.Clear(Color.Black);

        if (ShowGridlines)
        {
            RectangleShape verticalLine = new(new Vector2f(2, Values.WindowHeight))
            {
                FillColor = Color.Red
            };
            RectangleShape horizontalLine = new(new Vector2f(Values.WindowWidth, 2))
            {
                FillColor = Color.Red
            };
            
            for (int i = 0; i < Values.PixelToMeter(Values.WindowWidth); i+=5)
                _window.Draw(new RectangleShape(verticalLine) { Position = new(Values.MeterToPixel(i), 0)});
            for (int i = 0; i < Values.PixelToMeter(Values.WindowHeight); i+=5)
                _window.Draw(new RectangleShape(horizontalLine) { Position = new(0,
                    (-Values.MeterToPixel(i) + Values.WindowHeight)-2)});
        }
        
        if (_fpsUpdateCount != _frameRateLimit/2)
        {
            _fps += 1 / elapsed;
            _fpsUpdateCount++;
        }
        else
        {
            _fpsDisplay.DisplayedString = "FPS: " + _fps / (_frameRateLimit / 2f);
            _fpsUpdateCount = 0;
            _fps = 0;
        }
        
        _window.Draw(_fpsDisplay);
        if (Paused)
        {
            _pausedText.Position = new Vector2f(Values.WindowWidth - _pausedText.GetGlobalBounds().Width,0);
            _window.Draw(_pausedText);
        }
       
        //Maybe check if an entity has moved before drawing
        foreach (var entity in _entities.Values)
        {
            _window.Draw(entity);
        }
    }

    public void AddEntity(string id, Entity entity)
    {
        if (!_entities.ContainsKey(id)) _entities.Add(id, entity);
    }

    public void DeleteEntity(string id)
    {
        if (_entities.ContainsKey(id)) _entities.Remove(id);
    }
}