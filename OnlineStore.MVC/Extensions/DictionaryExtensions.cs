using OnlineStore.MVC.Services.Base;

namespace OnlineStore.MVC.Extensions
{
    public static class DictionaryExtensions
    {
        public static IEnumerable<ValidationFailure> ToValidationFailures(this IDictionary<string, string[]> dictionary)
        {
            var validationFailures = new List<ValidationFailure>();

            foreach (var pair in dictionary)
                foreach (var failure in pair.Value)
                    validationFailures.Add(new ValidationFailure(pair.Key, failure));

            return validationFailures;
        }
    }
}
