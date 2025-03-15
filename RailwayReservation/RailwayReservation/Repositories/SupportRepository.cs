using Microsoft.EntityFrameworkCore;
using RailwayReservation.Models;
using RailwayReservation.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace RailwayReservation.Repositories
{
    public class SupportRepository : ISupport
    {
        private readonly OnlineRailwayReservationSystemDbContext _context;

        public SupportRepository(OnlineRailwayReservationSystemDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Query>> GetAllQueries()
        {
            return _context.Queries.ToList();
        }

        public async Task<Support> CreateSupportAsync(string userQuery, string userId)
        {
            // First, check if the user query matches with any Query Keywords
            var query = await GetQueryByKeywordAsync(userQuery);

            Support newSupport = new Support
            {
                QueryText = userQuery,
                UserId = userId,
                Status = "Pending"
            };

            if (query != null)
            {
                // If a matching Query is found, associate the support ticket with the first QueryList
                var queryList = query.QueryLists.FirstOrDefault(); // For now, returning the first QueryList, adjust as needed
                if (queryList != null)
                {
                    newSupport.QueryListId = queryList.QueryListId;
                    newSupport.Status = "Resolved"; // If a query match is found, consider the issue resolved
                }
            }
            else
            {
                // If no Query was found, search in QueryLists for matching descriptions
                var queryLists = await GetQueryListsByDescriptionAsync(userQuery);

                if (queryLists.Any())
                {
                    // If matching QueryList(s) found, assign to the first matching QueryList
                    var queryList = queryLists.FirstOrDefault();
                    newSupport.QueryListId = queryList.QueryListId;
                    newSupport.Status = "Resolved"; // If a queryList match is found, consider it resolved
                }
                else
                {
                    // If no match is found, status is "Not Found"
                    newSupport.Status = "Not Found";
                }
            }

            // Save the new support ticket to the database
            _context.Supports.Add(newSupport);
            await _context.SaveChangesAsync();

            return newSupport;
        }

        public async Task<Support> UpdateSupportAsync(string supportId, string newQueryText)
        {
            // Fetch the existing support ticket by SupportId
            var support = await _context.Supports
                .Include(s => s.QueryList) // Include QueryList for association
                .FirstOrDefaultAsync(s => s.SupportId == supportId);

            if (support == null)
            {
                throw new Exception("Support ticket not found.");
            }

            // Update the QueryText and Status based on the new query
            support.QueryText = newQueryText;

            // First, try to find a matching Query
            var query = await GetQueryByKeywordAsync(newQueryText);

            if (query != null)
            {
                // If a matching Query is found, associate the support ticket with the first matching QueryList
                var queryList = query.QueryLists.FirstOrDefault();
                if (queryList != null)
                {
                    support.QueryListId = queryList.QueryListId;
                    support.Status = "Resolved"; // If a query match is found, mark as resolved
                }
            }
            else
            {
                // If no Query was found, search in QueryLists for matching descriptions
                var queryLists = await GetQueryListsByDescriptionAsync(newQueryText);

                if (queryLists.Any())
                {
                    // If matching QueryList(s) found, assign to the first matching QueryList
                    var queryList = queryLists.FirstOrDefault();
                    support.QueryListId = queryList.QueryListId;
                    support.Status = "Resolved"; // If a queryList match is found, mark as resolved
                }
                else
                {
                    // If no match is found, status is "Not Found"
                    support.Status = "Not Found";
                }
            }

            // Save the updated support ticket to the database
            _context.Supports.Update(support);
            await _context.SaveChangesAsync();

            return support;
        }

        public async Task<bool> DeleteSupportAsync(string supportId)
        {
            // Fetch the existing support ticket by SupportId
            var support = await _context.Supports
                .FirstOrDefaultAsync(s => s.SupportId == supportId);

            if (support == null)
            {
                return false; // If support ticket not found, return false
            }

            // Remove the support ticket from the database
            _context.Supports.Remove(support);
            await _context.SaveChangesAsync();

            return true; // Return true if the deletion was successful
        }

        // Method to find the query from Query model if the user's query matches the keyword
        public async Task<Query> GetQueryByKeywordAsync(string userQuery)
        {
            // Search for a query by keyword
            var query = await _context.Queries
                .Where(q => q.Keywords != null && q.Keywords.Contains(userQuery))
                .Include(q => q.QueryLists) // Include QueryLists related to the Query
                .FirstOrDefaultAsync();

            return query;
        }

        // Method to find the QueryList by description if no direct match in Query model
        public async Task<List<QueryList>> GetQueryListsByDescriptionAsync(string userQuery)
        {
            // Search for matching descriptions in QueryLists
            var queryLists = await _context.QueryLists
                .Where(q => q.QueryDescription != null && q.QueryDescription.Contains(userQuery, StringComparison.OrdinalIgnoreCase))
                .Include(q => q.Query) // Include related Query
                .ToListAsync();

            return queryLists;
        }

        // Method to get the support query response
        public async Task<Support> GetSupportResponseAsync(string userQuery)
        {
            // First, check if the user query matches with any Query Keywords
            var query = await GetQueryByKeywordAsync(userQuery);

            if (query != null)
            {
                // If a matching Query is found, return the first support from that query's QueryList
                var queryList = query.QueryLists.FirstOrDefault(); // For now, returning the first QueryList, adjust as needed
                if (queryList != null)
                {
                    return new Support
                    {
                        QueryListId = queryList.QueryListId,
                        QueryText = userQuery,
                        Status = "Resolved",
                        QueryList = queryList
                    };
                }
            }

            // If no Query was found, search in QueryLists
            var queryLists = await GetQueryListsByDescriptionAsync(userQuery);

            if (queryLists.Any())
            {
                // If matching QueryList(s) found, return the first match
                var queryList = queryLists.FirstOrDefault();
                return new Support
                {
                    QueryListId = queryList.QueryListId,
                    QueryText = userQuery,
                    Status = "Resolved",
                    QueryList = queryList
                };
            }

            // If no matching query or query list, return support object with no results
            return new Support
            {
                QueryText = userQuery,
                Status = "Not Found"
            };
        }
    }

}