namespace AchieveCreative.eWayProvider.eWayLibs
{
    public class LineItem
    {
        public LineItem ( string sku, string description )
        {
            SKU = sku;
            Description = description;
        }

        public string SKU;

        public string Description;
    }
}