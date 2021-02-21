namespace Services.Services
{
    public class Transient
    {
        private int Index { get; set; } = 0;
        public int GetIndex() => Index+=2;

    }
    public class Scoped
    {
        private int Index { get; set; } = 0;
        public int GetIndex() => Index+=2;

    }
    public class Singleton
    {
        private int Index { get; set; } = 0;
        public int GetIndex() => Index+=2;

    }
}
