using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AthenaWebApp.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace AthenaWebApp.Pagings
{
    public class UserPaginatedList<T> : List<T>
    {
        private readonly Context _context;

        public UserPaginatedList(Context context)
        {
            _context = context;
        }

        public int PageIndex { get; private set; }
        public int TotalPages { get; private set; }

        public UserPaginatedList(List<T> items, int count, int pageIndex, int pageSize)
        {
            PageIndex = pageIndex;
            TotalPages = (int)Math.Ceiling(count / (double)pageSize);

            this.AddRange(items);
        }

        public bool HasPreviousPage
        {
            get
            {
                return (PageIndex > 1);
            }
        }

        public bool HasNextPage
        {
            get
            {
                return (PageIndex < TotalPages);
            }
        }

        public static async Task<UserPaginatedList<T>> CreateAsync(IQueryable<T> source, int pageIndex, int pageSize)
        {
            var count = await source.CountAsync();
            var items = await source.Skip((pageIndex - 1) * pageSize).Take(pageSize).ToListAsync();
            return new UserPaginatedList<T>(items, count, pageIndex, pageSize);
        }


        /*
        
        public async Task <IActionResult> UserCount()
        {


            return await _context.Users.FromSqlRaw("SELECT COUNT(*) FROM [dbo].[AspNetUsers]");

            return userCount;
        }
        */
    }
}