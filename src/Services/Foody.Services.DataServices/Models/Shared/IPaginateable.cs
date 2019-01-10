using System.Collections.Generic;

namespace Foody.Services.DataServices.Models.Shared
{
    public interface IPaginateable<T>
    {
        ICollection<T> Items { get; set; }

        PaginationModel PaginationModel { get; set; }
    }
}
