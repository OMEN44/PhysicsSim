﻿using System.Runtime.InteropServices.ComTypes;
using SFML.Graphics;
using SFML.System;
using static PhysicsSim.Values;

// ReSharper disable MemberCanBePrivate.Global

namespace PhysicsSim.Entities;

public class Entity : Transformable, Drawable
{
    // Physics properties
    public bool IsStatic { get; }
    // mass in kg
    public int Mass { get; }
    // acceleration in m/s²
    public Vector2f Acceleration;
    // velocity in m/s
    public Vector2f Velocity;
    // position on the screen in m from bottom left origin 
    public Vector2f Pos
    {
        get => PixelToPosition(Position);
        private set => Position = PositionToPixel(value);
    }

    // shape properties
    private readonly Vertex[] _vertices;
    private readonly Vector2f[] _points;

    public Entity(Color color, Vector2f position, bool isStatic, int mass, Vector2f initialVelocity, params Vector2f[] points) : this(color, position, isStatic, mass, points)
    {
        Velocity = initialVelocity;
    }
    
    public Entity(Color color, Vector2f position, bool isStatic, int mass, params Vector2f[] points)
    {
        // physics properties
        _points = points;
        IsStatic = isStatic;
        Mass = mass;
        Acceleration = new(0, G);

        // render properties
        Position = PositionToPixel(position);
        _vertices = new Vertex[points.Length];
        for (int i = 0; i < points.Length; i++)
            _vertices[i] = new Vertex(MeterToPixel(points[i]), color);
    }

    public void ResolveMotion(float dt, (int, int, int, int) collisions)
    {
        Velocity += Acceleration * dt;
        
        if (collisions.Item1 == 1 && Velocity.X < 0) Console.WriteLine("1");
        if (collisions.Item2 == 1 && Velocity.X > 0) Console.WriteLine("2");
        if (collisions.Item3 == 1 && Velocity.Y > 0) Console.WriteLine("3");
        if (collisions.Item4 == 1 && Velocity.Y < 0) Console.WriteLine("4");

        if (collisions.Item1 == 1 && Velocity.X < 0)
        {
            Velocity.X = 0;
            Acceleration.X = 0;
        }

        if (collisions.Item2 == 1 && Velocity.X > 0)
        {
            Velocity.X = 0;
            Acceleration.X = 0;
        }

        if (collisions.Item3 == 1 && Velocity.Y > 0)
        {
            Velocity.Y = 0;
            Acceleration.Y = 0;
        }

        if (collisions.Item4 == 1 && Velocity.Y < 0)
        {
            Velocity.Y = 0;
            Acceleration.Y = 0;
            Pos = new(Pos.X, _points.Max(p => p.Y) - _points.Min(p => p.Y));
        }
        Console.WriteLine(Pos);
        Pos += Velocity * dt;
    }

    public (int, int, int, int) CheckCollision()
    {
        int xLeftCollision = 0;
        int xRightCollision = 0;
        int yTopCollision = 0;
        int yBelowCollision = 0;
        for (int i = 0; i < _points.Length; i++)
        {
            if (GetPointPosition(i).X <= 0)
                xLeftCollision = 1;
            if (GetPointPosition(i).X >= PixelToMeter(WindowWidth))
                xRightCollision = 1;

            if (GetPointPosition(i).Y <= 0)
                yBelowCollision = 1;
            if (GetPointPosition(i).Y >= PixelToMeter(WindowHeight))
                yTopCollision = 1;
        }

        return (xLeftCollision, xRightCollision, yTopCollision, yBelowCollision);
    }

    public Vector2f GetPointPosition(int pointIndex)
    {
        return Pos + _points[pointIndex];
    }

    /*public override uint GetPointCount() => Convert.ToUInt32(_points.Count);
    public override Vector2f GetPoint(uint index) => _points[Convert.ToInt32(index)];*/
    public void Draw(RenderTarget target, RenderStates states)
    {
        // Apply the transform of the drawable object
        states.Transform *= Transform;

        // Draw the shape using the vertex array
        target.Draw(_vertices, PrimitiveType.Quads, states);
    }
}