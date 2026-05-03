using System.Diagnostics.Metrics;
using Group2Flight.Models.DomainModels;

namespace Group2Flight.Models.ExtensionMethods
{
    public class FlightSessions
    {
        private const string SelectionKey = "selectionKey";
        private const string SelectedFlightsCountKey = "selectedFlightsCount";
        private const string ActiveFromKey = "activeFrom";
        private const string ActiveToKey = "activeTo";
        private const string ActiveDepartureDate = "activeDeptDate";
        private const string ActiveCabinType = "activeCabinType";

        private ISession session { get; set; }
        public FlightSessions(ISession session) => this.session = session;

        public void SetSelections(List<FlightSelection> flightBookings)
        {
            session.SetObject(SelectionKey, flightBookings);
            session.SetInt32(SelectedFlightsCountKey, flightBookings.Count);
        }
        public List<FlightSelection> GetSelections() =>
            session.GetObject<List<FlightSelection>>(SelectionKey) ?? new List<FlightSelection>();
        public int? GetSelectedCount() => session.GetInt32(SelectedFlightsCountKey);

        public void SetActiveFrom(string activeFrom) =>
            session.SetString(ActiveFromKey, activeFrom);
        public string GetActiveFrom() =>
            session.GetString(ActiveFromKey) ?? string.Empty;

        public void SetActiveTo(string activeTo) =>
            session.SetString(ActiveToKey, activeTo);
        public string GetActiveTo() =>
            session.GetString(ActiveToKey) ?? string.Empty;

        public void SetActiveDepartureDate(string activeDeptDate) =>
            session.SetString(ActiveDepartureDate, activeDeptDate);
        public string GetActiveDepartureDate() =>
            session.GetString(ActiveDepartureDate) ?? string.Empty;

        public void SetActiveCabinType(string activeCabinType) =>
            session.SetString(ActiveCabinType, activeCabinType);
        public string GetActiveCabinType() =>
            session.GetString(ActiveCabinType) ?? string.Empty;
        public void RemoveSelections()
        {
            session.Remove(SelectionKey);
            session.Remove(SelectedFlightsCountKey);
        }
    }
}
