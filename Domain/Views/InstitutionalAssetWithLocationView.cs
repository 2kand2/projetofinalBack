namespace WarehouseAPI.Domain.Views
{
    public class InstitutionalAssetWithLocationView
    {
        public InstitutionalAssetWithLocationView(string locationName, List<InstitutionalAssetView> institutionalAssetViews)
        {
            LocationName = locationName;
            this.institutionalAssetViews = institutionalAssetViews;
        }

        public string LocationName { get; set;}

        public List<InstitutionalAssetView> institutionalAssetViews {  get; set; }

    }
}
