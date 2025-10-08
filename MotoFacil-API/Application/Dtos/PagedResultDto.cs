namespace MotoFacilAPI.Application.Dtos
{
    /// <summary>
    /// Estrutura para retorno paginado nos endpoints
    /// </summary>
    public class PagedResultDto<T>
    {
        /// <example>1</example>
        public int Page { get; set; }
        /// <example>10</example>
        public int PageSize { get; set; }
        /// <example>100</example>
        public int TotalCount { get; set; }
        public IEnumerable<T> Items { get; set; } = new List<T>();
    }
}