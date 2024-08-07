namespace CoelacanthEngine.state
{
    public class ResourceManifest
    {
        public List<string> Textures { get; private set; }
        public List<string> Fonts { get; private set; }
        public List<string> Sounds { get; private set; }
        public List<string> Songs { get; private set; }

        public ResourceManifest()
        {
            Textures = new List<string>();
            Fonts = new List<string>();
            Sounds = new List<string>();
            Songs = new List<string>();
        }
    }
}
