// Copyright (c) Bruno Sales <me@baliestri.dev>. Licensed under the MIT License.
// See the LICENSE file in the repository root for full license text.

namespace Calliope.Core.Extensions;

/// <summary>
///   Extensions for <see cref="IQueryable{T}" />.
/// </summary>
public static class QueryableExtensions {
  /// <summary>
  ///   Paginates a <see cref="IQueryable{T}" />.
  /// </summary>
  /// <param name="source">The <see cref="IQueryable{T}" /> to be paginated.</param>
  /// <param name="pageIndex">The page index.</param>
  /// <param name="pageSize">The page size.</param>
  /// <typeparam name="T">The type of the <see cref="IQueryable{T}" />.</typeparam>
  /// <returns>The paginated <see cref="IQueryable{T}" />.</returns>
  public static IQueryable<T> Page<T>(this IQueryable<T> source, int pageIndex, int pageSize)
    => source.Skip((pageIndex - 1) * pageSize).Take(pageSize);

  /// <summary>
  ///   Counts the pages of a <see cref="IQueryable{T}" />.
  /// </summary>
  /// <param name="source">The <see cref="IQueryable{T}" /> to be paginated.</param>
  /// <param name="pageSize">The page size.</param>
  /// <typeparam name="T">The type of the <see cref="IQueryable{T}" />.</typeparam>
  /// <returns>The number of pages.</returns>
  public static int CountPages<T>(this IQueryable<T> source, int pageSize)
    => (source.Count() / pageSize) + ((source.Count() % pageSize) > 0 ? 1 : 0);
}