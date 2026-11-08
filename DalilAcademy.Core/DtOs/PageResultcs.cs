namespace DalilAcademy.Core.DtOs
{
    public record PageResultcs<T>(List<T>Item,int Page,int PageSize,int TotalCount)
    {
        public int TotalPages => (int)Math.Ceiling((double)TotalCount / PageSize);
    }
}
