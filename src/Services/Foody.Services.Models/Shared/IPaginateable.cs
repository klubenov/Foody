using System.Collections.Generic;

namespace Foody.Services.Models.Shared
{
    public interface IPaginateable<T>
    {
        ICollection<T> Items { get; set; }

        PaginationModel PaginationModel { get; set; }
    }
}
