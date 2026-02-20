namespace HerdSync.Shared.Enums.Data.Extensions
{
    public static class ColourExtensionEnum
    {
        public static string ToDisplayColour(this ColourEnum colour)
        {
            return (colour) switch
            {
                (ColourEnum.Yellow) => "Yellow",
                (ColourEnum.Green) => "Green",
                (ColourEnum.None) => "None",
                _ => "None"
            };
        }
    }
}