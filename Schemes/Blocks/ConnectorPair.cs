namespace AutoScheme.Schemes.Blocks
{
    public class ConnectorPair
    {
        public int Id { get; set; }
        public Connector Connector1 { get; private set; }  
        public Connector Connector2 { get; private set; }

        public ConnectorPair(Connector connector1, Connector connector2)
        {
            Connector1 = connector1;
            Connector2 = connector2;
        }
    }
}