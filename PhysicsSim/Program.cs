using SFML.Graphics;
using SFML.System;

namespace PhysicsSim;

public class Program
{
    static void Main()
    {
        GameBase application = new PhysicsGame();

        Vector2f[] vertices =
        {
            new(0, 0), 
            new(1, 0), 
            new(1, 1), 
            new(0, 1)
        };

        Vector2f pos = new Vector2f(3, 40);
    
        Entity rect = new Entity(Color.Green, pos, false, 10, new(5,0), vertices);


        application.AddEntity("rect", rect);
        
        application.Run();
        
        /* Create a window to display the simulation
            RenderWindow window = new RenderWindow(new VideoMode(800, 600), "Physics Simulation");
            window.SetFramerateLimit(60);

            // Define the properties of the simulation
            const float gravity = 9.81f;
            const float restitution = 0.7f;
            const float friction = 0.5f;

            // Create a list to store the objects in the simulation
            List<PhysicsObject> objects = new List<PhysicsObject>();

            // Create a ground object
            PhysicsObject ground = new PhysicsObject(new Vector2f(400, 550), new Vector2f(800, 50), Color.Green, true);
            objects.Add(ground);

            // Create a ball object
            PhysicsObject ball = new PhysicsObject(new Vector2f(100, 100), new Vector2f(50, 50), Color.Red);
            ball.Velocity = new Vector2f(100, 0);
            ball.Mass = 1;
            objects.Add(ball);

            // Start the simulation loop
            Clock clock = new Clock();
            while (window.IsOpen)
            {
                // Handle events
                window.DispatchEvents();

                // Update the simulation
                float deltaTime = clock.Restart().AsSeconds();
                foreach (PhysicsObject obj in objects)
                {
                    // Apply gravity
                    if (!obj.IsStatic)
                    {
                        obj.ApplyForce(new Vector2f(0, gravity * obj.Mass));
                    }

                    // Update the position and velocity of the object
                    obj.Update(deltaTime);

                    // Check for collisions with the ground
                    if (obj.IsCollidingWith(ground))
                    {
                        // Calculate the collision response
                        Vector2f normal = new Vector2f(0, -1);
                        float impulse = -(1 + restitution) * Vector2f.Dot(obj.Velocity, normal);
                        impulse /= (1 / obj.Mass);
                        Vector2f collisionImpulse = impulse * normal;
                        Vector2f frictionImpulse = friction * obj.Mass * obj.Velocity;
                        obj.ApplyImpulse(collisionImpulse + frictionImpulse);
                    }

                    // Check for collisions with other objects
                    foreach (PhysicsObject other in objects)
                    {
                        if (obj != other && obj.IsCollidingWith(other))
                        {
                            // Calculate the collision response
                            Vector2f normal = (other.Position - obj.Position).Normalize();
                            float impulse = -(1 + restitution) * Vector2f.Dot(obj.Velocity - other.Velocity, normal);
                            impulse /= (1 / obj.Mass) + (1 / other.Mass);
                            Vector2f collisionImpulse = impulse * normal;
                            Vector2f frictionImpulse = friction * obj.Mass * (obj.Velocity - other.Velocity).Normalize();
                            obj.ApplyImpulse(collisionImpulse + frictionImpulse);
                            other.ApplyImpulse(-collisionImpulse - frictionImpulse);
                        }
                    }
                }

                // Clear the window
                window.Clear(Color.Black);

                // Draw the objects
                foreach (PhysicsObject obj in objects)
                {
                    obj.Draw(window);
                }

                // Display the window
                window.Display();
            }*/
            
            
    }
    
    class PhysicsObject
    {
        public Vector2f Position { get; set; }
        public Vector2f Size { get; set; }
        public Vector2f Velocity { get; set; }
        public float Mass { get; set; }
        public Color Color { get; set; }
        public bool IsStatic { get; set; }

        private RectangleShape shape;

        public PhysicsObject(Vector2f position, Vector2f size, Color color, bool isStatic = false)
        {
            Position = position;
            Size = size;
            Velocity = new Vector2f(0, 0);
            Mass = 0;
            Color = color;
            IsStatic = isStatic;

            shape = new RectangleShape(size);
            shape.Position = position;
            shape.FillColor = color;
        }

        public void Update(float deltaTime)
        {
            if (!IsStatic)
            {
                // Update the position and velocity using Euler integration
                Position += Velocity * deltaTime;
                Velocity += GetAcceleration() * deltaTime;
            }

            // Update the shape
            shape.Position = Position;
        }

        public void Draw(RenderWindow window)
        {
            window.Draw(shape);
        }

        public void ApplyForce(Vector2f force)
        {
            Velocity += force / Mass;
        }

        public void ApplyImpulse(Vector2f impulse)
        {
            Velocity += impulse / Mass;
        }

        public bool IsCollidingWith(PhysicsObject other)
        {
            // Check for collision between two objects using AABB test
            return Position.X < other.Position.X + other.Size.X &&
                   Position.X + Size.X > other.Position.X &&
                   Position.Y < other.Position.Y + other.Size.Y &&
                   Position.Y + Size.Y > other.Position.Y;
        }

        private Vector2f GetAcceleration()
        {
            // Calculate the acceleration of the object based on the net force
            return new Vector2f(0, 0);
        }
    }
}