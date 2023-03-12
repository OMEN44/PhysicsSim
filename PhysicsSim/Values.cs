using SFML.System;

namespace PhysicsSim;

public static class Values
{
    public const float G = -9.81f;
    public const uint TickSpeed = 30;
    public static uint WindowWidth = 1280;
    public static uint WindowHeight = 720;
    public static int PixelPerMeter = 16;

    // ReSharper disable once PossibleLossOfFraction
    public static float PixelToMeter(float pixels) => pixels / PixelPerMeter;
    public static float MeterToPixel(float meters) => PixelPerMeter * meters;

    public static Vector2f PixelToMeter(Vector2f pixel) => new(PixelToMeter(pixel.X), PixelToMeter(pixel.Y));
    public static Vector2f MeterToPixel(Vector2f pixel) => new(MeterToPixel(pixel.X), MeterToPixel(pixel.Y));

    public static Vector2f PixelToPosition(Vector2f x) => new(PixelToMeter(x.X), PixelToMeter(-x.Y + Convert.ToSingle(WindowHeight)));
    public static Vector2f PositionToPixel(Vector2f x) => new(MeterToPixel(x.X), -MeterToPixel(x.Y) + Convert.ToSingle(WindowHeight));
}