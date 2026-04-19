namespace Group2Flight.Models
{
    public static class Check
    {
        public static string FlightCodeDateExists(Group2FlightDatabaseContext ctx, DateTime date, string flightCode)
        {
            string msg = string.Empty;
            if (!string.IsNullOrWhiteSpace(flightCode) && date != DateTime.MinValue)
            {
                var flight = ctx.Flight.FirstOrDefault(f => f.FlightCode == flightCode && f.Date == date);
                if (flight != null)
                    msg = $"Flight {flightCode} already exists for this date.";
            }
            return msg;
        }
    }
}
