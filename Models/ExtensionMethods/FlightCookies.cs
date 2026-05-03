using Group2Flight.Models.DomainModels;

namespace Group2Flight.Models.ExtensionMethods
{
    public class FlightCookies
    {
        private const string SelectionKey = "selectionKey";
        private const string Delimiter = "-";

        private IRequestCookieCollection requestCookies { get; set; } = null!;
        private IResponseCookies responseCookies { get; set; } = null!;

        public FlightCookies(IRequestCookieCollection request, IResponseCookies response)
        {
            requestCookies = request;
            responseCookies = response;
        }
        public void RemoveSelectedId(int id)
        {
            string[] ids = GetSelectedIds();
            var updatedIds = ids.Where(rid => rid != id.ToString()).ToArray();
            SetSelectedIds(updatedIds);
        }
        public void SetSelectedIds(List<FlightSelection> flightSelections)
        {
            var ids = flightSelections.Select(r => r.FlightSelectionId.ToString()).ToList();
            SetSelectedIds(ids);
        }
        public void SetSelectedIds(IEnumerable<string> ids)
        {
            if (responseCookies == null)
                throw new InvalidOperationException("Response cookies are not initialized.");

            string idsString = string.Join(Delimiter, ids);
            CookieOptions options = new CookieOptions
            {
                Expires = DateTime.Now.AddDays(12),
                IsEssential = true
            };

            RemoveSelectedIds();
            responseCookies.Append(SelectionKey, idsString, options);
        }
        public string[] GetSelectedIds()
        {
            string cookie = requestCookies[SelectionKey] ?? string.Empty;
            if (string.IsNullOrEmpty(cookie))
                return Array.Empty<string>();
            else
                return cookie.Split(Delimiter);
        }

        public void RemoveSelectedIds()
        {
            if (responseCookies == null)
                throw new InvalidOperationException("Response cookies are not initialized.");

            responseCookies.Delete(SelectionKey);
        }
    }
}
