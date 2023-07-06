
public static class Util
{
    public static string CreateRandomName()
    {
        return Guid.NewGuid().ToString("N");
    }
}