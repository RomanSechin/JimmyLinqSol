partial class Program
{
    private static class ComicAnalyzer {
        private static PriceRange CalculatePriceRange(Comic comic) {
            if (comic.Price < 100) 
                return PriceRange.Cheap;
            return PriceRange.Expensive;
        }
        //GroupComicByPrice упорядочивает комиксы по цене, а затем группирует их
        //по CalculatePriceRange(comic) и возвращает последовательность групп комиксов
        //(IEnumerable<IGrouping<PriceRange, Comic>>).
        public static IEnumerable<IGrouping<PriceRange, Comic>> GroupComicsByPrice(IEnumerable<Comic> comics,
            IReadOnlyDictionary<int, decimal> prices) 
        { 
            IEnumerable<IGrouping<PriceRange, Comic>> grouped = 
                from comic in comics
                orderby prices[comic.Issue]
                group comic by CalculatePriceRange(comic) into priceGroup
                select priceGroup;
            return grouped;
        }
        //GetReviews упорядочивает комиксы по номеру выпуска, а затем выполняет объединение
        //(см. ранее в этой главе) и возвращает последовательность строк следующего вида:
        //MuddyCritic rated #74 'Black Monday' 84.20
        public static IEnumerable<string> GetReviews(IEnumerable<Comic> comics, IEnumerable<Review> reviews) { 
            var joined = 
                from comic in comics
                orderby comic.Issue
                join review in reviews on comic.Issue equals review.Issue
                select $"{review.Critic} rated #{comic.Issue} '{comic.Name}' {review.Score:0.00}";
            return joined;
        }
    }
}


