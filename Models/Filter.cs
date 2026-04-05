namespace Group2Flight.Models
{
    public class Filter
    {
        public Filter(string filterstring)
        {
            FilterString = filterstring ?? "all-all-all-all";
            string[] filters = FilterString.Split('-');
            FromKey = filters[0];
            ToKey = filters[1];
            DepartureDate = filters[2];
            CabinType = filters[3];
        }
        public string FilterString { get; }
        public string FromKey { get; }
        public string ToKey { get; }
        public string DepartureDate { get; }
        public string CabinType { get; }

        public bool HasFromKey => FromKey.ToLower() != "all";
        public bool HasToKey => ToKey.ToString().ToLower() != "all";
        public bool HasCabinType => CabinType.ToString().ToLower() != "all";
    }
}
