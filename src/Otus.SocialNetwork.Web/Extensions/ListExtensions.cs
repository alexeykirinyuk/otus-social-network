namespace Otus.SocialNetwork.Web.Extensions;

public static class ListExtensions
{
    public static IEnumerable<IGrouping<int, TSource>> GroupBy<TSource>(
        this IReadOnlyList<TSource> source,
        int itemsPerGroup)
    {
        var result = source.Zip(Enumerable.Range(0, source.Count),
                (s, r) => new { Group = r / itemsPerGroup, Item = s })
            .GroupBy(i => i.Group, g => g.Item)
            .ToList();

        return result;
    }
}