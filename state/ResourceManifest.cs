namespace CoelacanthEngine.state
{
    public class ResourceManifest
    {
        private static readonly List<string> EMPTY_LIST = new(0);
        public List<string> Textures { get; private set; }
        public List<string> Fonts { get; private set; }
        public List<string> Sounds { get; private set; }
        public List<string> Songs { get; private set; }

        public ResourceManifest(List<string>? textures = default, List<string>? fonts = default, List<string>? sounds = default, List<string>? songs = default)
        {
            Textures = textures ?? EMPTY_LIST;
            Fonts = fonts ?? EMPTY_LIST;
            Sounds = sounds ?? EMPTY_LIST;
            Songs = songs ?? EMPTY_LIST;
        }
    }
}
