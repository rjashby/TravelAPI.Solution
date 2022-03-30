using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Travel.Models
{
  public class UrlQuery
  {
    public int PageNumber { get; set; }
    public int PageSize { get; set; }
    
    public UrlQuery()
    {
      this.PageNumber = 1;
      this.PageSize = 10;
    }

    public UrlQuery(int pageNumber, int pageSize)
    {
      this.PageNumber = pageNumber < 1 ? 1 : pageNumber;
      this.PageSize = pageSize > 10 ? 10 : pageSize;
    }
  }
}

// namespace Travel.Models
// {
//   public class UrlQuery
//   {
//     private const int maxPageSize = 100;
//     public int? PageNumber { get; set; }

//     private int _pageSize = 50;
//     public int PageSize
//     {
//       get
//       {
//         return _pageSize;
//       }
//       set
//       {
//         _pageSize = (value < maxPageSize) ? value : maxPageSize;
//       }
//     }
//   }
// }