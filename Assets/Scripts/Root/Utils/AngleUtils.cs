namespace Root.Utils
{
    public static class AngleUtils
    {
        public static float FixAngle(this float angle)
        {
            return angle switch
            {
                > 180 => angle - 360,
                < -180 => angle + 360,
                _ => angle
            };
        }
    }
}