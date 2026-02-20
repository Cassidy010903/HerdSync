namespace HerdSync.Components.Layout
{
    public partial class MainLayout
    {
        private bool _drawerOpen = true;

        private void DrawerToggle()
        {
            _drawerOpen = !_drawerOpen;
        }

        private MudTheme HerdSyncThemes = new MudTheme()
        {
            PaletteLight = new PaletteLight()
            {
                Primary = "#748f7a",
                Secondary = "#90b499",
                Tertiary = "#FFFFFF"
            },
            PaletteDark = new PaletteDark()
            {
                Primary = Colors.Gray.Darken4
            }
        };
    }
}