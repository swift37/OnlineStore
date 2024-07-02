using Microsoft.Extensions.Primitives;

namespace OnlineStore.MVC.Extensions
{
    public static class StringValuesExtensions
    {
        public static IDictionary<int, ICollection<int>> GetAppliedFilters(this StringValues values)
        {
            var appliedFilters = new Dictionary<int, ICollection<int>>();

            foreach (var stringId in values)
            {
                if (int.TryParse(stringId?.Split(';')[0][4..], out var specTypeId) &&
                    int.TryParse(stringId?.Split(';')[1][3..], out var specId))
                {
                    if (!appliedFilters.TryAdd(specTypeId, new List<int> { specId }))
                        appliedFilters[specTypeId].Add(specId);
                }
            }

            return appliedFilters;
        }

        public static ICollection<int> GetAppliedFilterIds(this StringValues values)
        {
            var appliedFilters = new HashSet<int>();

            foreach (var stringId in values)
                if (int.TryParse(stringId?.Split(';')[1][3..], out var specId))
                    appliedFilters.Add(specId);

            return appliedFilters;
        }

        public static ICollection<int> GetAppliedFilterIds(this StringValues values, int specificationTypeId)
        {
            var appliedFilters = new HashSet<int>();
            var specs = values
                .Where(v => v?.StartsWith($"spid{specificationTypeId}") is true)
                .Select(v => v is not null ? v.Split(';')[1][3..] : string.Empty);

            foreach (var stringId in specs)
                if (int.TryParse(stringId, out var specId))
                        appliedFilters.Add(specId);

            return appliedFilters;
        }
    }
}
