using System;

namespace AutoScheme.Schemes
{
    [Serializable]
    public class GraphicScheme : Scheme
    {
        public string Name { get; set; }
        public string LastSaveFileName { get; set; }
        public GraphicScheme(SchemeSettings settings) : base(settings)
        {
        }
    }
}